using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MFarm.Inventory
{
    public class InventoryManager : Singleton<InventoryManager>
    {
        [Header("物品数据")]
        public ItemDataList_SO ItemDataList_SO;
        [Header("背包数据")]
        public InventoryBag_SO PlayerBag;

        /// <summary>获取一个物品信息</summary>
        public ItemDatails GetItemDatails(int ID) => ItemDataList_SO.ItemDatailsList.Find(i => i.itemID == ID);

        /// <summary>添加物品到背包</summary>
        public void AddItem(Item item, bool toDestory)
        {
            //背包是否有该物品
            int index = GetItemIndexBag(item.itemID);
            AddItemAtItem(item.itemID, index, 1);
            Debug.Log(item.itemID);
            if (toDestory)
                Destroy(item.gameObject);
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

        /// <summary>在指定背包序号位置添加物品</summary>
        /// <param name="ID">物品ID</param>
        /// <param name="index">物品序号</param>
        /// <param name="aumount">物品数量</param>
        private void AddItemAtItem(int ID, int index, int aumount)
        {
            if (index == -1&& CheckBagCapacity())//背包里面没有这个物品,同时背包有空位
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

    }
}
