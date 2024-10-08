using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Linq;
using System.Xml.Linq;

public class GameMap
{
    public int Width { get; private set; }
    public int Height { get; private set; }
    public int TileWidth { get; private set; }
    public int TileHeight { get; private set; }
    public int[,] TileData { get; private set; }
    private Texture2D[] tileTextures;

    public GameMap(string tmxFilePath, Texture2D[] textures)
    {
        tileTextures = textures;
        LoadTmxFile("Content/Map" + tmxFilePath);
    }

    private void LoadTmxFile(string filePath)
    {
        XDocument doc = XDocument.Load(filePath);
        var mapElement = doc.Root;

        Width = (int)mapElement.Attribute("width");
        Height = (int)mapElement.Attribute("height");
        TileWidth = (int)mapElement.Attribute("tilewidth");
        TileHeight = (int)mapElement.Attribute("tileheight");

        TileData = new int[Height, Width];
        LoadTileData(mapElement.Element("layer"));
    }

    private void LoadTileData(XElement layerElement)
    {
        var dataElement = layerElement.Element("data");
        if (dataElement != null)
        {
            var tileDataString = dataElement.Value;
            var tileIds = tileDataString.Split(',').Select(int.Parse).ToArray();

            for (int i = 0; i < Height; i++)
            {
                for (int j = 0; j < Width; j++)
                {
                    TileData[i, j] = tileIds[i * Width + j];
                }
            }
        }
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        for (int y = 0; y < Height; y++)
        {
            for (int x = 0; x < Width; x++)
            {
                int tileId = TileData[y, x];
                if (tileId != 0) // 0 usually means no tile
                {
                    spriteBatch.Draw(tileTextures[tileId - 1], 
                                     new Vector2(x * TileWidth, y * TileHeight), 
                                     Color.White);
                }
            }
        }
    }

    public bool IsTileBlocked(int x, int y)
    {
        if (x < 0 || x >= Width || y < 0 || y >= Height)
        {
            return true; // Out of bounds
        }
        return TileData[y, x] != 0; // Assume 0 is walkable
    }
}
