using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

namespace Assets.Scripts.Item_Management
{
    public class UI_Inventory : MonoBehaviour
    {
        private Inventory inventory;
        private Transform itemSlots;
        private Transform itemSlotTemplate;
        private GameObject player;

        [SerializeField] private InputHandler _inputHandler;
        
        public void Awake()
        {
            _inputHandler = new InputHandler();
            itemSlots = transform.Find("itemSlots");
            itemSlotTemplate = itemSlots.Find("itemSlotTemplate");
        }

        public void Drop_Item(int index)
        {
            Item item = inventory.GetItemList()[index];
            inventory.RemoveItem(item);
            ItemWorld.DropItem(player.transform.position ,item);
        }

        private void OnEnable()
        {
            _inputHandler.Enable();
        }
        private void OnDisable()
        {
            _inputHandler.Disable();
        }


        private void Start()
        {
            _inputHandler.DropItem.DropItem.performed += _ => Drop_Item(0);
            _inputHandler.DropItem.DropItem1.performed += _ => Drop_Item(1);
            _inputHandler.DropItem.DropItem2.performed += _ => Drop_Item(2);
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
            float itemSlotCellSize = 85f;

            foreach (Item item in inventory.GetItemList())
            {
                // GET EXISTING TEMPLATE
                RectTransform itemSlotRectTransform =
                    Instantiate(itemSlotTemplate, itemSlots).GetComponent<RectTransform>();
                itemSlotRectTransform.gameObject.SetActive(true);

                // PLACE ITEM WITH CORRECT SPRITE AND POSITION IN UI
                itemSlotRectTransform.anchoredPosition = new Vector2(x * itemSlotCellSize, 0);
                Image image = itemSlotRectTransform.Find("image").GetComponent<Image>();
                image.sprite = item.GetSprite();
                x++;
            }
        }
        
        
    }

}