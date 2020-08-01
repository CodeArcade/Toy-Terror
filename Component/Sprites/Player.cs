using brackeys_2020_2_jam.Models;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace brackeys_2020_2_jam.Component.Sprites
{
    public class Player : Sprite
    {
        private KeyboardState CurrentKeyboard { get; set; }
        private KeyboardState PreviousKeyboard { get; set; }

        public const float fallMultiplier = 0.5f;

        public const float jumpVelocity = 25f;
        public const float terminalVelocity = 25f;
        public const float conveyorSpeed = 0.1f;

        public const float ALIVE_CHARGE = 5f;
        public const float ALIVE_MAX = 1000f;
        public const float ALIVE_DRAIN = 1f;

        public float AliveTimer { get; set; }
        public bool IsInAir => Speed.Y != 0;
        public bool IsWindingUp { get; set; }
        public PlayerInput Input { get; set; }

        public bool IsGoingLeft => Speed.X < 0;
        public bool IsGoingRight => Speed.X > 0;
        public bool IsStandingStill => Speed.X == 0;

        public float Acceleration { get; set; }
        public float CurrentAcceleration { get; private set; }
        public float MaxAcceleration { get; set; }

        public float MaxFallAcceleration => 0.3f;
        public float FallAcceleration { get; set; }

        public Player(PlayerInput input)
        {
            AliveTimer = 0;
            Input = input;
            Texture = ContentManager.ProgressBarBackground;
            Speed = new Vector2(0, -1);
        }

        public void OnCollision(Sprite sprite)
        {
            if (sprite is null) return;
            if (IsTouchingTop(sprite))
            {
                // TODO
                // Speed = new Vector2(Speed.X - conveyorSpeed, 0);
                Speed = new Vector2(Speed.X, 0);
                Position = new Vector2(Position.X, sprite.Position.Y - Rectangle.Height);
                FallAcceleration = 0;
            }
            else if (IsTouchingBottom(sprite))
            {
                Speed = new Vector2(Speed.X, 0);
            }
            else if (IsTouchingRight(sprite))
            {
                Speed = new Vector2(0, Speed.Y);
                Position = new Vector2(sprite.Position.X, Position.Y);
            }
            else if (IsTouchingLeft(sprite))
            {
                Speed = new Vector2(0, Speed.Y);
                Position = new Vector2(sprite.Position.X - Rectangle.Width, Position.Y);
            }

        }

        public override void Update(GameTime gameTime)
        {
            PreviousKeyboard = CurrentKeyboard;
            CurrentKeyboard = Keyboard.GetState();

            if (AliveTimer > 0 && !IsWindingUp) AliveTimer -= ALIVE_DRAIN;

            FallDown();
            Windup(gameTime);
            Move(gameTime);

            Position += Speed;

            base.Update(gameTime);
        }

        private void FallDown()
        {
            Speed += Vector2.UnitY * GRAVITY.Y * FallAcceleration;

            if (FallAcceleration < MaxFallAcceleration)
                FallAcceleration += fallMultiplier;
            else
                FallAcceleration = MaxFallAcceleration;

            if (Speed.Y > terminalVelocity) Speed = new Vector2(Speed.X, terminalVelocity);
        }

        private void Windup(GameTime gameTime)
        {
            if ((Speed.X != 0 || Speed.Y != 0) && IsInAir)
            {
                IsWindingUp = false;
                return;
            }

            if (Keyboard.GetState().IsKeyDown(Input.Windup))
            {
                IsWindingUp = true;
                AliveTimer += ALIVE_CHARGE;
                if (AliveTimer > ALIVE_MAX) AliveTimer = ALIVE_MAX;
            }
            else IsWindingUp = false;
        }

        private void Move(GameTime gameTime)
        {
            CheckMove(gameTime);
            CheckJump(gameTime);
        }

        private void CheckMove(GameTime gameTime)
        {

            if (Keyboard.GetState().IsKeyDown(Input.Right) && AliveTimer > 0 && !IsWindingUp)
            {
                if (IsGoingLeft)
                {
                    Break();
                    return;
                }

                Accelerate();

                if (Speed.X < MaxSpeed.X)
                    Speed = new Vector2(Speed.X + CurrentAcceleration, Speed.Y);
                else
                    Speed = new Vector2(MaxSpeed.X, Speed.Y);
            }
            else if (Keyboard.GetState().IsKeyDown(Input.Left) && AliveTimer > 0 && !IsWindingUp)
            {
                if (IsGoingRight)
                {
                    Break();
                    return;
                }

                Accelerate();

                if (Speed.X < -MaxSpeed.X)
                    Speed = new Vector2(Speed.X - CurrentAcceleration, Speed.Y);
                else
                    Speed = new Vector2(-MaxSpeed.X, Speed.Y);
            }
            else Break();
        }

        private void Accelerate()
        {
            if (CurrentAcceleration < MaxAcceleration)
                CurrentAcceleration += Acceleration;
            else
                CurrentAcceleration = MaxAcceleration;
        }

        private void Decelerate()
        {
            if (CurrentAcceleration <= 0)
                CurrentAcceleration = 0;
            else
                CurrentAcceleration -= Acceleration;
        }

        private void Break()
        {
            if (IsStandingStill)
            {
                CurrentAcceleration = 0;
                return;
            }

            Decelerate();

            if (IsGoingRight)
            {
                Speed = new Vector2(Speed.X - CurrentAcceleration - Acceleration, Speed.Y);
                if (IsGoingLeft) Speed = new Vector2(0, Speed.Y);
            }
            else if (IsGoingLeft)
            {
                Speed = new Vector2(Speed.X + CurrentAcceleration + Acceleration, Speed.Y);
                if (IsGoingRight) Speed = new Vector2(0, Speed.Y);
            }
        }

        private void CheckJump(GameTime gameTime)
        {
            if (CurrentKeyboard.IsKeyDown(Input.Jump) && PreviousKeyboard.IsKeyUp(Input.Jump) && AliveTimer > 0)
            {
                Speed = Vector2.UnitY * -jumpVelocity;
                FallAcceleration = 0;
            }
        }

    }
}
