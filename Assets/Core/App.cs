using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class App : MonoBehaviour
{
    public static App Instance { get; private set; }

    private List<IManager> _managers = new List<IManager>();
    private DBManager m_DBManager;
    private AudioManager m_AudioManager;
    private EventManager m_EventManager;
    private NetManager m_NetManager;
    private ResourceManager m_ResourceManager;
    private UIManager m_UIManager;
    private UpdateManager m_UpdateManager;

    private void CreateManager()
    {
        m_DBManager = new DBManager();
        m_AudioManager = new AudioManager();
        m_EventManager = new EventManager();
        m_NetManager = new NetManager();
        m_ResourceManager = new ResourceManager();
        m_UIManager = new UIManager();
        m_UpdateManager = new UpdateManager();
        _managers.Add(m_DBManager);
        _managers.Add(m_AudioManager);
        _managers.Add(m_EventManager);
        _managers.Add(m_NetManager);
        _managers.Add(m_ResourceManager);
        _managers.Add(m_UIManager);
        _managers.Add(m_UpdateManager);
    }
    private void DisposeManager()
    {
        foreach (var manager in _managers)
        {
            manager.Dispose();
        }
        _managers.Clear();
    }



    void Start()
    {

    }

    private void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
    void Update()
    {
        m_UpdateManager.Execute(UpdateType.Update);
    }
    void FixedUpdate()
    {
        m_UpdateManager.Execute(UpdateType.FixedUpdate);
    }
    void Dispose()
    {

    }


    public void ExitApp()
    {
        Debug.Log("Quit requested.");

        // 使用预处理器指令来处理平台差异
#if UNITY_EDITOR
        // 如果在 Unity 编辑器中运行，则停止 Play 模式
        // 需要在脚本顶部或这个 #if 块内引用 UnityEditor
        UnityEditor.EditorApplication.isPlaying = false;
#else
            // 在实际构建的应用程序中，调用退出
            Application.Quit();
#endif
    }
}
