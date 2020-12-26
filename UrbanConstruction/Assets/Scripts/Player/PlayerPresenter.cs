using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPresenter : PresenterBase<PlayerView, PlayerModel>
{
    //向上
    private bool isUp;
    //向下
    private bool isDown;
    //向左
    private bool isLeft;
    //向右
    private bool isRight;
    /// <summary>
    /// 判断是否可控制行走
    /// </summary>
    private bool canBeControled = false;

    protected override void OnAwake()
    {
        base.OnAwake();//这个必须得保留
    }

    protected override void OnStart()
    {
        EventManager.Instance.AddEvent(EventEnum.WhenPlayerConsumeEnergy, view.SetEneryImageValue);
        EventManager.Instance.AddEvent(EventEnum.PlayerCanBeControled, MonitorCanBeControled);
    }

    protected void OnEnable()
    {
        EventManager.Instance.AddEvent(EventEnum.WhenPlayerConsumeEnergy, view.SetEneryImageValue);
        EventManager.Instance.AddEvent(EventEnum.PlayerCanBeControled, MonitorCanBeControled);
    }

    protected void OnDisable()
    {
        EventManager.Instance.RemoveEvent(EventEnum.WhenPlayerConsumeEnergy, view.SetEneryImageValue);
        EventManager.Instance.RemoveEvent(EventEnum.PlayerCanBeControled, MonitorCanBeControled);
    }

    /// <summary>
    /// 控制玩家行走
    /// </summary>
    protected override void OnUpdate()
    {
        if (canBeControled)
        {
            SetPlayerWalk();
            MonitorKeyDownOrUp();
            SetControlPlayerDirection();
        }
    }

    /// <summary>
    /// 控制玩家走路
    /// </summary>
    private void SetPlayerWalk()
    {
        if (isLeft && isRight)//左右按键，停下来
        {
            view.SetPlayerDirection(DirectionEnum.IDLE);
            return;
        }
        if (isUp && isDown)//上下按键，停下来
        {
            view.SetPlayerDirection(DirectionEnum.IDLE);
            return;
        }
        Vector2 walkVector = new Vector2(Input.GetAxis("Horizontal") * model.WalkSpeed, Input.GetAxis("Vertical") * model.WalkSpeed);
        view.PlayerRigidbody.AddForce(walkVector);
    }

    /// <summary>
    /// 监控按键触发
    /// </summary>
    private void MonitorKeyDownOrUp()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            isUp = true;
        }
        if (Input.GetKeyUp(KeyCode.W))
        {
            isUp = false;
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            isLeft = true;
        }
        if (Input.GetKeyUp(KeyCode.A))
        {
            isLeft = false;
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            isDown = true;
        }
        if (Input.GetKeyUp(KeyCode.S))
        {
            isDown = false;
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            isRight = true;
        }
        if (Input.GetKeyUp(KeyCode.D))
        {
            isRight = false;
        }
    }

    /// <summary>
    /// 控制玩家方向
    /// 键盘上的WASD键
    /// </summary>
    private void SetControlPlayerDirection()
    {
        if (!isLeft && !isRight && !isUp && !isDown)//没有任何按键停下来
        {
            view.SetPlayerDirection(DirectionEnum.IDLE);
            return;
        }
        if (isLeft && isRight)//左右按键，停下来
        {
            view.SetPlayerDirection(DirectionEnum.IDLE);
            return;
        }
        if (isUp && isDown)//上下按键，停下来
        {
            view.SetPlayerDirection(DirectionEnum.IDLE);
            return;
        }
        //检测四个斜向的按键
        if (isUp && isLeft)
        {
            SetPlayerZRotation(45);
            view.SetPlayerDirection(DirectionEnum.UP);
        }
        else if (isUp && isRight)
        {
            SetPlayerZRotation(-45);
            view.SetPlayerDirection(DirectionEnum.UP);
        }
        else if (isDown && isLeft)
        {
            SetPlayerZRotation(-45);
            view.SetPlayerDirection(DirectionEnum.DOWN);
        }
        else if (isDown && isRight)
        {
            SetPlayerZRotation(45);
            view.SetPlayerDirection(DirectionEnum.DOWN);
        }
        else
        {
            if (isUp)
            {
                SetPlayerZRotation(0);
                view.SetPlayerDirection(DirectionEnum.UP);
            }
            if (isLeft)
            {
                SetPlayerZRotation(0);
                view.SetPlayerDirection(DirectionEnum.LEFT);
            }
            if (isDown)
            {
                SetPlayerZRotation(0);
                view.SetPlayerDirection(DirectionEnum.DOWN);
            }
            if (isRight)
            {
                SetPlayerZRotation(0);
                view.SetPlayerDirection(DirectionEnum.RIGHT);
            }
        }
    }

    /// <summary>
    /// 设置玩家的Z轴的旋转值
    /// </summary>
    /// <param name="value"></param>
    private void SetPlayerZRotation(float value)
    {
        transform.localRotation = Quaternion.Euler(0, 0, value);
    }

    private void MonitorCanBeControled(IEventParam ie)
    {
        if(ie is BooleanParam)
        {
            canBeControled = (ie as BooleanParam).value;
        }
    }
}
