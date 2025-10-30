using System;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class GameEntry : IEntry
{
    public async UniTask Init()
    {
        //初始化游戏
        await UniTask.Yield();
        Debug.Log("GameEntry Init");
        await App.Instance.UI.PushView<LoginView>();
    }
}