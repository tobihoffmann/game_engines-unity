using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace LevelGeneration
{
    public class Trash : MonoBehaviour
    {
        /// <summary>
        /// The road opening needed on the new tile to spawn:
        /// North, East, South, West
        /// </summary>
        [SerializeField][Tooltip("Amount of RoadTiles the level is going to exist of")]
        private int levelSize;

        private int _currentLevelSize;

        private TileTemplates _roadTiles;

        private List<GameObject> _queue;
        private List<GameObject> _subQueue;

        private int _random;

        private int _junctionsThree;
        private int _junctionsFour;
        private int _maxJunctionsThree;
        private int _maxJunctionsFour;

        private void Start()
        {
            _roadTiles = gameObject.GetComponent<TileTemplates>();
            GenerateLevel();
        }

        private void GenerateLevel()
        {
            SpawnRoads(gameObject.GetComponent<Tile>());
        }
        
        private void SpawnRoads(Tile roadTile)
        {
            while (_currentLevelSize < levelSize)
            {
                foreach (GameObject sp in roadTile.GETSpawnPoints())
                {
                    TileSpawner spawnPoint = sp.GetComponent<TileSpawner>();
                    
                    if (!spawnPoint.IsSpawned)
                    {
                        switch (spawnPoint.roadDirection)
                        {
                            case TileSpawner.Direction.North:
                                _random = Random.Range(0, _roadTiles.northTiles.Count);
                                _subQueue.Add(Instantiate(_roadTiles.northTiles[_random], sp.transform.position, Quaternion.identity)); 
                                break;
                            case TileSpawner.Direction.East:
                                _random = Random.Range(0, _roadTiles.eastTiles.Count);
                                _subQueue.Add(Instantiate(_roadTiles.eastTiles[_random], sp.transform.position, Quaternion.identity)); 
                                break;
                            case TileSpawner.Direction.South:
                                _random = Random.Range(0, _roadTiles.southTiles.Count);
                                _subQueue.Add(Instantiate(_roadTiles.southTiles[_random], sp.transform.position, Quaternion.identity));
                                break;
                            case TileSpawner.Direction.West:
                                _random = Random.Range(0, _roadTiles.westTiles.Count);
                                _subQueue.Add(Instantiate(_roadTiles.westTiles[_random], sp.transform.position, Quaternion.identity));
                                break;
                            default:
                                Debug.Log("There is no direction set on this spawn point");
                                break;
                        }
                        spawnPoint.IsSpawned = true;
                    }
                    _currentLevelSize++;
                }
            }
        }

        // private void ManageJunction()
        // {
        //     if (newTile.isJunction)
        //     {
        //         if (newTile.isJunctionThree)
        //         {
        //             _junctionsThree++;
        //             if (_junctionsThree >= _maxJunctionsThree)
        //                 removealljunctionsthree();
        //         } else if (newTile.isJunctionFour)
        //         {
        //             _junctionsFour++;
        //             if (_junctionsFour >= _maxJunctionsFour)
        //                 removealljunctionsfour();
        //         }
        //     }
        // }
        //
        //
        //
        // private bool canPlaceJunction()
        // {
        //     // if junctionsOf4 >= maxJunctionsOf4 || junctionsOf3 >= maxJunctionsOf3 || is previous tile a junction
        //     // return false
        //     // else return true
        //     return false;
        // }
    }
}