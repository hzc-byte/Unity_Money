using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActivateView : ViewBase
{
    /// <summary>
    /// 激活按钮
    /// </summary>
    public Button ActivateBtn;

    /// <summary>
    /// 收集按钮
    /// </summary>
    public Button CollentBtn;

    public Button PlantationBtn;

    public Button PetBtn;

    public Button RepairBtn;

    public Button MedicationBtn;

    /// <summary>
    /// 建造按钮
    /// </summary>
    public Button BuildBtn;

    protected override void OnStart()
    {
        base.OnStart();
        ActivateBtn.onClick.RemoveAllListeners();
        ActivateBtn.onClick.AddListener(delegate
        {
            EventManager.Instance.DispatchEvent(EventEnum.WhenRobotFollowPlayer, new BooleanParam(true));
        });
    }
}
