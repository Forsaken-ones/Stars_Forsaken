using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Stars_Forsaken.Entities.Map;

namespace Stars_Forsaken
{
    public class MapComposition
    {
        private MapLogic map;

        private TextureManager textureManager;

        public MapComposition(MapLogic map, TextureManager textureManager)
        {
            this.map = map;
            this.textureManager = textureManager;
        }

        public void ComposeMap()
        {
            for (int x = 1; x < 10; x++)
            {
                for (int y = 1; y < 10; y++)
                {
                    Texture2D texture = GetTextureForTile();
                    Tile tile = new Tile(texture, new Vector2(x * Tile.Size, y * Tile.Size), true, false);
                    map.SetTile(x, y, tile);
                }
            }
        }

        private Texture2D GetTextureForTile()
        {
            return textureManager.GetTexture("Tile1");
            // Example logic to determine which texture to use for a tile
            // if ((x + y) % 2 == 0)
            // {
            //     return TextureManager.GetTexture("Tile1");
            // }
            // else if ((x + y) % 3 == 0)
            // {
            //     return TextureManager.GetTexture("Tile8");
            // }
            // else
            // {
            //     return TextureManager.GetTexture("Tile5");
            // }
        }
    }
}