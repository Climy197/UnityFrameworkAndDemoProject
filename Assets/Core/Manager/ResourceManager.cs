using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using YooAsset;


public class ResourceManager : IManager
{
    private readonly string DefaultPackageName = "DefaultPackage";
    Dictionary<string, UnityEngine.Object> m_AssetCache = new Dictionary<string, UnityEngine.Object>();
    
    public async UniTask<T> LoadAssetAsync<T>(string assetName) where T : UnityEngine.Object
    {
        if (m_AssetCache.TryGetValue(assetName, out var asset))
        {
            return asset as T;
        }
        var handle = YooAssets.LoadAssetAsync<T>(assetName);
        await handle.ToUniTask();
        if (handle.Status != EOperationStatus.Succeed)
        {
            Debug.LogError($"[Res] 加载失败: {assetName} | {handle.LastError}");
            handle.Release(); // 失败立即释放
            return null;
        }
        m_AssetCache[assetName] = handle.AssetObject;
        return handle.AssetObject as T;
    }

    void IManager.Init()
    {
        YooAssets.Initialize();
        var package = YooAssets.GetPackage(DefaultPackageName);
        YooAssets.SetDefaultPackage(package);
    }

    void IManager.Dispose()
    {
        throw new System.NotImplementedException();
    }
}
