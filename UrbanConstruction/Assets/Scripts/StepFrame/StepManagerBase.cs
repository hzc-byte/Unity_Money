using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StepManagerBase : MonoBehaviour
{
    public StepBase[] steps;

    public int m_Current = -1;
    [SerializeField]
    private bool isStart = true;

    private void Awake()
    {
        for (int i = 0; i < steps.Length; i++)
        {
            if (i != 0)
            {
                steps[i].lastStep = steps[i - 1];
            }
            if (GetInterface<IStepSetTitle>(steps[i]) != null)
            {
                GetInterface<IStepSetTitle>(steps[i]).OnSetTitle();
            }
        }
    }

    private void Update()
    {
        if (m_Current >= steps.Length || m_Current == -1)
        {
            m_Current = -1;
            OnComplete();
            return;
        }
        if (isStart)
        {
            isStart = false;
            OnStart();
        }
        OnUpdate();
    }

    protected virtual void OnUpdate()
    {
        if (m_Current >= 0 && m_Current < steps.Length)
        {
            if (GetInterface<IStep>(steps[m_Current]) != null)
            {
                GetInterface<IStep>(steps[m_Current]).OnUpdate();
            }
        }
    }


    protected void OnStart()
    {
        if (m_Current >= 0 && m_Current < steps.Length)
        {
            for (int i = 0; i < steps.Length; i++)
            {
                if (m_Current == steps[i].id)
                {
                    if (GetInterface<IStepInit>(steps[m_Current]) != null)
                    {
                        GetInterface<IStepInit>(steps[m_Current]).OnInit();
                    }
                    if (GetInterface<IStep>(steps[m_Current]) != null)
                    {
                        GetInterface<IStep>(steps[m_Current]).OnStart();
                    }
                    steps[m_Current].onCompleteEvent += OnStepCompleted;
                }
            }
        }
    }

    protected virtual void OnComplete()
    {
        Debug.Log("base 流程结束");
    }

    protected void OnStepCompleted(StepBase step)
    {
        if (step.check())
        {
            isStart = true;
            m_Current = step.id + 1;
        }
    }

    protected T GetInterface<T>(StepBase stepBase)
    {
        T step = stepBase.GetComponent<T>();
        if (step == null)
        {
            return default(T);
        }
        return step;
    }
}
