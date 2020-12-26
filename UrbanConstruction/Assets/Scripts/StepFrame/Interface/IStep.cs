using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IStep
{
    void OnStart();

    void OnUpdate();

    void OnComplete();

    void OnReset();
}
