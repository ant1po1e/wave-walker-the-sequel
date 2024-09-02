using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileGenerator : MonoBehaviour
{
    public Tilemap tilemap;  
    public Tile wallTile;    
    public Vector2Int mapSize = new Vector2Int(10, 10); 
    public float wallDensity = 0.3f; 

    public void ClearMap()
    {
        tilemap.ClearAllTiles();
    }

    public void GenerateMap()
    {
        ClearMap(); 

        for (int x = 0; x < mapSize.x; x++)
        {
            for (int y = 0; y < mapSize.y; y++)
            {
                if (Random.value < wallDensity)
                {
                    tilemap.SetTile(new Vector3Int(x, y, 0), wallTile);
                }
            }
        }
    }
}
