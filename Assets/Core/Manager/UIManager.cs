using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : IManager, IShowPopup
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

    Dictionary<UIBase, GameObject> m_UICache = new Dictionary<UIBase, GameObject>();

    List<PopupBase> m_PopupCache = new List<PopupBase>();


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

    private void DisposePopup(PopupBase popup)
    {
        if (popup == null)
        {
            Debug.LogError("Popup is null");
            return;
        }
        m_PopupCache.Remove(popup);
        GameObject.Destroy(popup.gameObject);
    }
    private void DisposeView(ViewBase view)
    {
        if (view == null)
        {
            Debug.LogError("View is null");
            return;
        }
        GameObject.Destroy(view.gameObject);
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

    public void ShowPopup(PopupBase popup)
    {
        throw new System.NotImplementedException();
    }
}
