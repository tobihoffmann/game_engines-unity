using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace LevelGeneration
{
    public class LevelGenerator : MonoBehaviour
    {
        [SerializeField] 
        private int levelWidth, levelHeight;

        [SerializeField] 
        private int tileSize;
        
        [SerializeField] 
        private GameObject startRoad;
        
        [SerializeField]
        private TileTemplates tiles;
        
        private GameObject[,] _levelGrid;

        private int _maxTiles;

        private int _tileCount;

        private Queue<GameObject> q = new Queue<GameObject>();

        private void Start()
        {
            _levelGrid = new GameObject[levelWidth,levelHeight];
            _maxTiles = (levelHeight -4) * (levelWidth -4) / 2;
            GenerateRoad();
            BuildLevel();
            DrawLevelArray();
        }
        
        private void GenerateRoad()
        {
            // Put Level Start in the middle of the grid and enqueue
            int startPosX = levelWidth/2;
            int startPosY = levelHeight/2;
            _levelGrid[startPosX, startPosY] = startRoad;
            _tileCount++;
            startRoad.GetComponent<Road>().X = levelWidth / 2;
            startRoad.GetComponent<Road>().Y = levelHeight / 2;
            q.Enqueue(startRoad);
            
            while (q.Count != 0 && _tileCount < _maxTiles)
            {
                Road road = q.Peek().GetComponent<Road>();
                
                // Apply new Road Tiles if current Tile has respective exits
                if (road.north)
                {
                    AddTile(road.X, road.Y+1);
                }
                if (road.east)
                {
                    AddTile(road.X+1, road.Y);
                }
                if (road.south)
                {
                    AddTile(road.X, road.Y-1);
                }
                if (road.west)
                {
                    AddTile(road.X-1, road.Y);
                }
                q.Dequeue();
            }
            EndRoads();
        }
        
        private void AddTile(int x, int y)
        {
            // In bounds of levelGrid (-2 for level border layer and road endings)
            if (x > 1 && x < levelWidth -2 && y > 1 && y < levelHeight -2 )
            {
                bool foundMatch = false;
                int whileCount = 0;
                while (whileCount < 100)
                {
                    // Roll a random road tile
                    GameObject newRoadTile = tiles.roadTiles[Random.Range(0, tiles.roadTiles.Count)];
                    Road newRoad = newRoadTile.GetComponent<Road>();
                    
                
                    // check if new tile matches with neighbours, else roll again
                    if ((_levelGrid[x, y+1] == null || newRoad.north == _levelGrid[x, y+1].GetComponent<Road>().south) && 
                        (_levelGrid[x+1, y] == null || newRoad.east == _levelGrid[x+1, y].GetComponent<Road>().west) &&
                        (_levelGrid[x, y-1] == null || newRoad.south == _levelGrid[x, y-1].GetComponent<Road>().north) &&
                        (_levelGrid[x-1, y] == null || newRoad.west == _levelGrid[x-1, y].GetComponent<Road>().east))
                    {
                        //TODO: top of scope (only condition)
                        if (_levelGrid[x, y] == null)
                        {   
                            newRoad.X = x;
                            newRoad.Y = y;
                            _levelGrid[x, y] = newRoadTile;
                            _tileCount++;
                            q.Enqueue(newRoadTile);
                            
                            break;
                        }
                    }
                    else
                    {
                        whileCount++;
                    }
                }
               
            }
        }

        private void EndRoads()
        {
            for (int x = 1; x < levelWidth - 1; x++)
            {
                for (int y = 1; y < levelHeight - 1; y++)
                {
                    if (_levelGrid[x,y] == null)
                    {
                        //Check if Road ending matches with Tile above and apply
                        if (_levelGrid[x, y + 1] != null && _levelGrid[x, y + 1].GetComponent<Road>().south)
                        {
                            Debug.Log("Hi");
                            _levelGrid[x, y] = tiles.endingSouth;
                        }
                        //Check if Road ending matches with Tile below and apply
                        else if (_levelGrid[x, y - 1] != null && _levelGrid[x, y - 1].GetComponent<Road>().north)
                        {
                            Debug.Log("Hi");
                            _levelGrid[x, y] = tiles.endingNorth;
                        }
                        //Check if Road ending matches with Tile to the right and apply
                        else if (_levelGrid[x + 1, y] != null && _levelGrid[x + 1, y].GetComponent<Road>().west)
                        {
                            Debug.Log("Hi");
                            _levelGrid[x, y] = tiles.endingWest;
                        }
                        //Check if Road ending matches with Tile to the left and apply
                        else if (_levelGrid[x - 1, y] != null &&_levelGrid[x - 1, y].GetComponent<Road>().east)
                        {
                            Debug.Log("Hi");
                            _levelGrid[x, y] = tiles.endingEast;
                        }
                    }
                }
            }
            
            
        }

        private void BuildLevel()
        {
            for (int x = 0; x < levelWidth; x++)
            {
                for (int y = 0; y < levelHeight; y++)
                {
                    if (_levelGrid[x, y] != null)
                    {
                        Instantiate(_levelGrid[x, y], new Vector3(x * tileSize, y * tileSize, 0), Quaternion.identity);
                    }
                }
            }
        }
        
        private void DrawLevelArray()
        {
            string arrayRepresentation = "";
            for (int y = 0; y < levelHeight; y++)
            {
                for (int x = 0; x < levelWidth; x++)
                {
                    if (_levelGrid[x, y] != null)
                    {
                        arrayRepresentation = arrayRepresentation + "X";
                    }
                    else
                    {
                        arrayRepresentation = arrayRepresentation + "0";
                    }

                    if (x == levelWidth -1)
                    {
                        arrayRepresentation = arrayRepresentation + "\n";
                    }
                }
            }
            Debug.Log(arrayRepresentation + "\n" + "Tiles placed: " + _tileCount  + "\n" + "Max Tiles: " + 
                      _maxTiles);
        }
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        private List<GameObject> GetNeighbours(GameObject road)
        {
            List<GameObject> neighbours = new List<GameObject>();
            Road r = road.GetComponent<Road>();
            
            for (int x = r.X-1; x < r.X+1; x++)
            {
                for (int y = r.Y-1; y < r.Y+1 ; y++)
                {
                    if (!(x == r.X && y == r.Y))
                    {
                        if (_levelGrid[x,y] != null)
                        {
                            neighbours.Add(_levelGrid[x,y]);
                        }
                    }
                }
            }
            return neighbours;
        }

        private bool IsWrongCurve(GameObject tile)
        {
            Road r = tile.GetComponent<Road>();

            foreach (GameObject g in GetNeighbours(tile))
            {
                Road neighbourRoad = g.GetComponent<Road>();
                if (neighbourRoad.north == r.north || neighbourRoad.east == r.east || neighbourRoad.south == r.south ||
                    neighbourRoad.west == r.west)
                    return true;
            }
            return false;
        }
        
        
        
    }
}