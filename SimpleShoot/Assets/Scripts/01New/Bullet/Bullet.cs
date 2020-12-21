using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : BulletBase
{
    public override void Awake()
    {
        base.Awake();
    }

    public override void Update()
    {
        base.Update();
    }

    public override void OnDisable()
    {
        base.OnDisable();
    }

    public override void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(bulletType + "   " + collision.name);
        switch (bulletType)
        {
            case BulletType.player:
                if (collision.CompareTag("Enemy"))
                {
                    isMove = false;
                    Debug.Log("Damage = " + damage);
                    collision.GetComponent<Enemy>().GetDamage(damage);
                    Disappear();
                }
                break;
            case BulletType.enemy:
                if (collision.CompareTag("Player"))
                {
                    isMove = false;
                    Debug.Log("Damage = " + damage);
                    collision.GetComponent<CompatController>().GetDamage(damage);
                    Disappear();
                }
                break;
        }
    }

    public override void Disappear()
    {
        base.Disappear();
        Destroy(this.gameObject);
    }

    public void SetColor(Color color)
    {
        this.GetComponent<SpriteRenderer>().color = color;
    }
}
