using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Stars_Forsaken.Entities.Map;

namespace Stars_Forsaken.Entities.Sprites
{
    public class PlayerSprite : ScaledSprite
    {
        public float Speed;
        public Texture2D texture { get; set; }
        
        private readonly MapLogic map;

        public PlayerSprite(Texture2D texture, Vector2 position, float speed, MapLogic map) : base(texture, position)
        {
            this.texture = texture;
            this.Speed = speed;
            this.map = map;
        }

        public override void Update(GameTime gameTime)
        {
            // Handle player input for movement
            KeyboardState state = Keyboard.GetState();

            Vector2 newPosition = Position;

            if (state.IsKeyDown(Keys.W))
            {
                newPosition.Y -= Speed;
            }
            if (state.IsKeyDown(Keys.S))
            {
                newPosition.Y += Speed;
            }
            if (state.IsKeyDown(Keys.A))
            {
                newPosition.X -= Speed;
            }
            if (state.IsKeyDown(Keys.D))
            {
                newPosition.X += Speed;
            }
            Position = newPosition;
            // // Convert new position to map tile coordinates
            // float tileX = (newPosition.X / Tile.Size);
            // float tileY = (newPosition.Y / Tile.Size);

            // //Check if the new position is within map bounds and the tile is walkable
            // if (tileX >= 0 && tileX < map.mapWidth && tileY >= 0 && tileY < map.mapHeight)
            // {
            //     Tile tile = map.GetTile(tileX, tileY);
            //     Position = newPosition;
            //     // if (tile != null && tile.isWalkable)
            //     // {
            //     //     // If the tile is walkable, update the player’s position
            //     //     Position = newPosition;
            //     // }
            // }
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, Position, Color.White);
        }
    }
}
