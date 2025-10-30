using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class App : MonoBehaviour
{
    public static App Instance { get; private set; }
    public Canvas UICanvas;

    private List<IManager> _managers = new List<IManager>();
    private DBManager m_DBManager;
    private AudioManager m_AudioManager;
    private EventManager m_EventManager;
    private NetManager m_NetManager;
    private ResourceManager m_ResourceManager;
    private UIManager m_UIManager;
    private UpdateManager m_UpdateManager;

    public ResourceManager Res => m_ResourceManager;
    public UIManager UI => m_UIManager;
    public EventManager Evt => m_EventManager;
    public NetManager Net => m_NetManager;
    public UpdateManager UpdateM => m_UpdateManager;
    public DBManager DB => m_DBManager;
    public AudioManager Audio => m_AudioManager;

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



    async void Start()
    {
        //启动游戏
        //查找所有标记为GameEntry的类
        var entryType = typeof(IEntry);
        var entries = new List<IEntry>();

        // 扫描所有已加载的程序集（包括 Business.asmdef）
        foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
        {
            // 跳过系统程序集
            if (assembly.FullName.StartsWith("Unity") ||
                assembly.FullName.StartsWith("System") ||
                assembly.FullName.StartsWith("mscorlib") ||
                assembly.FullName.Contains("Editor"))
                continue;

            try
            {
                foreach (var type in assembly.GetTypes())
                {
                    if (!type.IsAbstract && !type.IsInterface && entryType.IsAssignableFrom(type))
                    {
                        var instance = (IEntry)Activator.CreateInstance(type);
                        entries.Add(instance);
                    }
                }
            }
            catch (System.Exception ex)
            {
                Debug.LogWarning($"[App] 扫描程序集失败: {assembly.FullName} | {ex.Message}");
            }
        }
        foreach (var entry in entries)
        {
            await entry.Init();
        }
    }

    private void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(this.UICanvas);
        CreateManager();
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
        this.DisposeManager();
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
