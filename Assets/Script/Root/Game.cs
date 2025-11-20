using System.Threading.Tasks;
using Cysharp.Threading.Tasks;

public class Game
{
    private Game()
    {

    }
    public static Game instance;

    public static async UniTask Create()
    {
        instance = new Game();
        await instance.Init();
    }

    private LoginModule m_LoginModule;

    public LoginModule Login => m_LoginModule;

    private async UniTask Init()
    {
        await this.InitResource();
        await this.InitModule();

    }
    private Config m_Config;
    private async UniTask InitResource()
    {
        m_Config = new Config();
        await m_Config.Init();
    }
    private async UniTask InitModule()
    {
        m_LoginModule = new LoginModule();
        await m_LoginModule.Init();
    }

}