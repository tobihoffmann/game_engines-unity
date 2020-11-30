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

        private Queue<GameObject> q = new Queue<GameObject>();

        private void Start()
        {
            _levelGrid = new GameObject[levelWidth, levelHeight];
            _maxTiles = (levelHeight - 4) * (levelWidth - 4) / 2;
            GenerateRoad();
            AddPlanes();
            AddBorders();
            AddBorderCorners();
            BuildLevel();
            DrawLevelArray();
            _levelGrid = new GameObject[0,0];
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
            AddRoadEnd();
        }

        private void AddTile(int x, int y)
        {
            // In bounds of levelGrid (-2 for level border layer and road endings)
            if (_levelGrid[x, y] == null && x > 2 && x < levelWidth - 3 && y > 2 && y < levelHeight - 3)
            {
                // bool foundMatch = false; -> If everything is cool we can use this as while condition
                int whileCount = 0;
                while (whileCount < 100)
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

        private void AddRoadEnd()
        {
            for (int x = 2; x < levelWidth - 2; x++)
            {
                for (int y = 2; y < levelHeight - 2; y++)
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
            for (int x = 2; x < levelWidth - 2; x++)
            {
                for (int y = 2; y < levelHeight - 2; y++)
                {
                    if (_levelGrid[x, y] == null)
                    {
                        // if grid cell to the west and to the east or to the north and to the south has a tile place plane
                        if (_levelGrid[x + 1, y] != null && _levelGrid[x - 1, y] != null || _levelGrid[x, y + 1] != null && _levelGrid[x, y - 1] != null)
                        {
                            _levelGrid[x, y] = tiles.planeTiles[Random.Range(0, tiles.planeTiles.Count)];
                        }
                    }
                }
            }
            // 2nd rotation to catch empty fields within the level space
            for (int x = 2; x < levelWidth - 2; x++)
            {
                for (int y = 2; y < levelHeight - 2; y++)
                {
                    if (_levelGrid[x, y] == null)
                    {
                        // if all neighbours have a tile place plane
                        if (_levelGrid[x + 1, y] != null && _levelGrid[x - 1, y] != null && _levelGrid[x, y + 1] != null && _levelGrid[x, y - 1] != null)
                        {
                            _levelGrid[x, y] = tiles.planeTiles[Random.Range(0, tiles.planeTiles.Count)];
                        }
                    }
                }
            }
        }

        private void AddBorders()
        {
            
            for (int x = 1; x < levelWidth - 1; x++)
            {
                for (int y = 1; y < levelHeight -1 ; y++)
                {
                    // Check if Grid Cell is empty
                    if (_levelGrid[x, y] == null)
                    {
                        // Check if grid cell has a tile on any neighbor
                        if (CountNeighbours(x,y) > 0)
                        {
                            for (int i = 0; i < tiles.borders.Count; i++)
                            {
                                GameObject newBorderTile = tiles.borders[i];
                                Border newBorder = newBorderTile.GetComponent<Border>();

                                // Does selected tile match with Grid cell in the north?
                                if (_levelGrid[x, y + 1] == null && !newBorder.north ||
                                    _levelGrid[x, y + 1] != null && (_levelGrid[x, y + 1].GetComponent<LevelTile>().type == LevelTile.Type.Plane || _levelGrid[x, y + 1].GetComponent<LevelTile>().type == LevelTile.Type.Road) && newBorder.north ||
                                    _levelGrid[x, y + 1] != null && _levelGrid[x, y + 1].GetComponent<LevelTile>().type == LevelTile.Type.Border && newBorder.north == _levelGrid[x, y + 1].GetComponent<Border>().south)
                                {
                                    // Does selected tile match with Grid cell in the south?
                                    if (_levelGrid[x, y - 1] == null && !newBorder.south ||
                                        _levelGrid[x, y - 1] != null && (_levelGrid[x, y - 1].GetComponent<LevelTile>().type == LevelTile.Type.Plane || _levelGrid[x, y - 1].GetComponent<LevelTile>().type == LevelTile.Type.Road) && newBorder.south ||
                                        _levelGrid[x, y - 1] != null && _levelGrid[x, y - 1].GetComponent<LevelTile>().type == LevelTile.Type.Border && newBorder.south == _levelGrid[x, y - 1].GetComponent<Border>().north)
                                    {
                                        // Does selected tile match with Grid cell in the west?
                                        if (_levelGrid[x + 1, y] == null && !newBorder.east ||
                                            _levelGrid[x + 1, y] != null && (_levelGrid[x + 1, y].GetComponent<LevelTile>().type == LevelTile.Type.Plane || _levelGrid[x + 1, y].GetComponent<LevelTile>().type == LevelTile.Type.Road) && newBorder.east ||
                                            _levelGrid[x + 1, y] != null && _levelGrid[x + 1, y].GetComponent<LevelTile>().type == LevelTile.Type.Border && newBorder.east == _levelGrid[x + 1, y].GetComponent<Border>().west)
                                        {
                                            // Does selected tile match with Grid cell in the east?
                                            if (_levelGrid[x - 1, y] == null && !newBorder.west ||
                                                _levelGrid[x - 1, y] != null && (_levelGrid[x - 1, y].GetComponent<LevelTile>().type == LevelTile.Type.Plane || _levelGrid[x -1, y].GetComponent<LevelTile>().type == LevelTile.Type.Road) && newBorder.west ||
                                                _levelGrid[x - 1, y] != null && _levelGrid[x - 1, y].GetComponent<LevelTile>().type == LevelTile.Type.Border && newBorder.west == _levelGrid[x - 1, y].GetComponent<Border>().east)
                                            {
                                                _levelGrid[x, y] = newBorderTile;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
        
        private void AddBorderCorners()
        {
            for (int x = 1; x < levelWidth - 1; x++)
            {
                for (int y = 1; y < levelHeight - 1; y++)
                {
                    if (_levelGrid[x, y] == null && CountNeighbours(x,y) >= 2)
                    {
                        //Check if
                        if (_levelGrid[x, y + 1] != null &&
                            _levelGrid[x + 1, y] != null &&
                            _levelGrid[x, y + 1].GetComponent<Border>() != null &&
                            _levelGrid[x + 1, y].GetComponent<Border>() != null &&
                            !_levelGrid[x, y + 1].GetComponent<Border>().isCorner &&
                            !_levelGrid[x + 1, y].GetComponent<Border>().isCorner &&
                            _levelGrid[x, y + 1].GetComponent<Border>().east &&
                            _levelGrid[x + 1, y].GetComponent<Border>().north)
                        {
                            _levelGrid[x, y] = tiles.borderCornerSouthWest;
                        }
                        //Check if
                        else if (_levelGrid[x, y - 1] != null &&
                                 _levelGrid[x + 1, y] != null &&
                                 _levelGrid[x, y - 1].GetComponent<Border>() != null &&
                                 _levelGrid[x + 1, y].GetComponent<Border>() != null &&
                                 !_levelGrid[x, y - 1].GetComponent<Border>().isCorner &&
                                 !_levelGrid[x + 1, y].GetComponent<Border>().isCorner &&
                                 _levelGrid[x, y - 1].GetComponent<Border>().east &&
                                 _levelGrid[x + 1, y].GetComponent<Border>().south)
                        {
                            _levelGrid[x, y] = tiles.borderCornerNorthWest;
                        }
                        //Check if
                        else if (_levelGrid[x, y + 1] != null &&
                                 _levelGrid[x - 1, y] != null &&
                                 _levelGrid[x, y + 1].GetComponent<Border>() != null &&
                                 _levelGrid[x - 1, y].GetComponent<Border>() != null &&
                                 !_levelGrid[x, y + 1].GetComponent<Border>().isCorner &&
                                 !_levelGrid[x - 1, y].GetComponent<Border>().isCorner &&
                                 _levelGrid[x, y + 1].GetComponent<Border>().west &&
                                 _levelGrid[x - 1, y].GetComponent<Border>().north)
                        {
                            _levelGrid[x, y] = tiles.borderCornerSouthEast;
                        }
                        //Check if
                        else if (_levelGrid[x, y - 1] != null &&
                                 _levelGrid[x - 1, y] != null &&
                                 _levelGrid[x, y - 1].GetComponent<Border>() != null &&
                                 _levelGrid[x - 1, y].GetComponent<Border>() != null &&
                                 !_levelGrid[x, y - 1].GetComponent<Border>().isCorner &&
                                 !_levelGrid[x - 1, y].GetComponent<Border>().isCorner &&
                                 _levelGrid[x, y - 1].GetComponent<Border>().west &&
                                 _levelGrid[x - 1, y].GetComponent<Border>().south)
                        {
                            _levelGrid[x, y] = tiles.borderCornerNorthEast;
                        }
                        
                    }
                }
            }
        }

        private bool IsNeighbourAJunction(int gridPosX, int gridPosY, bool checkDiagonals)
        {
            for (int x = gridPosX - 1; x < gridPosX + 2; x++)
            {
                for (int y = gridPosY - 1; y < gridPosY + 2; y++)
                {
                    if (x >= 0 && y >= 0 && x <= levelWidth - 1 && y <= levelHeight - 1)
                    {
                        if (checkDiagonals)
                        {
                            if (x == gridPosX - 1 && y == gridPosY || x == gridPosX && y == gridPosY - 1 || x == gridPosX && y == gridPosY + 1 || x == gridPosX + 1 && y == gridPosY)
                            {
                                if (!(x == gridPosX && y == gridPosY) && _levelGrid[x, y] != null && _levelGrid[x, y].GetComponent<Road>().isJunction) return true;
                            }
                        }
                        else if (!(x == gridPosX && y == gridPosY) && _levelGrid[x, y] != null && _levelGrid[x, y].GetComponent<Road>().isJunction) return true;
                    }
                }
            }
            return false;
        }
        
        private bool IsJunctionInRadius(int gridPosX, int gridPosY, int radius)
        {
            for (int x = gridPosX - radius; x < gridPosX + radius + 1; x++)
            {
                for (int y = gridPosY - radius; y < gridPosY + radius + 1; y++)
                {
                    if (x >= 0 && y >= 0 && x <= levelWidth - 1 && y <= levelHeight - 1)
                    {
                        if (!(x == gridPosX && y == gridPosY) && _levelGrid[x, y] != null && _levelGrid[x, y].GetComponent<Road>().isJunction) return true;
                    }
                }
            }
            return false;
        }

        private bool IsUTurn(int gridPosX, int gridPosY, Road newRoad)
        {
            if (newRoad.isCurve)
            {
                if (_levelGrid[gridPosX, gridPosY+1] != null)
                {
                    if (_levelGrid[gridPosX, gridPosY+1].GetComponent<Road>().isCurve)
                    {
                        if (newRoad.west && _levelGrid[gridPosX, gridPosY+1].GetComponent<Road>().west ||
                            newRoad.east && _levelGrid[gridPosX, gridPosY+1].GetComponent<Road>().east)
                        {
                            return true;
                        }
                    }
                }
                if (_levelGrid[gridPosX+1, gridPosY] != null)
                {
                    if (_levelGrid[gridPosX+1, gridPosY].GetComponent<Road>().isCurve)
                    {
                        if (newRoad.north && _levelGrid[gridPosX+1, gridPosY].GetComponent<Road>().north ||
                            newRoad.south && _levelGrid[gridPosX+1, gridPosY].GetComponent<Road>().south)
                        {
                            return true;
                        }
                    }
                }
                if (_levelGrid[gridPosX, gridPosY-1] != null)
                {
                    if (_levelGrid[gridPosX, gridPosY-1].GetComponent<Road>().isCurve)
                    {
                        if (newRoad.west && _levelGrid[gridPosX, gridPosY-1].GetComponent<Road>().west ||
                            newRoad.east && _levelGrid[gridPosX, gridPosY-1].GetComponent<Road>().east)
                        {
                            return true;
                        }
                    }
                }
                if (_levelGrid[gridPosX-1, gridPosY] != null)
                {
                    if (_levelGrid[gridPosX-1, gridPosY].GetComponent<Road>().isCurve)
                    {
                        if (newRoad.north && _levelGrid[gridPosX-1, gridPosY].GetComponent<Road>().north ||
                            newRoad.south && _levelGrid[gridPosX-1, gridPosY].GetComponent<Road>().south)
                        {
                            return true;
                        }
                    }
                }
            }

            return false;
        }

        private int CountNeighbours(int gridPosX, int gridPosY)
        {
            int amountNeighbours = 0;
            
            if (_levelGrid[gridPosX, gridPosY + 1] != null)
            {
                amountNeighbours++;
            }

            if (_levelGrid[gridPosX + 1, gridPosY] != null)
            {
                amountNeighbours++;
            }

            if (_levelGrid[gridPosX, gridPosY - 1] != null)
            {
                amountNeighbours++;
            }

            if (_levelGrid[gridPosX - 1, gridPosY] != null)
            {
                amountNeighbours++;
            }
            
            return amountNeighbours;
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
    }
}