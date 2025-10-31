using System;
using System.Collections.Generic;
using UnityEngine;

public class UIPool<T> where T : UIBase
{
    private UIPool(UIBase originItem)
    {
        m_OriginItem = originItem;
    }
    private UIBase m_OriginItem;
    private Transform m_StorageRoot;
    private Stack<T> StorageItems = new Stack<T>();
    private static Dictionary<Type, UIPool<T>> m_Items = new Dictionary<Type, UIPool<T>>();
    public static UIPool<T> Create(T originItem)
    {
        var type = originItem.GetType();
        if (m_Items.TryGetValue(type, out var pool))
        {
            return pool;
        }
        pool = new UIPool<T>(originItem);
        m_Items.Add(type, pool);
        pool.m_StorageRoot = App.Instance.UI.UIPoolRoot;
        return pool;
    }

    public T GetItem()
    {
        if (StorageItems.Count > 0)
        {
            return StorageItems.Pop();
        }
        else
        {
            return GameObject.Instantiate(m_OriginItem).GetComponent<T>();
        }
    }
    public void Recycle(T item)
    {
        item.transform.SetParent(m_StorageRoot);
        this.StorageItems.Push(item);
    }


}
