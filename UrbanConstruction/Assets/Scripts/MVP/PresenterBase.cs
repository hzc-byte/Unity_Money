using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PresenterBase<V, M> : AllBase 
{
    protected M model;
    protected V view;
    protected override void OnAwake()
    {
        Init();
    }
    protected void Init()
    {
        model = GetComponent<M>();
        view = GetComponent<V>();
    }

    protected override void OnStart()
    {
        
    }

    protected override void OnUpdate()
    {
        
    }
}
