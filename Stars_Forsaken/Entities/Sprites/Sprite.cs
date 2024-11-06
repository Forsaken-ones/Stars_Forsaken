using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Stars_Forsaken.Entities.Sprites
{
    public class Sprite
    {
        protected internal Texture2D Texture { get; protected set; }
        protected internal Vector2 Position { get; set; } // Changed to property

        public Sprite(Vector2 position)
        {
            this.Position = position;
        }
        public Sprite(Texture2D texture, Vector2 position)
        {
            this.Texture = texture;
            this.Position = position;
        }

        public void LoadTexture(Texture2D texture)
        {
            this.Texture = texture;
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
