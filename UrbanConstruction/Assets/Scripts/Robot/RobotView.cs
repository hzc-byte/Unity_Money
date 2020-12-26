using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotView : ViewBase
{
    /// <summary>
    /// 动画状态机
    /// </summary>
    public Animator animator;

    /// <summary>
    /// 设置玩家的方向
    /// </summary>
    public void SetPlayerDirection(DirectionEnum pde)
    {
        switch (pde)
        {
            case DirectionEnum.IDLE:
                animator.SetInteger("direction", -1);
                break;
            case DirectionEnum.LEFT:
                animator.SetInteger("direction", 1);
                break;
            case DirectionEnum.RIGHT:
                animator.SetInteger("direction", 2);
                break;
            case DirectionEnum.UP:
                animator.SetInteger("direction", 3);
                break;
            case DirectionEnum.DOWN:
                animator.SetInteger("direction", 4);
                break;
        }
    }
}
