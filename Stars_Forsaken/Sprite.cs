using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using System.Numerics;
using Microsoft.Xna.Framework;

namespace Stars_Forsaken
{
    internal class Sprite
    {
        public Texture2D texture;
        public Microsoft.Xna.Framework.Vector2 position;

        public Sprite(Texture2D texture, Microsoft.Xna.Framework.Vector2 position)
        {
            this.texture = texture;
            this.position = position;
        }

        public virtual void Update()
        {

        }
    }
}