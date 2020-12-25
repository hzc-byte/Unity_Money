using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEventParam
{

}

public class BooleanParam : IEventParam
{
    public bool value;
    public BooleanParam(bool value)
    {
        this.value = value;
    }
}

public class FloatParam : IEventParam
{
    public float value;
    public FloatParam(float value)
    {
        this.value = value;
    }
}
