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
    /// ��ʼ��קʱ�Ĳ���
    /// </summary>
    /// <param name="eventData"></param>
    public void OnBeginDrag(PointerEventData eventData)
    {
        if (ItemDetail != null)
        {
            // ��ֹ���������Ϊ
            Player.Instance.DisablePlayerInput();
            // ����һ����ק����
            draggedItem = Instantiate(inventoryBar.inventoryBarGraggedItem, inventoryBar.transform);
            // ����ק���帳�辫��
            draggedItem.GetComponentInChildren<Image>().sprite = inventorySlotImage.sprite;
        }
    }

    /// <summary>
    /// ��ק�����еĲ���
    /// </summary>
    /// <param name="eventData"></param>
    public void OnDrag(PointerEventData eventData)
    {
        if (draggedItem != null)
        {
            // ��������ƶ���������ק����
            draggedItem.transform.position = Input.mousePosition;
        }
    }

    /// <summary>
    /// ��ק����ʱ�Ĳ���
    /// </summary>
    /// <param name="eventData"></param>
    public void OnEndDrag(PointerEventData eventData)
    {
        if (draggedItem != null)
        {
            // ������ק����
            Destroy(draggedItem);
            // �����קλ�������ק���������������²���
            if (eventData.pointerCurrentRaycast.gameObject != null && eventData.pointerCurrentRaycast.gameObject.GetComponent<UIInventorySlot>() != null)
            {
                // ��ȡ���ָ��λ�õ�����ĸ������
                int toSlotNumber = eventData.pointerCurrentRaycast.gameObject.GetComponent<UIInventorySlot>().slotNumber;
                // ���ý�����Ʒ�ķ���
                InventoryManager.Instance.SwapInventoryItems(InventoryLocation.player, slotNumber, toSlotNumber);
                // ������Ʒʱ������Ʒ��������
                DestroyInventoryTextBox();
            }
            else
            {
                // ���û����ק���������������²���
                if (ItemDetail.canBeDropped)
                {
                    DropSelectedItemAtMousePosition();
                }
            }
            // �����������
            Player.Instance.EnablePlayerInput();
        }
    }

    /// <summary>
    /// �����λ�ö�����Ʒ
    /// </summary>
    private void DropSelectedItemAtMousePosition()
    {
        // ��ȡ�����������λ��
        Vector3 worldPosition = mainCamera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, -mainCamera.transform.position.z));
        // �����λ�ô���һ������
        GameObject itemObject = Instantiate(itemPrefab, worldPosition, Quaternion.identity, parentItem);
        Item item = itemObject.GetComponent<Item>();
        // ����ק�����code��ֵ���´���������
        item.ItemCode = ItemDetail.itemCode;
        // ���ٹ������еĿ��
        InventoryManager.Instance.RemoveItem(InventoryLocation.player, item.ItemCode);
    }

    /// <summary>
    /// �������ͣ��ʱ�򴥷�
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerEnter(PointerEventData eventData)
    {
        // ������ƶ�����������������ʱ
        if (itemQuanlity != 0)
        {
            // ��ʼ����������
            inventoryBar.inventoryTextBoxGameObject = Instantiate(inventoryTextBoxPrefab, transform.position, Quaternion.identity, parentCanvas.transform);

            // ��ȡUIInventoryTextBox�ű� 
            UIInventoryTextBox inventoryTextBox = inventoryBar.inventoryTextBoxGameObject.GetComponent<UIInventoryTextBox>();

            // ��ȡItemDetailDescription
            string itemDescription = InventoryManager.Instance.GetItemTypeDescription(ItemDetail.itemType);
            // �������ֵ
            inventoryTextBox.SetTextBoxtContent(ItemDetail.itemDescription, itemDescription, "", ItemDetail.itemLongDescription, "", "");

            // �Զ���Ӧ������ֵ�λ��
            if (inventoryBar.IsInventoryBarPositionBottom)
            {
                // ���������ڵײ���ʱ��
                inventoryBar.inventoryTextBoxGameObject.GetComponent<RectTransform>().pivot = new Vector2(0.5f, 0f);
                inventoryBar.inventoryTextBoxGameObject.transform.position = new Vector3(transform.position.x, transform.position.y + 50f, transform.position.z);
            }
            else
            {
                // ���������ڶ�����ʱ��
                inventoryBar.inventoryTextBoxGameObject.GetComponent<RectTransform>().pivot = new Vector2(0.5f, 1f);
                inventoryBar.inventoryTextBoxGameObject.transform.position = new Vector3(transform.position.x, transform.position.y - 50f, transform.position.z);
            }
        }
    }

    /// <summary>
    /// ����뿪��ʱ�򴥷�
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerExit(PointerEventData eventData)
    {
        DestroyInventoryTextBox();
    }

    /// <summary>
    /// ���ٵ�����
    /// </summary>
    private void DestroyInventoryTextBox()
    {
        if (inventoryBar.inventoryTextBoxGameObject != null)
        {
            Destroy(inventoryBar.inventoryTextBoxGameObject);
        }
    }
}
