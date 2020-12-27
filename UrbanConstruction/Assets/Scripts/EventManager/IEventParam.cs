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

public class IntParam : IEventParam
{
    public int value;
    public IntParam(int value)
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

public class GameObjectParam : IEventParam
{
    public GameObject value;
    public GameObjectParam(GameObject value)
    {
        this.value = value;
    }
}

public class GameObjectListParam : IEventParam
{
    public List<GameObject> value;
    public GameObjectListParam(List<GameObject> value)
    {
        this.value = value;
    }
}

public class PositionListParam : IEventParam
{
    public List<Vector2> value;
    public PositionListParam(List<Vector2> value)
    {
        this.value = value;
    }
}

public class ActivateTypeParam : IEventParam
{
    public ActivateType value;
    public ActivateTypeParam(ActivateType value)
    {
        this.value = value;
    }
}

public class ChooseMaterialType : IEventParam
{
    public MaterialType value;
    public ChooseMaterialType(MaterialType value)
    {
        this.value = value;
    }
}

