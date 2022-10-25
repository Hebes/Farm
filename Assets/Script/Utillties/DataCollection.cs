using System;
using UnityEngine;

[Serializable]//反序列化到界面显示
public class ItemDetails
{
    /// <summary>
    /// 物品id
    /// </summary>
    public int itemID;

    /// <summary>
    /// 物品名称
    /// </summary>
    public string itemName;

    /// <summary>
    /// 物品数量
    /// </summary>
    public int itemAmount;

    /// <summary>
    /// 物品类型
    /// </summary>
    public EItemType eItemType;

    /// <summary>
    /// 物品图标
    /// </summary>
    public Sprite itemIcon;

    /// <summary>
    /// 物品在世界的图标
    /// </summary>
    public Sprite itemOnWorldSprite;

    /// <summary>
    /// 物品描述
    /// </summary>
    public string itemDescription;

    /// <summary>
    /// 物品使用半径
    /// </summary>
    public int itemUseRadius;

    /// <summary>
    /// 物品能否拾起
    /// </summary>
    public bool canPickedUp;

    /// <summary>
    /// 物品能否扔掉
    /// </summary>
    public bool canDropped;

    /// <summary>
    /// 物品能否举起
    /// </summary>
    public bool canCarried;

    /// <summary>
    /// 物品价格
    /// </summary>
    public int itemPrice;

    /// <summary>
    /// 物品出售价格比例
    /// </summary>
    [Range(0, 1)]
    public float sellPercentage;
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

/// <summary>动画类型 和角色举东西有关</summary>
[Serializable]
public class AnimatorType
{
    public EPartType ePartType;
    public EPartName ePartName;
    public AnimatorOverrideController overrideController;
}

[Serializable]
/// <summary>序列化物品的坐标存储类</summary>
public class SerializableVector3
{
    public float x, y, z;

    /// <summary>Vector3 转成数值</summary><param name="pos"></param>
    public SerializableVector3(Vector3 pos)
    {
        this.x = pos.x;
        this.y = pos.y;
        this.z = pos.z;
    }
    /// <summary>转Vector3 </summary><returns></returns>
    public Vector3 ToVector3() => new Vector3(x, y, z);
    /// <summary>转Vector2 </summary><returns></returns>
    public Vector2Int ToVector2Int() => new Vector2Int((int)x, (int)y);
}

[Serializable]
public class SceneItem
{
    public int itemID;
    public SerializableVector3 position;
}

/// <summary>
/// 格子的属性
/// </summary>
[Serializable]
public class TileProperty
{
    /// <summary>
    /// 时间坐标
    /// </summary>
    public Vector2Int tileCoordinate;
    public EGridType eGridType;
    public bool boolTypeValue;
}

/// <summary>
/// 地图格子信息
/// </summary>
[System.Serializable]
public class TileDetails
{
    public int girdX, gridY;
    /// <summary>能否挖坑</summary>
    public bool canDig;
    /// <summary>能否扔东西</summary>
    public bool canDropItem;
    /// <summary>能否放置家具</summary>
    public bool canPlaceFurniture;
    /// <summary>是否是NPC的障碍</summary>
    public bool isNPCObstacle;
    /// <summary>记录这个坐标是否被挖过</summary>
    public int daysSinceDug = -1;
    /// <summary>记录这个坐标是否被浇水</summary>
    public int daysSinceWatered = -1;
    /// <summary>种子信息</summary>
    public int seedItemID = -1;
    /// <summary>成长了多少天了</summary>
    public int growthDays = -1;
    /// <summary>上一次收割过了多少天</summary>
    public int daysSinceLastHarvest = -1;
}

