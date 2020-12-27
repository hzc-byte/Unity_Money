using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager
{
    private const string TAG = "[EventManager]:";

    /// <summary>
    /// 单例
    /// </summary>
    private static EventManager instance;

    /// <summary>
    /// 供外部调用的唯一入口
    /// </summary>
    public static EventManager Instance
    {
        get
        {
            if (null == instance)
            {
                instance = new EventManager();
            }
            return instance;
        }
    }

    public delegate void mEvent(IEventParam ie);

    /// <summary>
    /// 存放所有的事件信息
    /// </summary>
    private Dictionary<EventEnum, List<mEvent>> events = new Dictionary<EventEnum, List<mEvent>>();

    /// <summary>
    /// 构造函数
    /// </summary>
    private EventManager() { }

    /// <summary>
    /// 执行事件
    /// </summary>
    public void DispatchEvent(EventEnum ec, IEventParam ie)
    {
        List<mEvent> lists = null;
        if (events.TryGetValue(ec, out lists))
        {
            foreach (var v in lists)
            {
                v(ie);
            }
        }
        else
        {
            Debug.LogWarning(TAG + "events dont contain " + ec);
        }
    }

    /// <summary>
    /// 添加事件
    /// </summary>
    public void AddEvent(EventEnum ec, mEvent me)
    {
        List<mEvent> lists = null;
        if (events.TryGetValue(ec, out lists))
        {
            if (lists.Contains(me))
            {
                return;
            }
            events[ec].Add(me);
        }
        else
        {
            lists = new List<mEvent>
            {
                me
            };
            events.Add(ec, lists);
        }
    }

    /// <summary>
    /// 移除某个事件
    /// </summary>
    public void RemoveEvent(EventEnum ec, mEvent me)
    {
        List<mEvent> lists = null;
        if (events.TryGetValue(ec, out lists))
        {
            if (lists.Contains(me))
            {
                events[ec].Remove(me);
            }
            else
            {
                Debug.LogWarning(TAG + "events dont contain this list " + ec);
            }
        }
        else
        {
            Debug.LogWarning(TAG + "events dont contain this list " + ec);
        }
    }

    /// <summary>
    /// 移除所有事件
    /// </summary>
    public void RemoveAll(EventEnum ec)
    {
        List<mEvent> lists = null;
        if (events.TryGetValue(ec, out lists))
        {
            foreach (var v in lists)
            {
                RemoveEvent(ec, v);
            }
        }
        else
        {
            Debug.LogWarning(TAG + "events dont contain this list");
        }
    }
}
