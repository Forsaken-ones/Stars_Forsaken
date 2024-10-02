using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Stars_Forsaken;

public class Game1 : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    Texture2D texture;
    MovingSprite sprite;

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

        texture = Content.Load<Texture2D>("player");
        //sprite = new ColoredSprite(texture, Vector2.Zero, Color.Red);
        sprite = new MovingSprite(texture, Vector2.Zero, 1f);
    }
    //System.Numerics.Vector2.Zero
    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        sprite.Update();

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);

        //sutvarko pixelArt, kad butu geros kokybes jei didini paveiksleli
        _spriteBatch.Begin(samplerState: SamplerState.PointClamp);
        
        //white - blendina image, realiai tsg palieka orginalu
        _spriteBatch.Draw(sprite.texture, sprite.Rect, Color.White);
        //specialiai taip su sprites padaryta, kad butu aisku kur kas, ir kaip lengviau pakeisit tai
        _spriteBatch.End();

        base.Draw(gameTime);
    }
}
