using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace Stars_Forsaken
{
    public class TextureManager
    {
        public Dictionary<string, Texture2D> _textures = new Dictionary<string, Texture2D>();

        public void LoadContent(ContentManager content)
        {
            _textures["Background"] = content.Load<Texture2D>("Sprites/Backgrounds/test_bg");
            _textures["Character"] = content.Load<Texture2D>("Sprites/Characters/test_player");


            //_textures.Add("Tile1", content.Load<Texture2D>("Map/til1"));
            _textures["Tile1"] = content.Load<Texture2D>("Map/til5");
            // for (int i = 1; i <= 9; i++)
            // {
            //     String tileName = "Tile" + i;
            //     //_textures.Add("Tile" + i, content.Load<Texture2D>("Map/til" + i));
            //     _textures.Add(tileName, content.Load<Texture2D>("Map/til" + i));
            // }
        }

        public Texture2D GetTexture(string name)
        {
            return _textures[name];
        }
    }
}