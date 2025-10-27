using System;
using System.Collections.Generic;
public class UpdateManager : IManager
{
    private Dictionary<UpdateType, Action> _updateDic = new Dictionary<UpdateType, Action>();
    void IManager.Init()
    {

    }
    internal void Execute(UpdateType type)
    {
        if (_updateDic.ContainsKey(type))
        {
            _updateDic[type]?.Invoke();
        }
    }
    public void RegisterUpdate(UpdateType type, Action action)
    {
        if (_updateDic.ContainsKey(type))
        {
            _updateDic[type] += action;
        }
        else
        {
            _updateDic[type] = action;
        }
    }
    public void UnRegisterUpdate(UpdateType type, Action action)
    {
        if (_updateDic.ContainsKey(type))
        {
            _updateDic[type] -= action;
        }
    }


    void IManager.Dispose()
    {

    }
}
public enum UpdateType
{
    FixedUpdate,
    Update,
    LateUpdate,
}
