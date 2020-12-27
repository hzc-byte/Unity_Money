using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllResources : MonoBehaviour
{
    private const string TAG = "[AllResources]:";
    public List<GameObject> TotalResources = new List<GameObject>();
    public List<Vector2> positions = new List<Vector2>();

    private void Awake()
    {
        EventManager.Instance.AddEvent(EventEnum.ShowOrHideResources, ShowOrHideResources);
        EventManager.Instance.AddEvent(EventEnum.RemoveResourceFromList, RemoveResourceFromList);
    }

    private void OnEnable()
    {
        EventManager.Instance.AddEvent(EventEnum.ShowOrHideResources, ShowOrHideResources);
        EventManager.Instance.AddEvent(EventEnum.RemoveResourceFromList, RemoveResourceFromList);
    }

    private void Start()
    {
        UpdateRobotResourcesPositionList();
    }

    private void OnDisable()
    {
        EventManager.Instance.RemoveEvent(EventEnum.ShowOrHideResources, ShowOrHideResources);
        EventManager.Instance.RemoveEvent(EventEnum.RemoveResourceFromList, RemoveResourceFromList);
    }

    /// <summary>
    /// 显示隐藏资源
    /// </summary>
    private void ShowOrHideResources(IEventParam ie)
    {
        if (ie is BooleanParam)
        {
            bool isShow = (ie as BooleanParam).value;
            foreach (var v in TotalResources)
            {
                v.SetActive(isShow);
            }
        }
    }

    /// <summary>
    /// 从列表里移除资源
    /// </summary>
    private void RemoveResourceFromList(IEventParam ie)
    {
        if (ie is GameObjectParam)
        {
            GameObject obj = (ie as GameObjectParam).value;
            if (TotalResources.Contains(obj))
            {
                TotalResources.Remove(obj);
                UpdateRobotResourcesPositionList();
            }
            else
            {
                Debug.LogWarning(TAG + "TotalResources dont contains " + obj.name);
            }
        }
    }

    /// <summary>
    /// 更新机器人获取到的资源位置列表
    /// </summary>
    private void UpdateRobotResourcesPositionList()
    {
        //只能放到Start中，因为All Resources执行的顺序比Robot早很多
        EventManager.Instance.DispatchEvent(EventEnum.RobotGetAllResources, new GameObjectListParam(TotalResources));
    }
}
