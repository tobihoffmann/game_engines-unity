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

        protected override void Awake()
        {
            base.Awake();
            _playerState = player.GetComponent<PlayerState>();
        }

        public GameObject GetPlayer()
        {
            return player;
        }
    }
}