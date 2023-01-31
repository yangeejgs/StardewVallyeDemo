using UnityEngine;

public class ItemPickUp : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Item item = collision.GetComponent<Item>();
        if (item != null)
        {
            // ���ݴ�������Ʒ��Item�ű��е� ItemCode ͨ�� InventoryManager ��ȡ��Ʒ����
            ItemDetail itemDetail = InventoryManager.Instance.GetItemDetails(item.ItemCode);

            // ������Ʒʱ���ü�����Ʒ����
            if (itemDetail.canBePickedUp)
            {
                InventoryManager.Instance.AddItem((int)InventoryLocation.player, item, collision.gameObject);
            }
        }
    }
}