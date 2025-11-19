using SQLite4Unity3d;
public class Player : IDBData
{
    [PrimaryKey]
    public int uid { get; set; }
    public string name { get; set; }
    public int level { get; set; }
    public int exp { get; set; }
}