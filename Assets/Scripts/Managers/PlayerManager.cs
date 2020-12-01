using Assets.Scripts.Item_Management;
using Entity.Player;
using Interfaces;
using UnityEngine;

namespace Managers
{
    public class PlayerManager : Singleton<PlayerManager>
    {
        [SerializeField]
        private GameObject player;
        
        private PlayerState _playerState;
        
        private Vector3 _playerPosition;
        
        [SerializeField] private UI_Inventory  uiInventory;
        
        private Inventory inventory;

        
        protected override void Awake()
        {
            base.Awake();
            
            _playerState = player.GetComponent<PlayerState>();
            _playerPosition = player.GetComponent<Transform>().position;
            
            inventory = new Inventory();
            uiInventory.SetInventory(inventory);

            ItemWorld.SpawnItemWorld(new Vector3(10, 10), new Item {itemType = Item.ItemType.JuggernautBuff, amount = 1});
            ItemWorld.SpawnItemWorld(new Vector3(5, 5), new Item {itemType = Item.ItemType.JuggernautBuff, amount = 1});
            
        }
        
        
        private void OnTriggerEnter2D(Collider2D collider)
        {
            Debug.Log("SECONDARY TRIGGER");
            ItemWorld itemWorld = collider.GetComponent<ItemWorld>();
            if (itemWorld != null)
            {
                inventory.AddItem(itemWorld.GetItem());
                itemWorld.DestroySelf();
            }
            
        }

        /// <summary>
        /// Returns the current player position as a Vector3
        /// </summary>
        public Vector3 GetPlayerPosition()
        {
            return _playerPosition;
        }
        
        /// <summary>
        /// Returns the PlayerState to access Damaging, Healing, etc
        /// </summary>
        public PlayerState GetPlayerState()
        {
            return _playerState;
        }
        
        /// <summary>
        /// Returns the GameObject of the player for collision detection, etc
        /// </summary>
        public GameObject GetPlayer()
        {
            return player;
        }

    }
}