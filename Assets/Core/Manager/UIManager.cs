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

        var popup = await view.Init(this.m_BottomLayer);
        popup.name = typeof(T).Name;
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
        var popup = await view.Init(this.m_MiddleLayer);
        popup.name = typeof(T).Name;
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
        var Component = bottom.AddComponent<RectTransform>();
        bottom.name = "BottomLayer";
        bottom.transform.SetParent(m_UIRoot);
        Component.localPosition = Vector3.zero;
        m_BottomLayer = bottom.transform;
        Component.anchorMin = Vector2.zero;
        Component.anchorMax = Vector2.one;
        Component.offsetMin = Vector2.zero;
        Component.offsetMax = Vector2.zero;

        var middle = new GameObject();
        Component = middle.AddComponent<RectTransform>();
        middle.name = "MiddleLayer";
        middle.transform.SetParent(m_UIRoot);
        Component.localPosition = Vector3.zero;
        m_MiddleLayer = middle.transform;
        Component.anchorMin = Vector2.zero;
        Component.anchorMax = Vector2.one;
        Component.offsetMin = Vector2.zero;
        Component.offsetMax = Vector2.zero;

        var top = new GameObject();
        Component = top.AddComponent<RectTransform>();
        top.name = "TopLayer";
        top.transform.SetParent(m_UIRoot);
        Component.localPosition = Vector3.zero;
        m_TopLayer = top.transform;
        Component.anchorMin = Vector2.zero;
        Component.anchorMax = Vector2.one;
        Component.offsetMin = Vector2.zero;
        Component.offsetMax = Vector2.zero;
    }
}
