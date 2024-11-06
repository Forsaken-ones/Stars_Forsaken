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
    internal class CrewmateMovementController : CharacterMovementController
    {
        private static readonly Random _random = new Random(Environment.TickCount);
        private Stopwatch stopWatch = new Stopwatch();

        private Microsoft.Xna.Framework.Vector2 ROAM_DIRECTION;
        private Microsoft.Xna.Framework.Vector2 ROAM_DISPLACEMENT;
        private Microsoft.Xna.Framework.Vector2 ROAM_DESTINATION;

        private float IDLE_TIME = 0f; 
        private float IDLE_TIMER = 0f;

        public CrewmateMovementController(Crewmate entity) : base(entity)
        { }
        public Microsoft.Xna.Framework.Vector2 GetRandomDirection()
        {
            float angle = (float)(_random.NextDouble() * Math.PI * 2);

            return new Microsoft.Xna.Framework.Vector2((float)Math.Cos(angle), (float)Math.Sin(angle));
        }
        public static Microsoft.Xna.Framework.Vector2 GetRandomDirection(Random random)
        {
            float angle = (float)(random.NextDouble() * Math.PI * 2);

            return new Microsoft.Xna.Framework.Vector2((float)Math.Cos(angle), (float)Math.Sin(angle));
        }

        public void SeedRoaming(int minimumDistance, int maximumDistance, float idleTime)
        {
            while (ROAM_DIRECTION == Microsoft.Xna.Framework.Vector2.Zero)
            {
                ROAM_DIRECTION = GetRandomDirection();
            }
            int distance = _random.Next(minimumDistance, maximumDistance);
            ROAM_DISPLACEMENT = ROAM_DIRECTION * distance;
            ROAM_DESTINATION = entity.Position + ROAM_DISPLACEMENT;

            movementState = Constants.Enums.MovementState.ROAMING;
        }

        public void Roam(int minimumDistance, int maximumDistance, float idleTime)
        {
            if(IDLE_TIME == 0f)
            {
                IDLE_TIME = idleTime;
            }
            if (movementState == Constants.Enums.MovementState.IDLE)
            {
                if (!stopWatch.IsRunning)
                {
                    stopWatch.Start();
                }

                IDLE_TIMER = (float)stopWatch.Elapsed.TotalSeconds;

                // If the NPC has waited long enough, stop waiting and roam again
                if (IDLE_TIMER >= IDLE_TIME)
                {
                    stopWatch.Reset();
                    IDLE_TIMER = 0f;  // Reset the timer for the next wait cycle                    

                    SeedRoaming(minimumDistance, maximumDistance, idleTime);
                }
            }

            else if(movementState == Constants.Enums.MovementState.ROAMING)
            {
                if (entity.Position == ROAM_DESTINATION)
                {
                    movementState = Constants.Enums.MovementState.IDLE;

                    ROAM_DIRECTION = Microsoft.Xna.Framework.Vector2.Zero;
                    ROAM_DISPLACEMENT = Microsoft.Xna.Framework.Vector2.Zero;
                    ROAM_DESTINATION = Microsoft.Xna.Framework.Vector2.Zero;

                    stopWatch.Reset();
                    stopWatch.Start();
                }

                else
                {
                    MoveToCoordinate(ROAM_DESTINATION);
                }
            }          
        }
        public override void Update()
        {
            Roam(50, 300, 2f);
        }
    }
}
