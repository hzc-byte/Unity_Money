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
    /// <summary>
    /// 主角行走速度
    /// </summary>
    private float speed = 0.05f;

    protected override void OnAwake()
    {
        base.OnAwake();//这个必须得保留
    }

    protected override void OnStart()
    {
        EventManager.Instance.AddEvent(EventEnum.WhenPlayerConsumeEnergy, SetEnergyValue);
        EventManager.Instance.AddEvent(EventEnum.PlayerCanBeControled, MonitorCanBeControled);
        EventManager.Instance.AddEvent(EventEnum.HouseIsBuilded, WhenHouseIsBuildedSetPlayerPosition);
    }

    protected void OnEnable()
    {
        EventManager.Instance.AddEvent(EventEnum.WhenPlayerConsumeEnergy, SetEnergyValue);
        EventManager.Instance.AddEvent(EventEnum.PlayerCanBeControled, MonitorCanBeControled);
        EventManager.Instance.AddEvent(EventEnum.HouseIsBuilded, WhenHouseIsBuildedSetPlayerPosition);
    }

    protected void OnDisable()
    {
        EventManager.Instance.RemoveEvent(EventEnum.WhenPlayerConsumeEnergy, SetEnergyValue);
        EventManager.Instance.RemoveEvent(EventEnum.PlayerCanBeControled, MonitorCanBeControled);
        EventManager.Instance.AddEvent(EventEnum.HouseIsBuilded, WhenHouseIsBuildedSetPlayerPosition);
    }

    /// <summary>
    /// 控制玩家行走
    /// </summary>
    protected override void OnUpdate()
    {
        if (canBeControled)
        {
            MonitorKeyDown();
            SetControlPlayerDirection();
        }
        MonitorKeyUp();
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
    private void MonitorKeyDown()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            isUp = true;
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            isLeft = true;
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            isDown = true;
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            isRight = true;
        }
    }

    /// <summary>
    /// 监控按键触发
    /// </summary>
    private void MonitorKeyUp()
    {
        if (Input.GetKeyUp(KeyCode.W))
        {
            isUp = false;
        }

        if (Input.GetKeyUp(KeyCode.A))
        {
            isLeft = false;
        }

        if (Input.GetKeyUp(KeyCode.S))
        {
            isDown = false;
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
        if (isUp)
        {
            SetPlayerZRotation(0);
            view.SetPlayerDirection(DirectionEnum.UP);
            this.transform.Translate(Vector3.up * speed);
        }
        if (isLeft)
        {
            SetPlayerZRotation(0);
            view.SetPlayerDirection(DirectionEnum.LEFT);
            this.transform.Translate(Vector3.left * speed);
        }
        if (isDown)
        {
            SetPlayerZRotation(0);
            view.SetPlayerDirection(DirectionEnum.DOWN);
            this.transform.Translate(Vector3.down * speed);
        }
        if (isRight)
        {
            SetPlayerZRotation(0);
            view.SetPlayerDirection(DirectionEnum.RIGHT);
            this.transform.Translate(Vector3.right * speed);
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

    /// <summary>
    /// 检测玩家是否能够被控制
    /// </summary>
    /// <param name="ie"></param>
    private void MonitorCanBeControled(IEventParam ie)
    {
        if (ie is BooleanParam)
        {
            canBeControled = (ie as BooleanParam).value;
        }
    }

    private void SetEnergyValue(IEventParam ie)
    {
        if (ie is FloatParam)
        {
            model.EnergyValue = model.EnergyValue - (ie as FloatParam).value;
            Debug.Log("Energy = " + model.EnergyValue);
            if (model.EnergyValue <= 0)
            {
                model.EnergyValue = 0;
                if (!UIView.hasHouse)
                {
                    EventManager.Instance.DispatchEvent(EventEnum.GameFailure, null);
                }
            }
            else if (model.EnergyValue >= 100)
            {
                model.EnergyValue = 100;
            }
            view.SetEneryImageValue(model.EnergyValue * 0.01f);
        }
    }
    /// <summary>
    /// 当房子建好之后玩家的位置
    /// </summary>
    /// <param name="ie"></param>
    private void WhenHouseIsBuildedSetPlayerPosition(IEventParam ie)
    {
        transform.position = new Vector3(0.76f, -5.05f, 0);
        view.SetPlayerDirection(DirectionEnum.DOWN);
    }
}
