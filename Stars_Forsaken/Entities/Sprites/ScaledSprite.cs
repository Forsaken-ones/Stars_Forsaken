using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

//sutvarko dydi, ji gali didinti, mazinti
//inherits from Sprite
namespace Stars_Forsaken.Entities.Sprites
{
    internal class ScaledSprite : Sprite
    {
        public Rectangle Rect{
            get{
                return new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);
            }
        }
        public ScaledSprite(Texture2D texture, Vector2 position) : base(texture, position)
        {
            
        }

    }
}