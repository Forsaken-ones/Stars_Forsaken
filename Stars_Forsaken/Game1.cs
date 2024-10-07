using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Stars_Forsaken.Entities.CharacterEntities;
using Stars_Forsaken.Logic.EntityControllers.MovementControllers;

namespace Stars_Forsaken
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        Crewmate crewmate;
        Player player;

        CrewmateMovementController crewmateController;
        PlayerMovementController playerController;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            crewmate = new Crewmate(Content.Load<Texture2D>("player"), new Vector2(300, 300), 2f);
            player = new Player(Content.Load<Texture2D>("player"), Microsoft.Xna.Framework.Vector2.Zero, 6f);

            crewmateController = new CrewmateMovementController(crewmate);
            playerController = new PlayerMovementController(player);

            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            crewmateController.Update();
            playerController.Update();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            _spriteBatch.Begin();
            crewmate.Draw(_spriteBatch);
            player.Draw(_spriteBatch);

            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
