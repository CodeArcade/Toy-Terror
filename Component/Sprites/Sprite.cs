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
                    if (AnimationManager.IsPlaying)
                        InternalSize = new Size(AnimationManager.AnimationRectangle.Width, AnimationManager.AnimationRectangle.Height);
                    else
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

        public int HitBoxXOffSet { get; set; }
        public int HitBoxYOffSet { get; set; }
        public Size HitboxSize { get; set; }

        public Rectangle Hitbox
        {
            get
            {
                if (HitboxSize == Size.Empty)
                    return new Rectangle((int)Position.X + HitBoxXOffSet, (int)Position.Y + HitBoxYOffSet, Size.Width, Size.Height);
                else
                    return new Rectangle((int)Position.X + HitBoxXOffSet, (int)Position.Y + HitBoxYOffSet, HitboxSize.Width, HitboxSize.Height);
            }
        }

        public virtual void OnCollision(Sprite sprite, GameTime gameTime)
        {
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {

            if (AnimationManager.IsPlaying)
            {
                AnimationManager.Draw(spriteBatch);
                ParticleManager.Draw(gameTime, spriteBatch);
                return;
            }

            spriteBatch.Draw(Texture, Rectangle, Color.White);
            ParticleManager.Draw(gameTime, spriteBatch);

        }

        public override void Update(GameTime gameTime)
        {
            AudioManager.Update();
            ParticleManager.Update(gameTime);

            if (AnimationManager.IsPlaying) AnimationManager.Update(gameTime);
        }

        #region Collision

        protected bool IsTouchingRight(Sprite sprite)
        {
            return Hitbox.Left < sprite.Hitbox.Right && Hitbox.Left > sprite.Hitbox.Left &&  // Sides collide
                  !IsAbove(sprite) && !IsBelow(sprite) && !IsLeft(sprite);
        }

        protected bool IsTouchingLeft(Sprite sprite)
        {
            return Hitbox.Right > sprite.Hitbox.Left && Hitbox.Right < sprite.Hitbox.Right && // Sides collide
                   !IsAbove(sprite) && !IsBelow(sprite) && !IsRight(sprite);
        }

        protected bool IsTouchingBottom(Sprite sprite)
        {
            return Hitbox.Top < sprite.Hitbox.Bottom && Hitbox.Top > sprite.Hitbox.Top && // Sides collide
                   !IsRight(sprite) && !IsLeft(sprite) && IsBelow(sprite);
        }

        protected bool IsTouchingTop(Sprite sprite)
        {
            return Hitbox.Bottom > sprite.Hitbox.Top && Hitbox.Bottom < sprite.Hitbox.Bottom && // Sides collide
                   !IsRight(sprite) && !IsLeft(sprite) && IsAbove(sprite);
        }

        private bool IsAbove(Sprite sprite)
        { return Hitbox.Bottom + -Speed.Y <= sprite.Hitbox.Top + sprite.HitBoxYOffSet; }
        private bool IsBelow(Sprite sprite)
        { return Hitbox.Top + -Speed.Y >= sprite.Hitbox.Bottom - sprite.HitBoxYOffSet; }
        private bool IsRight(Sprite sprite)
        { return Hitbox.Left + Speed.X >= sprite.Hitbox.Right; }
        private bool IsLeft(Sprite sprite)
        { return Hitbox.Right + Speed.X <= sprite.Hitbox.Left; }

        #endregion

    }
}
