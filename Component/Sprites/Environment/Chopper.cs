using brackeys_2020_2_jam.Component.Sprites.Obstacles;
using Microsoft.Xna.Framework;

namespace brackeys_2020_2_jam.Component.Sprites.Environment
{
    public class Chopper: Sprite
    {
        public Chopper()
        {
            Texture = ContentManager.ProgressBarBackground;
            AnimationManager.Scale = 0.2f;
            AnimationManager.Parent = this;
            Texture = ContentManager.ProgressBarBackground; // TODO:
                                                            // AnimationManager.Play(new Models.Animation(ContentManager.ProgressBarOutline, 1));
            ParticleManager.Textures = ContentManager.HurtParticles;
        }

        public override void OnCollision(Sprite sprite, GameTime gameTime)
        {
            base.OnCollision(sprite, gameTime);
            if (sprite is StaticObstacle)
            {
                sprite.IsRemoved = true;
                ParticleManager.EmitterLocation = new Vector2(sprite.Hitbox.X - (sprite.Hitbox.Width / 2), Position.Y);
                ParticleManager.GenerateNewParticle(Color.White, 20);
                AudioManager.PlayEffect(ContentManager.GrindingSoundEffect, 0.1f);
            }
        }
    }
}
