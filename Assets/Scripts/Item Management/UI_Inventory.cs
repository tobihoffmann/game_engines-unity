using System;
using CodeMonkey.Utils;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Item_Management
{
    public class UI_Inventory : MonoBehaviour
    {
        private Inventory inventory;
        private Transform itemSlots;
        private Transform itemSlotTemplate;
        private GameObject player;
        
        public void Awake()
        {    
            
            itemSlots = transform.Find("itemSlots");
            itemSlotTemplate = itemSlots.Find("itemSlotTemplate");
            
        }

        public void SetPlayer(GameObject player)
        {
            this.player = player;
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
                // GET EXISTING TEMPLATE
                RectTransform itemSlotRectTransform =
                    Instantiate(itemSlotTemplate, itemSlots).GetComponent<RectTransform>();
                itemSlotRectTransform.gameObject.SetActive(true);
                Debug.Log(itemSlotRectTransform);
                
                // DROP ITEM
                
                var button = itemSlotRectTransform.gameObject.GetComponent<Button>();
                Debug.Log(button);
                button.onClick.AddListener(delegate() {Drop_Item(item);});
                
                //TEST    
                Button buttons = GameObject.FindWithTag("TEST").GetComponent<Button>();
                Debug.Log(buttons);
                buttons.onClick.AddListener(delegate() {tezt();});
                
                // PLACE ITEM WITH CORRECT SPRITE AND POSITION IN UI
                itemSlotRectTransform.anchoredPosition = new Vector2(x * itemSlotCellSize, 0);
                Image image = itemSlotRectTransform.Find("image").GetComponent<Image>();
                image.sprite = item.GetSprite();
                x++;
            }
        }
        
        
        public void tezt(){
            Debug.Log("DROP233333");

        }
        public void Drop_Item(Item item){
            Debug.Log("DROP2");
            inventory.RemoveItem(item);
            ItemWorld.DropItem(player.transform.position ,item);
        }
    }
    


}