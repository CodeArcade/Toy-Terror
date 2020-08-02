using Microsoft.Xna.Framework;

namespace brackeys_2020_2_jam.Component.Sprites.Obstacles
{
    public class StaticObstacle : Sprite
    {
        public float ConveyorSpeed { get; set; }
        public override Vector2 MaxSpeed { get => new Vector2(ConveyorSpeed, base.MaxSpeed.Y); set => base.MaxSpeed = value; }

        public float Acceleration { get; set; }
        public float CurrentAcceleration { get; private set; }
        public float MaxAcceleration { get; set; }

        public override void OnCollision(Sprite sprite, GameTime gameTime)
        {
            if (sprite == this) return;

            if (IsTouchingTop(sprite))
            {
                Speed = new Vector2(Speed.X, 0);
                Position = new Vector2(Position.X, sprite.Position.Y - Rectangle.Height);
            }

        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            FallDown();

            Position = new Vector2(Position.X - ConveyorSpeed, Position.Y);

            Position += Speed;
        }

        private void FallDown()
        {
            Speed += Vector2.UnitY * GRAVITY.Y;

            if (Speed.Y > TERMINAL_VELOCITY) Speed = new Vector2(Speed.X, TERMINAL_VELOCITY);
        }

    }
}
