using Cysharp.Threading.Tasks;
using UnityEngine;
public static class UIExtension
{
    public static async UniTask<T> UICreate<T>(this T UIClass) where T : UIBase, new()
    {
        var name = typeof(T).Name;
        var GameObject = await App.Instance.Res.LoadAssetAsync<GameObject>($"UI/{name}");
        var ui = GameObject.Instantiate(GameObject);
        var uiComponent = ui.GetComponent<T>();
        return uiComponent;
    }
}