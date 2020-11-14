using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    
    // Performance Improvement: we could make the as serializable Property and drag every spawnpoint into it manually
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
