using brackeys_2020_2_jam.Models;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace brackeys_2020_2_jam.Component.Sprites
{
    class Player : Sprite
    {
        private const float fallMultiplier = 2.5f;
        private const float lowJumpMultiplier = 2f;
        private const float jumpVelocity = 10f;
        private const float runVelocity = 10f;
        private const float brake = 2f;

        private const float ALIVE_CHARGE = 50f;
        private const float ALIVE_MAX = 1000f;
        private const float ALIVE_DRAIN = 5f;
        public float AliveTimer { get; set; }
        public bool AllowMovement;
        public bool IsInAir { get; set; }
        public PlayerInput Input { get; set; }

        public Player(PlayerInput input)
        {
            AliveTimer = 0;
            AllowMovement = false;
            Input = input;
            IsInAir = false;
            MaxSpeed = new Vector2(500f, 0);
        }

        public override void Update(GameTime gameTime)
        {
            if (AliveTimer > 0)
            {
                AliveTimer -= ALIVE_DRAIN * gameTime.ElapsedGameTime.Seconds;
            }
            Windup(gameTime);
            Move(gameTime);
            
            base.Update(gameTime);
        }

        private void Windup(GameTime gameTime)
        {
            if (IsInAir) return;
            if (Velocity.X > 0 || Velocity.Y > 0) return;

            if (Keyboard.GetState().IsKeyDown(Input.Windup))
            {
                AliveTimer += ALIVE_CHARGE * gameTime.ElapsedGameTime.Seconds;
            }
        }

        private void Move(GameTime gameTime)
        {
            // falling
            if (Velocity.Y < 0)
            {
                Velocity += Vector2.UnitY * GRAVITY.Y * (fallMultiplier) * gameTime.ElapsedGameTime.Seconds;
            }

            CheckMove(gameTime);
            CheckJump(gameTime);

        }

        private void CheckMove(GameTime gameTime)
        {
            Vector2 Acceleration = new Vector2(0, 0);
            if (Keyboard.GetState().IsKeyDown(Input.Left))
            {

                return;
            } else if (Keyboard.GetState().IsKeyDown(Input.Right))
            {
                
                return;
            } else
            {
                // brake
                Velocity += Velocity * brake;
            }
        }

        private void CheckJump(GameTime gameTime)
        { 
            if (Keyboard.GetState().IsKeyDown(Input.Jump))
            {
                Velocity = Vector2.UnitY * jumpVelocity;
            }
        }
    }
}
