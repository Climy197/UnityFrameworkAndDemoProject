public class Game
{
    private Game()
    {

    }
    private static Game m_instance;
    public static Game instance
    {
        get
        {
            if (m_instance == null)
            {
                m_instance = new Game();
                m_instance.Init();
            }
            return m_instance;
        }
    }
    private LoginModule m_LoginModule;

    public LoginModule Login => m_LoginModule;



    private void Init()
    {
        m_LoginModule = new LoginModule();
        m_LoginModule.Init();
    }
}