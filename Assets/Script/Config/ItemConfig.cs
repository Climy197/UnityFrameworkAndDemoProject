using System.Collections.Generic;
using UnityEngine;
using Unity.Plastic.Newtonsoft.Json;
using Cysharp.Threading.Tasks;
using System;

public class ItemConfig
{

    public  long ID;
    public  string Name;
    public  int Type;
    public  string Desc;
    public  string Pic;
}

public class ItemConfigCollection
{
    private Dictionary<int, ItemConfig> m_Dict ;
    private List<ItemConfig> m_List ;

    public async UniTask Init()
    {
        var json = await App.Instance.Res.LoadAssetAsync<TextAsset>("ItemConfig");
        m_List = JsonConvert.DeserializeObject<List<ItemConfig>>(json.text);
        m_Dict = new Dictionary<int, ItemConfig>();
        foreach (var item in m_List)
        {
            m_Dict.Add((int)item.ID, item);
        }
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
