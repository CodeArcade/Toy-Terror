using Microsoft.Xna.Framework;
using brackeys_2020_2_jam.Models;
using brackeys_2020_2_jam.Component.Controls;
using System;

namespace brackeys_2020_2_jam.States
{
    public class MenuState : State
    {
        Progressbar progressbar;

        public override void Load()
        {
            base.Load();

            Button button = new Button(ContentManager.ButtonTexture, ContentManager.ButtonFont)
            {
                Text = "Start Game",
                Position = new Vector2(0, 50)
            };
            button.OnClick += StartButtonClicked;

            Components.Add(button);

            progressbar = new Progressbar(button, new System.Drawing.Size(80, 20))
            {
                MaxValue = 100,
                Value = 100
            };
            Components.Add(progressbar);

            Label label = new Label(ContentManager.ButtonFont)
            {
                Text = "Game Title Here",
                Position = new Vector2(0, 0)
            };
            Components.Add(label);
        }

        private void StartButtonClicked(object sender, EventArgs e)
        {
            StateManager.ChangeToGame();
        }

        public override void Update(GameTime gameTime)
        {
            progressbar.Value -= 0.3f;
            if (progressbar.Value == 0) progressbar.Value = 100;
            base.Update(gameTime);
        }
    }

}