using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPresenter : PresenterBase<PlayerView, PlayerModel>
{
    protected override void OnAwake()
    {
        base.OnAwake();//这个必须得保留
    }

    protected override void OnStart()
    {
        EventManager.Instance.AddEvent(EventEnum.WhenPlayerConsumeEnergy, view.SetEneryImageValue);
    }

    protected void OnEnable()
    {
        EventManager.Instance.AddEvent(EventEnum.WhenPlayerConsumeEnergy, view.SetEneryImageValue);
    }

    protected void OnDisable()
    {
        EventManager.Instance.RemoveEvent(EventEnum.WhenPlayerConsumeEnergy, view.SetEneryImageValue);
    }

    /// <summary>
    /// 控制玩家行走
    /// </summary>
    protected override void OnUpdate()
    {
        SetPlayerWalk();
        SetWASDControlPlayerDirection();
        SetDirectionKeyControlPlayerDirection();
    }



    /// <summary>
    /// 控制玩家走路
    /// </summary>
    private void SetPlayerWalk()
    {
        Vector2 walkVector = new Vector2(Input.GetAxis("Horizontal") * model.WalkSpeed, Input.GetAxis("Vertical") * model.WalkSpeed);
        view.PlayerRigidbody.AddForce(walkVector);
    }

    /// <summary>
    /// 控制玩家方向
    /// 键盘上的WASD键
    /// </summary>
    private void SetWASDControlPlayerDirection()
    {
        //检测四个斜向的按键
        if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.A))
        {
            SetPlayerZRotation(-45);
            view.SetPlayerDirection(PlayerDirectionEnum.UP);
        }
        else if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.D))
        {
            SetPlayerZRotation(45);
            view.SetPlayerDirection(PlayerDirectionEnum.UP);
        }
        else if (Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.A))
        {
            SetPlayerZRotation(-135);
            view.SetPlayerDirection(PlayerDirectionEnum.DOWN);
        }
        else if (Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.D))
        {
            SetPlayerZRotation(135);
            view.SetPlayerDirection(PlayerDirectionEnum.DOWN);
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                SetPlayerZRotation(0);
                view.SetPlayerDirection(PlayerDirectionEnum.UP);
            }
            if (Input.GetKeyDown(KeyCode.A))
            {
                SetPlayerZRotation(0);
                view.SetPlayerDirection(PlayerDirectionEnum.LEFT);
            }
            if (Input.GetKeyDown(KeyCode.S))
            {
                SetPlayerZRotation(0);
                view.SetPlayerDirection(PlayerDirectionEnum.DOWN);
            }
            if (Input.GetKeyDown(KeyCode.D))
            {
                SetPlayerZRotation(0);
                view.SetPlayerDirection(PlayerDirectionEnum.RIGHT);
            }
        }
    }

    /// <summary>
    /// 控制玩家方向
    /// 键盘上的方向键
    /// </summary>
    private void SetDirectionKeyControlPlayerDirection()
    {
        //检测四个斜向的按键
        if (Input.GetKeyDown(KeyCode.UpArrow) && Input.GetKeyDown(KeyCode.LeftArrow))
        {
            SetPlayerZRotation(-45);
            view.SetPlayerDirection(PlayerDirectionEnum.UP);
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow) && Input.GetKeyDown(KeyCode.RightArrow))
        {
            SetPlayerZRotation(45);
            view.SetPlayerDirection(PlayerDirectionEnum.UP);
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow) && Input.GetKeyDown(KeyCode.LeftArrow))
        {
            SetPlayerZRotation(-135);
            view.SetPlayerDirection(PlayerDirectionEnum.DOWN);
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow) && Input.GetKeyDown(KeyCode.RightArrow))
        {
            SetPlayerZRotation(135);
            view.SetPlayerDirection(PlayerDirectionEnum.DOWN);
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                SetPlayerZRotation(0);
                view.SetPlayerDirection(PlayerDirectionEnum.UP);
            }
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                SetPlayerZRotation(0);
                view.SetPlayerDirection(PlayerDirectionEnum.LEFT);
            }
            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                SetPlayerZRotation(0);
                view.SetPlayerDirection(PlayerDirectionEnum.DOWN);
            }
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                SetPlayerZRotation(0);
                view.SetPlayerDirection(PlayerDirectionEnum.RIGHT);
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
}
