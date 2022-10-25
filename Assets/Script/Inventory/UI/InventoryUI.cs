using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MFarm.Inventory
{
    public class InventoryUI : MonoBehaviour
    {
        /// <summary>信息提示框</summary>
        public ItemToolTip itemToolTip;

        /// <summary>拖拽的物体</summary>
        [Header("拖拽中的物品图片")]
        public Image dragItem;
        [Header("玩家背包")]
        [SerializeField]
        private GameObject bagUI;
        /// <summary>背包是否被打开了</summary>
        private bool bagOpened;
        [SerializeField]
        private Button bagButton;

        [SerializeField]
        private SlotUI[] playerSlot;

        /// <summary>添加事件</summary>
        private void OnEnable()
        {
            EventHandler.UpdateInvenoryUI += OnUpdateInventoryUI;
            EventHandler.BeforeSceneUnloadEvent += OnBeforeSceneUnloadEvent;
        }
        /// <summary>移除事件</summary>
        private void OnDisable()
        {
            EventHandler.UpdateInvenoryUI -= OnUpdateInventoryUI;
            EventHandler.BeforeSceneUnloadEvent -= OnBeforeSceneUnloadEvent;
        }
        private void Awake()
        {
            itemToolTip.gameObject.SetActive(false);//默认是关闭的
            bagUI.SetActive(false);//确保游戏开始的时候肯定是关闭状态
            bagButton.onClick.AddListener(OpenBagUI);
        }
        /// <summary>Start</summary>
        private void Start()
        {
            for (int i = 0; i < playerSlot.Length; i++)
                playerSlot[i].slotIndex = i;//给每个各自序列号
            bagOpened = bagUI.activeInHierarchy;
        }
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.B))
                OpenBagUI();
        }

        /// <summary>取消物品选择高亮</summary>
        private void OnBeforeSceneUnloadEvent() => UpdateSlotHightLight(-1);
        /// <summary>打开背包</summary>
        public void OpenBagUI()
        {
            bagOpened = !bagOpened;
            bagUI.SetActive(bagOpened);
        }
        /// <summary>更新Slot高亮显示</summary>
        public void UpdateSlotHightLight(int index)
        {
            foreach (var slot in playerSlot)
            {
                if (slot.isSelected && slot.slotIndex == index)
                {
                    slot.slotHightLight.gameObject.SetActive(true);
                }
                else
                {
                    slot.isSelected = false;
                    slot.slotHightLight.gameObject.SetActive(false);
                }
            }
        }
        /// <summary>监听的事件的具体执行方法</summary>
        private void OnUpdateInventoryUI(EInventoryLocation location, List<InventoryItem> list)
        {
            switch (location)
            {
                case EInventoryLocation.Player:
                    for (int i = 0; i < playerSlot.Length; i++)
                    {
                        if (list[i].itemAmount > 0)//有物品
                        {
                            ItemDetails item = InventoryManager.Instance.GetItemDatails(list[i].itemID);
                            playerSlot[i].UpdateSlot(item, list[i].itemAmount);
                        }
                        else
                        {
                            playerSlot[i].UpdateEmptySlot();
                        }
                    }
                    break;
                case EInventoryLocation.Box:
                    break;
                default:
                    break;
            }
        }
    }
}
