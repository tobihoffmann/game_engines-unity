using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace LevelGeneration
{
    public class LevelGenerator : MonoBehaviour
    {
        [SerializeField] private int levelWidth, levelHeight;

        [SerializeField] private int tileSize;

        [SerializeField] private GameObject startRoad;

        [SerializeField] private TileTemplates tiles;

        private GameObject[,] _levelGrid;

        private int _maxTiles;
        private int _tileCount;
        
        [SerializeField]
        private int maxJunctions;

        private int _junctionCount;

        private Queue<GameObject> q = new Queue<GameObject>();

        private void Start()
        {
            _junctionCount = 0;
            _levelGrid = new GameObject[levelWidth, levelHeight];
            _maxTiles = (levelHeight - 4) * (levelWidth - 4) / 2;
            GenerateRoad();
            BuildLevel();
            DrawLevelArray();
        }

        private void GenerateRoad()
        {
            // Put Level Start in the middle of the grid and enqueue
            int startPosX = levelWidth / 2;
            int startPosY = levelHeight / 2;
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
                    AddTile(road.X, road.Y + 1);
                }

                if (road.east)
                {
                    AddTile(road.X + 1, road.Y);
                }

                if (road.south)
                {
                    AddTile(road.X, road.Y - 1);
                }

                if (road.west)
                {
                    AddTile(road.X - 1, road.Y);
                }

                q.Dequeue();
            }

            EndRoads();
            AddPlanes();
        }

        private void AddTile(int x, int y)
        {
            // In bounds of levelGrid (-2 for level border layer and road endings)
            if (_levelGrid[x, y] == null && x > 1 && x < levelWidth - 2 && y > 1 && y < levelHeight - 2)
            {
                int whileCount = 0;
                while (whileCount < 1000)
                {
                    // Roll a random road tile
                    GameObject newRoadTile = tiles.roadTiles[Random.Range(0, tiles.roadTiles.Count)];
                    Road newRoad = newRoadTile.GetComponent<Road>();

                    // check if new tile matches with neighbours, else roll again
                    if ((_levelGrid[x, y + 1] == null || newRoad.north == _levelGrid[x, y + 1].GetComponent<Road>().south) &&
                        (_levelGrid[x + 1, y] == null || newRoad.east == _levelGrid[x + 1, y].GetComponent<Road>().west) &&
                        (_levelGrid[x, y - 1] == null || newRoad.south == _levelGrid[x, y - 1].GetComponent<Road>().north) &&
                        (_levelGrid[x - 1, y] == null || newRoad.west == _levelGrid[x - 1, y].GetComponent<Road>().east))
                    {
                        // Rule: A Junction is not adjacent to another Junction
                        /*if (newRoad.isJunction && (IsNeighbourAJunction(x,y,true) || _junctionCount >= maxJunctions))
                        {
                            whileCount++;
                        }*/
                        
                        //Rule: A Junction must have at least 5 tiles between another Junction
                        if (newRoad.isJunction && (IsJunctionInRadius(x,y,5) ))
                        {
                            whileCount++;
                        }
                        // Rule: A Curve is not adjacent to a Junction
                        else if (newRoad.isCurve && IsNeighbourAJunction(x,y,false))
                        {
                            whileCount++;
                        }
                        // Rule: A curve cant make a direct U-Turn
                        else if (IsUTurn(x,y,newRoad))
                        {
                            whileCount++;
                        }
                        else
                        {
                            if (newRoad.isJunction) _junctionCount++;
                            newRoad.X = x;
                            newRoad.Y = y;
                            _levelGrid[x, y] = newRoadTile;
                            _tileCount++;
                            q.Enqueue(newRoadTile);
                            break;
                        }
                    }
                    whileCount++;
                }
            }
        }

        private void EndRoads()
        {
            for (int x = 1; x < levelWidth - 1; x++)
            {
                for (int y = 1; y < levelHeight - 1; y++)
                {
                    if (_levelGrid[x, y] == null)
                    {
                        //Check if Road ending matches with Tile above and apply
                        if (_levelGrid[x, y + 1] != null && _levelGrid[x, y + 1].GetComponent<Road>().south)
                        {
                            _levelGrid[x, y] = tiles.endingSouth;
                        }
                        //Check if Road ending matches with Tile below and apply
                        else if (_levelGrid[x, y - 1] != null && _levelGrid[x, y - 1].GetComponent<Road>().north)
                        {
                            _levelGrid[x, y] = tiles.endingNorth;
                        }
                        //Check if Road ending matches with Tile to the right and apply
                        else if (_levelGrid[x + 1, y] != null && _levelGrid[x + 1, y].GetComponent<Road>().west)
                        {
                            _levelGrid[x, y] = tiles.endingWest;
                        }
                        //Check if Road ending matches with Tile to the left and apply
                        else if (_levelGrid[x - 1, y] != null && _levelGrid[x - 1, y].GetComponent<Road>().east)
                        {
                            _levelGrid[x, y] = tiles.endingEast;
                        }
                    }
                }
            }
        }

        private void AddPlanes()
        {
            for (int x = 1; x < levelWidth - 1; x++)
            {
                for (int y = 1; y < levelHeight - 1; y++)
                {
                    if (_levelGrid[x, y] == null)
                    {
                        int adjacentTiles = 0;
                        if (_levelGrid[x + 1, y] != null) adjacentTiles++;
                        if (_levelGrid[x - 1, y] != null) adjacentTiles++;
                        if (_levelGrid[x, y + 1] != null) adjacentTiles++;
                        if (_levelGrid[x, y - 1] != null) adjacentTiles++;
                        if (adjacentTiles >= 3)
                        {
                            _levelGrid[x, y] = tiles.planeTiles[Random.Range(0, tiles.planeTiles.Count)];
                        }
                    }
                }
            }
            
        }    

        private bool IsNeighbourAJunction(int tilePosX, int tilePosY, bool checkDiagonals)
        {
            for (int x = tilePosX - 1; x < tilePosX + 2; x++)
            {
                for (int y = tilePosY - 1; y < tilePosY + 2; y++)
                {
                    if (x >= 0 && y >= 0 && x <= levelWidth - 1 && y <= levelHeight - 1)
                    {
                        if (checkDiagonals)
                        {
                            if (x == tilePosX - 1 && y == tilePosY || x == tilePosX && y == tilePosY - 1 || x == tilePosX && y == tilePosY + 1 || x == tilePosX + 1 && y == tilePosY)
                            {
                                if (!(x == tilePosX && y == tilePosY) && _levelGrid[x, y] != null && _levelGrid[x, y].GetComponent<Road>().isJunction) return true;
                            }
                        }
                        else if (!(x == tilePosX && y == tilePosY) && _levelGrid[x, y] != null && _levelGrid[x, y].GetComponent<Road>().isJunction) return true;
                    }
                }
            }
            return false;
        }
        
        private bool IsJunctionInRadius(int tilePosX, int tilePosY, int radius)
        {
            for (int x = tilePosX - radius; x < tilePosX + radius + 1; x++)
            {
                for (int y = tilePosY - radius; y < tilePosY + radius + 1; y++)
                {
                    if (x >= 0 && y >= 0 && x <= levelWidth - 1 && y <= levelHeight - 1)
                    {
                        if (!(x == tilePosX && y == tilePosY) && _levelGrid[x, y] != null && _levelGrid[x, y].GetComponent<Road>().isJunction) return true;
                    }
                }
            }
            return false;
        }

        private bool IsUTurn(int tilePosX, int tilePosY, Road newRoad)
        {
            if (newRoad.isCurve)
            {
                if (_levelGrid[tilePosX, tilePosY+1] != null)
                {
                    if (_levelGrid[tilePosX, tilePosY+1].GetComponent<Road>().isCurve)
                    {
                        if (newRoad.west && _levelGrid[tilePosX, tilePosY+1].GetComponent<Road>().west ||
                            newRoad.east && _levelGrid[tilePosX, tilePosY+1].GetComponent<Road>().east)
                        {
                            return true;
                        }
                    }
                }
                if (_levelGrid[tilePosX+1, tilePosY] != null)
                {
                    if (_levelGrid[tilePosX+1, tilePosY].GetComponent<Road>().isCurve)
                    {
                        if (newRoad.north && _levelGrid[tilePosX+1, tilePosY].GetComponent<Road>().north ||
                            newRoad.south && _levelGrid[tilePosX+1, tilePosY].GetComponent<Road>().south)
                        {
                            return true;
                        }
                    }
                }
                if (_levelGrid[tilePosX, tilePosY-1] != null)
                {
                    if (_levelGrid[tilePosX, tilePosY-1].GetComponent<Road>().isCurve)
                    {
                        if (newRoad.west && _levelGrid[tilePosX, tilePosY-1].GetComponent<Road>().west ||
                            newRoad.east && _levelGrid[tilePosX, tilePosY-1].GetComponent<Road>().east)
                        {
                            return true;
                        }
                    }
                }
                if (_levelGrid[tilePosX-1, tilePosY] != null)
                {
                    if (_levelGrid[tilePosX-1, tilePosY].GetComponent<Road>().isCurve)
                    {
                        if (newRoad.north && _levelGrid[tilePosX-1, tilePosY].GetComponent<Road>().north ||
                            newRoad.south && _levelGrid[tilePosX-1, tilePosY].GetComponent<Road>().south)
                        {
                            return true;
                        }
                    }
                }
            }

            return false;
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

                        if (x == levelWidth - 1)
                        {
                            arrayRepresentation = arrayRepresentation + "\n";
                        }
                    }
                }

                Debug.Log(arrayRepresentation + "\n" + "Tiles placed: " + _tileCount + "\n" + "Max Tiles: " +
                          _maxTiles);
            }

            private List<GameObject> GetNeighbours(GameObject road)
            {
                List<GameObject> neighbours = new List<GameObject>();
                Road r = road.GetComponent<Road>();

                for (int x = r.X - 1; x < r.X + 2; x++)
                {
                    for (int y = r.Y - 1; y < r.Y + 2; y++)
                    {
                        if (!(x == r.X && y == r.Y))
                        {
                            if (_levelGrid[x, y] != null)
                            {
                                neighbours.Add(_levelGrid[x, y]);
                            }
                        }
                    }
                }
                return neighbours;
            }
    }
}