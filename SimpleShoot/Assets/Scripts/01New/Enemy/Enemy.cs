using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public bool isGuDingDian = false;

    public float blood = 100.0f;

    public float damage = 10;

    public Transform target;//玩家

    public float detectDistance = 10;//检测距离

    public float attackDistance = 10;//攻击距离

    public Vector3 originalPos;

    public float moveSpeed = 10;

    public float attakcTimer = 3;

    public CreateBullet createBullet;

    protected float temp = 0;

    public bool isDie = false;

    protected SpriteRenderer spriteRenderer;

    protected CompatController player;

    public CreateReward createReward;

    public RewardType rewardType = RewardType.None;

    public bool isTop = true;//判断玩家是否顶部触发

    public virtual void Start()
    {
        originalPos = transform.position;
        spriteRenderer = GetComponent<SpriteRenderer>();
        player = target.GetComponent<CompatController>();
        temp = attakcTimer;
        attakcTimer = 0;
    }

    public virtual void Update()
    {
        //死亡的时候停止一切的操作
        if (!isDie)
            JudgeDistance();
    }

    public virtual void OnDisable()
    {
        
    }

    public virtual void GetDamage(float damage)
    {
        blood -= damage;
        if (blood <= 0)
        {
            isDie = true;
            spriteRenderer.DOFade(0, 0.5f).OnComplete(delegate
            {
                if (target.GetComponent<CompatController>().hasKill < target.GetComponent<CompatController>().enemyNum - 1)
                {
                    target.GetComponent<CompatController>().hasKill += 1;
                }

                createReward.Create(rewardType);
                this.gameObject.SetActive(false);
                spriteRenderer.color = new Color(1, 1, 1, 1);
                Destroy(this.gameObject);
            });
        }
    }

    public virtual void JudgeDistance()
    {
        if (player.isDie) return;
        //玩家进入检测范围
        if (Mathf.Abs(originalPos.x - target.position.x) < detectDistance)
        {
            //当玩家进入警戒的距离,且不再攻击距离当中
            if (Mathf.Abs(transform.position.x - target.position.x) >= attackDistance)
            {
                if (!isGuDingDian)
                {
                    Vector3 dir = new Vector3(originalPos.x - target.position.x, 0, 0).normalized;
                    transform.Translate(-dir * Time.deltaTime * moveSpeed);
                }
            }
            else
            {
                //玩家进入攻击范围
                attakcTimer -= Time.deltaTime;
                if (attakcTimer < 0)
                {
                    attakcTimer = temp;
                    CreateBullet();
                }
            }
        }
        else
        {
            //脱离战斗距离
            attakcTimer = 0;
            if (!isGuDingDian)
            {
                transform.position = Vector3.Lerp(transform.position, originalPos, Time.deltaTime * 3);
            }
        }
    }

    public void CreateBullet()
    {
        createBullet.Create(BulletType.enemy);
    }
}
