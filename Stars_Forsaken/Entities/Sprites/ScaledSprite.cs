using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Stars_Forsaken.Entities.Sprites
{
    public class ScaledSprite : Sprite  // Changed 'internal' to 'public'
    {
        internal protected Rectangle Rectangle
        {
            get
            {
                return new Rectangle((int)Position.X, (int)Position.Y, Texture.Width, Texture.Height);
            }
            protected set { }
        }

        public ScaledSprite(Vector2 position) : base(position) { }
        public ScaledSprite(Texture2D texture, Vector2 position) : base(texture, position) { }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, Rectangle, Color.White);
        }
    }
}

