using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace Stars_Forsaken
{
    public static class TextureManager
    {
        private static Dictionary<string, Texture2D> _textures = new Dictionary<string, Texture2D>();

        public static void LoadContent(ContentManager content)
        {
            _textures["Background"] = content.Load<Texture2D>("Sprites/Backgrounds/test_bg");
            _textures["Character"] = content.Load<Texture2D>("Sprites/Characters/test_player");
        }

        public static Texture2D GetTexture(string name)
        {
            return _textures[name];
        }
    }
}