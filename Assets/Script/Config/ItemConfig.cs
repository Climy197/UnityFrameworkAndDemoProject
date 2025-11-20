using System.Collections.Generic;
using UnityEngine;
using LitJson;
using Cysharp.Threading.Tasks;

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
        m_List = JsonMapper.ToObject<List<ItemConfig>>(json.text);
        m_Dict = new Dictionary<int, ItemConfig>();
        foreach (var item in m_List)
        {
            m_Dict.Add((int)item.ID, item);
        }
    }
}
