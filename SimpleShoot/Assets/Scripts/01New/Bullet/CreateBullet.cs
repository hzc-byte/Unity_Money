using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class CreateBullet : MonoBehaviour
{
    public CompatController compatController;
    public Enemy enemy;
    public int dir = 1;
    public Color BulletColor = new Color(0, 0, 0, 1);
    private void Start()
    {
        compatController = GameObject.Find("Player").GetComponent<CompatController>();
    }

    public void Create(BulletType bulletType)
    {
        switch (bulletType)
        {
            case BulletType.player:
                GameObject Player_Bullet = Instantiate(Resources.Load<GameObject>("Bullets/PlayerBullet"));
                Player_Bullet.transform.SetParent(transform);
                Player_Bullet.transform.localPosition = new Vector3(-0.34f, 0, 0);
                Player_Bullet.transform.localEulerAngles = Vector3.zero;
                Player_Bullet.transform.localScale = new Vector3(1.2f, 1.2f, 1.2f);
                Player_Bullet.transform.SetParent(GameObject.Find("Bullet").transform);
                Bullet playerBullet = Player_Bullet.GetComponent<Bullet>();
                playerBullet.speed *= dir;
                playerBullet.damage += compatController.damage;
                Debug.Log("BulletColor =" + BulletColor);
                playerBullet.SetColor(BulletColor);
                break;
            case BulletType.enemy:
                GameObject Enemy_Bullet = Instantiate(Resources.Load<GameObject>("Bullets/EnemyBullet"));
                Enemy_Bullet.transform.SetParent(transform);
                Enemy_Bullet.transform.localPosition = new Vector3(-0.34f, 0, 0);
                Enemy_Bullet.transform.localEulerAngles = Vector3.zero;
                Enemy_Bullet.transform.localScale = new Vector3(1.2f, 1.2f, 1.2f);
                Enemy_Bullet.transform.SetParent(GameObject.Find("Bullet").transform);
                Bullet enemyBullet = Enemy_Bullet.GetComponent<Bullet>();
                enemyBullet.speed *= dir;
                enemyBullet.damage += enemy.damage;
                break;
        }

    }
}
