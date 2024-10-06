using System.Windows.Forms.VisualStyles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Graphics.PackedVector;
using Microsoft.Xna.Framework.Input;
using SharpDX.DXGI;

namespace Stars_Forsaken;

public class Game1 : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    Camera _camera;
    Texture2D texture;
    Texture2D Background;
    MovingSprite sprite;
    ScaledSprite Bg;

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

        texture = Content.Load<Texture2D>("player");
        Background = Content.Load<Texture2D>("background");

        Bg = new ScaledSprite(Background, Vector2.Zero);
        sprite = new MovingSprite(texture, Vector2.Zero, 1f);
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();
        sprite.Update();

        _camera.Position = sprite.position;

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);

        _spriteBatch.Begin(transformMatrix: _camera.GetTransformation(GraphicsDevice));
        _spriteBatch.Draw(Bg.texture, Bg.Rect, Color.White);

        //Veliau reiks perkelti i kita vieta
        Rectangle originalRect = sprite.Rect;
        Rectangle scaledRect = new Rectangle(
            originalRect.X,
            originalRect.Y,
            originalRect.Width / 5,
            originalRect.Height / 5
        );
        _spriteBatch.Draw(sprite.texture, scaledRect, Color.White);

        _spriteBatch.End();

        base.Draw(gameTime);
    }
}
