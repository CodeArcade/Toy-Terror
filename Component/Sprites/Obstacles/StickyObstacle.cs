using Microsoft.Xna.Framework;

namespace brackeys_2020_2_jam.Component.Sprites.Obstacles
{
    public class StickyObstacle : StaticObstacle
    {
        private double Timer { get; set; }
        public float Damage { get; set; }
        public double ImmunityDuration { get; set; }

        public StickyObstacle(float damage, double immunityDuration)
        {
            Damage = damage;
            ImmunityDuration = immunityDuration;
            Timer = ImmunityDuration + 1; // Set timer to max to instantly trigger damage
        }

        public override void OnCollision(Sprite sprite, GameTime gameTime)
        {
            base.OnCollision(sprite, gameTime);

            if (sprite.GetType() != typeof(Player)) return;

            Timer += gameTime.ElapsedGameTime.TotalSeconds;

            Player player = (Player)sprite;
            if (Timer < ImmunityDuration) return;

            player.AliveTimer -= Damage;
            Timer = 0;
        }

    }
}
