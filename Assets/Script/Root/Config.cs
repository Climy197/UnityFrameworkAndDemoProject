using System.Threading.Tasks;

public class Config
{
    private ItemConfigCollection itemConfigCollection = new ItemConfigCollection();
    public ItemConfigCollection Item => itemConfigCollection;
    public async Task Init()
    {
        await itemConfigCollection.Init();
    }
}