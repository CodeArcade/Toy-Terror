using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace brackeys_2020_2_jam.Component.Controls
{
    public class Label : Component
    {
        public Color FontColor { get; set; }
        public SpriteFont Font { get; set; }
        public string Text { get; set; }

        public Label(SpriteFont font)
        {
            Font = font;
            FontColor = Color.Black;
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if (!string.IsNullOrEmpty(Text)) spriteBatch.DrawString(Font, Text, new Vector2(Position.X, Position.Y), FontColor);
        }

        public override void Update(GameTime gameTime) { }
    }
}
