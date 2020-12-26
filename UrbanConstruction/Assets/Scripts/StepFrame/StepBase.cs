using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StepBase : MonoBehaviour
{
    [SerializeField]
    public int id { set { } get { return lastStep == null ? 0 : lastStep.id + 1; } }

    public StepBase lastStep;

    public Action<StepBase> onCompleteEvent = null;

    public bool isComplete = false;

    public virtual bool check()
    {
        return isComplete;
    }
}
