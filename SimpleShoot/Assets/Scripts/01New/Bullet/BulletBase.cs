using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum BulletType
{
    enemy,
    player
}
public class BulletBase : MonoBehaviour
{
    public BulletType bulletType = BulletType.player;

    public float damage = 10;

    public float timer = 3;

    public float speed = 100;

    public Vector3 originalPos;

    protected Animator animator;

    protected new Rigidbody2D rigidbody2D;

    protected bool isMove = true;

    public virtual void Awake()
    {
        originalPos = this.transform.localPosition;
        animator = this.GetComponent<Animator>();
        rigidbody2D = this.GetComponent<Rigidbody2D>();
    }

    public virtual void Update()
    {
        Move();
        Timer();
    }

    public virtual void OnDisable()
    {
        this.transform.localPosition = originalPos;
    }

    public virtual void OnTriggerEnter2D(Collider2D collision)
    {
        
    }

    public void Move()
    {
        if (isMove)
            transform.Translate(Vector3.left * Time.deltaTime * speed, Space.Self);
    }

    public void Timer()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            timer = 3;
            Destroy(this.gameObject);
        }
    }

    public virtual void Disappear()
    {
        this.gameObject.SetActive(false);
        Destroy(this.gameObject);
    }
}
