using System.Collections.Generic;
using UnityEngine;


public class UIInventoryBar : MonoBehaviour
{
    [SerializeField] private Sprite black16x16Sprite = null;
    [SerializeField] private UIInventorySlot[] inventorySlot = null;
    [HideInInspector] public GameObject inventoryTextBoxGameObject;

    public GameObject inventoryBarGraggedItem;

    private RectTransform rectTransform;

    private bool _isInventoryBarPositionBottom = true;

    public bool IsInventoryBarPositionBottom { get => _isInventoryBarPositionBottom; set => _isInventoryBarPositionBottom = value; }

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        // 控制工具栏位置
        SwitchInventoryBarPosition();
    }

    private void OnEnable()
    {
        EventHandler.InventoryUpdatedEvent += InventoryUpdated;
    }

    private void OnDisable()
    {
        EventHandler.InventoryUpdatedEvent -= InventoryUpdated;
    }

    /// <summary>
    /// 监听库存修改时调用的方法
    /// </summary>
    /// <param name="inventoryLocation">库存类型</param>
    /// <param name="inventoryList">库存列表</param>
    private void InventoryUpdated(InventoryLocation inventoryLocation, List<InventoryItem> inventoryList)
    {
        // 如果事件中的修改库存类型是玩家的时候触发
        if (inventoryLocation == InventoryLocation.player)
        {
            // 清空所有格子的填充
            ClearInventorySlot();

            // 当工具栏中的格子数量和玩家库存都大于0时遍历格子内容
            if (inventorySlot.Length > 0 && inventoryList.Count > 0)
            {
                for (int i = 0; i < inventorySlot.Length; i++)
                {
                    // 当格子下标小于玩家库存数量的时候给格子赋值否则跳出方法
                    if (i < inventoryList.Count)
                    {
                        // 获取物品code
                        int itemCode = inventoryList[i].itemCode;
                        // 根据物品 code 获取物品详情
                        ItemDetail itemDetail = InventoryManager.Instance.GetItemDetails(itemCode);
                        if (itemDetail != null)
                        {
                            // 给格子中初始化属性
                            inventorySlot[i].inventorySlotImage.sprite = itemDetail.itemSprite;
                            inventorySlot[i].textMeshProUGUI.text = inventoryList[i].itemQuantily.ToString();
                            inventorySlot[i].ItemDetail = itemDetail;
                            inventorySlot[i].itemQuanlity = inventoryList[i].itemQuantily;
                        }
                    }
                    else
                    {
                        break;
                    }
                }
            }

        }
    }

    /// <summary>
    /// 清空格子属性
    /// </summary>
    private void ClearInventorySlot()
    {
        if(inventorySlot.Length > 0)
        {
            // 循环清除格子中的属性
            for(int i = 0; i < inventorySlot.Length; i++)
            {
                inventorySlot[i].inventorySlotImage.sprite = black16x16Sprite;
                inventorySlot[i].textMeshProUGUI.text = "";
                inventorySlot[i].ItemDetail = null;
                inventorySlot[i].itemQuanlity = 0;
            }
        }
    }

    /// <summary>
    /// 控制工具栏位置
    /// </summary>
    private void SwitchInventoryBarPosition()
    {
        // 获取玩家位置
        Vector3 playerPosition = Player.Instance.GetPlayerViewportPosition();
        // 当玩家相对位置高于屏幕的1/3 并且工具栏不在底部的时候，将工具栏移动到底部
        if (playerPosition.y > 0.3f && !IsInventoryBarPositionBottom)
        {
            rectTransform.pivot = new Vector2(0.5f, 0);
            rectTransform.anchorMin = new Vector2(0.5f, 0);
            rectTransform.anchorMax = new Vector2(0.5f, 0);
            rectTransform.anchoredPosition = new Vector2(0f, 2.5f);
            IsInventoryBarPositionBottom = true;
        }
        // 当玩家相对位置小于等于屏幕1/3并且工具栏在底部的时候，将工具栏移动到上方
        else if (playerPosition.y <= 0.3f && IsInventoryBarPositionBottom)
        {
            rectTransform.pivot = new Vector2(0.5f, 1f);
            rectTransform.anchorMin = new Vector2(0.5f, 1f);
            rectTransform.anchorMax = new Vector2(0.5f, 1f);
            rectTransform.anchoredPosition = new Vector2(0f, -2.5f);
            IsInventoryBarPositionBottom = false;
        }

    }
}
