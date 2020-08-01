using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace brackeys_2020_2_jam.Component.Sprites
{
    public class Sprite : Component
    {
        public Vector2 Velocity { get; set; }
        public float MaxSpeed { get; protected set; }
        public float Speed { get; set; }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if (AnimationManager.IsPlaying)
            {
                AnimationManager.Draw(spriteBatch);
                return;
            }

            spriteBatch.Draw(Texture, Position, Color.White);
        }

        public override void Update(GameTime gameTime)
        {
            if (AnimationManager.IsPlaying) AnimationManager.Update(gameTime);
        }

    }
}
