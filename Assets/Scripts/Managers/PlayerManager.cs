﻿using Entity.Player;
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

        protected override void Awake()
        {
            base.Awake();
            _playerState = player.GetComponent<PlayerState>();
            _playerPosition = player.GetComponent<Transform>().position;
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

        public GameObject GetPlayer()
        {
            return player;
        }
    }
}