using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Diagnostics;
using UnityEngine.SceneManagement;
using DG.Tweening;
/// <summary>
/// 动画类型 
/// </summary>
public enum AnimationType
{
    Idel = 0,
    Walk,
    Run,
    Attack,
}
/// <summary>
/// 玩家控制类 继承Controller
/// </summary>
public class CompatController : Controller
{
    public int enemyNum = 4;

    public int hasKill = 0;

    public float MoveSpeed = 0.5f; //移动速度

    public MainPanel mainPanel;

    public float damage = 0;

    public bool isDie = false;

    public bool enemyIsAllDie = false;

    public Color bulletColor = new Color(0, 0, 0, 1);

    protected override void Awake()
    {
        base.Awake();
        MoveDown = false;
    }

    protected void Update()
    {
        if (isDie) return;

        if (MoveDown && !isDie)
        {
            if (transform.position.x < 103f && transform.position.x > 0f)
                transform.Translate(Vector3.right * Time.deltaTime * MoveSpeed);
            else
            {
                if (transform.position.x < 0f)
                    transform.position = new Vector3(0.1f, transform.position.y, transform.position.z);
                if (transform.position.x > 103)
                    transform.position = new Vector3(102.9f, transform.position.y, transform.position.z);
            }
        }

        //判断玩家是否在上跳还是在下落
        if (!isJump)
        {
            if (!isJumpDown)
            {
                if (rigidbody2d.velocity.y < 0)
                {
                    isJumpDown = true;
                }
            }
        }
    }

    /// <summary>
    /// 碰到陷阱
    /// </summary>
    /// <param name="collision"></param>
    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        base.OnCollisionEnter2D(collision);
        if (collision.collider.CompareTag("Over"))
        {
            GetDamage(100);
        }
    }

    /// <summary>
    /// 攻击时建造子弹
    /// </summary>
    public void CreateBullet()
    {
        createBullet.BulletColor = bulletColor;
        createBullet.Create(BulletType.player);
    }

    /// <summary>
    /// 受到伤害
    /// </summary>
    /// <param name="damge"></param>
    public void GetDamage(float damge)
    {
        if (totalBlood <= 0) return;
        totalBlood -= damge;
        mainPanel.UpdatePlayerData(totalBlood);
        if (totalBlood <= 0)
        {
            isDie = true;
            return;
        }
    }

    /// <summary>
    /// 吃到药包
    /// </summary>
    /// <param name="blood"></param>
    public void GetHealth(float blood)
    {
        totalBlood += blood;
        if (totalBlood > 100)
            totalBlood = 100;
        mainPanel.UpdatePlayerData(totalBlood);
    }

    /// <summary>
    /// 吃到增长攻击
    /// </summary>
    /// <param name="attack"></param>
    public void GetAttack(float attack)
    {
        damage += attack;
        bulletColor = new Color(1, 0, 0, 1);
    }
}


