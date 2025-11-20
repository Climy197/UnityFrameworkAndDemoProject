using System.Collections.Generic;
using UnityEngine;
using LitJson;
using System.Threading.Tasks;
using System;

public class ItemConfig
{
    public double ID;
    public string Name;
    public double Type;
    public string Desc;
    public string Pic;
}
public class ItemConfigCollection
{
    private Dictionary<int, ItemConfig> m_Dict;
    private List<ItemConfig> m_List;
    public async Task Init()
    {
        var json = await App.Instance.Res.LoadAssetAsync<TextAsset>("ItemConfig");
        m_List = JsonMapper.ToObject<List<ItemConfig>>(json.text);
        m_Dict = new Dictionary<int, ItemConfig>();
        foreach (var item in m_List)
        {
            m_Dict.Add((int)item.ID, item);
        }
        Debug.Log($"ItemConfigCollection Init {m_List.Count}");
    }
    public ItemConfig Find(Predicate<ItemConfig> predicate)
    {
        return m_List.Find(predicate);
    }
    public ItemConfig Get(int id)
    {
        return m_Dict[id];
    }
}
