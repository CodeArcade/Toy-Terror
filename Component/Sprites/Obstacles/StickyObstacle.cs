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

        public StickyObstacle(float damage)
        {
            Damage = damage;
        }

        public override void OnCollision(Sprite sprite, GameTime gameTime)
        {
            base.OnCollision(sprite, gameTime);

            if (sprite.GetType() != typeof(Player)) return;


            Player player = (Player)sprite;
            player.IFramesTimer += gameTime.ElapsedGameTime.TotalSeconds;

            if (!player.CanTakeDamage) return;

            if (ParticleManager.Textures is null) ParticleManager.Textures = new List<Texture2D>() { ContentManager.ProgressBarValue };
            ParticleManager.EmitterLocation = player.Rectangle.Center.ToVector2();
            ParticleManager.GenerateNewParticle(Color.White, 10, 10);

            AudioManager.PlayEffect(ContentManager.HurtSoundEffect);
            player.AliveTimer -= Damage;
            player.IFramesTimer = 0;
        }

    }
}
