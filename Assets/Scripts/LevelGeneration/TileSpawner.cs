using System;
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
        
        private float _maxLvlDistance = 250f;

        private TileTemplates _templates;

        private int _random;

        private bool _isSpawned;

        private float _distance;

        private GameObject _startPoint;
        
        /// <summary>
        /// The road opening needed on the new tile to spawn:
        /// North, East, South, West
        /// </summary>
        [SerializeField][Tooltip("Amount of Tiles the level is going to exist of")]
        private int levelSize;

        private void Start()
        {
            _startPoint = GameObject.FindGameObjectWithTag("Player");
            _distance = Vector2.Distance(transform.position, _startPoint.transform.position);
            _templates = GameObject.FindGameObjectWithTag("RoadTiles").GetComponent<TileTemplates>();
            if (_distance < _maxLvlDistance)
                Invoke(nameof(Spawn),0.3f);
        }

        private void Spawn()
        {
            if (!_isSpawned)
            {
                switch (roadDirection)
                {
                    case Direction.North:
                        _random = Random.Range(0, _templates.northTiles.Count);
                        if (Instantiate(_templates.northTiles[_random], transform.position, Quaternion.identity).CompareTag("Junction"))
                            RemoveJunctions();
                        break;
                    case Direction.East:
                        _random = Random.Range(0, _templates.eastTiles.Count);
                        if (Instantiate(_templates.eastTiles[_random], transform.position, Quaternion.identity).CompareTag("Junction"))
                            RemoveJunctions();
                        break;
                    case Direction.South:
                        _random = Random.Range(0, _templates.southTiles.Count);
                        if (Instantiate(_templates.southTiles[_random], transform.position, Quaternion.identity).CompareTag("Junction"))
                            RemoveJunctions();
                        break;
                    case Direction.West:
                        _random = Random.Range(0, _templates.westTiles.Count);
                        if (Instantiate(_templates.westTiles[_random], transform.position, Quaternion.identity).CompareTag("Junction"))
                            RemoveJunctions();
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

        
        private void RemoveJunctions()
        {
            for (int i = 0; i < _templates.northTiles.Count; i++)
            {
                GameObject tile = _templates.northTiles[i];
                if (tile.CompareTag("Junction")) _templates.northTiles.Remove(tile);
            }
            for (int i = 0; i < _templates.eastTiles.Count; i++)
            {
                GameObject tile = _templates.eastTiles[i];
                if (tile.CompareTag("Junction")) _templates.eastTiles.Remove(tile);
            }
            for (int i = 0; i < _templates.southTiles.Count; i++)
            {
                GameObject tile = _templates.southTiles[i];
                if (tile.CompareTag("Junction")) _templates.southTiles.Remove(tile);
            }
            for (int i = 0; i < _templates.westTiles.Count; i++)
            {
                GameObject tile = _templates.westTiles[i];
                if (tile.CompareTag("Junction")) _templates.westTiles.Remove(tile);
            }
        }
    }
}
