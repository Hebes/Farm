using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

public class ItemWindow : EditorWindow
{
    private ItemDataList_SO dataBase;
    private List<ItemDetails> itemList = new List<ItemDetails>();
    private VisualTreeAsset ItemRowTempLate;

    // 获得VisualElement
    private ListView itemListView;
    //编辑器的右边部分
    private ScrollView itemDetailsSection;
    //被激活的物体
    private ItemDetails activeItem;
    //默认图片
    private Sprite defaultIcon;
    private VisualElement iconPreview;


    [MenuItem("M STUDIO/ItemEditor")]
    public static void ShowExample()
    {
        ItemWindow wnd = GetWindow<ItemWindow>();
        wnd.titleContent = new GUIContent("ItemWindow");
    }

    public void CreateGUI()
    {
        // Each editor window contains a root VisualElement object
        VisualElement root = rootVisualElement;

        // VisualElements objects can contain other VisualElement following a tree hierarchy.
        //VisualElement label = new Label("Hello World! From C#");
        //root.Add(label);

        // Import UXML
        var visualTree = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Assets/Editor/ItemWindow.uxml");
        VisualElement labelFromUXML = visualTree.Instantiate();
        root.Add(labelFromUXML);

        //加载列表按钮的模板
        ItemRowTempLate = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Assets/Editor/ItemRowTemplate.uxml");//列表按钮的模板

        //获取默认图片
        defaultIcon = AssetDatabase.LoadAssetAtPath<Sprite>("Assets/M Studio/Art/Items/Icons/icon_M.png");

        //获取变量
        itemListView = root.Q<VisualElement>("ItemList").Q<ListView>("ListView");
        itemDetailsSection = root.Q<ScrollView>("ItemDetails");
        iconPreview = itemDetailsSection.Q<VisualElement>("Icon");

        //获取按键
        root.Q<Button>("AddButton").clicked += OnAddItemClicked;
        root.Q<Button>("DeleteButton").clicked += OnDeleteItemClicked;

        //加载数据
        LoadDataBase();
        //生成列表
        GenerateListView();
    }

    #region 按键事件
    /// <summary>删除按钮</summary>
    private void OnDeleteItemClicked()
    {
        itemList.Remove(activeItem);
        itemListView.Rebuild();//刷新左侧列表
        itemDetailsSection.visible = false;//隐藏右侧面板
    }
    /// <summary>生成按钮</summary>
    private void OnAddItemClicked()
    {
        ItemDetails newItem = new ItemDetails();
        newItem.itemName = "NEW ITEM";
        newItem.itemID = 1000 + itemList.Count;
        newItem.itemIcon = defaultIcon;
        itemList.Add(newItem);
        itemListView.Rebuild();//刷新左侧列表
    }
    #endregion


    private void LoadDataBase()
    {
        string[] dataArray = AssetDatabase.FindAssets("ItemDataList_SO");
        if (dataArray.Length > 0)
        {
            string path = AssetDatabase.GUIDToAssetPath(dataArray[0]);
            dataBase = AssetDatabase.LoadAssetAtPath(path, typeof(ItemDataList_SO)) as ItemDataList_SO;
        }
        itemList = dataBase.ItemDatailsList;
        //如果不标记则无法保存数据
        EditorUtility.SetDirty(dataBase);
        Debug.Log("获取到第一个数据：" + itemList[0].itemID);
    }
    /// <summary>克隆按钮模板列表</summary>
    private void GenerateListView()
    {
        Func<VisualElement> makeItem = () => ItemRowTempLate.CloneTree();
        //设置按钮信息
        Action<VisualElement, int> bindItem = (e, i) =>
        {
            if (itemList.Count > i)
            {
                if (itemList[i].itemIcon != null)
                    e.Q<VisualElement>("Icon").style.backgroundImage = itemList[i].itemIcon.texture;//物品图片
                e.Q<Label>("Name").text = itemList[i].itemName == null ? "No Item" : itemList[i].itemName;//物品名称
            }
        };
        itemListView.fixedItemHeight = 50;
        //生成list View
        itemListView.itemsSource = itemList;
        itemListView.makeItem = makeItem;
        itemListView.bindItem = bindItem;

        itemListView.onSelectionChange += OnListSeLectionChange;
        // 右侧信息面板不可见
        itemDetailsSection.visible = false;

    }
    private void OnListSeLectionChange(IEnumerable<object> sleectItem)
    {
        activeItem = (ItemDetails)sleectItem.First();
        GetItemDetails();
        // 右侧信息面板不可见
        itemDetailsSection.visible = true;
    }
    /// <summary>生成右侧的详细数据</summary>
    private void GetItemDetails()
    {
        itemDetailsSection.MarkDirtyRepaint();//刷新
        //物品编号
        itemDetailsSection.Q<IntegerField>("ItemID").value = activeItem.itemID;
        itemDetailsSection.Q<IntegerField>("ItemID").RegisterValueChangedCallback((evt) =>
        {
            activeItem.itemID = evt.newValue;//更新的话会重新给activeItem.itemID赋值
        });
        //名称
        itemDetailsSection.Q<TextField>("ItemName").value = activeItem.itemName;
        itemDetailsSection.Q<TextField>("ItemName").RegisterValueChangedCallback(evt =>
        {
            activeItem.itemName = evt.newValue;
            itemListView.Rebuild();
        });
        //类型
        itemDetailsSection.Q<EnumField>("ItemType").Init(activeItem.eItemType);
        itemDetailsSection.Q<EnumField>("ItemType").value = activeItem.eItemType;
        itemDetailsSection.Q<EnumField>("ItemType").RegisterValueChangedCallback(evt =>
        {
            activeItem.eItemType = (EItemType)evt.newValue;
        });
        //设置图片
        iconPreview.style.backgroundImage = activeItem.itemIcon == null ? defaultIcon.texture : activeItem.itemIcon.texture;
        itemDetailsSection.Q<ObjectField>("ItemIcon").value = activeItem.itemIcon;
        itemDetailsSection.Q<ObjectField>("ItemIcon").RegisterValueChangedCallback(evt =>
        {
            Sprite newIcon = evt.newValue as Sprite;
            activeItem.itemIcon = newIcon;
            iconPreview.style.backgroundImage = newIcon == null ? defaultIcon.texture : newIcon.texture;
            itemListView.Rebuild();
        });
        //世界图片
        itemDetailsSection.Q<ObjectField>("ItemSprite").value = activeItem.itemOnWorldSprite;
        itemDetailsSection.Q<ObjectField>("ItemSprite").RegisterValueChangedCallback(evt =>
        {
            activeItem.itemOnWorldSprite = (Sprite)evt.newValue;
        });
        //描述
        itemDetailsSection.Q<TextField>("Description").value = activeItem.itemDescription;
        itemDetailsSection.Q<TextField>("Description").RegisterValueChangedCallback(evt =>
        {
            activeItem.itemDescription = evt.newValue;
        });
        //使用半径
        itemDetailsSection.Q<IntegerField>("ItemUseRadius").value = activeItem.itemUseRadius;
        itemDetailsSection.Q<IntegerField>("ItemUseRadius").RegisterValueChangedCallback(evt =>
        {
            activeItem.itemUseRadius = evt.newValue;
        });
        //能否拾起
        itemDetailsSection.Q<Toggle>("CanPickedUp").value = activeItem.canPickedUp;
        itemDetailsSection.Q<Toggle>("CanPickedUp").RegisterValueChangedCallback(evt =>
        {
            activeItem.canPickedUp = evt.newValue;
        });
        //能否扔掉
        itemDetailsSection.Q<Toggle>("CanDropped").value = activeItem.canDropped;
        itemDetailsSection.Q<Toggle>("CanDropped").RegisterValueChangedCallback(evt =>
        {
            activeItem.canDropped = evt.newValue;
        });
        //能否举起
        itemDetailsSection.Q<Toggle>("CanCarried").value = activeItem.canCarried;
        itemDetailsSection.Q<Toggle>("CanCarried").RegisterValueChangedCallback(evt =>
        {
            activeItem.canCarried = evt.newValue;
        });
        //物品价格
        itemDetailsSection.Q<IntegerField>("Price").value = activeItem.itemPrice;
        itemDetailsSection.Q<IntegerField>("Price").RegisterValueChangedCallback(evt =>
        {
            activeItem.itemPrice = evt.newValue;
        });
        //物品出售价格比例
        itemDetailsSection.Q<Slider>("SellPercentage").value = activeItem.sellPercentage;
        itemDetailsSection.Q<Slider>("SellPercentage").RegisterValueChangedCallback(evt =>
        {
            activeItem.sellPercentage = evt.newValue;
        });
    }

}