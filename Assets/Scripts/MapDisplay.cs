/*using UnityEngine;
using UnityEngine.Tilemaps;

[System.Serializable]
public struct MapTilePair
{
    public TileType type;
    public TileBase tile;
}
public class MapDisplay : MonoBehaviour
{
    public Tilemap tilemap;
    public MapTilePair[] tilePairs;

    private Map map;

    public void DisplayMap(Map map)
    {
        this.map = map;
        tilemap.ClearAllTiles();
        for (int x = 0; x < map.width; x++)
        {
            for (int y = 0; y < map.height; y++)
            {
                TileType type = this.map.GetTileType(x, y);
                TileBase tile = GetTileForType(type);
                tilemap.SetTile(new Vector3Int(x, -y, 0), tile);
            }
        }
    }
    private TileBase GetTileForType(TileType type)
    {
        foreach (var pair in tilePairs)
        {
            if (pair.type == type)
                return pair.tile;
        }
        return null; // Return null if no matching tile is found
    }

  
}*/