using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Cysharp.Threading.Tasks;

public class EventManager : IManager
{
    private Dictionary<EventType, Action> _eventDic = new Dictionary<EventType, Action>();
    void IManager.Dispose()
    {

    }

    async UniTask IManager.Init()
    {

    }
    public void AddListener(EventType type, Action action)
    {
        if (_eventDic.ContainsKey(type))
        {
            _eventDic[type] += action;
        }
        else
        {
            _eventDic[type] = action;
        }
    }
    public void RemoveListener(EventType type, Action action)
    {
        if (_eventDic.ContainsKey(type))
        {
            _eventDic[type] -= action;
        }
    }
}
