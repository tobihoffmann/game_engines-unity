using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace LevelGeneration
{
    public class LevelGenerator : MonoBehaviour
    {
        [SerializeField] 
        private int levelWidth, levelHeight;

        private GameObject[,] _grid;

        [SerializeField] 
        private int tileSize;
        
        [SerializeField] 
        private GameObject startRoad;
        
        private Queue<GameObject> q = new Queue<GameObject>();

        private void Start()
        {
            _grid = new GameObject[levelWidth,levelHeight];
            GenerateRoad();
            DrawLevelArray();
        }
        [SerializeField]
        private TileTemplates _tiles;

        private void GenerateRoad()
        {
            
            
            // Put Start Road in the middle of the grid
            int startPosX = levelWidth/2;
            int startPosY = levelHeight/2;
            _grid[startPosX, startPosY] = startRoad;
            startRoad.GetComponent<Road>().X = levelWidth / 2;
            startRoad.GetComponent<Road>().Y = levelHeight / 2;
            
            q.Enqueue(startRoad);

            //TODO: HAS TO BE WHILE NOT FOREACH
            foreach (GameObject r in q)
            {
                Road road = r.GetComponent<Road>();
                // Apply new Road Tiles if there is an Exit
                if (road.north)
                {
                    AddTile(road.X, road.Y+1);
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
        }
        
        private void AddTile(int x, int y)
        {
            GameObject newRoadTile = _tiles.northTiles[Random.Range(0, _tiles.northTiles.Count)];
            Road newRoad = newRoadTile.GetComponent<Road>();
            newRoad.X = x;
            newRoad.Y = y;

            // check if new tile matches with neighbours, else roll again
            if (_grid[newRoad.X, newRoad.Y+1] == null || newRoad.north == _grid[newRoad.X, newRoad.Y+1].GetComponent<Road>().south && 
                _grid[newRoad.X+1, newRoad.Y] == null || newRoad.east == _grid[newRoad.X+1, newRoad.Y].GetComponent<Road>().west &&
                _grid[newRoad.X, newRoad.Y-1] == null || newRoad.south == _grid[newRoad.X, newRoad.Y-1].GetComponent<Road>().north &&
                _grid[newRoad.X-1, newRoad.Y] == null || newRoad.west == _grid[newRoad.X-1, newRoad.Y].GetComponent<Road>().east)
            {
                _grid[x, y] = newRoadTile;
                q.Enqueue(newRoadTile);
            }
            else
            {
                AddTile(x,y);
            }
        }

        private void DrawLevelArray()
        {
            for (int x = 0; x < levelWidth; x++)
            {
                for (int y = 0; y < levelHeight; y++)
                {
                    Instantiate(_grid[x, y], new Vector3(x * tileSize, y * tileSize, 0), Quaternion.identity);
                }
            }
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
                        if (_grid[x,y] != null)
                        {
                            neighbours.Add(_grid[x,y]);
                        }
                    }
                }
            }
            return neighbours;
        }

        private void CheckNeighbours(GameObject tile)
        { 
            List<GameObject> neighbours = GetNeighbours(tile);
            
            //if (IsJunction(tile)) 
        }

        private bool IsJunction(GameObject tile)
        {
            Road r = tile.GetComponent<Road>();
            int exits = 0;
            
            if (r.north) exits++;
            if (r.east) exits++;
            if (r.south) exits++;
            if (r.west) exits++;

            if (exits >= 3)
            {
                return true;
            }
            return false;
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