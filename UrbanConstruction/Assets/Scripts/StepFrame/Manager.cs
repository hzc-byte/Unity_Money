using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : StepManagerBase
{
    protected override void OnComplete()
    {
        base.OnComplete();
        Debug.Log("流程结束");
    }
}
