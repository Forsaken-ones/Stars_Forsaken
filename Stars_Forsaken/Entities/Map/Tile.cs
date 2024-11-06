using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Stars_Forsaken
{
    public class Tile
    {
        public static int Size = 87;
        public Texture2D Texture { get; private set; }
        public Vector2 Position { get; private set; }
        public bool isWalkable { get; private set; }
        public bool isOccupied { get; private set; }

        public Tile(Texture2D texture, Vector2 position, bool walkable, bool occupied)
        {
            Texture = texture;
            Position = position;
            isWalkable = true;
            isOccupied = false;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, Position, Color.White);
        }
    }
}
