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

        public Rectangle Rectangle
        {
            get
            {
                if (AnimationManager.IsPlaying) return AnimationManager.AnimationRectangle;

                return new Rectangle((int)Position.X, (int)Position.Y, Texture.Width, Texture.Height);
            }
        }

        public Texture2D Texture { get; set; }

        #region Collision

        protected bool IsTouchingLeft(Sprite sprite)
        {
            return this.Rectangle.Right + this.Velocity.X > sprite.Rectangle.Left &&
              this.Rectangle.Left < sprite.Rectangle.Left &&
              this.Rectangle.Bottom > sprite.Rectangle.Top &&
              this.Rectangle.Top < sprite.Rectangle.Bottom;
        }

        protected bool IsTouchingRight(Sprite sprite)
        {
            return this.Rectangle.Left + this.Velocity.X < sprite.Rectangle.Right &&
              this.Rectangle.Right > sprite.Rectangle.Right &&
              this.Rectangle.Bottom > sprite.Rectangle.Top &&
              this.Rectangle.Top < sprite.Rectangle.Bottom;
        }

        protected bool IsTouchingTop(Sprite sprite)
        {
            return this.Rectangle.Bottom + this.Velocity.Y > sprite.Rectangle.Top &&
              this.Rectangle.Top < sprite.Rectangle.Top &&
              this.Rectangle.Right > sprite.Rectangle.Left &&
              this.Rectangle.Left < sprite.Rectangle.Right;
        }

        protected bool IsTouchingBottom(Sprite sprite)
        {
            return this.Rectangle.Top + this.Velocity.Y < sprite.Rectangle.Bottom &&
              this.Rectangle.Bottom > sprite.Rectangle.Bottom &&
              this.Rectangle.Right > sprite.Rectangle.Left &&
              this.Rectangle.Left < sprite.Rectangle.Right;
        }

        #endregion

    }
}
