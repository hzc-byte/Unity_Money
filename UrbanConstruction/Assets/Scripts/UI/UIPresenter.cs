using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIPresenter : PresenterBase<UIView, UIModel>
{
    void Start()
    {
        StartCoroutine(view.StartText.Print(model.StartContent, 0.1f, delegate
        {
            view.SetStartBlackBGFadeOut(3, delegate
            {
                view.StartBlackBG.gameObject.SetActive(false);
                //控制主角可以被控制
                EventManager.Instance.DispatchEvent(EventEnum.PlayerCanBeControled, new BooleanParam(true));
            });
        }));
    }
}
