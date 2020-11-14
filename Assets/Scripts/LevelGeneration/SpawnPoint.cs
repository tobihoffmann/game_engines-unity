using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using Random = UnityEngine.Random;

namespace LevelGeneration
{
    public class SpawnPoint : MonoBehaviour
    {
        public enum Direction { North, East, South, West }
        
        /// <summary>
        /// The road opening needed on the new tile to spawn:
        /// North, East, South, West
        /// </summary>
        [SerializeField][Tooltip("road opening needed on the new tile to spawn")]
        public Direction roadDirection;
        
        public bool isSpawned;
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("SpawnPoint")) Destroy(gameObject);
        }
        
        public List<GameObject> getSpawnPoints()
        {
            List<GameObject> spawnPoints = new List<GameObject>();
            
            foreach (Transform child in transform)
            {
                if (child.CompareTag("SpawnPoint")) spawnPoints.Add(child.gameObject);
            }

            return spawnPoints;
        }
    }
}
