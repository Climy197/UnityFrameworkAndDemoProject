using Cysharp.Threading.Tasks;

internal interface IManager
{
    UniTask Init();
    void Dispose();
}