
using System;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : SingletonMonoBehavior<InventoryManager>
{
    // ˽�е���Ʒ�ֵ�
    private Dictionary<int, ItemDetail> itemDetailDictionary;
    // ��Ʒ�б�
    [SerializeField] private SOItemList itemList = null;

    // ����б�����
    public List<InventoryItem>[] inventoryLists;
    // ����б���������
    [HideInInspector] public int[] inventoryListCapacityIntArray;

    protected override void Awake()
    {
        base.Awake();

        // ��ʼ�����
        CreateInventoryLists();

        // ��ʼ��˽���ֵ�
        CreateItemDetailDictionary();
    }

    private void CreateInventoryLists()
    {
        // ��ʼ������б�����
        inventoryLists = new List<InventoryItem>[(int)InventoryLocation.count];
        for (int i = 0; i < (int)InventoryLocation.count; i++)
        {
            inventoryLists[i] = new List<InventoryItem>();
        }
        // ��ʼ������б���������
        inventoryListCapacityIntArray = new int[(int)InventoryLocation.count];
        // ��ʼ����ұ�������
        inventoryListCapacityIntArray[(int)InventoryLocation.player] = Settings.playerInitialInventoryCapacity;
    }

    /// <summary>
    /// �����Ʒ
    /// </summary>
    /// <param name="inventoryLocation">���������</param>
    /// <param name="item">��Ʒ��</param>
    /// <param name="gameObjectToDelete">Ҫɾ������Ϸ����</param>
    public void AddItem(InventoryLocation inventoryLocation, Item item, GameObject gameObjectToDelete)
    {
        // �����Ʒ
        AddItem(inventoryLocation, item);
        // ɾ����Ϸ����
        Destroy(gameObjectToDelete);
    }

    /// <summary>
    /// �����Ʒ
    /// </summary>
    /// <param name="inventoryLocation">���������</param>
    /// <param name="item">��Ʒ��</param>
    public void AddItem(InventoryLocation inventoryLocation, Item item)
    {
        // ��ȡitemCode
        int itemCode = item.ItemCode;
        // ��ȡ��Ӧ���
        List<InventoryItem> inventoryList = inventoryLists[(int)inventoryLocation];
        // �����Ʒ�Ƿ��ڿ����
        int itemPosition = FindItemInInventory(inventoryLocation, itemCode);
        // �����Ʒ�ڿ���У���Ӧλ�õ�����+1
        if (itemPosition != -1)
        {
            AddItemAtPosition(inventoryList, item, itemPosition);
        }
        else
        {
            //�����Ʒ���ڿ���У���ӵ����ĩβ
            AddItemAtPosition(inventoryList, item);
        }
        // �����޸���Ʒ�¼�
        EventHandler.callInventoryUpdatedEvent(inventoryLocation, inventoryList);
    }

    /// <summary>
    /// �����Ʒ�����ָ��λ��
    /// </summary>
    /// <param name="inventoryList">����б�</param>
    /// <param name="item">��Ʒ</param>
    private void AddItemAtPosition(List<InventoryItem> inventoryList, Item item)
    {
        // ��ʼ���������Ʒ����
        InventoryItem inventoryItem = new InventoryItem();
        inventoryItem.itemCode = item.ItemCode;
        // �������Ϊ1
        inventoryItem.itemQuantily = 1;
        // ��������뵽�����
        inventoryList.Add(inventoryItem);
        // ��ӡ��浽����̨
        //DebugPrintInventoryList(inventoryList);
    }

    /// <summary>
    /// �����Ʒ�����ָ��λ��
    /// </summary>
    /// <param name="inventoryList">����б�</param>
    /// <param name="item">��Ʒ</param>
    /// <param name="itemPosition">��Ʒ�ڿ���е��±�</param>
    private void AddItemAtPosition(List<InventoryItem> inventoryList, Item item, int itemPosition)
    {
        // ��ʼ���������Ʒ����
        InventoryItem inventoryItem = new InventoryItem();
        inventoryItem.itemCode = item.ItemCode;
        // �ڿ��ָ��λ����Ʒ+1
        inventoryItem.itemQuantily = inventoryList[itemPosition].itemQuantily + 1;
        // ���¶���ֵ�������
        inventoryList[itemPosition] = inventoryItem;

        // ��ӡ��浽����̨
        //DebugPrintInventoryList(inventoryList);
    }



    /// <summary>
    /// ���� itemCode �ڿ���в���
    /// </summary>
    /// <param name="inventoryLocation">�������</param>
    /// <param name="itemCode">��ƷCode</param>
    /// <returns>��Ʒ�ڿ���ж�Ӧ���±꣬û��Ϊ-1</returns>
    private int FindItemInInventory(InventoryLocation inventoryLocation, int itemCode)
    {
        // ��ȡ��Ӧ�Ŀ��
        List<InventoryItem> inventoryList = inventoryLists[(int)inventoryLocation];
        // �ڿ����ѭ�����Ҷ�Ӧ��itemCode
        for (int i = 0; i < inventoryList.Count; i++)
        {
            if (inventoryList[i].itemCode == itemCode)
            {
                return i;
            }
        }
        return -1;
    }

    private void CreateItemDetailDictionary()
    {
        // ��ʼ���ֵ�
        itemDetailDictionary = new Dictionary<int, ItemDetail>();
        // ������Ʒ�б���Ʒ���뵽˽���ֵ���
        foreach (ItemDetail itemDetail in itemList.itemsDetails)
        {
            itemDetailDictionary.Add(itemDetail.itemCode, itemDetail);
        }
    }
    public ItemDetail GetItemDetails(int itemCode)
    {
        // ���Ը��ݲ��� itemCode ���ֵ��л�ȡ��Ʒ�������Ʒ���ڷ��أ������ڷ���null
        if (itemDetailDictionary.TryGetValue(itemCode, out ItemDetail itemDetail))
        {
            return itemDetail;
        }
        else
        {
            return null;
        }
    }

    /// <summary>
    /// �ӿ�����Ƴ���Ʒ�ķ���
    /// </summary>
    /// <param name="inventoryLocation">���λ��</param>
    /// <param name="itemCode">��ƷCode</param>
    internal void RemoveItem(InventoryLocation inventoryLocation, int itemCode)
    {
        // ��ȡ���
        List<InventoryItem> inventoryItems = inventoryLists[(int)inventoryLocation];
        // ���ҿ�����Ƿ�����Ʒ
        int position = FindItemInInventory(inventoryLocation, itemCode);
        // �������д�����Ʒ����ָ��λ��ɾ�����
        if (position != -1)
        {
            RemoveItemAtPosation(inventoryItems, itemCode, position);
        }
        // ���ÿ��仯�¼�
        EventHandler.callInventoryUpdatedEvent(inventoryLocation, inventoryItems);

    }

    /// <summary>
    /// �ڿ���ָ��λ��ɾ��һ����Ʒ
    /// </summary>
    /// <param name="inventoryItems">����б�</param>
    /// <param name="itemCode">��Ʒcode</param>
    /// <param name="position">��Ʒ�ڿ���е�λ��</param>
    private void RemoveItemAtPosation(List<InventoryItem> inventoryItems, int itemCode, int position)
    {
        // �����µĿ�����
        InventoryItem inventoryItem = new InventoryItem();
        // �������-1
        int quantily = inventoryItems[position].itemQuantily - 1;
        // ������ٺ�Ŀ�����0�����µĿ������������
        if (quantily > 0)
        {
            inventoryItem.itemQuantily = quantily;
            inventoryItem.itemCode = itemCode;
            inventoryItems[position] = inventoryItem;
        }
        else
        {
            // ������ٺ�Ŀ��С�ڵ���0��ɾ������еĶ���
            inventoryItems.RemoveAt(position);
        }
    }

    /// <summary>
    /// ��������е���Ʒ
    /// </summary>
    /// <param name="inventoryLocation">���λ��</param>
    /// <param name="slotNumber">��ǰ�������</param>
    /// <param name="toSlotNumber">�����ĸ������</param>
    internal void SwapInventoryItems(InventoryLocation inventoryLocation, int slotNumber, int toSlotNumber)
    {
        // �жϽ����Ĳ���
        if (slotNumber >= 0 && toSlotNumber >= 0 && slotNumber != toSlotNumber && slotNumber < inventoryLists[(int)inventoryLocation].Count && toSlotNumber < inventoryLists[(int)inventoryLocation].Count)
        {
            // ��ȡ��������Ʒ
            InventoryItem fromItem = inventoryLists[(int)inventoryLocation][slotNumber];
            InventoryItem toItem = inventoryLists[(int)inventoryLocation][toSlotNumber];
            // ������Ʒ
            inventoryLists[(int)inventoryLocation][slotNumber] = toItem;
            inventoryLists[(int)inventoryLocation][toSlotNumber] = fromItem;
            // ������Ʒ�޸��¼�
            EventHandler.callInventoryUpdatedEvent(inventoryLocation, inventoryLists[(int)inventoryLocation]);

        }
    }

    /// <summary>
    /// ��ȡ��Ʒ��������
    /// </summary>
    /// <param name="itemType">��Ʒ����</param>
    /// <returns>��Ʒ��������</returns>
    public string GetItemTypeDescription(ItemType itemType)
    {
        string itemTypeDescription;
        switch (itemType)
        {
            case ItemType.Breaking_tool:
                itemTypeDescription = Settings.BreakingTool;
                break;
            case ItemType.Choping_tool:
                itemTypeDescription = Settings.ChoppingTool;
                break;
            case ItemType.Hoeing_tool:
                itemTypeDescription = Settings.HoeingTool;
                break;
            case ItemType.Reaping_tool:
                itemTypeDescription = Settings.ReapingTool;
                break;
            case ItemType.Wartering_tool:
                itemTypeDescription = Settings.WateringTool;
                break;
            case ItemType.Collecting_tool:
                itemTypeDescription = Settings.CollectingTool;
                break;
            default:
                itemTypeDescription = itemType.ToString();
                break;
        }
        return itemTypeDescription;
    }

    /// <summary>
    /// ��ӡ��Ʒ�б�����̨
    /// </summary>
    /// <param name="inventoryList">��Ʒ�б�</param>
    //private void DebugPrintInventoryList(List<InventoryItem> inventoryList)
    //{
    //    foreach (InventoryItem item in inventoryList)
    //    {
    //        Debug.Log("������" + GetItemDetails(item.itemCode).itemDescription + "    ������" + item.itemQuantily);
    //    }
    //    Debug.Log("--------------------------------------------------");
    //}
}
