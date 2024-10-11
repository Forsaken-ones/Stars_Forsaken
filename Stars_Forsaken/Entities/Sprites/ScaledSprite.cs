using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Stars_Forsaken.Entities.Sprites
{
    public class ScaledSprite : Sprite  // Changed 'internal' to 'public'
    {
        public ScaledSprite(Texture2D texture, Vector2 position) : base(texture, position)
        {
        }

        public Rectangle Rect
        {
            get
            {
                return new Rectangle((int)Position.X, (int)Position.Y, Texture.Width, Texture.Height);
            }
        }

        public override void Update(GameTime gameTime)
        {
            // Optionally add update logic for scaled sprite
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            // Optionally apply scaling logic here before drawing
            base.Draw(spriteBatch);
        }
    }
}
