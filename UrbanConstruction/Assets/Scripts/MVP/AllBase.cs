using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AllBase : MonoBehaviour
{
    private void Awake()
    {
        OnAwake();
    }
    protected virtual void OnAwake() { }

    private void Start()
    {
        OnStart();
    }
    protected virtual void OnStart() { }

    private void Update()
    {
        OnUpdate();
    }
    protected virtual void OnUpdate() { }

    private void LateUpdate()
    {
        OnLateUpdate();
    }
    protected virtual void OnLateUpdate() { }
}
