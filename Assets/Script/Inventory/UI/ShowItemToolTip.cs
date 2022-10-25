using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


namespace MFarm.Inventory
{
    [RequireComponent(typeof(SlotUI))]
    public class ShowItemToolTip : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        private SlotUI slotUI => GetComponent<SlotUI>();
        private InventoryUI inventoryUI => GetComponentInParent<InventoryUI>();


        /// <summary>鼠标进入事件</summary>
        public void OnPointerEnter(PointerEventData eventData)
        {
            if (slotUI.itemDatails != null)
            {
                inventoryUI.itemToolTip.gameObject.SetActive(true);
                inventoryUI.itemToolTip.SetupTooltip(slotUI.itemDatails, slotUI.eSlotType);

                inventoryUI.itemToolTip.GetComponent<RectTransform>().pivot = new Vector2(0f, 0f);//设置锚点
                inventoryUI.itemToolTip.transform.position = transform.position + Vector3.up * 60;//设置距离
            }
            else
            {
                inventoryUI.itemToolTip.gameObject.SetActive(false);
            }
        }
        /// <summary>鼠标移出事件</summary>
        public void OnPointerExit(PointerEventData eventData) => inventoryUI.itemToolTip.gameObject.SetActive(false);


    }
}
