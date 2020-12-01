using Managers;
using UnityEngine;

namespace Assets.Scripts.Item_Management
{
    public class ItemPickup :MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D collider)
        {
            Inventory inventory = PlayerManager.Instance.GetInventory();
            ItemWorld itemWorld = collider.GetComponent<ItemWorld>();
            if (itemWorld != null && inventory.GetItemList().Count<3)
            {
                inventory.AddItem(itemWorld.GetItem());
                itemWorld.DestroySelf();
            }
            
        }
    }
}