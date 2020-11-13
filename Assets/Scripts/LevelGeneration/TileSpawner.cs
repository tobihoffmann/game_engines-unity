﻿using System;
using UnityEngine;
using UnityEngine.UIElements;
using Random = UnityEngine.Random;

namespace LevelGeneration
{
    public class TileSpawner : MonoBehaviour
    {
        private enum Direction { North, East, South, West }
        
        /// <summary>
        /// The road opening needed on the new tile to spawn:
        /// North, East, South, West
        /// </summary>
        [SerializeField][Tooltip("road opening needed on the new tile to spawn")]
        private Direction roadDirection;

        private TileTemplates _templates;

        private int _random;

        private bool _isSpawned;
        
        /// <summary>
        /// The road opening needed on the new tile to spawn:
        /// North, East, South, West
        /// </summary>
        [SerializeField][Tooltip("Amount of Tiles the level is going to exist of")]
        private int levelSize;

        private void Start()
        {
            _templates = GameObject.FindGameObjectWithTag("RoadTiles").GetComponent<TileTemplates>();
            Invoke(nameof(Spawn), 0.3f);
        }

        private void Spawn()
        {
            if (!_isSpawned)
            {
                switch (roadDirection)
                {
                    case Direction.North:
                        _random = Random.Range(0, _templates.northTiles.Length);
                        Instantiate(_templates.northTiles[_random], transform.position, Quaternion.identity);
                        break;
                    case Direction.East:
                        _random = Random.Range(0, _templates.eastTiles.Length);
                        Instantiate(_templates.eastTiles[_random], transform.position, Quaternion.identity);
                        break;
                    case Direction.South:
                        _random = Random.Range(0, _templates.southTiles.Length);
                        Instantiate(_templates.southTiles[_random], transform.position, Quaternion.identity);
                        break;
                    case Direction.West:
                        _random = Random.Range(0, _templates.westTiles.Length);
                        Instantiate(_templates.westTiles[_random], transform.position, Quaternion.identity);
                        break;
                    default:
                        Debug.Log("There is no direction set on this spawn point");
                        break;
                }
                _isSpawned = true;
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("SpawnPoint")) Destroy(gameObject);
        }
    }
}
