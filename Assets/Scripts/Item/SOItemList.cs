
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "so_itemList", menuName = "Scriptable Object/Item/Item List")]
public class SOItemList : ScriptableObject
{
    [SerializeField]
    public List<ItemDetail> itemsDetails;

}
