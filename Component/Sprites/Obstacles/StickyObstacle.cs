using brackeys_2020_2_jam.Manager;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;

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

            if (ParticleManager.Textures is null) ParticleManager.Textures = new List<Texture2D>() { ContentManager.ProgressBarValue };
            ParticleManager.EmitterLocation = player.Rectangle.Center.ToVector2();
            ParticleManager.GenerateNewParticle(Color.White, 10, 10);

            AudioManager.PlayEffect(ContentManager.HurtSoundEffect);
            player.AliveTimer -= Damage;
            Timer = 0;
        }

    }
}
