using brackeys_2020_2_jam.Component.Controls;
using brackeys_2020_2_jam.Component.Sprites;
using brackeys_2020_2_jam.Models;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace brackeys_2020_2_jam.States
{
    public class GameState : State
    {

        private Player Player;
        private Progressbar Progressbar;
#if DEBUG
        private List<Component.Component> debugComponents;
#endif
        public override void Load()
        {
            base.Load();
            Player = new Player(new PlayerInput() { Jump = Keys.W, Left = Keys.A, Right = Keys.D, Windup = Keys.Space })
            {
                MaxSpeed = new Vector2(10, 10),
                MaxAcceleration = 5,
                Acceleration = 0.1f,
                Position = new Vector2(250, 250)
            };

            Progressbar = new Progressbar(Player, new System.Drawing.Size(80, 20))
            {
                MaxValue = Player.ALIVE_MAX,
                Value = 0
            };

            Components.Add(Progressbar);
            Components.Add(Player);

            Components.Add(new Sprite() { Texture = ContentManager.ButtonTexture, Position = new Vector2(20, 400) });
            
            AddDebugInfo();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            Progressbar.Value = Player.AliveTimer;

            foreach (Sprite sprite in Components.Where(x => x is Sprite && x != Player).Select(x => x))
            {
                if (Player.Rectangle.Intersects(sprite.Rectangle)) Player.OnCollision(sprite);
            }

            UpdateDebugInfo();
        }

#if DEBUG
        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            foreach (Component.Component c in debugComponents)
            {
                c.Draw(gameTime, spriteBatch);
            }
            base.Draw(gameTime, spriteBatch);
        }
#endif

        private void UpdateDebugInfo()
        {
#if DEBUG
            ((Label)debugComponents[0]).Text = $"Position: {Player.Position.X}X; {Player.Position.Y}Y";
            ((Label)debugComponents[1]).Text = $"Acceleration: {Player.CurrentAcceleration}X | {Player.FallAcceleration}Y";
            ((Label)debugComponents[1]).Text = $"Speed: {Player.Speed}X";
            ((Label)debugComponents[1]).Text = $"In Air: {Player.IsInAir}X";
#endif
        }

        private void AddDebugInfo()
        {
#if DEBUG
            debugComponents = new List<Component.Component>();
            debugComponents.Add(new Label(ContentManager.ButtonFont)
            {
                Text = $"Position: {Player.Position.X}X | {Player.Position.Y}Y",
                Position = new Vector2(0, 0)
            });

            debugComponents.Add(new Label(ContentManager.ButtonFont)
            {
                Text = $"Acceleration: {Player.CurrentAcceleration}X | {Player.FallAcceleration}Y",
                Position = new Vector2(0, 15)
            });

            debugComponents.Add(new Label(ContentManager.ButtonFont)
            {
                Text = $"Speed: {Player.Speed}X",
                Position = new Vector2(0, 30)
            });

            debugComponents.Add(new Label(ContentManager.ButtonFont)
            {
                Text = $"In Air: {Player.IsInAir}X",
                Position = new Vector2(0, 45)
            });
#endif
        }

    }
}
