using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using YooAsset;


public class ResourceManager : IManager
{
    private readonly string DefaultPackageName = "DefaultPackage";

    public async UniTask<T> LoadAssetAsync<T>(string assetName) where T : UnityEngine.Object
    {
        var assetOperation = YooAssets.LoadAssetAsync<T>(assetName);
        await assetOperation.ToUniTask();
        return assetOperation.AssetObject as T;
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
