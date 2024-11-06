using Microsoft.VisualBasic.Devices;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SharpDX.Direct3D9;
using SharpDX.XInput;
using Stars_Forsaken.Entities.CharacterEntities;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Stars_Forsaken.Logic.EntityControllers.MovementControllers
{
    internal class PlayerMovementController : CharacterMovementController
    {
        // allows to change values of input keys without modifying methods (can later be used for loading custom keybinds)
        private Keys up_key = Keys.W;
        private Keys down_key = Keys.S;
        private Keys left_key = Keys.A;
        private Keys right_key = Keys.D;

        public PlayerMovementController(Crewmate entity) : base(entity)
        { }

        public void Control()
        {
            // reading the keyboard state once per frame
            // instead of doing it on each IF statement
            // improves performance
            Microsoft.Xna.Framework.Input.KeyboardState _keyboardState = Microsoft.Xna.Framework.Input.Keyboard.GetState();


            // resetting the movement direction to zero on each tick so that the player stops moving when no keys are pressed
            // on every subsequent tick, it is assumed that the player isn't moving until a key press is detected
            // this protects from undefined behavior when two opposite keys are pressed (now they cancel each other out, previously it'd make the character disappear)
            entity.MovementDirection = Microsoft.Xna.Framework.Vector2.Zero;


            if (_keyboardState.IsKeyDown(up_key))
            {
                UpdateMovementDirection(vec2_up_direction);
            }

            if (_keyboardState.IsKeyDown(down_key))
            {
                UpdateMovementDirection(vec2_down_direction);
            }

            if (_keyboardState.IsKeyDown(right_key))
            {
                UpdateMovementDirection(vec2_right_direction);
            }

            if (_keyboardState.IsKeyDown(left_key))
            {
                UpdateMovementDirection(vec2_left_direction);
            }

            MoveStep();
        }

        public override void Update()
        {
            Control();
        }
    }
}
