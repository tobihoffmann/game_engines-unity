using Assets.Scripts.Item_Management;
using System;
using Entity.Player;
using Interfaces;
using Item_Management;
using UnityEngine;

namespace Managers
{
    public class PlayerManager : Singleton<PlayerManager>
    {
        [SerializeField]
        private GameObject player;
        
        private PlayerState _playerState;
        
        private Vector3 _playerPosition;
        
        [SerializeField] private UIInventory  uiInventory;
        
        private Inventory inventory;

        private Transform _playerTransform;

        protected override void Awake()
        {
            base.Awake();
            _playerState = player.GetComponent<PlayerState>();
            _playerTransform = player.GetComponent<Transform>();
            
            inventory = new Inventory();
            uiInventory.SetInventory(inventory);
        }
        
        private void Update()
        {
            _playerPosition = _playerTransform.position;
        }

        /// <summary>
        /// Returns the current player position as a Vector3
        /// </summary>
        public Vector3 GetPlayerPosition()
        {
            return _playerPosition;
        }
        
        /// <summary>
        /// Sets the player position as a Vector3
        /// </summary>
        public void SetPlayerPosition(float x, float y)
        {
            player.GetComponent<Transform>().position = new Vector3(x, y, 0);
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