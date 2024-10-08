using System.Windows.Forms.VisualStyles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Graphics.PackedVector;
using Microsoft.Xna.Framework.Input;
using SharpDX.DXGI;
using Stars_Forsaken.Entities.Sprites;

namespace Stars_Forsaken;

public class Game1 : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    Camera _camera;
    MovingSprite player;
    ScaledSprite background;

    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {   
        _graphics.IsFullScreen = true;
        _camera = new Camera();
        base.Initialize();

        // Set the window size
        _graphics.PreferredBackBufferWidth = 1920;
        _graphics.PreferredBackBufferHeight = 1080;
        _graphics.ApplyChanges();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);
        TextureManager.LoadContent(Content);

        InitializeSprites();
    }

    private void InitializeSprites()
    {
            background = new ScaledSprite(TextureManager.GetTexture("Background"), Vector2.Zero);
            player = new MovingSprite(TextureManager.GetTexture("Character"), Vector2.Zero, 1f);
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();
        player.Update();
        _camera.Position = player.position;

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);

        _spriteBatch.Begin(transformMatrix: _camera.GetTransformation(GraphicsDevice));
        _spriteBatch.Draw(background.texture, background.Rect, Color.White);
        //Veliau reiks perkelti i kita vieta
        Rectangle originalRect = player.Rect;
        Rectangle scaledRect = new Rectangle(
            originalRect.X,
            originalRect.Y,
            originalRect.Width / 10,
            originalRect.Height / 10
        );
        _spriteBatch.Draw(player.texture, scaledRect, Color.White);

        _spriteBatch.End();

        base.Draw(gameTime);
    }
}
