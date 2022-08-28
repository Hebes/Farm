public enum EItemType
{
    /// <summary>
    /// 种子
    /// </summary>
    Seed ,
    /// <summary>
    /// 商品
    /// </summary>
    Commdity,
    /// <summary>
    /// 家具
    /// </summary>
    Furniture,


    /// <summary>
    /// 锄头
    /// </summary>
    HoeTool,
    /// <summary>
    /// 砍树工具
    /// </summary>
    ChopTool,
    /// <summary>
    /// 砸石头工具
    /// </summary>
    BreakTool,
    /// <summary>
    /// 割草工具
    /// </summary>
    ReapTool,
    /// <summary>
    /// 浇水工具
    /// </summary>
    WaterTool,
    /// <summary>
    /// 菜篮子收割工具
    /// </summary>
    ClooectTool,

    /// <summary>
    /// 可以被割的杂草
    /// </summary>
    ReapableSceney,
}


[System.Serializable]
/// <summary>这个是为Inventory使用的   struct 可以初始化默认值</summary>
public struct InventoryItem
{
    public int itemID;

    /// <summary>
    /// 物品数量
    /// </summary>
    public int itemAmount;
}