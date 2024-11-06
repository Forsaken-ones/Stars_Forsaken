using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Stars_Forsaken.Entities.Sprites
{
    internal class MovingSprite : ScaledSprite
    {
        protected internal Vector2 MovementDirection { get; set; }
        protected internal float Speed { get; set; }

        public MovingSprite(Vector2 position) : base(position) { }
        public MovingSprite(Vector2 position, float speed) : base(position)
        {
            this.Speed = speed;
        }
        public MovingSprite(Texture2D texture, Vector2 position) : base(texture, position) { }
        public MovingSprite(Texture2D texture, Vector2 position, float speed) : base(texture, position)
        {
            this.Speed = speed;
        }
    }
}