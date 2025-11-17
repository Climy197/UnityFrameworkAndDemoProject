using System;
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
    private Transform m_UIPoolRoot;

    public Transform wl => m_BottomLayer;

    public Transform pl => m_MiddleLayer;

    public Transform gl => m_TopLayer;
    internal Transform UIPoolRoot => this.m_UIPoolRoot;



    Stack<ViewBase> m_UIViewStack = new Stack<ViewBase>();
    Dictionary<Type, ViewBase> m_UICache = new Dictionary<Type, ViewBase>();
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
    internal void DisposeAllPopup()
    {
        for (int i = m_PopupCache.Count - 1; i >= 0; i--)
        {
            DisposePopup(m_PopupCache[i]);
        }
        this.m_PopupCache.Clear();
    }
    internal void DisposeAllView()
    {
        foreach (var view in this.m_UIViewStack)
        {
            DisposeView(view);
        }
        this.m_UIViewStack.Clear();
    }

    public async UniTask<T> ShowPopup<T>() where T : ViewBase, new()
    {
        var type = typeof(T);
        ViewBase view = null;
        if (this.m_UICache.TryGetValue(type, out view))
        {
            view.Show();
            return view as T;
        }
        else
        {
            view = new T();
            var popup = await view.Init(this.m_MiddleLayer);
            popup.name = typeof(T).Name;
            view.Show();
            m_PopupCache.Add(view);
            m_UICache.Add(type, view);
            return view as T;
        }
    }
    public void HidePopup<T>(T popup) where T : ViewBase
    {
        if (!this.m_PopupCache.Contains(popup))
        {
            Debug.LogError("试图关闭的窗口不存在");
            return;
        }
        popup.Hide();
        this.m_PopupCache.Remove(popup);
    }
    public async UniTask HomeView<T>() where T : ViewBase, new()
    {
        foreach (var item in m_UIViewStack)
        {
            item.Hide();
        }
        m_UIViewStack.Clear();
        await this.PushView<T>();
    }
    public async UniTask PushView<T>() where T : ViewBase, new()
    {
        var type = typeof(T);
        ViewBase view = null;
        if (this.m_UICache.TryGetValue(type, out view))
        {
        }
        else
        {
            view = new T();
            var popup = await view.Init(this.m_BottomLayer);
            popup.name = typeof(T).Name;
            m_UICache.Add(type, view);
        }
        view.Show();
        if (m_UIViewStack.Count > 0)
        {
            m_UIViewStack.Peek().Hide();
        }
        m_UIViewStack.Push(view);
    }
    public void BackView()
    {
        if (m_UIViewStack.Count <= 1)
        {
            return;
        }
        var view = m_UIViewStack.Pop();
        m_UIViewStack.Peek().Show();
        view.Hide();
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
        m_BottomLayer.localScale = Vector3.one;
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
        m_MiddleLayer.localScale = Vector3.one;
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
        m_TopLayer.localScale = Vector3.one;
        Component.anchorMin = Vector2.zero;
        Component.anchorMax = Vector2.one;
        Component.offsetMin = Vector2.zero;
        Component.offsetMax = Vector2.zero;
    }
}
