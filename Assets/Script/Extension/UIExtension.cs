using Cysharp.Threading.Tasks;
using UnityEngine;
public static class UIExtension
{
    public static async UniTask<T> Create<T>(Transform parent=null) where T : UIBase
    {
        var name = typeof(T).Name;
        var GameObject = await App.Instance.Res.LoadAssetAsync<GameObject>(name);
        var ui = GameObject.Instantiate(GameObject);
        var uiComponent = ui.GetComponent<T>();
        return uiComponent;
    }
}