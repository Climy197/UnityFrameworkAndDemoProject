using System.Collections;
using System.Collections.Generic;
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

    public Transform BottomLayer
    {
        get { return m_BottomLayer; }
    }
    public Transform MiddleLayer
    {
        get { return m_MiddleLayer; }
    }
    public Transform TopLayer
    {
        get { return m_TopLayer; }
    }

    Stack<ViewBase> m_UIViewStack = new Stack<ViewBase>();
    List<UIBase> m_UICache = new List<UIBase>();
    List<ViewBase> m_PopupCache = new List<ViewBase>();


    public void Init()
    {
        if (m_UIRoot == null)
        {
            Debug.LogError("UIRoot is null");
            return;
        }

    }
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

    public void ShowPopup<T>(T popup) where T : ViewBase
    {

    }
    public void HidePopup<T>(T popup) where T : ViewBase
    {
        
    }
    public void HomeView<T>(T view) where T : ViewBase
    {

    }
    public void PushView<T>(T view) where T : ViewBase
    {

    }
    public void BackView()
    {

    }

}
