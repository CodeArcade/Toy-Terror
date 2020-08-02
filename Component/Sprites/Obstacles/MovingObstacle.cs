using Microsoft.Xna.Framework;
namespace brackeys_2020_2_jam.Component.Sprites.Obstacles
{
    public class MovingObstacle : StaticObstacle
    {
        public float AdditionalSpeed { get; set; }

        public override void OnCollision(Sprite sprite, GameTime gameTime)
        {
            base.OnCollision(sprite, gameTime);
        
            if(IsTouchingRight(sprite) && sprite is StaticObstacle)
            {
                AdditionalSpeed = 0;
                Position = new Vector2(sprite.Position.X + sprite.Rectangle.Width, Position.Y);
            }
        }

        public override void Update(GameTime gameTime)
        {
            Speed = new Vector2(Speed.X - AdditionalSpeed, Speed.Y);
            
            base.Update(gameTime);
        }

    }
}
