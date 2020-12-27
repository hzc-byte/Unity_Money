using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

public class RobotPresenter : PresenterBase<RobotView, RobotModel>
{
    private const string TAG = "[RobotPresenter]:";
    /// <summary>
    /// 机器人说hi的组件
    /// </summary>
    public GameObject HIObj;
    public bool IsFollowing { get; set; }//用来判断是否跟随
    public bool CancelRobotInVisibleHideBotBtn = false;//当机器人被激活之后取消在不可见的状态下Bot按钮消失的功能
    public bool IsCollect = false;//判断当前机器人是否去单独收集材料
    public Transform player;//获得角色
    public Vector2 Margin;//相机与角色的相对范围
    public Vector2 smoothing;//相机移动的平滑度
    private float speed = 1f;//机器人收集资源的移动速度
    /// <summary>
    /// 所有的资源坐标供给机器人去收集
    /// </summary>
    [SerializeField]
    private List<GameObject> allResources = new List<GameObject>();

    public static GameObject currentCollectingResource;

    protected override void OnAwake()
    {
        base.OnAwake();
        EventManager.Instance.AddEvent(EventEnum.RobotGetAllResources, GetAllResources);
        EventManager.Instance.AddEvent(EventEnum.RobotCollectNextResource, RobotStartCollectResource);
    }

    protected override void OnStart()
    {
        base.OnStart();
        EventManager.Instance.AddEvent(EventEnum.WhenRobotFollowPlayer, ActivateRobot);
        EventManager.Instance.AddEvent(EventEnum.WhenRobotCollectResource, SetCollect);
    }

    protected void OnEnable()
    {
        EventManager.Instance.AddEvent(EventEnum.WhenRobotFollowPlayer, ActivateRobot);
        EventManager.Instance.AddEvent(EventEnum.WhenRobotCollectResource, SetCollect);
        EventManager.Instance.AddEvent(EventEnum.RobotGetAllResources, GetAllResources);
        EventManager.Instance.AddEvent(EventEnum.RobotCollectNextResource, RobotStartCollectResource);
    }

    protected void OnDisable()
    {
        EventManager.Instance.RemoveEvent(EventEnum.WhenRobotFollowPlayer, ActivateRobot);
        EventManager.Instance.RemoveEvent(EventEnum.WhenRobotCollectResource, SetCollect);
        EventManager.Instance.RemoveEvent(EventEnum.RobotGetAllResources, GetAllResources);
        EventManager.Instance.RemoveEvent(EventEnum.RobotCollectNextResource, RobotStartCollectResource);
    }

    /// <summary>
    /// 这里有个坑就是scene界面如果能看到的话也会调用
    /// </summary>
    protected void OnBecameVisible()
    {
        //TODO：显示Bot按钮
        if (!CancelRobotInVisibleHideBotBtn)
        {
            EventManager.Instance.DispatchEvent(EventEnum.WhenRobotBecameVisible, new BooleanParam(true));
        }
    }
    protected void OnBecameInvisible()
    {
        //TODO:隐藏Bot按钮
        if (!CancelRobotInVisibleHideBotBtn)
        {
            EventManager.Instance.DispatchEvent(EventEnum.WhenRobotBecameVisible, new BooleanParam(false));
        }
    }

    protected override void OnLateUpdate()
    {
        base.OnLateUpdate();
        var x = transform.position.x;
        var y = transform.position.y;
        if (IsFollowing)
        {
            if (Mathf.Abs(x - player.position.x) > Margin.x)
            {
                //如果相机与角色的x轴距离超过了最大范围则将x平滑的移动到目标点的x
                x = Mathf.Lerp(x, player.position.x, smoothing.x * Time.deltaTime);
            }
            if (Mathf.Abs(y - player.position.y) > Margin.y)
            {
                //如果相机与角色的y轴距离超过了最大范围则将x平滑的移动到目标点的y
                y = Mathf.Lerp(y, player.position.y, smoothing.y * Time.deltaTime);
            }
        }
        transform.position = new Vector3(x, y, transform.position.z);//改变相机的位置
    }

    /// <summary>
    /// 激活机器人
    /// </summary>
    protected void ActivateRobot(IEventParam ie)
    {
        if (ie is BooleanParam)
        {
            bool m_value = (ie as BooleanParam).value;
            transform.DORotate(new Vector3(0, 0, 0), 0.5f).OnComplete(delegate
            {
                HIObj.SetActive(true);
                StartCoroutine(Delay(1, delegate
                {
                    HIObj.SetActive(false);
                    IsFollowing = m_value;
                    CancelRobotInVisibleHideBotBtn = m_value;
                    StartCoroutine(Delay(0.8f, delegate
                    {
                        EventManager.Instance.DispatchEvent(EventEnum.FirstShowTaskPanel, new BooleanParam(true));
                        EventManager.Instance.DispatchEvent(EventEnum.ShowOrHideResources, new BooleanParam(true));
                    }));
                }));
            });
        }
    }

    /// <summary>
    /// 激活机器人的收集的功能
    /// </summary>
    private void SetCollect(IEventParam ie)
    {
        if (ie is BooleanParam)
        {
            bool m_value = (ie as BooleanParam).value;
            IsCollect = m_value;
            IsFollowing = !m_value;
            if (IsCollect)
            {
                EventManager.Instance.DispatchEvent(EventEnum.RobotCollectNextResource, ie);
            }
        }
    }

    /// <summary>
    /// 获取所有的资源
    /// </summary>
    private void GetAllResources(IEventParam ie)
    {
        if (ie is GameObjectListParam)
        {
            allResources = (ie as GameObjectListParam).value;
        }
    }

    /// <summary>
    /// 机器人开始收集资源
    /// </summary>
    private void RobotStartCollectResource(IEventParam ie)
    {
        if (allResources.Count <= 0) return;
        currentCollectingResource = GetNearestPosition();
        float length = Vector2.Distance(transform.position, currentCollectingResource.transform.position);
        float timer = length / speed;
        Debug.Log(TAG + "robot move time = " + timer);
        transform.DOMove(currentCollectingResource.transform.position, timer).SetEase(Ease.Linear);
    }

    /// <summary>
    /// 获取离机器人最近的资源
    /// </summary>
    private GameObject GetNearestPosition()
    {
        GameObject minObj = allResources[0];
        Vector2 minPos = minObj.transform.position;
        float minLength = Vector2.Distance(transform.position, minPos);
        foreach (var v in allResources)
        {
            float tempLength = Vector2.Distance(transform.position, v.transform.position);
            if (tempLength < minLength)
            {
                minLength = tempLength;
                minPos = v.transform.position;
                minObj = v;
            }
        }
        return minObj;
    }

    /// <summary>
    /// 延时
    /// </summary>
    protected IEnumerator Delay(float timer, Action action)
    {
        yield return new WaitForSeconds(timer);
        action();
    }
}
