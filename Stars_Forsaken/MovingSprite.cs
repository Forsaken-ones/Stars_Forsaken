using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Stars_Forsaken
{
    internal class MovingSprite : ScaledSprite
    {
        public float Speed;

        public MovingSprite(Texture2D texture, Vector2 position, float Speed) : base(texture, position)
        {
            this.Speed = Speed;
        }

        public override void Update()
        {
            base.Update();
            KeyboardState state = Keyboard.GetState();

            if (state.IsKeyDown(Keys.W))
            {
                position.Y -= Speed;
            }
            if (state.IsKeyDown(Keys.S))
            {
                position.Y += Speed;
            }
            if (state.IsKeyDown(Keys.A))
            {
                position.X -= Speed;
            }
            if (state.IsKeyDown(Keys.D))
            {
                position.X += Speed;
            }
        }
    }
}