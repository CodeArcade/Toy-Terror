using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Drawing;
using Color = Microsoft.Xna.Framework.Color;
using Rectangle = Microsoft.Xna.Framework.Rectangle;

namespace brackeys_2020_2_jam.Component.Controls
{
    public class Progressbar : Component
    {
        private float InternalValue { get; set; }
        private float InternalMaxValue { get; set; } = 1;

        public float MaxValue
        {
            get => InternalMaxValue;
            set
            {
                InternalMaxValue = value;

                if (value < 1) InternalMaxValue = 1;
            }
        }
        public float Value
        {
            get => InternalValue;
            set
            {
                InternalValue = value;

                if (value < 0) InternalValue = 0;
                if (value > MaxValue) InternalValue = MaxValue;
            }
        }

        public Size Size { get; set; }

        private Texture2D OutlineTexture { get; set; }
        private Texture2D BackgroundTexture { get; set; }
        private Texture2D ValueTexture { get; set; }

        private int OffSet { get; set; } = 5;

        public Progressbar(Component parent, Size size)
        {
            if (parent is null) throw new ArgumentException("Parent of progress bar can not be null!");
            Parent = parent;
            Size = size;

            OutlineTexture = ContentManager.ProgressBarOutline;
            BackgroundTexture = ContentManager.ProgressBarBackground;
            ValueTexture = ContentManager.ProgressBarValue;
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(OutlineTexture, Position, Color.Black);

            Rectangle destinationRectangle = new Rectangle((int)Position.X + OffSet, (int)Position.Y + OffSet, Size.Width - (OffSet * 2), Size.Height - (OffSet * 2));
            spriteBatch.Draw(BackgroundTexture, destinationRectangle, Color.White);

            decimal sizeInPercent = (decimal)Value / (decimal)MaxValue;
            if (sizeInPercent > 1) sizeInPercent = 1;
            if (sizeInPercent <= 0)
                destinationRectangle = new Rectangle((int)Position.X + OffSet, (int)Position.Y + OffSet, 0, 0);
            else
                destinationRectangle = new Rectangle((int)Position.X + OffSet, (int)Position.Y + OffSet, (int)((Size.Width - (OffSet * 2)) * sizeInPercent), Size.Height - (OffSet * 2));
            
            spriteBatch.Draw(ValueTexture, destinationRectangle, Color.White);
        }

        public override void Update(GameTime gameTime)
        {
            Position = new Vector2(Parent.Position.X, Parent.Position.Y - 5 - Size.Height);
        }

    }
}
