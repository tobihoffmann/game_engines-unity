using System;
using UnityEngine;
using UnityEngine.UIElements;
using Random = UnityEngine.Random;

namespace LevelGeneration
{
    public class TileSpawner : MonoBehaviour
    {
        public enum Direction { North, East, South, West }
        
        /// <summary>
        /// The road opening needed on the new tile to spawn:
        /// North, East, South, West
        /// </summary>
        [SerializeField][Tooltip("road opening needed on the new tile to spawn")]
        public Direction roadDirection;
        

        private bool _isSpawned;
        
        /// <summary>
        /// The road opening needed on the new tile to spawn:
        /// North, East, South, West
        /// </summary>
        [SerializeField][Tooltip("Amount of Tiles the level is going to exist of")]
        private int levelSize;
        

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("SpawnPoint")) Destroy(gameObject);
        }
    }
}
