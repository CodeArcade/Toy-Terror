using Microsoft.Xna.Framework;
namespace brackeys_2020_2_jam.Component.Sprites.Obstacles
{
    public class MovingObstacle : StaticObstacle
    {
        public float AdditionalSpeed { get; set; }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            Position = new Vector2(Position.X - AdditionalSpeed - Speed.X, Position.Y);
        }

    }
}
