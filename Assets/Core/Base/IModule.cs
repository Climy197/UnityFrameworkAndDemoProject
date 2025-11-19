using Cysharp.Threading.Tasks;

public interface IModule
{
    UniTask Init();
    void Save();
}