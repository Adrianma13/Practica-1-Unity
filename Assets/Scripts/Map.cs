/*using System.IO;
using System.Collections.Generic;
public enum TileType
{
    Wall,
    Floor
}
public class Map
{
    public TileType[,] tiles;
    public int width { get; protected set; }
    public int height { get; protected set; }

protected Map(TileType[,]tiles)
    {
        this.tiles = tiles;
        this.width = tiles.GetLength(0);
        this.height = tiles.GetLength(1);
    }
    public TileType GetTile(int x, int y)
    {
        if (x < 0 || x >= width || y < 0 || y >= height)
            return TileType.Wall; // Treat out-of-bounds as walls
        return tiles[x, y];
    }
    public static Map CreateWithStringData(string mapdata)
    {
       StringReader reader = new StringReader(mapdata);
         List<TileType[]> rows = new List<TileType[]>();
         int mapWidth = 0;
         int mapHeight = 0;
        while (true)
        {
            string line = reader.ReadLine();
            if (line == null)
                break;
                line = line.Trim();
            if(line.Length == 0)
                continue;

            mapWidth = line.Length; // Assume all lines have the same width
            mapHeight++;
            foreach (char c in line)
            {
               switch (c)
                {
                    case '#':
                    rows.Add(TileType.Wall);
                        break;
                    case '.':
                    rows.Add(TileType.Floor);
                        break;
                    default:
                        throw new System.Exception($"Invalid character '{c}' in map data. Only '#' and '.' are allowed.");
                }
            }
          
        }
        TileType[,] tiles = new TileType[mapWidth, mapHeight];
        for (int y = 0; y < mapHeight; y++)
        {
            for (int x = 0; x < mapWidth; x++)
            {
                tiles[x, y] = rows[y * mapWidth + x];
            }
        }
        return new Map(tiles);
    }
}*/