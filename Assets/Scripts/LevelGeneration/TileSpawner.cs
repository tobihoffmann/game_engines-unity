using System;
using UnityEngine;
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

            private void Start()
            {
                _startPoint = GameObject.FindGameObjectWithTag("Player");
                _distance = Vector2.Distance(transform.position, _startPoint.transform.position);
                _templates = GameObject.FindGameObjectWithTag("RoadTiles").GetComponent<TileTemplates>();
                if (_distance < _maxLvlDistance)
                    Invoke(nameof(Spawn),1f);
            }

            private void Spawn()
            {
                if (!_isSpawned)
                {
                    GameObject tile;
                    switch (roadDirection)
                    {
                        case Direction.North:
                            _random = Random.Range(0, _templates.northTiles.Count);
                            tile = Instantiate(_templates.northTiles[_random], transform.position, Quaternion.identity);
                            if (tile.CompareTag("t_Junction"))
                                RemoveJunctions(true);
                            else if (tile.CompareTag("x_Junction"))
                                RemoveJunctions(false);
                            break;
                        case Direction.East:
                            _random = Random.Range(0, _templates.eastTiles.Count);
                            tile = Instantiate(_templates.eastTiles[_random], transform.position, Quaternion.identity);
                            if (tile.CompareTag("t_Junction"))
                                RemoveJunctions(true);
                            else if (tile.CompareTag("x_Junction"))
                                RemoveJunctions(false);
                            break;
                        case Direction.South:
                            _random = Random.Range(0, _templates.southTiles.Count);
                            tile = Instantiate(_templates.southTiles[_random], transform.position, Quaternion.identity);
                            if (tile.CompareTag("t_Junction"))
                                RemoveJunctions(true);
                            else if (tile.CompareTag("x_Junction"))
                                RemoveJunctions(false);
                            break;
                        case Direction.West:
                            _random = Random.Range(0, _templates.westTiles.Count);
                            tile = Instantiate(_templates.westTiles[_random], transform.position, Quaternion.identity);
                            if (tile.CompareTag("t_Junction"))
                                RemoveJunctions(true);
                            else if (tile.CompareTag("x_Junction"))
                                RemoveJunctions(false);
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

        
            private void RemoveJunctions(bool isTJunction)
            {
                String junctionType = isTJunction ? "t_Junction" : "x_Junction";

                for (int i = 0; i < _templates.northTiles.Count; i++)
                {
                    GameObject tile = _templates.northTiles[i];
                    if (tile.CompareTag(junctionType))
                    {
                        _templates.northTiles.Remove(tile);
                        i--;
                    }
                }
                for (int i = 0; i < _templates.eastTiles.Count; i++)
                {
                    GameObject tile = _templates.eastTiles[i];
                    if (tile.CompareTag(junctionType))
                    {
                        _templates.eastTiles.Remove(tile);
                        i--;
                    }
                }
                for (int i = 0; i < _templates.southTiles.Count; i++)
                {
                    GameObject tile = _templates.southTiles[i];
                    if (tile.CompareTag(junctionType))
                    {
                        _templates.southTiles.Remove(tile);
                        i--;
                    }
                }
                for (int i = 0; i < _templates.westTiles.Count; i++)
                {
                    GameObject tile = _templates.westTiles[i];
                    if (tile.CompareTag(junctionType))
                    {
                        _templates.westTiles.Remove(tile);
                        i--;
                    }
                }
            }
        }
    }

