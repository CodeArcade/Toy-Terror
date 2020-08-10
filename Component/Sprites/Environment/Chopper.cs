using brackeys_2020_2_jam.Component.Sprites.Obstacles;
using brackeys_2020_2_jam.Models;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Color = Microsoft.Xna.Framework.Color;

namespace brackeys_2020_2_jam.Component.Sprites.Environment
{
    public class Chopper : Sprite
    {
        public double ChopperSoundEffectInterval { get; set; } = 0.025f * 20;
        public double ChopperSoundEffectTimer { get; set; }

        public Chopper()
        {
            Texture = ContentManager.ProgressBarBackground;
            AnimationManager.Scale = 0.4f;
            AnimationManager.Parent = this;

            AnimationManager.Play(new Animation(ContentManager.ChopperAnimation, 15) { FrameSpeed = 0.025f });
            ParticleManager.Textures = ContentManager.HurtParticles;

            HitboxSize = this.Size;
            HitBoxYOffSet = 50;
        }

        public override void Update(GameTime gameTime)
        {
            ChopperSoundEffectTimer += gameTime.ElapsedGameTime.TotalSeconds;
            base.Update(gameTime);

            if(ChopperSoundEffectTimer >= ChopperSoundEffectInterval)
            {
                AudioManager.PlayEffect(ContentManager.ChopperSoundEffect, 0.1f);
                ChopperSoundEffectTimer = 0;
            }
        }

        public override void OnCollision(Sprite sprite, GameTime gameTime)
        {
            base.OnCollision(sprite, gameTime);

            if (sprite is StaticObstacle)
            {
                sprite.IsRemoved = true;
                ParticleManager.EmitterLocation = new Vector2(sprite.Position.X + (sprite.Rectangle.Width / 2), sprite.Position.Y + sprite.Rectangle.Height);
                ParticleManager.GenerateNewParticle(Color.White, 20);
                AudioManager.PlayEffect(ContentManager.GrindingSoundEffect, 0.1f);
            }
        }
    }
}
