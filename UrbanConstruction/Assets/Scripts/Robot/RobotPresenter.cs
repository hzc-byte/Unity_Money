using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotPresenter : PresenterBase<RobotView, RobotModel>
{
    public bool IsFollowing { get; set; }//用来判断是否跟随
    public Transform player;//获得角色
    public Vector2 Margin;//相机与角色的相对范围
    public Vector2 smoothing;//相机移动的平滑度

    protected override void OnAwake()
    {
        base.OnAwake();
    }

    protected override void OnStart()
    {
        base.OnStart();
        EventManager.Instance.AddEvent(EventEnum.WhenRobotFollowPlayer, SetRobotFollowPlayer);
    }

    protected void OnEnable()
    {
        EventManager.Instance.AddEvent(EventEnum.WhenRobotFollowPlayer, SetRobotFollowPlayer);
    }

    protected void OnDisable()
    {
        EventManager.Instance.RemoveEvent(EventEnum.WhenRobotFollowPlayer, SetRobotFollowPlayer);
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

    protected void SetRobotFollowPlayer(IEventParam ie)
    {
        if (ie is BooleanParam)
        {
            IsFollowing = (ie as BooleanParam).value;
        }
    }
}
