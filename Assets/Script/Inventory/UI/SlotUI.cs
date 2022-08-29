using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace MFarm.Inventory
{
    public class SlotUI : MonoBehaviour,IPointerClickHandler,IBeginDragHandler,IDragHandler,IEndDragHandler
    {
        [Header("获取组件")]
        [SerializeField]
        private Image slotImage;//图片

        [SerializeField]
        private TextMeshProUGUI amountText;//数量

        public Image slotHightLight;//高亮

        [SerializeField]
        private Button button;//按钮

        private InventoryUI inventoryUI => GetComponentInParent<InventoryUI>();

        public ESlotType eSlotType;//类型

        /// <summary>
        /// 是否启用高亮
        /// </summary>
        public bool isSelected;

        /// <summary>
        /// 物品信息
        /// </summary>
        public ItemDatails itemDatails;

        /// <summary>
        /// 物品数量
        /// </summary>
        public int itemAmount;

        /// <summary>
        /// 物品系列号
        /// </summary>
        public int slotIndex;

        private void Start()
        {
            isSelected = false;
            if (itemDatails.itemID == 0)
                UpdateEmptySlot();
        }

        /// <summary>
        /// 更新Slot显示
        /// </summary>
        /// <param name="item"></param>
        /// <param name="Amount"></param>
        public void UpdateSlot(ItemDatails item, int Amount)
        {
            itemDatails = item;
            slotImage.sprite = item.itemIcon;
            itemAmount = Amount;
            amountText.text = Amount.ToString();
            slotImage.enabled = true;
            button.interactable = true;//该组是否可交互（组下的元素是否处于启用状态）。
        }

        /// <summary>清空Slot更新</summary>
        public void UpdateEmptySlot()
        {
            if (isSelected)
                isSelected = false;
            slotImage.enabled = false;
            amountText.text = string.Empty;
            button.interactable = false;//该组是否可交互（组下的元素是否处于启用状态）。
        }


        #region 接口实现
        /// <summary>点按接口的实现</summary>
        public void OnPointerClick(PointerEventData eventData)
        {
            if (itemAmount == 0) return;
            isSelected = !isSelected;
            slotHightLight.gameObject.SetActive(isSelected);
            inventoryUI.UpdateSlotHightLight(slotIndex);
        }
        /// <summary>开始拖拽</summary>
        public void OnBeginDrag(PointerEventData eventData)
        {
            if (itemAmount!=0)
            {
                inventoryUI.dragItem.enabled = true;//启用拖拽的物体
                inventoryUI.dragItem.sprite = slotImage.sprite;//设置拖拽物体的图片
                slotImage.color = new Color(slotImage.color.r, slotImage.color.g, slotImage.color.b, .5f);
                inventoryUI.dragItem.SetNativeSize();

                isSelected = true;
                inventoryUI.UpdateSlotHightLight(slotIndex);
            }
        }
        /// <summary>拖拽中</summary>
        public void OnDrag(PointerEventData eventData)
        {
            inventoryUI.dragItem.transform.position = Input.mousePosition;//拖拽物品的移动
        }
        /// <summary>结束拖拽</summary>
        public void OnEndDrag(PointerEventData eventData)
        {
            inventoryUI.dragItem.enabled = false;
            if (eventData.pointerCurrentRaycast.gameObject==null)
            {
                slotImage.color = new Color(slotImage.color.r, slotImage.color.g, slotImage.color.b, 1);
            }
            Debug.Log(eventData.pointerCurrentRaycast.gameObject);//打印鼠标指针的射线检测到的物体
        }
        #endregion
    }
}