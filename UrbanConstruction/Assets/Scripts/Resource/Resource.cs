using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ResourceType
{
    TRIANGLE,//三角形
    HEXAGON//六边形
}
public class Resource : MonoBehaviour
{
    private const string TAG = "[Resource]:";

    public ResourceType type = ResourceType.TRIANGLE;
    /// <summary>
    /// 机器人是否被激活
    /// </summary>
    private bool RobotCanCollect = false;

    private void Awake()
    {
        EventManager.Instance.AddEvent(EventEnum.WhenRobotCollectResource, RobotCanCollectResource);
    }

    private void OnEnable()
    {
        EventManager.Instance.AddEvent(EventEnum.WhenRobotCollectResource, RobotCanCollectResource);
    }

    private void OnDisable()
    {
        EventManager.Instance.RemoveEvent(EventEnum.WhenRobotCollectResource, RobotCanCollectResource);
    }

    protected void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log(TAG + type);
            if (type == ResourceType.TRIANGLE)
            {
                Debug.Log(TAG + "isRunning");
                EventManager.Instance.DispatchEvent(EventEnum.SetTriangleNum, new IntParam(1));
            }
            else if (type == ResourceType.HEXAGON)
            {
                EventManager.Instance.DispatchEvent(EventEnum.SetHexagonNum, new IntParam(1));
            }
            EventManager.Instance.DispatchEvent(EventEnum.WhenPlayerConsumeEnergy, new FloatParam(10));
        }
        else if (other.CompareTag("Robot"))
        {
            if (!RobotCanCollect) return;
            if (type == ResourceType.TRIANGLE)
            {
                EventManager.Instance.DispatchEvent(EventEnum.SetTriangleNum, new IntParam(1));
            }
            else if (type == ResourceType.HEXAGON)
            {
                EventManager.Instance.DispatchEvent(EventEnum.SetHexagonNum, new IntParam(1));
            }
        }
        EventManager.Instance.DispatchEvent(EventEnum.RemoveResourceFromList, new GameObjectParam(this.gameObject));
        if (RobotCanCollect && this.gameObject == RobotPresenter.currentCollectingResource)
        {
            //说明已经被收集了，需要重新收集
            EventManager.Instance.DispatchEvent(EventEnum.RobotCollectNextResource, null);
        }
        Destroy(this.gameObject);
    }

    private void RobotCanCollectResource(IEventParam ie)
    {
        if (ie is BooleanParam)
        {
            Debug.Log(RobotCanCollect);
            RobotCanCollect = (ie as BooleanParam).value;
        }
    }
}
