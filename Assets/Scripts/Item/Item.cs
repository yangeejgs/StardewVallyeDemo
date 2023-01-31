using System;
using UnityEngine;

public class Item : MonoBehaviour
{
    [ItemCodeDescription]
    [SerializeField]
    private int _itemCode;

    private SpriteRenderer spriteRenderer;

    public int ItemCode { get => _itemCode; set => _itemCode = value; }

    private void Awake()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    private void Start()
    {
        if (ItemCode != 0)
        {
            Init(ItemCode);
        }
    }

    public void Init(int itemCode)
    {
        if (itemCode != 0)
        {
            ItemCode = itemCode;
            ItemDetail itemDetail = InventoryManager.Instance.GetItemDetails(itemCode);
            spriteRenderer.sprite = itemDetail.itemSprite;
            // �����ʼ������Ʒ�ǿ��ظ��羰���ͣ�������������ת�ű�
            if (itemDetail.itemType == ItemType.Reapable_scenary)
            {
                gameObject.AddComponent<ItemNudge>();
            }

        }
    }
}