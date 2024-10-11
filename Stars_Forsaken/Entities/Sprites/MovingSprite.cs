using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Stars_Forsaken.Entities.Sprites
{
    internal class MovingSprite : ScaledSprite
    {
        public float Speed;

        public MovingSprite(Texture2D texture, Vector2 position, float Speed) : base(texture, position)
        {
            this.Speed = Speed;
        }
    }
}