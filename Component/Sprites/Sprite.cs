using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace brackeys_2020_2_jam.Component.Sprites
{
    public class Sprite : Component
    {
        public Vector2 GRAVITY = new Vector2(0, 9.8f);
        public Vector2 Speed { get; set; }
        public Vector2 MaxSpeed { get;  set; }

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
            return this.Rectangle.Right > sprite.Rectangle.Left &&
              this.Rectangle.Left < sprite.Rectangle.Left &&
              this.Rectangle.Bottom > sprite.Rectangle.Top &&
              this.Rectangle.Top < sprite.Rectangle.Bottom;
        }

        protected bool IsTouchingRight(Sprite sprite)
        {
            return this.Rectangle.Left < sprite.Rectangle.Right &&
              this.Rectangle.Right > sprite.Rectangle.Right &&
              this.Rectangle.Bottom > sprite.Rectangle.Top &&
              this.Rectangle.Top < sprite.Rectangle.Bottom;
        }

        protected bool IsTouchingTop(Sprite sprite)
        {
            return this.Rectangle.Bottom > sprite.Rectangle.Top &&
              this.Rectangle.Top < sprite.Rectangle.Top &&
              this.Rectangle.Right > sprite.Rectangle.Left &&
              this.Rectangle.Left < sprite.Rectangle.Right;
        }

        protected bool IsTouchingBottom(Sprite sprite)
        {
            return this.Rectangle.Top < sprite.Rectangle.Bottom &&
              this.Rectangle.Bottom > sprite.Rectangle.Bottom &&
              this.Rectangle.Right > sprite.Rectangle.Left &&
              this.Rectangle.Left < sprite.Rectangle.Right;
        }

        #endregion

    }
}
