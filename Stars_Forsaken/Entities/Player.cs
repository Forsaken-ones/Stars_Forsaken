using Microsoft.VisualBasic.Devices;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SharpDX.Direct3D9;
using SharpDX.XInput;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Stars_Forsaken.Logic;

namespace Stars_Forsaken.Entities
{
    internal class Player : Crewmate
    {
        PlayerController _controller = new();

        public Player(Texture2D texture, Microsoft.Xna.Framework.Vector2 position, float speed) : base(texture, position, speed) { }

        public override void Update()
        {
            position += _controller.Move(position, speed);
        }
    }
}
