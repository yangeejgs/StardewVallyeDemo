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
        // ���ƹ�����λ��
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
    /// ��������޸�ʱ���õķ���
    /// </summary>
    /// <param name="inventoryLocation">�������</param>
    /// <param name="inventoryList">����б�</param>
    private void InventoryUpdated(InventoryLocation inventoryLocation, List<InventoryItem> inventoryList)
    {
        // ����¼��е��޸Ŀ����������ҵ�ʱ�򴥷�
        if (inventoryLocation == InventoryLocation.player)
        {
            // ������и��ӵ����
            ClearInventorySlot();

            // ���������еĸ�����������ҿ�涼����0ʱ������������
            if (inventorySlot.Length > 0 && inventoryList.Count > 0)
            {
                for (int i = 0; i < inventorySlot.Length; i++)
                {
                    // �������±�С����ҿ��������ʱ������Ӹ�ֵ������������
                    if (i < inventoryList.Count)
                    {
                        // ��ȡ��Ʒcode
                        int itemCode = inventoryList[i].itemCode;
                        // ������Ʒ code ��ȡ��Ʒ����
                        ItemDetail itemDetail = InventoryManager.Instance.GetItemDetails(itemCode);
                        if (itemDetail != null)
                        {
                            // �������г�ʼ������
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
    /// ��ո�������
    /// </summary>
    private void ClearInventorySlot()
    {
        if(inventorySlot.Length > 0)
        {
            // ѭ����������е�����
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
    /// ���ƹ�����λ��
    /// </summary>
    private void SwitchInventoryBarPosition()
    {
        // ��ȡ���λ��
        Vector3 playerPosition = Player.Instance.GetPlayerViewportPosition();
        // ��������λ�ø�����Ļ��1/3 ���ҹ��������ڵײ���ʱ�򣬽��������ƶ����ײ�
        if (playerPosition.y > 0.3f && !IsInventoryBarPositionBottom)
        {
            rectTransform.pivot = new Vector2(0.5f, 0);
            rectTransform.anchorMin = new Vector2(0.5f, 0);
            rectTransform.anchorMax = new Vector2(0.5f, 0);
            rectTransform.anchoredPosition = new Vector2(0f, 2.5f);
            IsInventoryBarPositionBottom = true;
        }
        // ��������λ��С�ڵ�����Ļ1/3���ҹ������ڵײ���ʱ�򣬽��������ƶ����Ϸ�
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
