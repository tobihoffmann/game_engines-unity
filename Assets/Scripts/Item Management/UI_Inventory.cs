using Assets.Scripts.Item_Management;
using UnityEngine;
using UnityEngine.UI;

namespace Item_Management
{
    public class UI_Inventory : MonoBehaviour
    {
        private Inventory inventory;
        private Transform itemSlots;
        private Transform itemSlotTemplate;
        private GameObject player;

        private Controls _controls;
        
        public void Awake()
        {
            _controls = new Controls();
            itemSlots = transform.Find("itemSlots");
            itemSlotTemplate = itemSlots.Find("itemSlotTemplate");
        }

        public void Drop_Item(int index)
        {
            Item item = inventory.GetItemList()[index];
            inventory.RemoveItem(item);
            ItemWorld.DropItem(player.transform.position ,item);
        }

        private void OnEnable() =>  _controls.Enable();
        private void OnDisable() => _controls.Disable();
        

        private void Start()
        {
            _controls.Inventory.DropItem_01.performed += _ => Drop_Item(0);
            _controls.Inventory.DropItem_02.performed += _ => Drop_Item(1);
            _controls.Inventory.DropItem_03.performed += _ => Drop_Item(2);
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