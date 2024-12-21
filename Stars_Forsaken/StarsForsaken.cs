using System;
using Microsoft.Extensions.Options;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Graphics.PackedVector;
using Microsoft.Xna.Framework.Input;
using SharpDX.DXGI;
using Stars_Forsaken.Logging;
using Stars_Forsaken.Entities.Map;
using Stars_Forsaken.Entities.Sprites;
using Stars_Forsaken.Logic;

using System.Text.Json;
using Stars_Forsaken.Config.Loader;
using Microsoft.Extensions.Logging;
using Stars_Forsaken.Config.DirFilEdit;

namespace Stars_Forsaken;

public class StarsForsaken : Game
{
    private Logging.ILoggerProvider _loggerProvider;
    private ILogger _logger;
    private IConfigurationLoader _configLoader;
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;

    public StarsForsaken()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        var loggerConfig = new LoggerConfiguration();
        _loggerProvider = new LoggerProvider(ConfigurationLoader.CreateOptions(loggerConfig), DirEdit.GetParentDir(AppContext.BaseDirectory, "Stars_Forsaken"));

        _logger = _loggerProvider.CreateLogger();

        DirEdit.Logger = _logger;
        FilEdit.Logger = _logger;

        _graphics.PreferredBackBufferWidth = 1920;
        _graphics.PreferredBackBufferHeight = 1080;
        _graphics.IsFullScreen = true;
        _graphics.ApplyChanges();

        _configLoader = new ConfigurationLoader(DirEdit.GetParentDir(AppContext.BaseDirectory, "Stars_Forsaken"), "Config/json", _logger);


        _logger.LogInformation("Application initialized");
        DirEdit.CreateDir(DirEdit.GetParentDir(AppContext.BaseDirectory, "Stars_Forsaken"), "Logs");

        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        _logger.LogInformation("Content loaded");
    }

    private void InitializeSprites()
    {
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);

        base.Draw(gameTime);
    }
}
