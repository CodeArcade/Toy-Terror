using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using brackeys_2020_2_jam.Models;
using brackeys_2020_2_jam.Component.Controls;
using System;

namespace brackeys_2020_2_jam.States
{
    public class MenuState : State
    {
        public override void Load()
        {
            base.Load();

            Button button = new Button(ContentManager.ButtonTexture, ContentManager.ButtonFont);
            button.OnClick += StartButtonClicked;
            button.Text = "Start Game";
            button.Position = new Vector2(0, 0);

            Components.Add(button);
        }

        private void StartButtonClicked(object sender, EventArgs e)
        {
            StateManager.ChangeToGame();
        }
    }

}