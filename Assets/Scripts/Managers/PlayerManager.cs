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

        
        protected void Start()
        {
            base.Awake();
            _playerState = player.GetComponent<PlayerState>();
            _playerPosition = player.GetComponent<Transform>().position;
              
            inventory = new Inventory();
            uiInventory.SetInventory(inventory);
            uiInventory.SetPlayer(player);


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

        public Inventory GetInventory()
        {
            return inventory;
        }

    }
}