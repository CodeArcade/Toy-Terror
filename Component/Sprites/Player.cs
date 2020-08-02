using brackeys_2020_2_jam.Component.Sprites.Environment;
using brackeys_2020_2_jam.Manager;
using brackeys_2020_2_jam.Models;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Drawing;

namespace brackeys_2020_2_jam.Component.Sprites
{
    public class Player : Sprite
    {
        private KeyboardState CurrentKeyboard { get; set; }
        private KeyboardState PreviousKeyboard { get; set; }

        public const float FALL_MULTIPLIER = 0.5f;
        public const float JUMP_VELOCITY = -25f;

        public float ConveyorSpeed { get; set; }

        public const float ALIVE_CHARGE = 200f;
        public const float ALIVE_MAX = 1000f;
        public const float ALIVE_DRAIN = 3f;
        public const double WINDUP_TIME_IN_SECONDS = 1;

        public float AliveTimer { get; set; }
        public double CurrentWindupTime { get; set; }
        public double IFrames { get; } = 2;
        public double IFramesTimer { get; set; }
        public bool CanTakeDamage => IFramesTimer > IFrames;

        public bool IsInAir => Speed.Y != 0;
        public bool IsWindingUp { get; set; }
        public PlayerInput Input { get; set; }

        public bool IsGoingLeft => Speed.X < 0;
        public bool IsGoingRight => Speed.X > 0;
        public bool IsStandingStill => Speed.X == 0;
        public bool IsOnConveyor { get; private set; }
        public bool IsJumping { get; private set; }

        public float Acceleration { get; set; }
        public float CurrentAcceleration { get; private set; }
        public float MaxAcceleration { get; set; }

        public float MaxFallAcceleration => 0.3f;
        public float FallAcceleration { get; set; }

        private Dictionary<string, Animation> Animations { get; set; }

        public Player(PlayerInput input)
        {
            AliveTimer = 0;
            Input = input;
            Texture = ContentManager.StandingAnimation;
            Speed = new Vector2(0, -1);
            IsOnConveyor = false;

            Animations = new Dictionary<string, Animation>
            {
                { "walk", new Animation(ContentManager.WalkingAnimation, 29) {  FrameSpeed = 0.03f} },
                { "standing", new Animation(ContentManager.StandingAnimation, 1) },
                { "jump", new Animation(ContentManager.JumpAnimation, 13) { FrameSpeed = 0.001f} }
            };
            AnimationManager.Scale = 0.2f;
            AnimationManager.Parent = this;
            AnimationManager.Play(Animations["standing"]);
            Size = new Size(AnimationManager.AnimationRectangle.Size.X, AnimationManager.AnimationRectangle.Size.Y);

            IFramesTimer = IFrames;
        }

        public override void OnCollision(Sprite sprite, GameTime gameTime)
        {
            if (sprite is null) return;
            if (sprite is Clock) return;

            if (IsTouchingRight(sprite))
            {
                if (!IsJumping) AnimationManager.Play(Animations["standing"]);
                Speed = new Vector2(0, Speed.Y);
                Position = new Vector2(sprite.Position.X + sprite.Rectangle.Width, Position.Y);
            }
            else if (IsTouchingLeft(sprite))
            {
                if (!IsJumping) AnimationManager.Play(Animations["standing"]);
                Speed = new Vector2(0, Speed.Y);
                Position = new Vector2(sprite.Position.X - Rectangle.Width, Position.Y);
            }
            else if (IsTouchingTop(sprite))
            {
                Speed = new Vector2(Speed.X, 0);
                Position = new Vector2(Position.X - (ConveyorSpeed * 2), sprite.Position.Y - Rectangle.Height); // speed needs to be doubled to be as fast as the obstacles - dont ask me why
                FallAcceleration = 0;
                IsOnConveyor = true;
                if (sprite is Chopper) IsRemoved = true;
            }

        }

        public override void Update(GameTime gameTime)
        {
            if (IsStandingStill & !IsInAir && !IsJumping) AnimationManager.Play(Animations["standing"]);
            IFramesTimer += gameTime.ElapsedGameTime.TotalSeconds;

            PreviousKeyboard = CurrentKeyboard;
            CurrentKeyboard = Keyboard.GetState();

            if (AliveTimer > 0 && !IsWindingUp) AliveTimer -= ALIVE_DRAIN;

            if (IsInAir) IsOnConveyor = false;

            Windup(gameTime);
            FallDown();
            Move();

            Position += Speed;

            base.Update(gameTime);
        }

        private void FallDown()
        {
            Speed += Vector2.UnitY * GRAVITY.Y * FallAcceleration;

            if (FallAcceleration < MaxFallAcceleration)
                FallAcceleration += FALL_MULTIPLIER;
            else
                FallAcceleration = MaxFallAcceleration;

            if (Speed.Y > TERMINAL_VELOCITY) Speed = new Vector2(Speed.X, TERMINAL_VELOCITY);
        }

        private void Windup(GameTime gameTime)
        {
            if (Speed.X != 0 || Speed.Y != 0 || IsInAir)
            {
                IsWindingUp = false;
                return;
            }

            if (Keyboard.GetState().IsKeyDown(Input.Windup))
            {
                IsWindingUp = true;
                if (CurrentWindupTime > WINDUP_TIME_IN_SECONDS)
                {
                    AliveTimer += ALIVE_CHARGE;
                    if (AliveTimer > ALIVE_MAX) AliveTimer = ALIVE_MAX;
                    CurrentWindupTime = 0;
                }

                CurrentWindupTime += gameTime.ElapsedGameTime.TotalSeconds;

            }
            else IsWindingUp = false;
        }

        private void Move()
        {
            CheckMove();
            CheckJump();
        }

        private void CheckMove()
        {
            if (Keyboard.GetState().IsKeyDown(Input.Right) && AliveTimer > 0 && !IsWindingUp)
            {
                AnimationManager.Flip = true;
                if (!IsInAir && !IsJumping) AnimationManager.Play(Animations["walk"]);

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
                AnimationManager.Flip = false;
                if (!IsInAir && !IsJumping) AnimationManager.Play(Animations["walk"]);

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

        private void CheckJump()
        {
            if (!IsOnConveyor || IsWindingUp) return;
            if (CurrentKeyboard.IsKeyDown(Input.Jump) && PreviousKeyboard.IsKeyUp(Input.Jump) && AliveTimer > 0)
            {
                AnimationManager.Play(Animations["standing"]);
                IsJumping = true;
                Speed = Vector2.UnitY * JUMP_VELOCITY;
                FallAcceleration = 0;
                IsOnConveyor = false;
                IsJumping = false;
            }
        }

    }
}
