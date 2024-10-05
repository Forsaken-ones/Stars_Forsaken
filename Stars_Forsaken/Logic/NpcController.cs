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

namespace Stars_Forsaken.Logic
{
    internal class NpcController
    {
        private Microsoft.Xna.Framework.Vector2 movementDirection;

        public NpcController()
        { }

        private void SetMovementDirection(Microsoft.Xna.Framework.Vector2 newDirection)
        {
            newDirection.Normalize();
            movementDirection = newDirection;
        }

        private void AlterMovementDirection(Microsoft.Xna.Framework.Vector2 newDirection)
        {
            newDirection.Normalize(); 
            movementDirection += newDirection;
        }

        public Microsoft.Xna.Framework.Vector2 Move(Microsoft.Xna.Framework.Vector2 direction, float speed)
        {
            return direction * speed;
        }

        public Microsoft.Xna.Framework.Vector2 MoveToCoordinate(Microsoft.Xna.Framework.Vector2 position, Microsoft.Xna.Framework.Vector2 target, float speed)
        {
            Microsoft.Xna.Framework.Vector2 direction = target - position;
            direction.Normalize();

            return Move(direction, speed);
        }

        public Microsoft.Xna.Framework.Vector2 Roam(Microsoft.Xna.Framework.Vector2 position, float speed)
        {
            Random random = new Random();
            int direction = random.Next(0, 4);
            int distance = random.Next(20, 200);
            switch (direction)
            {
                case 0:
                    position += new Microsoft.Xna.Framework.Vector2(0, -speed);
                    break;
                case 1:
                    position += new Microsoft.Xna.Framework.Vector2(0, speed);
                    break;
                case 2:
                    position += new Microsoft.Xna.Framework.Vector2(speed, 0);
                    break;
                case 3:
                    position += new Microsoft.Xna.Framework.Vector2(-speed, 0);
                    break;
            }
            return position;
        }
    }
}
