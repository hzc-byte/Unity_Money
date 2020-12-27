using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum ActivateType
{
    None,
    ACTIVATE,
    COLLECT,
    PLANTATION,
    PET,
    REPAIR,
    MEDICATION,
    BUILD
}

public class ActivateView : ViewBase
{
    //激活类型
    [SerializeField]
    private ActivateType type = ActivateType.None;
    [SerializeField]
    private List<ActivateType> hasActivatedType = new List<ActivateType>();

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

    protected override void OnAwake()
    {
        base.OnAwake();
        EventManager.Instance.AddEvent(EventEnum.ClickRegisterColorChange, SelcetWhichChangeColor);
    }

    protected override void OnStart()
    {
        base.OnStart();
        ActivateBtn.onClick.RemoveAllListeners();
        ActivateBtn.onClick.AddListener(delegate
        {
            SelectActivateType(ActivateType.ACTIVATE);
        });

        CollentBtn.onClick.RemoveAllListeners();
        CollentBtn.onClick.AddListener(delegate
        {
            SelectActivateType(ActivateType.COLLECT);
        });

        PlantationBtn.onClick.RemoveAllListeners();
        PlantationBtn.onClick.AddListener(delegate
        {
            SelectActivateType(ActivateType.PLANTATION);
        });

        PetBtn.onClick.RemoveAllListeners();
        PetBtn.onClick.AddListener(delegate
        {
            SelectActivateType(ActivateType.PET);
        });

        RepairBtn.onClick.RemoveAllListeners();
        RepairBtn.onClick.AddListener(delegate
        {
            SelectActivateType(ActivateType.REPAIR);
        });

        MedicationBtn.onClick.RemoveAllListeners();
        MedicationBtn.onClick.AddListener(delegate
        {
            SelectActivateType(ActivateType.MEDICATION);
        });

        BuildBtn.onClick.RemoveAllListeners();
        BuildBtn.onClick.AddListener(delegate
        {
            SelectActivateType(ActivateType.BUILD);
        });
    }

    protected void OnEnable()
    {
        EventManager.Instance.AddEvent(EventEnum.ClickRegisterColorChange, SelcetWhichChangeColor);
    }

    protected void OnDisable()
    {
        EventManager.Instance.RemoveEvent(EventEnum.ClickRegisterColorChange, SelcetWhichChangeColor);
    }

    /// <summary>
    /// 选择激活的类型
    /// </summary>
    private void SelectActivateType(ActivateType m_type)
    {
        type = m_type;
        EventManager.Instance.DispatchEvent(EventEnum.ClickWhatWhenInActivateChoosePanel, new ActivateTypeParam(m_type));
    }

    private void SelcetWhichChangeColor(IEventParam ie)
    {
        if (ie is BooleanParam)
        {
            if (hasActivatedType.Contains(type))
            {
                return;
            }
            switch (type)
            {
                case ActivateType.ACTIVATE:
                    hasActivatedType.Add(type);
                    ChangeColor(ActivateBtn);
                    break;
                case ActivateType.COLLECT:
                    //如果机器人没有激活是不允许收集功能激活的
                    if (!hasActivatedType.Contains(ActivateType.ACTIVATE))
                    {
                        return;
                    }
                    if (UIView.ControlRobotStartUpAndSayHi != 3)
                    {
                        return;
                    }
                    hasActivatedType.Add(type);
                    ChangeColor(CollentBtn);
                    break;
            }
        }
    }

    /// <summary>
    /// 修改颜色
    /// </summary>
    private void ChangeColor(Button btn)
    {
        btn.gameObject.GetComponent<Image>().color = new Color(0, 0, 0, 1);
        btn.transform.GetChild(0).GetComponent<Text>().color = new Color(1, 1, 1, 1);
    }
}
