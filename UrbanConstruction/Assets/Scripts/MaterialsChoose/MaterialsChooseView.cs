using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum MaterialType
{
    NONE,
    SEED,
    BLUEPRINT,
    MATERIAL
}

public class MaterialsChooseView : ViewBase
{
    public MaterialType type = MaterialType.NONE;

    public Button SeedBtn;

    public Button BluepringBtn;
    /// <summary>
    /// 收集到的材料
    /// </summary>
    public Button MaterialBtn;

    protected override void OnStart()
    {
        base.OnStart();
        MaterialBtn.onClick.RemoveAllListeners();
        MaterialBtn.onClick.AddListener(delegate
        {
            type = MaterialType.MATERIAL;
            EventManager.Instance.DispatchEvent(EventEnum.MaterialsChooseType, new ChooseMaterialType(type));
            ChangeButtonColor(MaterialBtn,Color.black,Color.white);
            ChangeButtonColor(SeedBtn, Color.white, Color.black);
            ChangeButtonColor(BluepringBtn, Color.white, Color.black);
        });

        SeedBtn.onClick.RemoveAllListeners();
        SeedBtn.onClick.AddListener(delegate
        {
            type = MaterialType.SEED;
            EventManager.Instance.DispatchEvent(EventEnum.MaterialsChooseType, new ChooseMaterialType(type));
            ChangeButtonColor(SeedBtn, Color.black, Color.white);
            ChangeButtonColor(MaterialBtn, Color.white, Color.black);
            ChangeButtonColor(BluepringBtn, Color.white, Color.black);
        });

        BluepringBtn.onClick.RemoveAllListeners();
        BluepringBtn.onClick.AddListener(delegate
        {
            type = MaterialType.BLUEPRINT;
            EventManager.Instance.DispatchEvent(EventEnum.MaterialsChooseType, new ChooseMaterialType(type));
            ChangeButtonColor(BluepringBtn, Color.black, Color.white);
            ChangeButtonColor(MaterialBtn, Color.white, Color.black);
            ChangeButtonColor(SeedBtn, Color.white, Color.black);
        });
    }

    private void ChangeButtonColor(Button button, Color color1, Color color2)
    {
        button.GetComponent<Image>().color = color1;
        button.transform.GetChild(0).GetComponent<Image>().color = color2;
    }
}
