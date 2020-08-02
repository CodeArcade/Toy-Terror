using Microsoft.Xna.Framework;

namespace brackeys_2020_2_jam.Component.Sprites.Obstacles
{
    public class StickyObstacle : StaticObstacle
    {
        private double Timer { get; set; }
        public double StickDuration { get; set; }
        public double ImmunityDuration { get; set; }

        public override void OnCollision(Sprite sprite, GameTime gameTime)
        {
            if (sprite.GetType() != typeof(Player)) return;
            Timer += gameTime.ElapsedGameTime.TotalSeconds;

            Player player = (Player)sprite;

            if (Timer < StickDuration)
            {
                player.Speed = Vector2.Zero;
            }
            else
            {
                if (Timer > StickDuration + ImmunityDuration) Timer = 0;
            }

        }

    }
}
