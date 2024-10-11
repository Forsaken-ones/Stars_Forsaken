using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Graphics.PackedVector;
using Microsoft.Xna.Framework.Input;
using SharpDX.DXGI;
using Stars_Forsaken.Entities.Map;
using Stars_Forsaken.Entities.Sprites;

namespace Stars_Forsaken;

public class Game1 : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    Camera _camera;
    PlayerSprite player;
    //ScaledSprite background;
    private MapLogic _map;
    public TextureManager TextureManager = new TextureManager();

    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        _graphics.PreferredBackBufferWidth = 1920;
        _graphics.PreferredBackBufferHeight = 1080;
        _graphics.IsFullScreen = true;
        _graphics.ApplyChanges();

        _camera = new Camera();
        _map = new MapLogic(10, 10); // Assuming Map constructor takes width and height
        

        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);
        TextureManager.LoadContent(Content);
        player = new PlayerSprite(TextureManager.GetTexture("Character"), Vector2.Zero, 10f, _map);
        MapComposition mapComposition = new MapComposition(_map, TextureManager);
        mapComposition.ComposeMap();
        //InitializeSprites();
    }

    private void InitializeSprites()
    {
        //background = new ScaledSprite(TextureManager.GetTexture("Background"), Vector2.Zero);
        //player = new PlayerSprite(TextureManager.GetTexture("Character"), Vector2.Zero, 1f, _map);
        Texture2D tileTexture = TextureManager.GetTexture("Tile1");
        // for (int x = 1; x < 10; x++)
        // {
        //     for (int y = 1; y < 10; y++)
        //     {   
        //         // String tileName = "Tile" + y;
        //         // Texture2D tileTexture = TextureManager.GetTexture(tileName);
        //         Texture2D tileTexture = TextureManager.GetTexture("Tile1");
        //         _map.SetTile(x, y, new Tile(tileTexture, new Vector2(x * Tile.Size, y * Tile.Size), true, false));
        //     }
        // }
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();
        player.Update(gameTime);
        _camera.Position = player.Position;

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);

        _spriteBatch.Begin(transformMatrix: _camera.GetTransformation(GraphicsDevice));
        _map.Draw(_spriteBatch);
        //_spriteBatch.Draw(background.texture, background.Rect, Color.White);
        //Veliau reiks perkelti i kita vieta
        Rectangle originalRect = player.Rect;
        Rectangle scaledRect = new Rectangle(
            originalRect.X,
            originalRect.Y,
            originalRect.Width / 5,
            originalRect.Height / 5
        );
        _spriteBatch.Draw(player.texture, scaledRect, Color.White);
        //player.Draw(_spriteBatch);

        _spriteBatch.End();

        base.Draw(gameTime);
    }
}
