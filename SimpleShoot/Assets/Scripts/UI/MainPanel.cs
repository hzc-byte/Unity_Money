using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;
using System;

public class MainPanel : MonoBehaviour
{
    #region 关卡相关控件
    public Image playerBlood;
    public Text text;

    #endregion
    public CompatController player;
    private Vector3 downScale = new Vector3(0.6f, 0.6f, 0.6f);


    public static Action saveGameEvent;
    public static Action loadGameEvent;

    private void Update()
    {
#if !UNITY_ANDROID
        if (Input.GetKeyDown(KeyCode.A))
        {
            if (player.isDie) return;
            player.transform.localScale = new Vector3(25.82f, 25.82f, 25.82f);
            player.createBullet.dir = 1;
            player.MoveDown = true;
            player.MoveSpeed = -5f;
        }
        if (Input.GetKeyUp(KeyCode.A))
        {
            if (player.isDie) return;
            player.MoveDown = false;
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            if (player.isDie) return;
            player.transform.localScale = new Vector3(-25.82f, 25.82f, 25.82f);
            player.createBullet.dir = -3;
            player.MoveDown = true;
            player.MoveSpeed = 5f;
        }
        if (Input.GetKeyUp(KeyCode.D))
        {
            if (player.isDie) return;
            player.MoveDown = false;
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            if (player.isDie) return;
            player.SetJumpUp();
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (player.isDie) return;
            player.MoveDown = false;
            player.CreateBullet();
        }
#endif
    }

    public void UpdatePlayerData(float blood)
    {
        playerBlood.DOFillAmount((1 - blood * 0.01f), 0.5f);
        if(blood<0)
            blood=0;
        text.text=blood.ToString();
    }
}
