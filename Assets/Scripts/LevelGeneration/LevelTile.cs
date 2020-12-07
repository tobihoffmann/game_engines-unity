using System;
using UnityEngine;


namespace LevelGeneration
{
    public class LevelTile : MonoBehaviour
    {
        internal enum Type {Road, Plane, Border};
        
        [SerializeField]
        internal Type type;

        private SpawnZone _spawnZone;
        private void Awake()
        {
            _spawnZone = GetComponentInChildren<SpawnZone>();
        }
    }
}
