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
using Stars_Forsaken.Entities.Sprites;

namespace Stars_Forsaken.Entities.CharacterEntities
{
    internal class CharacterEntity : MovingSprite
    {
        public static UInt16 CHARACTER_ENTITY_COUNT { get; protected set; } = 0;

        public CharacterEntity(Microsoft.Xna.Framework.Vector2 position) : base(position) { }
        public CharacterEntity(Microsoft.Xna.Framework.Vector2 position, float speed) : base(position, speed)
        {
            ++CHARACTER_ENTITY_COUNT;
            MovementDirection = Microsoft.Xna.Framework.Vector2.Zero;
        }
        public CharacterEntity(Texture2D texture, Microsoft.Xna.Framework.Vector2 position, float speed) : base(texture, position, speed)
        {
            ++CHARACTER_ENTITY_COUNT;
            MovementDirection = Microsoft.Xna.Framework.Vector2.Zero;
        }
    }
}
