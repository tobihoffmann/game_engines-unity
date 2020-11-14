using UnityEngine;

namespace LevelGeneration
{
    public class LevelGenerator : MonoBehaviour
    {
        /// <summary>
        /// The road opening needed on the new tile to spawn:
        /// North, East, South, West
        /// </summary>
        [SerializeField][Tooltip("Amount of RoadTiles the level is going to exist of")]
        private int levelSize;

        private int _currentLevelSize;
    
        [SerializeField][Tooltip("Entry tile")]
        private GameObject _entryPoint;

        private RoadTiles _roadTiles;

        private int _random;

        private int _junctionsThree;
        private int _junctionsFour;
        private int _maxJunctionsThree;
        private int _maxJunctionsFour;

        private void Start()
        {
            _roadTiles = gameObject.GetComponent<RoadTiles>();

            Invoke(nameof(SpawnRoads), 0.3f);
        }

        private void GenerateLevel()
        {
            Invoke(nameof(SpawnRoads), 0.3f);
        }
        private void SpawnRoads(Tile roadTile)
        {
            while (_currentLevelSize < levelSize)
            {
                foreach (GameObject sp in roadTile.getSpawnPoints())
                {
                    SpawnRoad(sp.GetComponent<SpawnPoint>());
                }
                _currentLevelSize++;
            }
        }

        private void SpawnRoad(SpawnPoint spawnPoint)
        {
            if (!spawnPoint.isSpawned)
            {
                switch (spawnPoint.roadDirection)
                {
                    case SpawnPoint.Direction.North:
                        _random = Random.Range(0, _roadTiles.northTiles.Length);
                        Instantiate(_roadTiles.northTiles[_random], transform.position, Quaternion.identity);
                        break;
                    case SpawnPoint.Direction.East:
                        _random = Random.Range(0, _roadTiles.eastTiles.Length);
                        Instantiate(_roadTiles.eastTiles[_random], transform.position, Quaternion.identity);
                        break;
                    case SpawnPoint.Direction.South:
                        _random = Random.Range(0, _roadTiles.southTiles.Length);
                        Instantiate(_roadTiles.southTiles[_random], transform.position, Quaternion.identity);
                        break;
                    case SpawnPoint.Direction.West:
                        _random = Random.Range(0, _roadTiles.westTiles.Length);
                        Instantiate(_roadTiles.westTiles[_random], transform.position, Quaternion.identity);
                        break;
                    default:
                        Debug.Log("There is no direction set on this spawn point");
                        break;
                }
                spawnPoint.isSpawned = true;
            }
        }
    
        private void ManageJunction()
        {
            if (newTile.isJunction)
            {
                if (newTile.isJunctionThree)
                {
                    _junctionsThree++;
                    if (_junctionsThree >= _maxJunctionsThree)
                        removealljunctionsthree();
                } else if (newTile.isJunctionFour)
                {
                    _junctionsFour++;
                    if (_junctionsFour >= _maxJunctionsFour)
                        removealljunctionsfour();
                }
            }
        }
    
    

        private bool canPlaceJunction()
        {
            // if junctionsOf4 >= maxJunctionsOf4 || junctionsOf3 >= maxJunctionsOf3 || is previous tile a junction
            // return false
            // else return true
            return false;
        }
    }
}
