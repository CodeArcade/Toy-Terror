using Microsoft.Xna.Framework;
using brackeys_2020_2_jam.Models;
using brackeys_2020_2_jam.Component.Controls;
using System;
using brackeys_2020_2_jam.Component.Sprites;
using System.Drawing;
using Microsoft.Xna.Framework.Input;

namespace brackeys_2020_2_jam.States
{
    public class MenuState : State
    {
        public override void Load()
        {
            base.Load();

            Size tempSize = new Size(500, 125);
            Sprite title = new Sprite()
            {
                Texture = ContentManager.ProgressBarBackground, //TODO
                Size = tempSize,
                Position = new Vector2((JamGame.ScreenWidth - tempSize.Width) / 2, 25)
            };
            Components.Add(title);

            Button button = new Button(ContentManager.ButtonTexture, ContentManager.ButtonFont);
            button.Position = new Vector2((JamGame.ScreenWidth - button.Texture.Width) / 2, title.Position.Y + title.Size.Height + 50);
            button.OnClick += StartButtonClicked;
            Components.Add(button);

            tempSize = new Size(350, 350);
            Sprite controls1 = new Sprite()
            {
                Texture = ContentManager.ProgressBarBackground, //TODO
                Size = tempSize,
                Position = new Vector2(tempSize.Width / 1.8f, button.Position.Y + button.Texture.Height + 50)
            };
            Components.Add(controls1);

            tempSize = new Size(350, 350);
            Sprite controls2 = new Sprite()
            {
                Texture = ContentManager.ProgressBarBackground, //TODO
                Size = tempSize,
                Position = new Vector2(JamGame.ScreenWidth - (tempSize.Width / 1.8f) - tempSize.Width, button.Position.Y + button.Texture.Height + 50)
            };
            Components.Add(controls2);
            
            AudioManager.ChangeSong(ContentManager.MenuMusic, true);
        }

        private void StartButtonClicked(object sender, EventArgs e)
        {
            ChangeToGame();
        }

        public override void Update(GameTime gameTime)
        {
            if(Keyboard.GetState().GetPressedKeyCount() > 0) ChangeToGame();
            base.Update(gameTime);
        }

        private void ChangeToGame()
        {
            StateManager.ChangeToGame();
            AudioManager.StopMusic();
        }

    }
}