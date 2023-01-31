
using System;
using UnityEngine;

[System.Serializable]
public class ItemDetail
{
    // 物品编码
    public int itemCode;
    // 物品类型
    public ItemType itemType;
    // 物品精灵
    public Sprite itemSprite;
    // 物品描述
    public string itemDescription;
    // 物品详细描述
    public string itemLongDescription;
    // 物品使用网格半径
    public short itemUseGridRadio;
    // 物品使用半径
    public float itemUseRadio;
    // 是否为初始物品
    public bool isStartingItem;
    // 是否可以捡起
    public bool canBePickedUp;
    // 是否可以丢弃
    public bool canBeDropped;
    // 是否可以吃
    public bool canBeEaten;
    // 是否可以拿起
    public bool canBeCarried;
}
