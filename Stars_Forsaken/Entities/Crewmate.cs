using Microsoft.VisualBasic.Devices;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SharpDX.XInput;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Stars_Forsaken.Entities
{
    internal class Crewmate
    {
        protected Texture2D texture { get; set; }
        protected Microsoft.Xna.Framework.Vector2 position { get; set; }
        protected float speed { get; set; }

        public Crewmate(Texture2D texture, Microsoft.Xna.Framework.Vector2 position, float speed)
        {
            this.texture = texture;
            this.position = position;
            this.speed = speed;
        }

        public virtual void Update() // overridable for Player class to have its own implementation
        {

        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, Color.White);
        }
    }
}
