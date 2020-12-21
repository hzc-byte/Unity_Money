using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    public CreateBullet createBullet;

    [HideInInspector]
    public SpriteRenderer render;

    public float totalBlood = 0;

    protected Rigidbody2D rigidbody2d;

    //是否按下移动按钮
    public bool MoveDown;

    public float jumpSpeed = 5f;

    public bool isJump = true;

    [SerializeField]
    protected bool isJumpDown = false;
    [SerializeField]
    protected float playerY = -5.7f;

    protected virtual void Awake()
    {
        render = this.GetComponent<SpriteRenderer>();
        rigidbody2d = this.GetComponent<Rigidbody2D>();
    }

    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Ground"))
        {
            isJump = true;
            isJumpDown = false;
        }
    }

    protected void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Ground") || collision.collider.CompareTag("XianJing"))
        {
            isJump = false;
            jumpTime = 0;
        }
    }

    private int jumpTime = 0;
    public void SetJumpUp()
    {
        jumpTime += 1;
        if (jumpTime >= 2)
        {
            if (!isJump) return;
        }
        isJumpDown = false;
        rigidbody2d.AddForce(Vector2.up * jumpSpeed);
    }
}
