using Microsoft.VisualBasic.Devices;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SharpDX.XInput;
using Stars_Forsaken.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Stars_Forsaken.Entities.CharacterEntities
{
    internal class Crewmate : CharacterEntity
    {
        public Crewmate(Texture2D texture, Microsoft.Xna.Framework.Vector2 position, float speed) : base(texture, position, speed)
        {
            this.texture = texture;
            this.position = position;
            this.speed = speed;
        }
    }
}
