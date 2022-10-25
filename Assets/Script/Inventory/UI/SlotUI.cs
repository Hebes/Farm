using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace MFarm.Inventory
{
    public class SlotUI : MonoBehaviour, IPointerClickHandler, IBeginDragHandler, IDragHandler, IEndDragHandler
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
        public ItemDetails itemDatails;

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
            if (itemDatails == null)
                UpdateEmptySlot();
        }

        /// <summary>
        /// 更新Slot显示
        /// </summary>
        /// <param name="item"></param>
        /// <param name="Amount"></param>
        public void UpdateSlot(ItemDetails item, int Amount)
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
            {
                isSelected = false;
                //清空所有高亮
                inventoryUI.UpdateSlotHightLight(-1);
                EventHandler.CallItemSelectEvent(itemDatails, isSelected);
            }
            itemDatails = null;
            slotImage.enabled = false;
            amountText.text = string.Empty;
            button.interactable = false;//该组是否可交互（组下的元素是否处于启用状态）。
        }


        #region 接口实现
        /// <summary>点按接口的实现</summary>
        public void OnPointerClick(PointerEventData eventData)
        {
            if (itemDatails == null) return;
            isSelected = !isSelected;
            slotHightLight.gameObject.SetActive(isSelected);
            inventoryUI.UpdateSlotHightLight(slotIndex);
            //判断是否是在商店中点击(商店不执行代码)
            if (eSlotType == ESlotType.Bag)
            {
                //通知物品被选中的状态和信息
                EventHandler.CallItemSelectEvent(itemDatails, isSelected);
            }
        }
        /// <summary>开始拖拽</summary>
        public void OnBeginDrag(PointerEventData eventData)
        {
            if (itemAmount != 0)
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
            if (eventData.pointerCurrentRaycast.gameObject != null)
            {
                //物品交换
                if (eventData.pointerCurrentRaycast.gameObject.GetComponent<SlotUI>() == null) return;
                var targetSlot = eventData.pointerCurrentRaycast.gameObject.GetComponent<SlotUI>();//如果是存在SlotUI组件的话
                int targetIndex = targetSlot.slotIndex;

                //在player自身背包范围内的交换
                if (eSlotType == ESlotType.Bag && targetSlot.eSlotType == ESlotType.Bag)//类型都相同的话
                    InventoryManager.Instance.SwapItem(slotIndex, targetIndex);
                Debug.Log(eventData.pointerCurrentRaycast.gameObject);//打印鼠标指针的射线检测到的物体
                slotImage.color = new Color(slotImage.color.r, slotImage.color.g, slotImage.color.b, 1);
            }
            //else //测试仍在地上  //TUDO  31集
            //{
            //    if (itemDatails.canDropped)
            //    {
            //        //屏幕坐标转成世界坐标 鼠标对应的世界坐标
            //        var pos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, -Camera.main.transform.position.z));
            //        EventHandler.CallInstantiateItemScen(itemDatails.itemID, pos);
            //        UpdateEmptySlot();
            //    }
            //}

            //清空所有高亮
            inventoryUI.UpdateSlotHightLight(-1);
        }
        #endregion
    }
}