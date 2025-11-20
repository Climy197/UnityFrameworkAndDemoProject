using Cysharp.Threading.Tasks;

public class Config
{
    private ItemConfigCollection ItemConfigCollection = new ItemConfigCollection();
    public ItemConfigCollection Item => ItemConfigCollection;

    public async UniTask Init()
    {
        await ItemConfigCollection.Init();

    }
}
