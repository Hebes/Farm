public enum EItemType
{
    /// <summary>
    /// 种子
    /// </summary>
    Seed,
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

/// <summary>
/// 格子的类型
/// </summary>
public enum ESlotType
{
    Bag,
    Box,
    Shop,
}

/// <summary>
/// 物品的位置
/// </summary>
public enum EInventoryLocation
{
    Player,
    Box,
}

/// <summary>
/// 物体类型类型
/// </summary>
public enum EPartType
{
    None,
    /// <summary>
    /// 举东西的状态
    /// </summary>
    Carry,
    /// <summary>
    /// 锄头
    /// </summary>
    Hoe,
    Break,
}

/// <summary>
/// 身体部位
/// </summary>
public enum EPartName
{
    Body,
    Hair,
    Arm,
    Tool,
}

/// <summary>
/// 春夏秋冬
/// </summary>
public enum ESeason
{
    春天,
    夏天,
    秋天,
    冬天
}

/// <summary>
/// 网格的类型
/// </summary>
public enum EGridType
{
    Diggable, DropItem, PlaceFurniture, NPCObstacle,
}