using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace brackeys_2020_2_jam.Component
{
    public class Particle : Component
    {
        public Vector2 Velocity { get; set; }
        public float Angle { get; set; }
        public float AngularVelocity { get; set; }
        public Color Color { get; set; }
        public float Size { get; set; }
        public int TTL { get; set; }

        public Particle(Texture2D texture, Vector2 position, Vector2 velocity, float angle, float angularVelocity, Color color, float size, int ttl)
        {
            Texture = texture;
            Position = position;
            Velocity = velocity;
            Angle = angle;
            AngularVelocity = angularVelocity;
            Color = color;
            Size = size;
            TTL = ttl;
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            Rectangle sourceRectangle = new Rectangle(0, 0, Texture.Width, Texture.Height);
            Vector2 origin = new Vector2(Texture.Width / 2, Texture.Height / 2);

            spriteBatch.Draw(Texture, Position, sourceRectangle, Color,
            Angle, origin, Size, SpriteEffects.None, 0f);
        }

        public override void Update(GameTime gameTime)
        {
            TTL -= 1 * gameTime.ElapsedGameTime.Seconds;
            Position += Velocity * gameTime.ElapsedGameTime.Seconds;
            Angle += AngularVelocity * gameTime.ElapsedGameTime.Seconds;
        }
    }
}