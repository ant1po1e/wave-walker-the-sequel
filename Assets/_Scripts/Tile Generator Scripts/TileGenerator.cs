using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileGenerator : MonoBehaviour
{
    public Tilemap tilemap;
    public TileBase wallTile;
    public TileBase pathTile;  // optional tile for path
    public Vector2Int mapSize;
    
    private bool[,] maze;

    public void GenerateMap()
    {
        tilemap.ClearAllTiles();

        maze = new bool[mapSize.x, mapSize.y];
        for (int x = 0; x < mapSize.x; x++)
        {
            for (int y = 0; y < mapSize.y; y++)
            {
                maze[x, y] = true;
            }
        }

        Vector2Int startPos = new Vector2Int(1, 1);
        GenerateMaze(startPos);

        for (int x = 0; x < mapSize.x; x++)
        {
            for (int y = 0; y < mapSize.y; y++)
            {
                Vector3Int tilePosition = new Vector3Int(x, y, 0);
                if (maze[x, y])
                {
                    tilemap.SetTile(tilePosition, wallTile);
                }
                else if (pathTile != null)
                {
                    tilemap.SetTile(tilePosition, pathTile); 
                }
            }
        }
    }

    private void GenerateMaze(Vector2Int pos)
    {
        maze[pos.x, pos.y] = false;

        // Define the directions (up, down, left, right) in random order
        List<Vector2Int> directions = new List<Vector2Int>
        {
            new Vector2Int(0, 2),  // Up
            new Vector2Int(0, -2), // Down
            new Vector2Int(2, 0),  // Right
            new Vector2Int(-2, 0)  // Left
        };
        Shuffle(directions);

        foreach (Vector2Int dir in directions)
        {
            Vector2Int newPos = pos + dir;
            Vector2Int betweenPos = pos + dir / 2;

            if (IsInBounds(newPos) && maze[newPos.x, newPos.y])
            {
                maze[betweenPos.x, betweenPos.y] = false;
                GenerateMaze(newPos);
            }
        }
    }

    private bool IsInBounds(Vector2Int pos)
    {
        return pos.x > 0 && pos.y > 0 && pos.x < mapSize.x - 1 && pos.y < mapSize.y - 1;
    }

    private void Shuffle<T>(List<T> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            T temp = list[i];
            int randomIndex = Random.Range(i, list.Count);
            list[i] = list[randomIndex];
            list[randomIndex] = temp;
        }
    }
}
