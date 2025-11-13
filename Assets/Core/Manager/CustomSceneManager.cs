using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using UnityEngine.SceneManagement;
using System;
public class CustomSceneManager : IManager
{
    public void Dispose()
    {

    }

    public UniTask Init()
    {
        return UniTask.CompletedTask;
    }

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
    public async UniTask LoadSceneAsync(string sceneName, Action<float> onProgress = null)
    {
        var asyncOp = SceneManager.LoadSceneAsync(sceneName);
        asyncOp.allowSceneActivation = false;

        while (asyncOp.progress < 0.9f)
        {
            onProgress?.Invoke(asyncOp.progress);
            await UniTask.Yield();
        }

        onProgress?.Invoke(1f);
        asyncOp.allowSceneActivation = true;
    }
}