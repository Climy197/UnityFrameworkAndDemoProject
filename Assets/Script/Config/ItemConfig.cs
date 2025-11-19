using System.Collections.Generic;
using UnityEngine;

public class ItemConfig
{
    public static Dictionary<int, ItemConfig> Dict = new Dictionary<int, ItemConfig>();

    public  long ID;
    public  string Name;
    public  int Type;
    public  string Desc;
    public  string Pic;
}
