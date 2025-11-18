using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class MainView : ViewBase
{
    MainPanel panel;
    protected override async UniTask<UIBase> OnInit()
    {
        panel = await UIExtension.Create<MainPanel>();
        return panel;
    }

}
