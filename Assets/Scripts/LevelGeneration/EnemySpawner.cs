using System.Collections.Generic;
using Managers;
using UnityEngine;
using Random = UnityEngine.Random;

namespace LevelGeneration
{
    public class EnemySpawner : MonoBehaviour
    {
        //this list holds all potential spawnpoints.
        internal readonly List<SpawnZone> SpawnZones = new List<SpawnZone>();
        
        //this list holds all the spawnPoints where enemies already spawned.
        private List<Vector2> _spawnZonesWithEnemies = new List<Vector2>();

        [SerializeField][Tooltip("How many enemies should be spawned in one spawn Point?")]
        private int enemiesPerSpawnPoint;
        
        [SerializeField]
        private int minDistanceBetweenActiveSpawnPoints;
        

        [SerializeField]
        private GameObject enemyToSpawn;
        
        private void Start()
        {
            Invoke(nameof(SpawnEnemies),0.5f);
        }
        
        private void SpawnEnemies()
        {
            int whileCount = 0;
            Debug.Log(PlayerManager.Instance.GetPlayerPosition());
            _spawnZonesWithEnemies.Add(PlayerManager.Instance.GetPlayerPosition());
            while (whileCount < 100)
            {
                int enemyCount = 0;
                SpawnZone firstSpawn = SpawnZones[Random.Range(0, SpawnZones.Count)];
                Vector2 spawnPos = firstSpawn.transform.position;
                    
                float distance = int.MaxValue;
                
                foreach (Vector2 pos in _spawnZonesWithEnemies)
                {
                    float tempDist = Vector2.Distance(pos, spawnPos);
                    if (tempDist < distance)
                    {
                        distance = tempDist;
                    }
                }
                if (distance > minDistanceBetweenActiveSpawnPoints)
                {
                    while (enemyCount < enemiesPerSpawnPoint)
                    {
                        Vector2 randomPositionInSphere = spawnPos + Random.insideUnitCircle * firstSpawn.SpawnRadius;
                        Instantiate(enemyToSpawn, randomPositionInSphere, Quaternion.identity);
                        enemyCount++;
                    }
                    //spawnPointCount++;
                    _spawnZonesWithEnemies.Add(spawnPos);
                }
                else
                {
                    whileCount++;
                }
            }
        }
    }
}