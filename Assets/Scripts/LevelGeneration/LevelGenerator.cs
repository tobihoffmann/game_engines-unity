﻿using System.Collections.Generic;
using Item_Management;
using Managers;
using UnityEngine;
using Pathfinding;
using Random = UnityEngine.Random;

namespace LevelGeneration
{
    public class LevelGenerator : MonoBehaviour
    {
        [SerializeField] private int levelWidth, levelHeight;

        [SerializeField] private int tileSize;

        [SerializeField] private GameObject startRoad;

        [SerializeField] private TileTemplates tiles;
        
        [SerializeField] private GameObject levelEndPoint;

        private GameObject[,] _levelGrid;

        private int _maxTiles;
        private int _minTiles;
        private int _tileCount;

        private Queue<GameObject> _q = new Queue<GameObject>();

        private List<Vector2> _endingPositions = new List<Vector2>();

        private Vector2[] _startAndEnd;

        private Vector2 _aStarSpawnPoint;

        private int _aStarWidth;
        private int _aStarHeight;

        private EnemySpawner _enemySpawner;
        
        private void Start()
        {
            _maxTiles = (levelHeight - 4) * (levelWidth - 4) / 2;
            _minTiles = _maxTiles / 5;
            GenerateRoad();
            AddPlanes();
            AddBorders();
            AddBorderCorners();
            _startAndEnd = GetLongestEndingDistance();
            SpawnPlayer();
            SpawnEndPoint();
            BuildLevel();
            InitiatePathFinding();
            SpawnPowerUps();
            _levelGrid = new GameObject[0,0];
        }

        private void GenerateRoad()
        {
            bool meetLevelRequirements = false;
            while (!meetLevelRequirements)
            {
                _q.Clear();
                _tileCount = 0;
                _levelGrid = new GameObject[levelWidth, levelHeight];
                _endingPositions.Clear();
                
                // Put Level Start in the middle of the grid and enqueue
                int startPosX = levelWidth / 2;
                int startPosY = levelHeight / 2;
                _levelGrid[startPosX, startPosY] = startRoad;
                _tileCount++;
                startRoad.GetComponent<Road>().X = levelWidth / 2;
                startRoad.GetComponent<Road>().Y = levelHeight / 2;
                _q.Enqueue(startRoad);

                while (_q.Count != 0 && _tileCount < _maxTiles)
                {
                    Road road = _q.Peek().GetComponent<Road>();

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

                    _q.Dequeue();
                }
                AddRoadEnd();
                if (_tileCount >= _minTiles && _endingPositions.Count >= 4)
                {
                    meetLevelRequirements = true;
                }
            }
            
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
                        if (newRoad.isJunction && (IsJunctionInRadius(x, y, 5)))
                        {
                            whileCount++;
                        }
                        // Rule: A Curve is not adjacent to a Junction
                        else if (newRoad.isCurve && IsNeighbourAJunction(x, y, false))
                        {
                            whileCount++;
                        }
                        // Rule: A curve cant make a direct U-Turn
                        else if (IsUTurn(x, y, newRoad))
                        {
                            whileCount++;
                        }
                        else
                        {
                            newRoad.X = x;
                            newRoad.Y = y;
                            _levelGrid[x, y] = newRoadTile;
                            _tileCount++;
                            _q.Enqueue(newRoadTile);
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
                        Vector2 endingPos;
                        //Check if Road ending matches with Tile above and apply
                        if (_levelGrid[x, y + 1] != null && _levelGrid[x, y + 1].GetComponent<Road>().south)
                        {
                            _levelGrid[x, y] = tiles.endingSouth;
                            endingPos = new Vector2(x, y);
                            _endingPositions.Add(endingPos);

                        }
                        //Check if Road ending matches with Tile below and apply
                        else if (_levelGrid[x, y - 1] != null && _levelGrid[x, y - 1].GetComponent<Road>().north)
                        {
                            _levelGrid[x, y] = tiles.endingNorth;
                            endingPos = new Vector2(x, y);
                            _endingPositions.Add(endingPos);
                        }
                        //Check if Road ending matches with Tile to the right and apply
                        else if (_levelGrid[x + 1, y] != null && _levelGrid[x + 1, y].GetComponent<Road>().west)
                        {
                            _levelGrid[x, y] = tiles.endingWest;
                            endingPos = new Vector2(x, y);
                            _endingPositions.Add(endingPos);
                        }
                        //Check if Road ending matches with Tile to the left and apply
                        else if (_levelGrid[x - 1, y] != null && _levelGrid[x - 1, y].GetComponent<Road>().east)
                        {
                            _levelGrid[x, y] = tiles.endingEast;
                            endingPos = new Vector2(x, y);
                            _endingPositions.Add(endingPos);
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
            _enemySpawner = GetComponent<EnemySpawner>();
            for (int x = 0; x < levelWidth; x++)
            {
                for (int y = 0; y < levelHeight; y++)
                {
                    if (_levelGrid[x, y] != null)
                    {
                        GameObject go = Instantiate(_levelGrid[x, y], new Vector3(x * tileSize, y * tileSize, 0), Quaternion.identity);
                        if (go.GetComponentInChildren<SpawnZone>() != null)
                        {
                            _enemySpawner.SpawnZones.Add(go.GetComponentInChildren<SpawnZone>());
                        }
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

            Debug.Log(arrayRepresentation + "\n" + "Tiles placed: " + _tileCount + "\n" + "Max Tiles: " + _maxTiles);
        }
        
        
        /// <summary>
        /// Calculate the longest distance between all ending tiles.
        /// </summary>
        /// <returns>A List of two vectors, wich are the furthest away from each other.</returns>
        private Vector2[] GetLongestEndingDistance()
        {
            int _tempI = 0;
            int _tempJ = 0;
            Vector2[] startAndEnd = new Vector2[2];
            float distance = 0;
            for (int i = 0; i < _endingPositions.Count - 1; i++)
            {
                Vector2 start = _endingPositions[i];
                for (int j = 1; j < _endingPositions.Count; j++)
                {
                    Vector2 end = _endingPositions[j];
                    float tempDistance = Vector2.Distance(start, end);
                    if (tempDistance > distance)
                    {
                        startAndEnd[0] = start;
                        _tempI = i;
                        startAndEnd[1] = end;
                        _tempJ = j;
                        distance = tempDistance;
                    }
                }
            }
            _endingPositions.Remove(_endingPositions[_tempI]);
            _endingPositions.Remove(_endingPositions[_tempJ - 1]);
           
            return startAndEnd;
        }

        private void SpawnPowerUps()
        {
            int count = _endingPositions.Count;
            for (int i = 0; i < count; i++)
            {
                int random = Random.Range(0, _endingPositions.Count - 1);
                Vector2 randomPos = _endingPositions[random];
                _endingPositions.Remove(_endingPositions[random]);
                
                //Select Random Power Up
                int randomPowerUp = Random.Range(0, 3);
                Item powerUp;
                if (randomPowerUp == 0) powerUp = new Item {itemType = Item.ItemType.JuggernautBuff};
                else if (randomPowerUp == 1) powerUp = new Item {itemType = Item.ItemType.SpeedBuff};
                else powerUp = new Item {itemType = Item.ItemType.ShootDamageBuff};
                
                //Spawn item in World coordinates
                randomPos.x *= tileSize;
                randomPos.y *= tileSize;
                ItemWorld.SpawnItemWorld(randomPos, powerUp);
            }
        }

        /// <summary>
        /// Spawn the player on the start positon and sets camera position.
        /// </summary>
        private void SpawnPlayer()
        {
            Vector2 start = new Vector2(_startAndEnd[0].x, _startAndEnd[0].y);
            start.x = start.x * tileSize;
            start.y = start.y * tileSize;
            PlayerManager.Instance.SetPlayerPosition(start.x, start.y);
            Camera mainCam = Camera.main;
            if (!(mainCam is null)) mainCam.transform.position = new Vector3(start.x, start.y, -45);
        }

        /// <summary>
        /// Spawn the level complete point on the end position
        /// </summary>
        private void SpawnEndPoint()
        {
            Vector2 end = new Vector2(_startAndEnd[1].x, _startAndEnd[1].y);
            end.x = end.x * tileSize;
            end.y = end.y * tileSize;
            Instantiate(levelEndPoint, end, Quaternion.identity);
        }

        /// <summary>
        /// Calculates the spawnpoint and the width and height for the A* graph.
        /// </summary>
        /// <returns>The spawnpoint of the A* graph (in the center of the generated level).</returns>
        private Vector2 GetAStarSpawnPoint()
        {
            int smallestX = int.MaxValue;
            int smallestY = int.MaxValue;
            int biggestX = int.MinValue;
            int biggestY = int.MinValue;
            
            for (int x = 0; x < levelWidth; x++)
            {      
                for (int y = 0; y < levelHeight; y++)
                {
                    if (_levelGrid[x,y] != null)
                    {
                        if (x < smallestX)
                            smallestX = x;
                        if (y < smallestY)
                            smallestY = y;
                        if (x > biggestX)
                            biggestX = x;
                        if (y > biggestY)
                            biggestY = y;
                    }
                }
            }
            float centerX = ((biggestX + smallestX) / 2) * tileSize;
            float centerY = ((biggestY + smallestY) / 2) * tileSize;
            _aStarWidth = (biggestX - smallestX + 1) * tileSize;
            _aStarHeight = (biggestY - smallestY + 1) * tileSize;
            Vector2 aiGridCenter = new Vector2(centerX, centerY);
            return aiGridCenter;
        }

        /// <summary>
        /// Sets the values of the active graph in the scene (center and dimensions) and scans the graph.
        /// </summary>
        private void InitiatePathFinding()
        {
            GridGraph gg = AstarPath.active.data.gridGraph;
            gg.center = new Vector3(GetAStarSpawnPoint().x, GetAStarSpawnPoint().y, 0);
            gg.SetDimensions(_aStarWidth, _aStarHeight, 1);
            AstarPath.active.Scan();
        }
    }
}
