using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Scripting;
using UnityEngine.UI;
using UnityEngine.UIElements;

[RequireComponent(typeof(ScrollView))]
public class UIScrollView : UIBase
{
    public UIBase OriginItems;
    private List<UIBase> m_ActiveItems = new List<UIBase>();
    private UIPool<UIBase> m_Pool;
    override public ComponentType Type => ComponentType.standard;
    ScrollRect m_scrollRect;
    protected override void OnInit()
    {
        m_scrollRect = GetComponent<ScrollRect>();
    }
    //暂不支持一个列表生成多物体
    // public Func<int> ItemSelect;
    public Action<int, UIBase> ItemRender;

    public int Count
    {
        get { return m_ActiveItems.Count; }
        set
        {
            this.Render(value);
        }
    }

    private void Render(int Count)
    {
        if (OriginItems == null)
        {
            return;
        }
        if (Count > m_ActiveItems.Count)
        {
            for (int i = m_ActiveItems.Count; i < Count; i++)
            {
                UIBase item = m_Pool.GetItem();
                item.transform.SetParent(m_scrollRect.content, false);
                m_ActiveItems.Add(item);
            }
        }
        else if (Count < m_ActiveItems.Count)
        {
            var offSet = m_ActiveItems.Count - Count;
            for (int i = m_ActiveItems.Count - 1; i >= m_ActiveItems.Count - 1 - offSet; i--)
            {
                m_Pool.Recycle(m_ActiveItems[i]);
            }
            m_ActiveItems.RemoveRange(m_ActiveItems.Count - 1 - offSet, offSet);
        }
    }
    public void Locate(int index)
    {
        if (m_scrollRect.vertical)
        {
            if (index < 0)
            {
                this.m_scrollRect.verticalNormalizedPosition = 0;
            }
            else if (index > this.Count)
            {
                this.m_scrollRect.verticalNormalizedPosition = 1;
            }
        }
        else if (m_scrollRect.horizontal)
        {
            if (index < 0)
            {
                this.m_scrollRect.horizontalNormalizedPosition = 0;
            }
            else if (index > this.Count)
            {
                this.m_scrollRect.horizontalNormalizedPosition = 1;
            }
        }
    }
}