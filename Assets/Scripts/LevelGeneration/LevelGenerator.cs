
using LevelGeneration;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    private int _junctionsThree;
    private int _junctionsFour;
    
    private int _maxJunctionsThree;
    private int _maxJunctionsFour;
    

    private TileTemplates _templates;

    private int _random;

    private bool _isSpawned;

    [SerializeField][Tooltip("Entry tile")]
    private GameObject _entryPoint;
        
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
        // spawn point aus dem prefab _entryPoint als variable
        for (int i = 0; i < levelSize; i++)
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
