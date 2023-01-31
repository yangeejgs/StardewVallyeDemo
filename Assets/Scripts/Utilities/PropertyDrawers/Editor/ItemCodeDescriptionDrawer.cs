using UnityEngine;
using UnityEditor;
using System;
using System.Collections.Generic;

[CustomPropertyDrawer(typeof(ItemCodeDescriptionAttribute))]
public class ItemCodeDescriptionDrawer : PropertyDrawer
{
    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        // 将返回的高度改为两倍，以满足我们附加的itemPropertyDescription属性
        return EditorGUI.GetPropertyHeight(property) * 2;
    }

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        // 使用的是父属性上的 BeginProperty/EndProperty  意味着预制重写逻辑对整个属性起作用。
        EditorGUI.BeginProperty(position, label, property);
        if (property.propertyType == SerializedPropertyType.Integer)
        {
            // 开始检测更改值
            EditorGUI.BeginChangeCheck();
            // 绘制原有的ItemCode
            var newValue = EditorGUI.IntField(new Rect(position.x, position.y, position.width, position.height / 2), label, property.intValue);
            // 绘制ItemDescription [position.y + position.height / 2] y方向定位，[position.height / 2] 高度, GetItemDescription 获取描述的方法
            EditorGUI.LabelField(new Rect(position.x, position.y + position.height / 2, position.width, position.height / 2), "Item Description", GetItemDescription(property.intValue));
            // 结束修改值的时候给属性复制
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
