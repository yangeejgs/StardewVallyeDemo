using UnityEngine;
using UnityEditor;
using System;
using System.Collections.Generic;

[CustomPropertyDrawer(typeof(ItemCodeDescriptionAttribute))]
public class ItemCodeDescriptionDrawer : PropertyDrawer
{
    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        // �����صĸ߶ȸ�Ϊ���������������Ǹ��ӵ�itemPropertyDescription����
        return EditorGUI.GetPropertyHeight(property) * 2;
    }

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        // ʹ�õ��Ǹ������ϵ� BeginProperty/EndProperty  ��ζ��Ԥ����д�߼����������������á�
        EditorGUI.BeginProperty(position, label, property);
        if (property.propertyType == SerializedPropertyType.Integer)
        {
            // ��ʼ������ֵ
            EditorGUI.BeginChangeCheck();
            // ����ԭ�е�ItemCode
            var newValue = EditorGUI.IntField(new Rect(position.x, position.y, position.width, position.height / 2), label, property.intValue);
            // ����ItemDescription [position.y + position.height / 2] y����λ��[position.height / 2] �߶�, GetItemDescription ��ȡ�����ķ���
            EditorGUI.LabelField(new Rect(position.x, position.y + position.height / 2, position.width, position.height / 2), "Item Description", GetItemDescription(property.intValue));
            // �����޸�ֵ��ʱ������Ը���
            if (EditorGUI.EndChangeCheck())
            {
                property.intValue = newValue;
            }
        }
        EditorGUI.EndProperty();
    }

    private string GetItemDescription(int itemCode)
    {
        SOItemList so_itemList;
        so_itemList = AssetDatabase.LoadAssetAtPath("Assets/Scriptable Object Asset/Item/so_itemList.asset", typeof(SOItemList)) as SOItemList;
        List<ItemDetail> itemsDetails = so_itemList.itemsDetails;
        ItemDetail itemDetail = itemsDetails.Find(x => x.itemCode == itemCode);
        if (itemDetail != null)
        {
            return itemDetail.itemDescription;
        }
        return "";
    }
}
