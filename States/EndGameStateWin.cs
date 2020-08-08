using brackeys_2020_2_jam.Models;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace brackeys_2020_2_jam.States
{
    public class EndGameStateWin : State
    {
        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
              spriteBatch.Draw(ContentManager.Background, new Rectangle(0, 0, JamGame.ScreenWidth, JamGame.ScreenHeight), Color.White);
        }

        public override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Enter))
            {
                StateManager.ChangeToMenu();
            }
            base.Update(gameTime);
        }
    }
}
