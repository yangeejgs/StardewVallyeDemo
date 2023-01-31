
using System;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : SingletonMonoBehavior<InventoryManager>
{
    // 私有的物品字典
    private Dictionary<int, ItemDetail> itemDetailDictionary;
    // 物品列表
    [SerializeField] private SOItemList itemList = null;

    // 库存列表数组
    public List<InventoryItem>[] inventoryLists;
    // 库存列表容量数组
    [HideInInspector] public int[] inventoryListCapacityIntArray;

    protected override void Awake()
    {
        base.Awake();

        // 初始化库存
        CreateInventoryLists();

        // 初始化私有字典
        CreateItemDetailDictionary();
    }

    private void CreateInventoryLists()
    {
        // 初始化库存列表数组
        inventoryLists = new List<InventoryItem>[(int)InventoryLocation.count];
        for (int i = 0; i < (int)InventoryLocation.count; i++)
        {
            inventoryLists[i] = new List<InventoryItem>();
        }
        // 初始化库存列表容量数组
        inventoryListCapacityIntArray = new int[(int)InventoryLocation.count];
        // 初始化玩家背包容量
        inventoryListCapacityIntArray[(int)InventoryLocation.player] = Settings.playerInitialInventoryCapacity;
    }

    /// <summary>
    /// 添加物品
    /// </summary>
    /// <param name="inventoryLocation">库存物体类</param>
    /// <param name="item">物品类</param>
    /// <param name="gameObjectToDelete">要删除的游戏物体</param>
    public void AddItem(InventoryLocation inventoryLocation, Item item, GameObject gameObjectToDelete)
    {
        // 添加物品
        AddItem(inventoryLocation, item);
        // 删除游戏物体
        Destroy(gameObjectToDelete);
    }

    /// <summary>
    /// 添加物品
    /// </summary>
    /// <param name="inventoryLocation">库存物体类</param>
    /// <param name="item">物品类</param>
    public void AddItem(InventoryLocation inventoryLocation, Item item)
    {
        // 获取itemCode
        int itemCode = item.ItemCode;
        // 获取对应库存
        List<InventoryItem> inventoryList = inventoryLists[(int)inventoryLocation];
        // 检查物品是否在库存中
        int itemPosition = FindItemInInventory(inventoryLocation, itemCode);
        // 如果物品在库存中，对应位置的数量+1
        if (itemPosition != -1)
        {
            AddItemAtPosition(inventoryList, item, itemPosition);
        }
        else
        {
            //如果物品不在库存中，添加到库存末尾
            AddItemAtPosition(inventoryList, item);
        }
        // 调用修改商品事件
        EventHandler.callInventoryUpdatedEvent(inventoryLocation, inventoryList);
    }

    /// <summary>
    /// 添加物品到库存指定位置
    /// </summary>
    /// <param name="inventoryList">库存列表</param>
    /// <param name="item">物品</param>
    private void AddItemAtPosition(List<InventoryItem> inventoryList, Item item)
    {
        // 初始化插入的物品对象
        InventoryItem inventoryItem = new InventoryItem();
        inventoryItem.itemCode = item.ItemCode;
        // 库存数量为1
        inventoryItem.itemQuantily = 1;
        // 将对象加入到库存中
        inventoryList.Add(inventoryItem);
        // 打印库存到控制台
        //DebugPrintInventoryList(inventoryList);
    }

    /// <summary>
    /// 添加物品到库存指定位置
    /// </summary>
    /// <param name="inventoryList">库存列表</param>
    /// <param name="item">物品</param>
    /// <param name="itemPosition">物品在库存中的下标</param>
    private void AddItemAtPosition(List<InventoryItem> inventoryList, Item item, int itemPosition)
    {
        // 初始化插入的物品对象
        InventoryItem inventoryItem = new InventoryItem();
        inventoryItem.itemCode = item.ItemCode;
        // 在库存指定位置物品+1
        inventoryItem.itemQuantily = inventoryList[itemPosition].itemQuantily + 1;
        // 将新对象赋值到库存中
        inventoryList[itemPosition] = inventoryItem;

        // 打印库存到控制台
        //DebugPrintInventoryList(inventoryList);
    }



    /// <summary>
    /// 根据 itemCode 在库存中查找
    /// </summary>
    /// <param name="inventoryLocation">库存名称</param>
    /// <param name="itemCode">物品Code</param>
    /// <returns>物品在库存中对应的下标，没有为-1</returns>
    private int FindItemInInventory(InventoryLocation inventoryLocation, int itemCode)
    {
        // 获取对应的库存
        List<InventoryItem> inventoryList = inventoryLists[(int)inventoryLocation];
        // 在库存中循环查找对应的itemCode
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
        // 初始化字典
        itemDetailDictionary = new Dictionary<int, ItemDetail>();
        // 遍历物品列表将物品加入到私有字典中
        foreach (ItemDetail itemDetail in itemList.itemsDetails)
        {
            itemDetailDictionary.Add(itemDetail.itemCode, itemDetail);
        }
    }
    public ItemDetail GetItemDetails(int itemCode)
    {
        // 尝试根据参数 itemCode 从字典中获取物品，如果物品存在返回，不存在返回null
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
    /// 从库存中移除物品的方法
    /// </summary>
    /// <param name="inventoryLocation">库存位置</param>
    /// <param name="itemCode">物品Code</param>
    internal void RemoveItem(InventoryLocation inventoryLocation, int itemCode)
    {
        // 获取库存
        List<InventoryItem> inventoryItems = inventoryLists[(int)inventoryLocation];
        // 查找库存中是否有物品
        int position = FindItemInInventory(inventoryLocation, itemCode);
        // 如果库存中存在物品，在指定位置删除库存
        if (position != -1)
        {
            RemoveItemAtPosation(inventoryItems, itemCode, position);
        }
        // 调用库存变化事件
        EventHandler.callInventoryUpdatedEvent(inventoryLocation, inventoryItems);

    }

    /// <summary>
    /// 在库存的指定位置删除一个物品
    /// </summary>
    /// <param name="inventoryItems">库存列表</param>
    /// <param name="itemCode">物品code</param>
    /// <param name="position">物品在库存中的位置</param>
    private void RemoveItemAtPosation(List<InventoryItem> inventoryItems, int itemCode, int position)
    {
        // 创建新的库存对象
        InventoryItem inventoryItem = new InventoryItem();
        // 库存数量-1
        int quantily = inventoryItems[position].itemQuantily - 1;
        // 如果减少后的库存大于0，将新的库存对象放入库存中
        if (quantily > 0)
        {
            inventoryItem.itemQuantily = quantily;
            inventoryItem.itemCode = itemCode;
            inventoryItems[position] = inventoryItem;
        }
        else
        {
            // 如果减少后的库存小于等于0，删除库存中的对象
            inventoryItems.RemoveAt(position);
        }
    }

    /// <summary>
    /// 交换库存中的物品
    /// </summary>
    /// <param name="inventoryLocation">库存位置</param>
    /// <param name="slotNumber">当前格子序号</param>
    /// <param name="toSlotNumber">交换的格子序号</param>
    internal void SwapInventoryItems(InventoryLocation inventoryLocation, int slotNumber, int toSlotNumber)
    {
        // 判断交换的参数
        if (slotNumber >= 0 && toSlotNumber >= 0 && slotNumber != toSlotNumber && slotNumber < inventoryLists[(int)inventoryLocation].Count && toSlotNumber < inventoryLists[(int)inventoryLocation].Count)
        {
            // 获取交换的物品
            InventoryItem fromItem = inventoryLists[(int)inventoryLocation][slotNumber];
            InventoryItem toItem = inventoryLists[(int)inventoryLocation][toSlotNumber];
            // 交换物品
            inventoryLists[(int)inventoryLocation][slotNumber] = toItem;
            inventoryLists[(int)inventoryLocation][toSlotNumber] = fromItem;
            // 调用物品修改事件
            EventHandler.callInventoryUpdatedEvent(inventoryLocation, inventoryLists[(int)inventoryLocation]);

        }
    }

    /// <summary>
    /// 获取物品类型描述
    /// </summary>
    /// <param name="itemType">物品类型</param>
    /// <returns>物品类型描述</returns>
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
    /// 打印商品列表到控制台
    /// </summary>
    /// <param name="inventoryList">商品列表</param>
    //private void DebugPrintInventoryList(List<InventoryItem> inventoryList)
    //{
    //    foreach (InventoryItem item in inventoryList)
    //    {
    //        Debug.Log("描述：" + GetItemDetails(item.itemCode).itemDescription + "    数量：" + item.itemQuantily);
    //    }
    //    Debug.Log("--------------------------------------------------");
    //}
}
