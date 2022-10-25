using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using MFarm.Map;

public class CursorManager : MonoBehaviour
{
    public Sprite normal, tool, seed, item;
    private ItemDetails currentItem;
    private Sprite currentSprite; // 存储当前鼠标图片
    private Image cursorImage;
    private RectTransform cursorCanvas;

    //网格鼠标检测
    private Camera mainCamera => Camera.main;
    private Grid currentGrid;

    private Vector3 mouseWorldPos;
    private Vector3Int mouseGridPos;

    private bool cursorEnable;//场景加载之前鼠标不可用
    private bool cursorPositionValid;//鼠标是否可点按

    private Transform playerTransform => FindObjectOfType<Player>().transform;

    private void OnEnable()
    {
        EventHandler.ItemSelectEvent += OnItemSelectEvent;
        EventHandler.BeforeSceneUnloadEvent += OnBeforeSceneUnloadEvent;
        EventHandler.AfterSceneLoadedEvent += OnAfterSceneLoadedEvent;
    }
    private void OnDisable()
    {
        EventHandler.ItemSelectEvent -= OnItemSelectEvent;
        EventHandler.BeforeSceneUnloadEvent -= OnBeforeSceneUnloadEvent;
        EventHandler.AfterSceneLoadedEvent -= OnAfterSceneLoadedEvent;
    }
    private void Start()
    {
        cursorCanvas = GameObject.FindGameObjectWithTag("CursorCanvas").GetComponent<RectTransform>();
        cursorImage = cursorCanvas.GetChild(0).GetComponent<Image>();
        currentSprite = normal;
        SetCursorImage(normal);
    }
    private void Update()
    {
        if (cursorCanvas == null) return;
        cursorImage.transform.position = Input.mousePosition;

        if (!InteractwithUI() && cursorEnable)
        {
            SetCursorImage(currentSprite);
            CheckCursorValid();
            CheckPlayerInput();
        }
        else
            SetCursorImage(normal);
    }


    private void CheckPlayerInput()
    {
        //执行方法
        if (Input.GetMouseButtonDown(0) && cursorPositionValid)
            EventHandler.CallMouseClickedEvent(mouseWorldPos, currentItem);
    }

    #region 设置鼠标样式
    /// <summary>
    /// 设置鼠标图片
    /// </summary>
    /// <param name="sprite"></param>
    private void SetCursorImage(Sprite sprite)
    {
        cursorImage.sprite = sprite;
        cursorImage.color = new Color(1, 1, 1, 1);
    }
    /// <summary>
    /// 设置鼠标可用
    /// </summary>
    private void SetCursorValid()
    {
        cursorPositionValid = true;
        cursorImage.color = new Color(1, 1, 1, 1);
    }
    /// <summary>
    /// 设置鼠标不可用
    /// </summary>
    private void SetCursorInValid()
    {
        cursorPositionValid = false;
        cursorImage.color = new Color(1, 0, 0, 0.4f);
    }
    #endregion



    /// <summary>
    /// 设置鼠标对应的图片
    /// </summary>
    /// <param name="itemDatails"></param>
    /// <param name="isSelected"></param>
    private void OnItemSelectEvent(ItemDetails itemDatails, bool isSelected)
    {


        currentSprite = !isSelected ? normal : itemDatails.eItemType switch
        {
            EItemType.Seed => seed,
            EItemType.Commdity => item,
            EItemType.ChopTool => tool,
            EItemType.HoeTool => tool,
            EItemType.WaterTool => tool,
            EItemType.BreakTool => tool,
            EItemType.ReapTool => tool,
            EItemType.Furniture => tool,
            _ => normal,
        };

        if (!isSelected)
        {
            currentItem = null;
            cursorEnable = false;
            currentSprite = normal;
        }
        else
        {
            cursorEnable = true;
            currentItem = itemDatails;
        }

        //原版老师的代码
        //if (!isSelected)
        //{
        //    currentSprite = normal;
        //}
        //else
        //{
        //    //workflow 添加所有类型对应的图片
        //    currentSprite = itemDatails.eItemType switch
        //    {
        //        EItemType.Seed => seed,
        //        EItemType.Commdity => item,
        //        EItemType.ChopTool => tool,
        //        _ => normal,
        //    };
        //}
    }
    private void OnAfterSceneLoadedEvent()
    {
        currentGrid = FindObjectOfType<Grid>();
    }
    private void OnBeforeSceneUnloadEvent() => cursorEnable = false;

    /// <summary>
    /// 是否与UI互动
    /// </summary>
    /// <returns></returns>
    private bool InteractwithUI() => EventSystem.current != null && EventSystem.current.IsPointerOverGameObject();

    /// <summary>
    /// 检查鼠标是否有效
    /// </summary>
    private void CheckCursorValid()
    {
        mouseWorldPos = mainCamera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, -mainCamera.transform.position.z));//屏幕转世界坐标
        mouseGridPos = currentGrid.WorldToCell(mouseWorldPos);//WorldToCell 将世界位置转换为单元格位置。
        Debug.Log("WorldPos:" + mouseWorldPos + "GridPos:" + mouseGridPos);

        //判断在使用范围内
        var playerGridPos = currentGrid.WorldToCell(playerTransform.position);
        if (Mathf.Abs(mouseGridPos.x - playerGridPos.x) > currentItem.itemUseRadius
            || Mathf.Abs(mouseGridPos.y - playerGridPos.y) > currentItem.itemUseRadius)
        {
            SetCursorInValid();
            return;
        }

        //能否扔东西
        TileDetails currentTile = GridMapManager.Instance.GetTileDetailsOnMousePosition(mouseGridPos);
        if (currentTile != null)
        {
            switch (currentItem.eItemType)
            {
                case EItemType.Commdity://商品的话
                    if (currentTile.canDropItem && currentItem.canDropped) SetCursorValid(); else SetCursorInValid();
                    break;
                case EItemType.HoeTool:
                    if (currentTile.canDig) SetCursorValid(); else SetCursorInValid();
                    break;
                case EItemType.WaterTool:
                    if (currentTile.daysSinceDug > -1 && currentTile.daysSinceWatered == -1) SetCursorValid(); else SetCursorInValid();
                    break;
            }
        }
        else
        {
            SetCursorInValid();
        }
    }


}
