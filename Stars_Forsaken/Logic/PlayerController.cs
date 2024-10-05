using Microsoft.VisualBasic.Devices;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SharpDX.Direct3D9;
using SharpDX.XInput;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Stars_Forsaken.Logic
{
    internal class PlayerController
    {
        // allows to change values of input keys without modifying methods (can later be used for loading custom keybinds)
        private Keys UP_KEY = Keys.W;
        private Keys DOWN_KEY = Keys.S;
        private Keys LEFT_KEY = Keys.A;
        private Keys RIGHT_KEY = Keys.D;

        // allows changing of cardinal movement directions without modifying methods
        private Microsoft.Xna.Framework.Vector2 VEC2_UP_DIRECTION = new Microsoft.Xna.Framework.Vector2(0, -1);
        private Microsoft.Xna.Framework.Vector2 VEC2_DOWN_DIRECTION = new Microsoft.Xna.Framework.Vector2(0, 1);
        private Microsoft.Xna.Framework.Vector2 VEC2_LEFT_DIRECTION = new Microsoft.Xna.Framework.Vector2(-1, 0);
        private Microsoft.Xna.Framework.Vector2 VEC2_RIGHT_DIRECTION = new Microsoft.Xna.Framework.Vector2(1, 0);

        // this field was transfered from the Player entity, the controller is responsible for modifying it
        // position is still a part of the entity (physical characteristic), but the controller is responsible for updating it
        private Microsoft.Xna.Framework.Vector2 movementDirection;

        public PlayerController()
        { }
        public PlayerController(Configuration config) // can later be used to load settings
        { }

        private void SetMovementDirection(Microsoft.Xna.Framework.Vector2 newDirection)
        {
            newDirection.Normalize();
            movementDirection = newDirection;
        }

        // vectors are normalized in these two methods to make sure that the player moves at the same speed in all directions
        private void AlterMovementDirection(Microsoft.Xna.Framework.Vector2 newDirection)
        {
            newDirection.Normalize();
            movementDirection += newDirection;
        }

        public Microsoft.Xna.Framework.Vector2 Move(Microsoft.Xna.Framework.Vector2 position, float speed)
        {
            // reading the keyboard state once per frame
            // instead of doing it on each IF statement
            // improves performance
            Microsoft.Xna.Framework.Input.KeyboardState _keyboardState = Microsoft.Xna.Framework.Input.Keyboard.GetState();


            // resetting the movement direction to zero on each tick so that the player stops moving when no keys are pressed
            // on every subsequent tick, it is assumed that the player isn't moving until a key press is detected
            // this protects from undefined behavior when two opposite keys are pressed (now they cancel each other out, previously it'd make the character disappear)
            movementDirection = Microsoft.Xna.Framework.Vector2.Zero;


            if (_keyboardState.IsKeyDown(UP_KEY))
            {
                AlterMovementDirection(VEC2_UP_DIRECTION);
            }

            if (_keyboardState.IsKeyDown(DOWN_KEY))
            {
                AlterMovementDirection(VEC2_DOWN_DIRECTION);
            }

            if (_keyboardState.IsKeyDown(RIGHT_KEY))
            {
                AlterMovementDirection(VEC2_RIGHT_DIRECTION);
            }

            if (_keyboardState.IsKeyDown(LEFT_KEY))
            {
                AlterMovementDirection(VEC2_LEFT_DIRECTION);
            }

            return movementDirection * speed;
        }
    }
}
