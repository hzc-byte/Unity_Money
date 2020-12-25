using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum PlayerDirectionEnum
{
    LEFT,
    RIGHT,
    UP,
    DOWN
}

public class PlayerView : ViewBase
{
    /// <summary>
    /// 精力值的表现
    /// </summary>
    public Image EnergyImage;

    /// <summary>
    /// 动画状态机
    /// </summary>
    public Animator animator;

    /// <summary>
    /// 刚体
    /// </summary>
    public Rigidbody2D PlayerRigidbody;

    /// <summary>
    /// 设置玩家的方向
    /// </summary>
    public void SetPlayerDirection(PlayerDirectionEnum pde)
    {
        switch (pde)
        {
            case PlayerDirectionEnum.LEFT:
                animator.SetInteger("direction", 1);
                break;
            case PlayerDirectionEnum.RIGHT:
                animator.SetInteger("direction", 2);
                break;
            case PlayerDirectionEnum.UP:
                animator.SetInteger("direction", 3);
                break;
            case PlayerDirectionEnum.DOWN:
                animator.SetInteger("direction", 4);
                break;
        }
    }

    /// <summary>
    /// 设置精力值图片的表现
    /// </summary>
    public void SetEneryImageValue(IEventParam ie)
    {
        if (ie is FloatParam)
        {
            EnergyImage.fillAmount = (ie as FloatParam).value;
        }
    }
}
