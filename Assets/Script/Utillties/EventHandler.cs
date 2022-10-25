using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 事件中心模块
/// </summary>
public static class EventHandler
{
    public static event Action<EInventoryLocation, List<InventoryItem>> UpdateInvenoryUI;
    /// <summary>呼叫事件中心，执行委托刷新UI的代码</summary>
    public static void CallUpdateInventoryUI(EInventoryLocation location, List<InventoryItem> list) => UpdateInvenoryUI?.Invoke(location, list);

    /// <summary>在世界地图上生成物品</summary>
    public static event Action<int, Vector3> InstantiateItemScen;
    public static void CallInstantiateItemScen(int ID, Vector3 pos) => InstantiateItemScen?.Invoke(ID, pos);

    /// <summary>物品选中状态，触发对应的动画播放</summary>
    public static event Action<ItemDetails, bool> ItemSelectEvent;
    public static void CallItemSelectEvent(ItemDetails itemDatails, bool idSelect) => ItemSelectEvent?.Invoke(itemDatails, idSelect);

    /// <summary>扔东西</summary>
    public static event Action<int, Vector3> DropItemEvent;
    /// <summary>扔东西</summary>
    public static void CallDropItemEvent(int ID, Vector3 pos) => DropItemEvent?.Invoke(ID, pos);


    /// <summary>和日期有关 分</summary>
    public static event Action<int, int> GameMinuteEvent;
    public static void CallGameMinuteEvent(int minute, int hour) => GameMinuteEvent?.Invoke(minute, hour);
    /// <summary>和日期有关 小时</summary>
    public static event Action<int, int, int, int, ESeason> GameDateEvent;
    public static void CallGameDateEvent(int hour, int day, int month, int year, ESeason season) => GameDateEvent?.Invoke(hour, day, month, year, season);

    /// <summary>场景的切换</summary>
    public static event Action<string, Vector3> TransitionEvent;
    public static void CallTransitionEvent(string sceneName, Vector3 pos) => TransitionEvent?.Invoke(sceneName, pos);

    /// <summary>卸载场景之后需要做的事件</summary>
    public static event Action BeforeSceneUnloadEvent;
    /// <summary>卸载场景之后需要做的事件</summary>
    public static void CallBeforeSceneUnLoadEvent() => BeforeSceneUnloadEvent?.Invoke();

    /// <summary>加载场景之后需要做的事件</summary>
    public static event Action AfterSceneLoadedEvent;
    /// <summary>加载场景之后需要做的事件</summary>
    public static void CallAfterSceneLoadedEvent() => AfterSceneLoadedEvent?.Invoke();

    /// <summary>人物加载场景时候的坐标</summary>
    public static event Action<Vector3> MoveToPosition;
    /// <summary>人物加载场景时候的坐标</summary><param name="targetPosition"></param>
    public static void CallMoveToPosition(Vector3 targetPosition) => MoveToPosition?.Invoke(targetPosition);

    /// <summary>鼠标点击事件</summary>
    public static event Action<Vector3, ItemDetails> MouseClickedEvent;
    /// <summary>鼠标点击事件</summary>
    public static void CallMouseClickedEvent(Vector3 pos, ItemDetails itemDetails) => MouseClickedEvent?.Invoke(pos, itemDetails);

    public static event Action<Vector3, ItemDetails> ExecuteActionAfterAnimation;
    /// <summary>执行操作后的动画播放</summary>
    public static void CallExecuteActionAfterAnimation(Vector3 pos, ItemDetails itemDetails) => ExecuteActionAfterAnimation?.Invoke(pos, itemDetails);
}
