using brackeys_2020_2_jam.Manager;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;

namespace brackeys_2020_2_jam.Component.Sprites.Obstacles
{
    public class StickyObstacle : StaticObstacle
    {
        public float Damage { get; set; }
        public double ImmunityDuration { get; set; }

        public StickyObstacle(float damage)
        {
            Damage = damage;
        }

        public override void OnCollision(Sprite sprite, GameTime gameTime)
        {
            base.OnCollision(sprite, gameTime);

            if (sprite.GetType() != typeof(Player)) return;

            Player player = (Player)sprite;

            if (!player.CanTakeDamage) return;
            if (!player.Rectangle.Intersects(Hitbox)) return;

            if (ParticleManager.Textures is null) ParticleManager.Textures = ContentManager.HurtParticles;
            ParticleManager.EmitterLocation = player.Rectangle.Center.ToVector2();
            ParticleManager.GenerateNewParticle(Color.White, 20, 10);

            AudioManager.PlayEffect(ContentManager.HurtSoundEffect);
            player.AliveTimer -= Damage;
            if (player.AliveTimer < 0)
                player.AliveTimer = 0;
            player.IFramesTimer = 0;
        }

    }
}
