using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class UIView : ViewBase
{
    /// <summary>
    /// 开始的黑屏
    /// </summary>
    public Image StartBlackBG;
    /// <summary>
    /// 结束的黑屏
    /// </summary>
    public Image EndBlackBG;
    /// <summary>
    /// 开始画面角色
    /// </summary>
    public Image StartRole;
    /// <summary>
    /// 开始字幕的文字
    /// </summary>
    public Text StartText;
    /// <summary>
    /// 结束字幕的文字
    /// </summary>
    public Text EndText;
    /// <summary>
    /// 六边形Text组件
    /// </summary>
    public Text HexagonNumText;
    /// <summary>
    /// 三边形Text组件
    /// </summary>
    public Text TriangleNumText;
    /// <summary>
    /// 任务按钮
    /// </summary>
    [Header("任务按钮")]
    public Button TaskBtn;
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
    /// 任务界面返回主界面的按钮
    /// </summary>
    [Header("任务界面返回主界面的按钮")]
    public Button TaskBackToMainPanelBtn;
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
    /// 建造按钮
    /// </summary>
    [Header("建造按钮")]
    public Button BuildBtn;
    /// <summary>
    /// 所有材料的面板
    /// </summary>
    public GameObject TaskPanel;
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
    /// <summary>
    /// 房子
    /// </summary>
    public GameObject House;
    /// <summary>
    /// 是否显示Bot按钮
    /// </summary>
    private bool isShowBotBtn;
    /// <summary>
    /// 激活面板的激活类型
    /// </summary>
    private ActivateType type = ActivateType.None;
    /// <summary>
    /// 材料的类型
    /// </summary>
    private MaterialType materialType = MaterialType.NONE;
    /// <summary>
    /// 控制机器人站起来并且说话
    /// </summary>
    [SerializeField]
    public static int ControlRobotStartUpAndSayHi = 0;
    /// <summary>
    /// 收集到的六边形数量
    /// </summary>
    private int hexagonNum = 0;
    /// <summary>
    /// 收集到的三边形数量
    /// </summary>
    private int triangleNum = 0;
    /// <summary>
    /// 用于判断是否有房子
    /// </summary>
    public static bool hasHouse;

    protected override void OnAwake()
    {
        base.OnAwake();
        EventManager.Instance.AddEvent(EventEnum.WhenRobotBecameVisible, ShowBotBtn);
        EventManager.Instance.AddEvent(EventEnum.ClickWhatWhenInActivateChoosePanel, SetActivateType);
        EventManager.Instance.AddEvent(EventEnum.FirstShowTaskPanel, ShowTaskPanel);
        EventManager.Instance.AddEvent(EventEnum.MaterialsChooseType, MaterialsChooseType);
        EventManager.Instance.AddEvent(EventEnum.SetHexagonNum, SetHexagonNum);
        EventManager.Instance.AddEvent(EventEnum.SetTriangleNum, SetTriangleNum);
        EventManager.Instance.AddEvent(EventEnum.GameFailure, Failure);
    }

    protected override void OnStart()
    {
        base.OnStart();
        EventManager.Instance.DispatchEvent(EventEnum.ShowOrHideResources, new BooleanParam(false));
        TaskBtn.onClick.RemoveAllListeners();
        TaskBtn.onClick.AddListener(delegate
        {
            AllUIElementInMainPanel(false);
            AllUIElementInTaskPanel(true);
            //控制主角取消控制
            EventManager.Instance.DispatchEvent(EventEnum.PlayerCanBeControled, new BooleanParam(false));
        });

        TaskBackToMainPanelBtn.onClick.RemoveAllListeners();
        TaskBackToMainPanelBtn.onClick.AddListener(delegate
        {
            AllUIElementInMainPanel(true);
            AllUIElementInTaskPanel(false);
            if (isShowBotBtn)
            {
                BotBtn.transform.parent.gameObject.SetActive(true);
            }
            //控制主角取消控制
            EventManager.Instance.DispatchEvent(EventEnum.PlayerCanBeControled, new BooleanParam(true));
        });

        BackpackBtn.onClick.RemoveAllListeners();
        BackpackBtn.onClick.AddListener(delegate
        {
            AllUIElementInMainPanel(false);
            AllUIElementInBackpackPanel(true);
            //控制主角取消控制
            EventManager.Instance.DispatchEvent(EventEnum.PlayerCanBeControled, new BooleanParam(false));
        });

        DetailsBtn.onClick.RemoveAllListeners();
        DetailsBtn.onClick.AddListener(delegate
        {
            switch (materialType)
            {
                case MaterialType.MATERIAL:
                    AllUIElementInDetailsPanel(true);
                    break;
                default:
                    return;
            }
            AllUIElementInBackpackPanel(false);

            //控制主角取消控制
            EventManager.Instance.DispatchEvent(EventEnum.PlayerCanBeControled, new BooleanParam(false));
        });

        BotBtn.onClick.RemoveAllListeners();
        BotBtn.onClick.AddListener(delegate
        {
            AllUIElementInMainPanel(false);
            AllUIElementInActivatePanel(true);
            //控制主角取消控制
            EventManager.Instance.DispatchEvent(EventEnum.PlayerCanBeControled, new BooleanParam(false));
        });

        BackToMainPanelBtn.onClick.RemoveAllListeners();
        BackToMainPanelBtn.onClick.AddListener(delegate
        {
            AllUIElementInActivatePanel(false);
            AllUIElementInBackpackPanel(false);
            AllUIElementInMainPanel(true);
            //控制主角取消控制
            EventManager.Instance.DispatchEvent(EventEnum.PlayerCanBeControled, new BooleanParam(true));
            //TODO:控制机器人站起来说hi
            if (ControlRobotStartUpAndSayHi == 1)
            {
                ControlRobotStartUpAndSayHi = 2;
                EventManager.Instance.DispatchEvent(EventEnum.WhenRobotFollowPlayer, new BooleanParam(true));
            }
            if (ControlRobotStartUpAndSayHi == 3)
            {
                ControlRobotStartUpAndSayHi = 4;
                EventManager.Instance.DispatchEvent(EventEnum.WhenRobotCollectResource, new BooleanParam(true));
            }
        });

        BackToMaterialsChoosePanelBtn.onClick.RemoveAllListeners();
        BackToMaterialsChoosePanelBtn.onClick.AddListener(delegate
        {
            AllUIElementInDetailsPanel(false);
            AllUIElementInBackpackPanel(true);
        });

        ActivateRobotBtn.onClick.RemoveAllListeners();
        ActivateRobotBtn.onClick.AddListener(delegate
        {
            switch (type)
            {
                case ActivateType.None:
                    break;
                case ActivateType.ACTIVATE:
                    if (ControlRobotStartUpAndSayHi == 0)
                    {
                        ControlRobotStartUpAndSayHi = 1;
                    }
                    break;
                case ActivateType.COLLECT:
                    if (ControlRobotStartUpAndSayHi == 2)
                    {
                        ControlRobotStartUpAndSayHi = 3;
                    }
                    break;
                case ActivateType.PLANTATION:
                    break;
                case ActivateType.PET:
                    break;
                case ActivateType.REPAIR:
                    break;
                case ActivateType.MEDICATION:
                    break;
                case ActivateType.BUILD:
                    break;
            }
            EventManager.Instance.DispatchEvent(EventEnum.ClickRegisterColorChange, new BooleanParam(true));
        });

        BuildBtn.onClick.RemoveAllListeners();
        BuildBtn.onClick.AddListener(delegate
        {
            House.SetActive(true);
            hasHouse = true;
            EndBlackBG.gameObject.SetActive(true);
            EndBlackBG.DOFade(1, 0.5f).OnComplete(delegate
            {
                AllUIElementInDetailsPanel(false);
                AllUIElementInActivatePanel(false);
                AllUIElementInBackpackPanel(false);
                AllUIElementInMainPanel(true);
                EventManager.Instance.DispatchEvent(EventEnum.HouseIsBuilded, null);
                StartCoroutine(EndText.Print("Congratulations! You built the shelter successfully! Have a good rest! ", 0.1f, delegate
                {
                    EndBlackBG.DOFade(0, 1f).OnComplete(delegate
                    {
                        EndBlackBG.gameObject.SetActive(false);
                        //控制主角取消控制
                        EventManager.Instance.DispatchEvent(EventEnum.PlayerCanBeControled, new BooleanParam(true));
                    });
                }));
            });
        });
    }

    protected void OnEnable()
    {
        EventManager.Instance.AddEvent(EventEnum.WhenRobotBecameVisible, ShowBotBtn);
        EventManager.Instance.AddEvent(EventEnum.ClickWhatWhenInActivateChoosePanel, SetActivateType);
        EventManager.Instance.AddEvent(EventEnum.FirstShowTaskPanel, ShowTaskPanel);
        EventManager.Instance.AddEvent(EventEnum.MaterialsChooseType, MaterialsChooseType);
        EventManager.Instance.AddEvent(EventEnum.SetHexagonNum, SetHexagonNum);
        EventManager.Instance.AddEvent(EventEnum.SetTriangleNum, SetTriangleNum);
        EventManager.Instance.AddEvent(EventEnum.GameFailure, Failure);
    }

    protected void OnDisable()
    {
        EventManager.Instance.RemoveEvent(EventEnum.WhenRobotBecameVisible, ShowBotBtn);
        EventManager.Instance.RemoveEvent(EventEnum.ClickWhatWhenInActivateChoosePanel, SetActivateType);
        EventManager.Instance.RemoveEvent(EventEnum.FirstShowTaskPanel, ShowTaskPanel);
        EventManager.Instance.RemoveEvent(EventEnum.MaterialsChooseType, MaterialsChooseType);
        EventManager.Instance.RemoveEvent(EventEnum.SetHexagonNum, SetHexagonNum);
        EventManager.Instance.RemoveEvent(EventEnum.SetTriangleNum, SetTriangleNum);
        EventManager.Instance.RemoveEvent(EventEnum.GameFailure, Failure);
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

    /// <summary>
    /// 是否展示Bot按钮
    /// </summary>
    /// <param name="ie"></param>
    private void ShowBotBtn(IEventParam ie)
    {
        if (ie is BooleanParam)
        {
            isShowBotBtn = (ie as BooleanParam).value;
            BotBtn.transform.parent.gameObject.SetActive(isShowBotBtn);
        }
    }

    #region 界面元素开关

    /// <summary>
    /// 主界面的所有元素
    /// </summary>
    /// <param name="isShow"></param>
    private void AllUIElementInMainPanel(bool isShow)
    {
        TaskBtn.transform.parent.gameObject.SetActive(isShow);
        BackpackBtn.transform.parent.gameObject.SetActive(isShow);
        if (isShowBotBtn)
        {
            BotBtn.transform.parent.gameObject.SetActive(isShow);
        }
    }

    /// <summary>
    /// 任务界面的所有元素
    /// </summary>
    private void AllUIElementInTaskPanel(bool isShow)
    {
        TaskPanel.SetActive(isShow);
        TaskBackToMainPanelBtn.transform.parent.gameObject.SetActive(isShow);
    }

    /// <summary>
    /// 背包界面的所有的元素
    /// </summary>
    /// <param name="isShow"></param>
    private void AllUIElementInBackpackPanel(bool isShow)
    {
        MaterialsPanel.SetActive(isShow);
        DetailsBtn.transform.parent.gameObject.SetActive(isShow);
        BackToMainPanelBtn.transform.parent.gameObject.SetActive(isShow);
    }

    /// <summary>
    /// 材料详细界面的所有的元素
    /// </summary>
    /// <param name="isShow"></param>
    private void AllUIElementInDetailsPanel(bool isShow)
    {
        DetailMaterialPanel.SetActive(isShow);
        BackToMaterialsChoosePanelBtn.transform.parent.gameObject.SetActive(isShow);
    }

    /// <summary>
    /// 机器人激活界面的所有的元素
    /// </summary>
    private void AllUIElementInActivatePanel(bool isShow)
    {
        ActivatePanel.SetActive(isShow);
        ActivateRobotBtn.transform.parent.gameObject.SetActive(isShow);
        BackToMainPanelBtn.transform.parent.gameObject.SetActive(isShow);
    }
    #endregion

    /// <summary>
    /// 设置激活的种类
    /// </summary>
    /// <param name="ie"></param>
    private void SetActivateType(IEventParam ie)
    {
        if (ie is ActivateTypeParam)
        {
            type = (ie as ActivateTypeParam).value;
        }
    }

    /// <summary>
    /// 初次展示任务面板
    /// </summary>
    private void ShowTaskPanel(IEventParam ie)
    {
        AllUIElementInMainPanel(false);
        AllUIElementInTaskPanel(true);
        //控制主角取消控制
        EventManager.Instance.DispatchEvent(EventEnum.PlayerCanBeControled, new BooleanParam(false));
    }

    /// <summary>
    /// 选择材料类型
    /// </summary>
    /// <param name="ie"></param>
    private void MaterialsChooseType(IEventParam ie)
    {
        if (ie is ChooseMaterialType)
        {
            materialType = (ie as ChooseMaterialType).value;
        }
    }
    /// <summary>
    /// 设置六边形的数量
    /// </summary>
    private void SetHexagonNum(IEventParam ie)
    {
        if (ie is IntParam)
        {
            hexagonNum += (ie as IntParam).value;
            HexagonNumText.text = hexagonNum.ToString();
            ShowBuildBtn();
        }
    }
    /// <summary>
    /// 设置三边形的数量
    /// </summary>
    private void SetTriangleNum(IEventParam ie)
    {
        if (ie is IntParam)
        {
            triangleNum += (ie as IntParam).value;
            TriangleNumText.text = triangleNum.ToString();
            ShowBuildBtn();
        }
    }
    /// <summary>
    /// 展示Build按钮
    /// </summary>
    private void ShowBuildBtn()
    {
        if (hexagonNum >= 10 && triangleNum >= 5)
        {
            BuildBtn.transform.parent.gameObject.SetActive(true);
            EventManager.Instance.DispatchEvent(EventEnum.WhenRobotCollectResource, new BooleanParam(false));
        }
        else
        {
            BuildBtn.transform.parent.gameObject.SetActive(false);
        }
    }

    /// <summary>
    /// 游戏失败
    /// </summary>
    private void Failure(IEventParam ie)
    {
        EndBlackBG.gameObject.SetActive(true);
        EndBlackBG.DOFade(1, 0.5f).OnComplete(delegate
        {
            StartCoroutine(EndText.Print("Sorry, you did not build the shelter successfully before you run out of your energy. Please try again.", 0.1f, delegate
            {

            }));
        });
    }
}
