
using System;
using UnityEngine;

[System.Serializable]
public class ItemDetail
{
    // ��Ʒ����
    public int itemCode;
    // ��Ʒ����
    public ItemType itemType;
    // ��Ʒ����
    public Sprite itemSprite;
    // ��Ʒ����
    public string itemDescription;
    // ��Ʒ��ϸ����
    public string itemLongDescription;
    // ��Ʒʹ������뾶
    public short itemUseGridRadio;
    // ��Ʒʹ�ð뾶
    public float itemUseRadio;
    // �Ƿ�Ϊ��ʼ��Ʒ
    public bool isStartingItem;
    // �Ƿ���Լ���
    public bool canBePickedUp;
    // �Ƿ���Զ���
    public bool canBeDropped;
    // �Ƿ���Գ�
    public bool canBeEaten;
    // �Ƿ��������
    public bool canBeCarried;
}
