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
    internal class CharacterEntity
    {
        public static UInt16 CHARACTER_ENTITY_COUNT { get; protected set; } = 0;
        // graphics
        protected internal Texture2D texture { get; protected set; }

        // movement data
        protected internal Microsoft.Xna.Framework.Vector2 position { get; set; }
        protected internal Microsoft.Xna.Framework.Vector2 movementDirection { get; set; }

        // physical characteristics and stats
        protected internal float speed { get; set; }


        public CharacterEntity(Microsoft.Xna.Framework.Vector2 position, float speed)
        {
            ++CHARACTER_ENTITY_COUNT;
            this.position = position;
            this.speed = speed;
            movementDirection = Microsoft.Xna.Framework.Vector2.Zero;
        }
        public CharacterEntity(Texture2D texture, Microsoft.Xna.Framework.Vector2 position, float speed) 
        {
            ++CHARACTER_ENTITY_COUNT;
            this.texture = texture;
            this.position = position;
            this.speed = speed;
            movementDirection = Microsoft.Xna.Framework.Vector2.Zero;
        }

        public void LoadTexture(Texture2D texture)
        {
            this.texture = texture;
        }

        public virtual void Update() // overridable for Player class to have its own implementation
        { }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, Color.White);
        }
    }
}
