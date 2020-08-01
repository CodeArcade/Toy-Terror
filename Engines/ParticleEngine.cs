using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using brackeys_2020_2_jam.Component;

namespace brackeys_2020_2_jam.Engines
{
    public class ParticleEngine : Component.Component {
        private readonly Random Random;
        public Vector2 EmitterLocation { get; set; }
        private readonly List<Particle> Particles;
        private readonly List<Texture2D> Textures;

        public ParticleEngine(Vector2 emitterLocation, List<Texture2D> textures)
        {
            Random = new Random();
            Particles = new List<Particle>();
            EmitterLocation = emitterLocation;
            Textures = textures;
        }

        public void GenerateNewParticle(Color color) {
            Texture2D texture = Textures[Random.Next(Textures.Count)];
            Vector2 position = EmitterLocation;
            Vector2 velocity = new Vector2(
                1f * (float)(Random.NextDouble() * 2 - 1),
                1f * (float)(Random.NextDouble() * 2 - 1)
            );
            float angle = 0;
            float angularVelocity = 0.1f * (float)(Random.NextDouble() * 2 - 1);
            float size = (float) Random.NextDouble();
            int ttl = 20 + Random.Next(40);

            Particle p = new Particle(texture, position, velocity, angle, angularVelocity, color, size, ttl);
            Particles.Add(p);
        }

        public void Update() {

        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();

            spriteBatch.End();
        }

        public override void Update(GameTime gameTime)
        {
            for (int i = Particles.Count; i >= 0; i--) {
                Particles[i].Update(gameTime);
                if (Particles[i].TTL <= 0) {
                    Particles.RemoveAt(i);
                }
            }
        }
    }
}