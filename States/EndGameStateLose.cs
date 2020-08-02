using brackeys_2020_2_jam.Models;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace brackeys_2020_2_jam.States
{
    public class EndGameStateLose : State
    {
        public override void Update(GameTime gameTime)
        {
            StateManager.ChangeToMenu();
        }
    }
}
