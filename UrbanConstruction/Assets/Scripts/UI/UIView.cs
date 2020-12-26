using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIView : ViewBase
{
    /// <summary>
    /// 开始的黑屏
    /// </summary>
    public Image StartBlackBG;
    /// <summary>
    /// 开始画面角色
    /// </summary>
    public Image StartRole;
    /// <summary>
    /// 开始字幕的文字
    /// </summary>
    public Text StartText;

    /// <summary>
    /// 背包按钮
    /// </summary>
    [Header("背包按钮")]
    public Button BackpackBtn;
    /// <summary>
    /// 详细材料的按钮
    /// </summary>
    [Header("详细材料的按钮")]
    public Button DetailsBtn;
    /// <summary>
    /// 机器人程序按钮
    /// </summary>
    [Header("机器人程序按钮")]
    public Button BotBtn;
    /// <summary>
    /// 返回上一个界面按钮
    /// </summary>
    [Header("返回主界面按钮")]
    public Button BackToMainPanelBtn;
    /// <summary>
    /// 返回上一个界面按钮
    /// </summary>
    [Header("从详细界面返回材料界面按钮")]
    public Button BackToMaterialsChoosePanelBtn;
    /// <summary>
    /// 激活机器人跟随的按钮
    /// </summary>
    [Header("激活机器人跟随的按钮")]
    public Button ActivateRobotBtn;
    /// <summary>
    /// 所有材料的面板
    /// </summary>
    public GameObject MaterialsPanel;
    /// <summary>
    /// 详细材料的面板
    /// </summary>
    public GameObject DetailMaterialPanel;
    /// <summary>
    /// 机器人激活面板
    /// </summary>
    public GameObject ActivatePanel;

    protected override void OnStart()
    {
        base.OnStart();
        BackpackBtn.onClick.RemoveAllListeners();
        BackpackBtn.onClick.AddListener(delegate
        {
            MaterialsPanel.SetActive(true);
            BackpackBtn.transform.parent.gameObject.SetActive(false);
            BotBtn.transform.parent.gameObject.SetActive(false);
            DetailsBtn.transform.parent.gameObject.SetActive(true);
            BackToMainPanelBtn.transform.parent.gameObject.SetActive(true);
            //控制主角取消控制
            EventManager.Instance.DispatchEvent(EventEnum.PlayerCanBeControled, new BooleanParam(false));
        });

        DetailsBtn.onClick.RemoveAllListeners();
        DetailsBtn.onClick.AddListener(delegate
        {
            MaterialsPanel.SetActive(false);
            DetailMaterialPanel.SetActive(true);
            DetailsBtn.transform.parent.gameObject.SetActive(false);
            BackToMainPanelBtn.transform.parent.gameObject.SetActive(false);
            BackToMaterialsChoosePanelBtn.transform.parent.gameObject.SetActive(true);
            //控制主角取消控制
            EventManager.Instance.DispatchEvent(EventEnum.PlayerCanBeControled, new BooleanParam(false));
        });

        BotBtn.onClick.RemoveAllListeners();
        BotBtn.onClick.AddListener(delegate
        {
            ActivatePanel.SetActive(true);
            BotBtn.transform.parent.gameObject.SetActive(false);
            ActivateRobotBtn.transform.parent.gameObject.SetActive(true);
            BackToMainPanelBtn.transform.parent.gameObject.SetActive(true);
            //控制主角取消控制
            EventManager.Instance.DispatchEvent(EventEnum.PlayerCanBeControled, new BooleanParam(false));
        });

        BackToMainPanelBtn.onClick.RemoveAllListeners();
        BackToMainPanelBtn.onClick.AddListener(delegate
        {
            MaterialsPanel.SetActive(false);
            ActivatePanel.SetActive(false);
            ActivateRobotBtn.transform.parent.gameObject.SetActive(false);
            BackpackBtn.transform.parent.gameObject.SetActive(true);
            BotBtn.transform.parent.gameObject.SetActive(true);
            DetailsBtn.transform.parent.gameObject.SetActive(false);
            BackToMainPanelBtn.transform.parent.gameObject.SetActive(false);
            //控制主角取消控制
            EventManager.Instance.DispatchEvent(EventEnum.PlayerCanBeControled, new BooleanParam(true));
        });

        BackToMaterialsChoosePanelBtn.onClick.RemoveAllListeners();
        BackToMaterialsChoosePanelBtn.onClick.AddListener(delegate
        {
            DetailMaterialPanel.SetActive(false);
            MaterialsPanel.SetActive(true);
            BackToMaterialsChoosePanelBtn.transform.parent.gameObject.SetActive(false);
            DetailsBtn.transform.parent.gameObject.SetActive(true);
            BackToMainPanelBtn.transform.parent.gameObject.SetActive(true);
        });
    }

    /// <summary>
    /// 开始黑色背景淡出
    /// </summary>
    public void SetStartBlackBGFadeOut(float timer, Action end = null)
    {
        StartBlackBG.PictureFadeOut(timer);
        StartText.PictureFadeOut(timer);
        StartRole.PictureFadeOut(timer, end);
    }
}
