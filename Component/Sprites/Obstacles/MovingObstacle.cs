using Microsoft.Xna.Framework;
namespace brackeys_2020_2_jam.Component.Sprites.Obstacles
{
    public class MovingObstacle : StaticObstacle
    {
        public float AdditionalSpeed { get; set; }

        public override void Update(GameTime gameTime)
        {
            Speed = new Vector2(Speed.X - AdditionalSpeed, Speed.Y);
            
            base.Update(gameTime);
        }

    }
}
