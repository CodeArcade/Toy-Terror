using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;

namespace brackeys_2020_2_jam.Component.Sprites.Environment
{
    public class Clock : Sprite
    {
        public List<Texture2D> ClockStates { get; private set; }
        public int SecondsBetweenState { get; private set; }
        public int CurrentClockState { get; private set; }
        public bool Run { get; set; } = false;
        public event EventHandler Tick;

        private double Timer { get; set; }

        public Clock(List<Texture2D> clockStates, int secondsBetweenStates)
        {
            ClockStates = clockStates;
            Texture = ClockStates.First();
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
                AudioManager.PlayEffect(ContentManager.ClockSoundEffect, 0.5f);
                if (CurrentClockState >= ClockStates.Count) CurrentClockState = 0;
                Timer = 0;
                Tick?.Invoke(this, new EventArgs());
            }

            base.Update(gameTime);
        }
    }
}
