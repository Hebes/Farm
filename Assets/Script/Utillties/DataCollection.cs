using System;
using UnityEngine;

[Serializable]//反序列化到界面显示
public class ItemDatails
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

    public static implicit operator ItemDatails(int v)
    {
        throw new NotImplementedException();
    }
}
