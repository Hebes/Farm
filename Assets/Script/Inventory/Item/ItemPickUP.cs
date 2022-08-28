using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace MFarm.Inventory
{
    public class ItemPickUP : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D collision)
        {
            Item item = collision.GetComponent<Item>();
            if (item != null)
            {
                if (item.itemDatails.canPickedUp)
                {
                    //拾取物品到背包
                    InventoryManager.Instance.AddItem(item, true);
                }
            }
        }
    }
}

