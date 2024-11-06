using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Stars_Forsaken.Entities.Sprites
{
    internal class ColoredSprite : ScaledSprite
    {
        public Color Color { get; set; }

        public ColoredSprite(Texture2D texture, Vector2 position,  Color color)
            : base(texture, position)
        {
            this.Color = color;
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, Position, Color);
        }
    }
}