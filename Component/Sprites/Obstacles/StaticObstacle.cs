using Microsoft.Xna.Framework;

namespace brackeys_2020_2_jam.Component.Sprites.Obstacles
{
    public class StaticObstacle : Sprite
    {
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            Position = new Vector2(Position.X - Speed.X, Position.Y);
        }
    }
}
