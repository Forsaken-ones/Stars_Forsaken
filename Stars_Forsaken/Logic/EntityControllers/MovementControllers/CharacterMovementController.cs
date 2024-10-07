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
using Stars_Forsaken.Constants.Enums;

namespace Stars_Forsaken.Logic.EntityControllers.MovementControllers
{
    internal class CharacterMovementController
    {
        public static UInt16 MOVEMENT_CONTROLLER_COUNT = 0;

        public CharacterEntity entity;

        // allows changing of cardinal movement directions without modifying methods
        protected Microsoft.Xna.Framework.Vector2 vec2_up_direction = new Microsoft.Xna.Framework.Vector2(0, -1);
        protected Microsoft.Xna.Framework.Vector2 vec2_down_direction = new Microsoft.Xna.Framework.Vector2(0, 1);
        protected Microsoft.Xna.Framework.Vector2 vec2_left_direction = new Microsoft.Xna.Framework.Vector2(-1, 0);
        protected Microsoft.Xna.Framework.Vector2 vec2_right_direction = new Microsoft.Xna.Framework.Vector2(1, 0);

        public MovementState movementState;

        public CharacterMovementController(CharacterEntity entity)
        {
            ++MOVEMENT_CONTROLLER_COUNT;
            this.entity = entity;
            movementState = MovementState.IDLE;
        }

        protected internal void MoveStep()
        {
            entity.position += entity.movementDirection * entity.speed;
        }
        protected internal void MoveStep(float speed)
        {
            entity.position += entity.movementDirection * Math.Abs(speed);
        }
        protected internal void MoveStep(Microsoft.Xna.Framework.Vector2 direction, float speed)
        {
            entity.position += direction * Math.Abs(speed);
        }   


        protected internal void SetMovementDirection(Microsoft.Xna.Framework.Vector2 newDirection)
        {
            newDirection.Normalize();
            entity.movementDirection = newDirection;
        }
        protected internal void UpdateMovementDirection(Microsoft.Xna.Framework.Vector2 deltaDirection)
        {
            deltaDirection.Normalize();
            entity.movementDirection += deltaDirection;
        }
        protected internal void SetSpeed(float newSpeed)
        {
            if(newSpeed < 0)
            {
                entity.movementDirection *= -1;
                newSpeed *= -1;
            }
            entity.speed = newSpeed;
        }
        protected internal void UpdateSpeed(float deltaSpeed)
        {
            if(entity.speed + deltaSpeed < 0)
            {
                entity.movementDirection *= -1;
                deltaSpeed *= -1;
            }
            entity.speed += deltaSpeed;
        }

        public float GetStepLength()
        {
            return Microsoft.Xna.Framework.Vector2.Distance(Microsoft.Xna.Framework.Vector2.Zero, entity.movementDirection * entity.speed);
        }
        static float GetStepLength(Microsoft.Xna.Framework.Vector2 movementDirection, float speed)
        {
            return Microsoft.Xna.Framework.Vector2.Distance(Microsoft.Xna.Framework.Vector2.Zero, movementDirection * speed);
        }

        public void MoveToCoordinate(Microsoft.Xna.Framework.Vector2 destination)
        {
            Microsoft.Xna.Framework.Vector2 direction = destination - entity.position;
            direction.Normalize();

            entity.movementDirection = direction;

            float distance = Microsoft.Xna.Framework.Vector2.Distance(entity.position, destination);

            if (distance < GetStepLength())
            {
                MoveStep(speed: distance);
            }
            else
            {
                MoveStep();
            }
        }

        public virtual void Update()
        { }

        public virtual void Draw(SpriteBatch spriteBatch)
        { }
    }
}
