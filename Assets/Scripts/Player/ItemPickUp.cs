using UnityEngine;

public class ItemPickUp : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Item item = collision.GetComponent<Item>();
        if (item != null)
        {
            // 根据触碰的物品上Item脚本中的 ItemCode 通过 InventoryManager 获取物品详情
            ItemDetail itemDetail = InventoryManager.Instance.GetItemDetails(item.ItemCode);

            // 捡起物品时调用捡起物品方法
            if (itemDetail.canBePickedUp)
            {
                InventoryManager.Instance.AddItem((int)InventoryLocation.player, item, collision.gameObject);
            }
        }
    }
}