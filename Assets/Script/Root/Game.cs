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
            }
            return m_instance;
        }
    }
    private LoginModule m_LoginModule;

    public LoginModule Login => m_LoginModule;



    public void Init()
    {

    }
}