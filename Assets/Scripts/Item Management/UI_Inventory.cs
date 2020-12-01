using System;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Item_Management
{
    public class UI_Inventory : MonoBehaviour
    {
        private Inventory inventory;
        private Transform itemSlots;
        private Transform itemSlotTemplate;

        public void Awake()
        {
            itemSlots = transform.Find("itemSlots");
            itemSlotTemplate = itemSlots.Find("itemSlotTemplate");
        }
        
        public void SetInventory(Inventory inv)
        {
            inventory = inv;
            inventory.OnItemListChanged += Inventory_OnItemListChanged;
            RefreshInventoryItems();
        }

        private void Inventory_OnItemListChanged(object sender, System.EventArgs e)
        {
            RefreshInventoryItems();
        }
        
        private void RefreshInventoryItems()
        {
            foreach (Transform child in itemSlots)
            {
                if (child == itemSlotTemplate) continue;
                Destroy(child.gameObject);
            }
            float x = 0.5f;
            float itemSlotCellSize = 80f;

            foreach (Item item in inventory.GetItemList())
            {
                RectTransform itemSlotRectTransform =
                    Instantiate(itemSlotTemplate, itemSlots).GetComponent<RectTransform>();
                    itemSlotRectTransform.gameObject.SetActive(true);
                    itemSlotRectTransform.anchoredPosition = new Vector2(x * itemSlotCellSize, 0);
                    Image image = itemSlotRectTransform.Find("image").GetComponent<Image>();
                    image.sprite = item.GetSprite();
                    x++;
            }
        }

    }


}