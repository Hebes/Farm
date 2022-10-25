using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemToolTip : MonoBehaviour
{

    [SerializeField]
    private TextMeshProUGUI nameText;

    [SerializeField]
    private TextMeshProUGUI typeText;

    [SerializeField]
    private TextMeshProUGUI descriptionText;

    [SerializeField]
    private Text valueText;//价格

    [SerializeField]
    private GameObject bottomPart;

    /// <summary>设置提示工具信息</summary>
    public void SetupTooltip(ItemDetails itemDatails, ESlotType eSlotType)
    {
        nameText.text = itemDatails.itemName;
        typeText.text = GetItemType(itemDatails.eItemType);
        descriptionText.text = itemDatails.itemDescription;
        switch (itemDatails.eItemType)
        {
            case EItemType.Seed:
            case EItemType.Commdity:
            case EItemType.Furniture:
                bottomPart.SetActive(true);
                valueText.text = SetSellPrice(itemDatails, eSlotType).ToString();
                break;
            case EItemType.HoeTool:
            case EItemType.ChopTool:
            case EItemType.BreakTool:
            case EItemType.ReapTool:
            case EItemType.WaterTool:
            case EItemType.ClooectTool:
            case EItemType.ReapableSceney:
                bottomPart.SetActive(false);
                break;
        }

        LayoutRebuilder.ForceRebuildLayoutImmediate(transform as RectTransform);//强制刷新,防止descriptionText描述延迟
    }
    /// <summary>设置出售价格</summary>
    private int SetSellPrice(ItemDetails itemDatails, ESlotType eSlotType)
    {
        int price = itemDatails.itemPrice;

        return eSlotType switch
        {
            ESlotType.Bag => (int)(price * itemDatails.sellPercentage),
            ESlotType.Box => price,
            ESlotType.Shop => price,
            _ => price
        };
    }
    /// <summary>把物品类型转换成字符串</summary>
    private string GetItemType(EItemType EItemType)
    {
        return EItemType switch
        {
            EItemType.Seed => "种子",
            EItemType.Commdity => "商品",
            EItemType.Furniture => "家具",
            EItemType.HoeTool => "锄头",
            EItemType.ChopTool => "砍树工具",
            EItemType.BreakTool => "砸石头工具",
            EItemType.ReapTool => "割草工具",
            EItemType.WaterTool => "浇水工具",
            EItemType.ClooectTool => "收割工具",
            EItemType.ReapableSceney => "杂草",
            _ => "无"
        };
    }
}
