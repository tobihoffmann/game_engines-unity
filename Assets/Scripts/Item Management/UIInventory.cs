using Assets.Scripts.Item_Management;
using Managers;
using UnityEngine;
using UnityEngine.UI;

namespace Item_Management
{
    public class UIInventory : MonoBehaviour
    {
        private Inventory inventory;
        [SerializeField]
        private Transform itemSlots;
        [SerializeField]
        private Transform itemSlotTemplate;

        private Controls _controls;
        
        public void Awake() => _controls = new Controls();

        private void OnEnable() =>  _controls.Enable();
        private void OnDisable() => _controls.Disable();

        private void Start()
        {
            _controls.Inventory.DropItem_01.performed += _ => DropItem(0);
            _controls.Inventory.DropItem_02.performed += _ => DropItem(1);
            _controls.Inventory.DropItem_03.performed += _ => DropItem(2);
        }

        private void DropItem(int index)
        {
            Item item = inventory.GetItemList()[index];
            inventory.RemoveItem(item);
            ItemWorld.DropItem(PlayerManager.Instance.GetPlayerPosition(), item);
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
            float x = 4f;
            float itemSlotCellSize = 37f;

            foreach (Item item in inventory.GetItemList())
            {
                // GET EXISTING TEMPLATE
                RectTransform itemSlotRectTransform =
                    Instantiate(itemSlotTemplate, itemSlots).GetComponent<RectTransform>();
                itemSlotRectTransform.gameObject.SetActive(true);

                // PLACE ITEM WITH CORRECT SPRITE AND POSITION IN UI
                itemSlotRectTransform.anchoredPosition = new Vector2((x * itemSlotCellSize) + 107, -76);
                Image image = itemSlotRectTransform.Find("image").GetComponent<Image>();
                image.sprite = item.GetIconSprite();
                x++;
            }
        }
        
        
    }

}