using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIInventorySlot : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerEnterHandler, IPointerExitHandler
{
    private Camera mainCamera;
    private Canvas parentCanvas;
    private Transform parentItem;
    private GameObject draggedItem;

    public Image inventorySlotHighLight;
    public Image inventorySlotImage;
    public TextMeshProUGUI textMeshProUGUI;
    public int slotNumber = 0;

    [SerializeField] private UIInventoryBar inventoryBar;
    [SerializeField] private GameObject itemPrefab;
    [SerializeField] private GameObject inventoryTextBoxPrefab;

    [HideInInspector] public ItemDetail ItemDetail;
    [HideInInspector] public int itemQuanlity;

    private void Awake()
    {
        parentCanvas = GetComponentInParent<Canvas>();
    }

    private void Start()
    {
        mainCamera = Camera.main;
        parentItem = GameObject.FindGameObjectWithTag(Tags.ItemParentTransform).transform;
    }

    /// <summary>
    /// 开始拖拽时的操作
    /// </summary>
    /// <param name="eventData"></param>
    public void OnBeginDrag(PointerEventData eventData)
    {
        if (ItemDetail != null)
        {
            // 禁止玩家输入行为
            Player.Instance.DisablePlayerInput();
            // 创建一个拖拽物体
            draggedItem = Instantiate(inventoryBar.inventoryBarGraggedItem, inventoryBar.transform);
            // 给拖拽物体赋予精灵
            draggedItem.GetComponentInChildren<Image>().sprite = inventorySlotImage.sprite;
        }
    }

    /// <summary>
    /// 拖拽过程中的操作
    /// </summary>
    /// <param name="eventData"></param>
    public void OnDrag(PointerEventData eventData)
    {
        if (draggedItem != null)
        {
            // 随着鼠标移动创建的拖拽物体
            draggedItem.transform.position = Input.mousePosition;
        }
    }

    /// <summary>
    /// 拖拽结束时的操作
    /// </summary>
    /// <param name="eventData"></param>
    public void OnEndDrag(PointerEventData eventData)
    {
        if (draggedItem != null)
        {
            // 销毁拖拽物体
            Destroy(draggedItem);
            // 检测拖拽位置如果拖拽到工具栏进行以下操作
            if (eventData.pointerCurrentRaycast.gameObject != null && eventData.pointerCurrentRaycast.gameObject.GetComponent<UIInventorySlot>() != null)
            {
                // 获取鼠标指向位置的物体的格子序号
                int toSlotNumber = eventData.pointerCurrentRaycast.gameObject.GetComponent<UIInventorySlot>().slotNumber;
                // 调用交换物品的方法
                InventoryManager.Instance.SwapInventoryItems(InventoryLocation.player, slotNumber, toSlotNumber);
                // 交换物品时销毁物品描述弹窗
                DestroyInventoryTextBox();
            }
            else
            {
                // 如果没有拖拽到工具栏进行以下操作
                if (ItemDetail.canBeDropped)
                {
                    DropSelectedItemAtMousePosition();
                }
            }
            // 开启玩家输入
            Player.Instance.EnablePlayerInput();
        }
    }

    /// <summary>
    /// 在鼠标位置丢弃物品
    /// </summary>
    private void DropSelectedItemAtMousePosition()
    {
        // 获取鼠标点击的世界位置
        Vector3 worldPosition = mainCamera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, -mainCamera.transform.position.z));
        // 在鼠标位置创建一个物体
        GameObject itemObject = Instantiate(itemPrefab, worldPosition, Quaternion.identity, parentItem);
        Item item = itemObject.GetComponent<Item>();
        // 将拖拽物体的code赋值给新创建的物体
        item.ItemCode = ItemDetail.itemCode;
        // 减少工具栏中的库存
        InventoryManager.Instance.RemoveItem(InventoryLocation.player, item.ItemCode);
    }

    /// <summary>
    /// 在鼠标悬停的时候触发
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerEnter(PointerEventData eventData)
    {
        // 当鼠标移动到库存格子上有物体时
        if (itemQuanlity != 0)
        {
            // 初始化描述弹窗
            inventoryBar.inventoryTextBoxGameObject = Instantiate(inventoryTextBoxPrefab, transform.position, Quaternion.identity, parentCanvas.transform);

            // 获取UIInventoryTextBox脚本 
            UIInventoryTextBox inventoryTextBox = inventoryBar.inventoryTextBoxGameObject.GetComponent<UIInventoryTextBox>();

            // 获取ItemDetailDescription
            string itemDescription = InventoryManager.Instance.GetItemTypeDescription(ItemDetail.itemType);
            // 给组件赋值
            inventoryTextBox.SetTextBoxtContent(ItemDetail.itemDescription, itemDescription, "", ItemDetail.itemLongDescription, "", "");

            // 自动适应组件出现的位置
            if (inventoryBar.IsInventoryBarPositionBottom)
            {
                // 当工具栏在底部的时候
                inventoryBar.inventoryTextBoxGameObject.GetComponent<RectTransform>().pivot = new Vector2(0.5f, 0f);
                inventoryBar.inventoryTextBoxGameObject.transform.position = new Vector3(transform.position.x, transform.position.y + 50f, transform.position.z);
            }
            else
            {
                // 当工具栏在顶部的时候
                inventoryBar.inventoryTextBoxGameObject.GetComponent<RectTransform>().pivot = new Vector2(0.5f, 1f);
                inventoryBar.inventoryTextBoxGameObject.transform.position = new Vector3(transform.position.x, transform.position.y - 50f, transform.position.z);
            }
        }
    }

    /// <summary>
    /// 鼠标离开的时候触发
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerExit(PointerEventData eventData)
    {
        DestroyInventoryTextBox();
    }

    /// <summary>
    /// 销毁弹出框
    /// </summary>
    private void DestroyInventoryTextBox()
    {
        if (inventoryBar.inventoryTextBoxGameObject != null)
        {
            Destroy(inventoryBar.inventoryTextBoxGameObject);
        }
    }
}
