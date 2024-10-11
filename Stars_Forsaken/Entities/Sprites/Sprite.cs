using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Stars_Forsaken.Entities.Sprites
{
    public class Sprite
    {
        public Texture2D Texture { get; private set; }
        public Vector2 Position { get; set; } // Changed to property

        public Sprite(Texture2D texture, Vector2 position)
        {
            this.Texture = texture;
            this.Position = position;
        }

        public virtual void Update(GameTime gameTime)
        {
            // Default update logic for sprite
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, Position, Color.White);
        }
    }
}
