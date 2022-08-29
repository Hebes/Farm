using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 事件中心模块
/// </summary>
public static class EventHandler 
{
    public static event Action<EInventoryLocation,List<InventoryItem>> UpdateInvenoryUI;

    /// <summary>呼叫事件中心，执行委托的代码</summary>
    public static void CallUpdateInventoryUI(EInventoryLocation location, List<InventoryItem> list)
    {
        UpdateInvenoryUI?.Invoke(location, list);
    }
}
