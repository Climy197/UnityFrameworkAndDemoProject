using SQLite4Unity3d; // 确保您导入了 SQLite 命名空间

// 1. 定义数据模型类 (这就是您的“表结构”)
public class Account:IDBData
{
    // [PrimaryKey]: 定义为主键 (唯一且不可重复)
    // [AutoIncrement]: 定义为自增，每次插入新行时，ID会自动+1
    // 账号ID
    [PrimaryKey]
    public int AccountID { get; set; }
    // 密码
    public string Password { get; set; }
}