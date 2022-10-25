using UnityEngine;

namespace MFarm.Inventory
{
    public class InventoryManager : Singleton<InventoryManager>
    {
        [Header("物品数据")]
        public ItemDataList_SO ItemDataList_SO;
        [Header("背包数据")]
        public InventoryBag_SO PlayerBag;

        private void OnEnable()
        {
            EventHandler.DropItemEvent += OnDropItemEvent;
        }
        private void OnDisable()
        {
            EventHandler.DropItemEvent -= OnDropItemEvent;
        }
        private void Start()
        {
            //更新物品UI 呼叫事件中心,执行委托的代码  这个用于游戏一开始 背包已经有物品的情况  记录读取
            EventHandler.CallUpdateInventoryUI(EInventoryLocation.Player, PlayerBag.itemList);
        }
        private void OnDropItemEvent(int ID, Vector3 pos) => RemoveItem(ID, 1); 

        /// <summary>获取一个物品信息</summary>
        public ItemDetails GetItemDatails(int ID) => ItemDataList_SO.ItemDatailsList.Find(i => i.itemID == ID);
        /// <summary>添加物品到背包</summary>
        public void AddItem(Item item, bool toDestory)
        {
            //背包是否有该物品
            int index = GetItemIndexBag(item.itemID);
            AddItemAtItem(item.itemID, index, 1);
            Debug.Log(item.itemID + "  " + item.itemDatails.itemName);
            if (toDestory)
                Destroy(item.gameObject);

            //更新物品UI 呼叫事件中心,执行委托的代码
            EventHandler.CallUpdateInventoryUI(EInventoryLocation.Player, PlayerBag.itemList);
        }
        /// <summary>通过物品ID找到背包已有物品位置</summary>
        private int GetItemIndexBag(int ID)
        {
            for (int i = 0; i < PlayerBag.itemList.Count; i++)
            {
                InventoryItem inventoryItem = PlayerBag.itemList[i];
                if (inventoryItem.itemID == ID)
                    return i;
            }
            return -1;
        }
        /// <summary>检查背包空位</summary>
        private bool CheckBagCapacity()
        {
            for (int i = 0; i < PlayerBag.itemList.Count; i++)
            {
                InventoryItem inventoryItem = PlayerBag.itemList[i];
                if (inventoryItem.itemID == 0)
                    return true;
            }
            return false;
        }
        /// <summary>Player范围内的物品数据交换</summary>
        public void SwapItem(int fromIndex, int targetIndex)
        {
            InventoryItem currentItem = PlayerBag.itemList[fromIndex];
            InventoryItem targetItem = PlayerBag.itemList[targetIndex];
            //数据交换
            if (targetItem.itemID != 0)//交换的目标有物品的情况下
            {
                PlayerBag.itemList[fromIndex] = targetItem;
                PlayerBag.itemList[targetIndex] = currentItem;
            }
            else
            {
                PlayerBag.itemList[targetIndex] = currentItem;
                PlayerBag.itemList[fromIndex] = new InventoryItem();
            }
            EventHandler.CallUpdateInventoryUI(EInventoryLocation.Player, PlayerBag.itemList);
        }

        /// <summary>在指定背包序号位置添加物品</summary>
        /// <param name="ID">物品ID</param>
        /// <param name="index">物品序号</param>
        /// <param name="aumount">物品数量</param>
        private void AddItemAtItem(int ID, int index, int aumount)
        {
            if (index == -1 && CheckBagCapacity())//背包里面没有这个物品,同时背包有空位
            {
                InventoryItem item = new InventoryItem { itemID = ID, itemAmount = aumount };
                for (int i = 0; i < PlayerBag.itemList.Count; i++)
                {
                    if (PlayerBag.itemList[i].itemID == 0)//找到一个空位
                    {
                        PlayerBag.itemList[i] = item;
                        break;
                    }
                }
            }
            else//背包里面有这个物品
            {
                int currentAmount = PlayerBag.itemList[index].itemAmount + aumount;
                InventoryItem item = new InventoryItem { itemID = ID, itemAmount = currentAmount };
                PlayerBag.itemList[index] = item;
            }
        }

        /// <summary>
        /// 移除指定数量的背包物品
        /// </summary>
        /// <param name="ID">物品ID</param>
        /// <param name="removeAmoun">数量</param>
        private void RemoveItem(int ID, int removeAmoun)
        {
            int index = GetItemIndexBag(ID);
            if (PlayerBag.itemList[index].itemAmount>removeAmoun)
            {
                int amount = PlayerBag.itemList[index].itemAmount - removeAmoun;
                PlayerBag.itemList[index] = new InventoryItem
                {
                    itemAmount = amount,
                    itemID = ID
                };
            }
            else if (PlayerBag.itemList[index].itemAmount == removeAmoun)
            {
                PlayerBag.itemList[index] = new InventoryItem();//清空 数量相减等于0 约等于没有物品了
            }
            EventHandler.CallUpdateInventoryUI(EInventoryLocation.Player, PlayerBag.itemList);//刷新UI
        }
    }
}
