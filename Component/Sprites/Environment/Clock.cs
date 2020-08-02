using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace brackeys_2020_2_jam.Component.Sprites.Environment
{
    public class Clock : Sprite
    {
        public List<Texture2D> ClockStates { get; private set; }
        public int SecondsBetweenState { get; private set; }
        public int CurrentClockState { get; private set; }
        public bool Run { get; set; } = false;

        private double Timer { get; set; }

        public Clock(List<Texture2D> clockStates, int secondsBetweenStates)
        {
            ClockStates = clockStates;
            SecondsBetweenState = secondsBetweenStates;
            CurrentClockState = 0;
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            Texture = ClockStates[CurrentClockState];
            base.Draw(gameTime, spriteBatch);
        }

        public override void Update(GameTime gameTime)
        {
            if (!Run) return;
            Timer += gameTime.ElapsedGameTime.TotalSeconds;

            if(Timer >= SecondsBetweenState)
            {
                CurrentClockState++;
                if (CurrentClockState >= ClockStates.Count) CurrentClockState = 0;
                Timer = 0;
            }

            base.Update(gameTime);
        }
    }
}
