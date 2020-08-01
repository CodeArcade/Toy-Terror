using brackeys_2020_2_jam.Models;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace brackeys_2020_2_jam.Manager
{
    public class AnimationManager
    {
        private Animation Animation { get; set; }
        private float Timer { get; set; }

        public Vector2 Position { get; set; }
        public bool IsPlaying { get; set; }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Animation.Texture,
                             Position,
                             new Rectangle(Animation.CurrentFrame * Animation.FrameWidth,
                                           0,
                                           Animation.FrameWidth,
                                           Animation.FrameHeight),
                             Color.White);
        }

        public void Update(GameTime gameTime)
        {
            Timer += (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (Timer > Animation.FrameSpeed)
            {
                Timer = 0f;

                Animation.CurrentFrame++;

                if (Animation.CurrentFrame >= Animation.FrameCount) Animation.CurrentFrame = 0;
            }
        }

        public void Play(Animation animation)
        {
            if (Animation == animation) return;
            IsPlaying = true;

            Animation = animation;

            Animation.CurrentFrame = 0;

            Timer = 0;
        }

        public void Stop()
        {
            IsPlaying = false;

            Timer = 0f;

            Animation.CurrentFrame = 0;
        }

    }
}
