using Cysharp.Threading.Tasks;
using SQLite4Unity3d;
public class PlayerModule : IModule
{
    Player m_player;
    public async UniTask Init()
    {
        await App.Instance.DB.AsyncCreateTable<Player>();
    }

    public void Save()
    {
        
    }
}