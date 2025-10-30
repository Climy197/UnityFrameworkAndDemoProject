using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class UIManager : IManager
{
    private Transform m_UIRoot;
    internal Transform UI
    {
        set { m_UIRoot = value; }
    }
    private Transform m_BottomLayer;
    private Transform m_MiddleLayer;
    private Transform m_TopLayer;

    public Transform wl => m_BottomLayer;

    public Transform pl => m_MiddleLayer;

    public Transform gl => m_TopLayer;


    Stack<ViewBase> m_UIViewStack = new Stack<ViewBase>();
    List<UIBase> m_UICache = new List<UIBase>();
    List<ViewBase> m_PopupCache = new List<ViewBase>();



    void IManager.Dispose()
    {
        DisposeAllPopup();
        DisposeAllView();
    }

    private void DisposePopup(ViewBase popup)
    {
        if (popup == null)
        {
            Debug.LogError("Popup is null");
            return;
        }
        m_PopupCache.Remove(popup);

    }
    private void DisposeView(ViewBase view)
    {
        if (view == null)
        {
            Debug.LogError("View is null");
            return;
        }
        view.Dispose();
    }
    private void DisposeAllPopup()
    {
        for (int i = m_PopupCache.Count - 1; i >= 0; i--)
        {
            DisposePopup(m_PopupCache[i]);
        }
    }
    private void DisposeAllView()
    {
        var view = m_UIViewStack.Pop();
        DisposeView(view);
    }

    public async UniTask<T> ShowPopup<T>() where T : ViewBase, new()
    {
        var view = new T();
        var root = new GameObject();
        root.name = typeof(T).Name;
        view.BindRoot(root);
        root.transform.SetParent(m_UIRoot);
        await view.Init();
        view.Show();
        m_PopupCache.Add(view);
        return view;
    }
    public void HidePopup<T>(T popup) where T : ViewBase
    {

    }
    public async UniTask HomeView<T>() where T : ViewBase, new()
    {
        foreach (var item in m_UIViewStack)
        {
            DisposeView(item);
        }
        m_UIViewStack.Clear();
        await this.PushView<T>();
    }
    public async UniTask PushView<T>() where T : ViewBase, new()
    {
        var view = new T();
        var root = new GameObject();
        root.name = typeof(T).Name;
        view.BindRoot(root);
        root.transform.SetParent(m_UIRoot);
        await view.Init();
        m_UIViewStack.Push(view);
        view.Show();
    }
    public void BackView()
    {
        if (m_UIViewStack.Count < 1)
        {
            return;
        }
        var view = m_UIViewStack.Pop();
        DisposeView(view);
    }
    public ViewBase GetCurrentView()
    {
        if (m_UIViewStack.Count == 0)
        {
            return null;
        }
        return m_UIViewStack.Peek();
    }

    async UniTask IManager.Init()
    {
        if (m_UIRoot == null)
        {
            Debug.LogError("UIRoot is null");
        }
        var bottom = new GameObject();
        bottom.name = "BottomLayer";
        bottom.transform.SetParent(m_UIRoot);
        m_BottomLayer = bottom.transform;
    }
}
