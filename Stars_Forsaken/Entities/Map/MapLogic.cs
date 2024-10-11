using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Stars_Forsaken.Entities.Map
{
    public class MapLogic
    {
        private Tile[,] tiles;
        public int mapWidth { get; private set; }
        public int mapHeight { get; private set; }

        public MapLogic(int width, int height)
        {
            mapWidth = width;
            mapHeight = height;
            tiles = new Tile[width, height];
        }
        // Method to load a tile into the map
        public void SetTile(int x, int y, Tile tile)
        {
            if (x >= 0 && x < mapWidth && y >= 0 && y < mapHeight)
            {
                tiles[x, y] = tile;
            }
        }

        // Method to get a tile from the map
        public Tile GetTile(int x, int y)
        {
            if (x >= 0 && x < mapWidth && y >= 0 && y < mapHeight)
            {
                return tiles[x, y];
            }
            return default(Tile);  // Return a default Tile if out of bounds
        }

        // Method to draw the map
        public void Draw(SpriteBatch spriteBatch)
        {
            for (int x = 0; x < mapWidth; x++)
            {
                for (int y = 0; y < mapHeight; y++)
                {
                    Tile tile = tiles[x, y];
                    if (tile != null)
                    {
                        tile.Draw(spriteBatch);
                    }
                }
            }
        }
    }
}