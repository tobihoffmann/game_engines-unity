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
        private GameObject startRoad;

        private void Start()
        {
            _grid = new GameObject[levelWidth,levelHeight];
        }
        
        private TileTemplates _tiles;

        private void GenerateRoad()
        {
             Queue<GameObject> q = new Queue<GameObject>();
            
            // Put Start Road in the middle of the grid
            int startPosX = levelWidth/2;
            int startPosY = levelHeight/2;
            _grid[startPosX, startPosY] = startRoad;
            startRoad.GetComponent<Road>().X = levelWidth / 2;
            startRoad.GetComponent<Road>().Y = levelHeight / 2;
            
            q.Enqueue(startRoad);

            foreach (GameObject r in q)
            {
                //check booleans
                Road road = r.GetComponent<Road>();
                if (road.north)
                {
                    GameObject newRoad = _tiles.northTiles[Random.Range(0, _tiles.northTiles.Count)];
                    if (_grid[road.X, road.Y + 1] != null)
                    {
                        _grid[road.X, road.Y + 1] = newRoad;
                        q.Enqueue(newRoad);
                    }
                    
                }
                if (road.east)
                {
                    GameObject newRoad = _tiles.eastTiles[Random.Range(0, _tiles.eastTiles.Count)];
                    if (_grid[road.X + 1, road.Y] != null)
                    {
                        _grid[road.X + 1, road.Y] = newRoad;
                        q.Enqueue(newRoad);
                    }
                }
                if (road.south)
                {
                    GameObject newRoad = _tiles.southTiles[Random.Range(0, _tiles.southTiles.Count)];
                    if (_grid[road.X, road.Y - 1] != null)
                    {
                        _grid[road.X, road.Y - 1] = newRoad;
                        q.Enqueue(newRoad);
                    }
                    
                }
                if (road.west)
                {
                    GameObject newRoad = _tiles.westTiles[Random.Range(0, _tiles.westTiles.Count)];
                    if (_grid[road.X - 1, road.Y] != null)
                    {
                        _grid[road.X - 1, road.Y] = newRoad;
                        q.Enqueue(newRoad);
                    }
                }
                q.Dequeue();
            }
        }

        
        private void SetStartCoordinates()
        {
            _grid[levelWidth/2, levelHeight/2] = new Road();
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