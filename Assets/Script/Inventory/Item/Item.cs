using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MFarm.Inventory
{
    public class Item : MonoBehaviour
    {
        public int itemID;
        public ItemDetails itemDatails;

        private SpriteRenderer spriteRenderer;
        private BoxCollider2D coll;

        private void Awake()
        {
            spriteRenderer = GetComponentInChildren<SpriteRenderer>();
            coll = GetComponent<BoxCollider2D>();
        }

        private void Start()
        {
            if (itemID != 0)
                Init(itemID);
        }

        /// <summary>根据ID生成物品</summary>
        public void Init(int ID)
        {
            itemID = ID;
            itemDatails = InventoryManager.Instance.GetItemDatails(ID);
            if (itemDatails != null)
            {
                spriteRenderer.sprite = itemDatails.itemOnWorldSprite != null ? itemDatails.itemOnWorldSprite : itemDatails.itemIcon;
                //修改碰撞体尺寸，因为SpriteRenderer的SpriteSortPoInt修改的原因
                Vector2 newSize = new Vector2(spriteRenderer.sprite.bounds.size.x, spriteRenderer.sprite.bounds.size.y);
                coll.size = newSize;//设置尺寸
                coll.offset = new Vector2(0, spriteRenderer.sprite.bounds.center.y);//设置他的偏移，根据中心点
            }
        }
    }
}
