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
        private const float terminalVelocity = 25f;
        private const float brake = 2f;
        private const float conveyorSpeed = 2f;

        private const float ALIVE_CHARGE = 50f;
        private const float ALIVE_MAX = 1000f;
        private const float ALIVE_DRAIN = 5f;
        public float AliveTimer { get; set; }
        public bool AllowMovement;
        public bool IsInAir { get; set; }
        public bool IsWindingUp { get; set; }
        public PlayerInput Input { get; set; }

        public float Acceleration { get; set; }
        public float CurrentAcceleration { get; private set; }
        public float MaxAcceleration { get; set; }

        public Player(PlayerInput input)
        {
            AliveTimer = 0;
            AllowMovement = false;
            Input = input;
            IsInAir = false;
            MaxSpeed = new Vector2(500f, 0);
            Texture = ContentManager.ProgressBarBackground;
        }

        public void OnCollision(Sprite sprite)
        {
            if (sprite is null) return;
            if (IsTouchingBottom(sprite))
            {
                IsInAir = false;
                Speed = new Vector2(Speed.X - conveyorSpeed);
            }
            else IsInAir = true;
        }

        public override void Update(GameTime gameTime)
        {
            if (AliveTimer > 0 && !IsWindingUp) AliveTimer -= ALIVE_DRAIN * gameTime.ElapsedGameTime.Seconds;

            Windup(gameTime);
            Move(gameTime);

            Position += Speed;

            if (Position.Y > 720) Position = new Vector2(Position.X, 0);

            base.Update(gameTime);
        }

        private void Windup(GameTime gameTime)
        {
            if ((Speed.X > 0 || Speed.Y > 0) && IsInAir)
            {
                IsWindingUp = false;
                return;
            }

            if (Keyboard.GetState().IsKeyDown(Input.Windup))
            {
                IsWindingUp = true;
                AliveTimer += ALIVE_CHARGE * gameTime.ElapsedGameTime.Seconds;
            }
            else IsWindingUp = false;
        }

        private void Move(GameTime gameTime)
        {
            // falling
            if (IsInAir) Speed += Vector2.UnitY * GRAVITY.Y * (fallMultiplier);
            if (Speed.Y > terminalVelocity) Speed = new Vector2(Speed.X, terminalVelocity);

            CheckMove(gameTime);
            CheckJump(gameTime);
        }

        private void CheckMove(GameTime gameTime)
        {

            if (Keyboard.GetState().IsKeyDown(Input.Right) && AliveTimer > 0)
            {
                if (CurrentAcceleration < MaxAcceleration)
                    CurrentAcceleration += Acceleration;
                else
                    CurrentAcceleration = MaxAcceleration;

                if (Speed.X < MaxSpeed.X)
                    Speed = new Vector2(Speed.X + CurrentAcceleration, Speed.Y);
                else
                    Speed = new Vector2(MaxSpeed.X, MaxSpeed.Y);

                return;
            }
            else if (Keyboard.GetState().IsKeyDown(Input.Left) &&  AliveTimer > 0)
            {
                if (CurrentAcceleration < MaxAcceleration)
                    CurrentAcceleration += Acceleration;
                else
                    CurrentAcceleration = MaxAcceleration;

                if (Speed.X < -MaxSpeed.X)
                    Speed = new Vector2(Speed.X - CurrentAcceleration, Speed.Y);
                else
                    Speed = new Vector2(-MaxSpeed.X, MaxSpeed.Y);

                return;
            }
            else
            {
                bool isLeft = Speed.X < 0;
                if (Speed.X == 0) return;

                if (CurrentAcceleration < 0)
                    CurrentAcceleration = 0;
                else
                    CurrentAcceleration -= Acceleration;

                if (Speed.X > 0)
                {
                    Speed = new Vector2(Speed.X - CurrentAcceleration, Speed.Y);
                    if (isLeft) Speed = new Vector2(0, Speed.Y);
                }
                else if (Speed.X < 0)
                {
                    Speed = new Vector2(Speed.X + CurrentAcceleration, Speed.Y);
                    if (!isLeft) Speed = new Vector2(0, Speed.Y);
                }
            }

        }

        private void CheckJump(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Input.Jump) && AliveTimer > 0)
            {
                Speed = Vector2.UnitY * jumpVelocity;
            }
        }
    }
}
