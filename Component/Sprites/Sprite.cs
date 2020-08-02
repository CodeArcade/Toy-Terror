using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Drawing;
using Color = Microsoft.Xna.Framework.Color;
using Rectangle = Microsoft.Xna.Framework.Rectangle;

namespace brackeys_2020_2_jam.Component.Sprites
{
    public class Sprite : Component
    {
        private Size InternalSize { get; set; }

        public const float TERMINAL_VELOCITY = 25f;

        public Vector2 GRAVITY => new Vector2(0, 9.83f);
        public Vector2 Speed { get; set; }
        public virtual Vector2 MaxSpeed { get; set; }
        public Texture2D Texture { get; set; }

        public Size Size
        {
            get
            {
                if (InternalSize == Size.Empty)
                {
                    InternalSize = new Size(Texture.Width, Texture.Height);
                }
                return InternalSize;
            }
            set
            {
                InternalSize = value;
            }
        }

        public Rectangle Rectangle
        {
            get
            {
                Rectangle rectangle;

                if (AnimationManager.IsPlaying)
                {
                    rectangle = AnimationManager.AnimationRectangle;
                    rectangle.Width = Size.Width;
                    rectangle.Height = Size.Height;
                }
                else
                    rectangle = new Rectangle((int)Position.X, (int)Position.Y, Size.Width, Size.Height);

                return rectangle;
            }
        }

        public virtual void OnCollision(Sprite sprite, GameTime gameTime)
        {
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            ParticleManager.Draw(gameTime, spriteBatch);

            if (AnimationManager.IsPlaying)
            {
                AnimationManager.Draw(spriteBatch);
                return;
            }

            spriteBatch.Draw(Texture, Rectangle, Color.White);
        }

        public override void Update(GameTime gameTime)
        {
            AudioManager.Update();
            ParticleManager.Update(gameTime);

            if (AnimationManager.IsPlaying) AnimationManager.Update(gameTime);
        }

        #region Collision

        protected bool IsTouchingLeft(Sprite sprite)
        {
            return Rectangle.Left < sprite.Rectangle.Right && // Sides collide
                  !IsAbove(sprite) && !IsBelow(sprite) && !IsLeft(sprite);
        }

        protected bool IsTouchingRight(Sprite sprite)
        {
            return Rectangle.Right > sprite.Rectangle.Left && // Sides collide
                   !IsAbove(sprite) && !IsBelow(sprite) && !IsRight(sprite); ;
        }

        protected bool IsTouchingBottom(Sprite sprite)
        {
            return Rectangle.Top < sprite.Rectangle.Bottom && // Sides collide
                   !IsRight(sprite) && !IsLeft(sprite) && IsBelow(sprite);
        }

        protected bool IsTouchingTop(Sprite sprite)
        {
            return Rectangle.Bottom > sprite.Rectangle.Top && // Sides collide
                   !IsRight(sprite) && !IsLeft(sprite) && IsAbove(sprite);
        }

        private bool IsAbove(Sprite sprite)
        { return Rectangle.Bottom + -Speed.Y < sprite.Rectangle.Top; }
        private bool IsBelow(Sprite sprite)
        { return Rectangle.Top + -Speed.Y > sprite.Rectangle.Bottom; }
        private bool IsRight(Sprite sprite)
        { return Rectangle.Left + -Speed.X > sprite.Rectangle.Right; }
        private bool IsLeft(Sprite sprite)
        { return Rectangle.Right + -Speed.X < sprite.Rectangle.Left; }

        #endregion

    }
}
