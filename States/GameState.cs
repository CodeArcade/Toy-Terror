﻿using brackeys_2020_2_jam.Component;
using brackeys_2020_2_jam.Component.Controls;
using brackeys_2020_2_jam.Component.Sprites;
using brackeys_2020_2_jam.Component.Sprites.Obstacles;
using brackeys_2020_2_jam.Manager;
using brackeys_2020_2_jam.Models;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;

namespace brackeys_2020_2_jam.States
{
    public class GameState : State
    {
        private double Timer { get; set; }
        private double SpawnIntervall { get; set; } = 5;
        private Random Random { get; } = new Random();

        private Player Player;
        private Progressbar Progressbar;
        private Sprite Conveyor;
#if DEBUG
        private List<Component.Component> debugComponents;
#endif
        public override void Load()
        {
            base.Load();

            Player = new Player(new PlayerInput() { Jump = Keys.W, Left = Keys.A, Right = Keys.D, Windup = Keys.Space })
            {
                MaxSpeed = new Vector2(10, 10),
                MaxAcceleration = 3,
                Acceleration = 1f,
                Position = new Vector2(250, 250)
            };

            Progressbar = new Progressbar(Player, new System.Drawing.Size(80, 20))
            {
                MaxValue = Player.ALIVE_MAX,
                Value = 0
            };

            Conveyor = new Sprite() { Texture = ContentManager.ButtonTexture, Position = new Vector2(20, 600) };

            Components.Add(Progressbar);
            Components.Add(Player);
            Components.Add(Conveyor);

            AddDebugInfo();
        }

        public override void Update(GameTime gameTime)
        {
            Timer += gameTime.ElapsedGameTime.TotalSeconds;

            base.Update(gameTime);

            Progressbar.Value = Player.AliveTimer;

            IEnumerable<Sprite> sprites = (IEnumerable<Sprite>)Components.Where(x => x is Sprite).Select(x => x as Sprite);
            foreach (Sprite sprite in sprites)
            {
                foreach (Sprite sprite2 in sprites)
                {
                    if (sprite == sprite2) continue;
                    if (sprite.Rectangle.Intersects(sprite2.Rectangle)) sprite.OnCollision(sprite2, gameTime);
                }
            }

            if (Timer > SpawnIntervall)
            {
                Timer = 0;
                Spawn();
            }

            UpdateDebugInfo();
        }

        private void Spawn()
        {

            Components.Add(new StickyObstacle()
            {
                Texture = ContentManager.ButtonTexture,
                Position = new Vector2(Conveyor.Position.X + (Conveyor.Rectangle.Width * 4), Conveyor.Position.Y - Conveyor.Rectangle.Height),
                Speed = new Vector2(3, 0),
                ImmunityDuration = 1,
                StickDuration = 3
            });

        }

        public override void PostUpdate(GameTime gameTime)
        {
            foreach(Component.Component component in Components)
            {
                if (component.Position.X < 0) component.IsRemoved = true;
            }

            base.PostUpdate(gameTime);
        }

        #region Debug

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
            ((Label)debugComponents[0]).Text = $"Position: {Player.Position}";
            ((Label)debugComponents[1]).Text = $"Acceleration: {Player.CurrentAcceleration}X | {Player.FallAcceleration}Y";
            ((Label)debugComponents[2]).Text = $"Speed: {Player.Speed}";
            ((Label)debugComponents[3]).Text = $"In Air: {Player.IsInAir}";
#endif
        }

        private void AddDebugInfo()
        {
#if DEBUG
            debugComponents = new List<Component.Component>
            {
                new Label(ContentManager.ButtonFont)
                {
                    Text = $"Position: {Player.Position}",
                    Position = new Vector2(0, 0)
                },

                new Label(ContentManager.ButtonFont)
                {
                    Text = $"Acceleration: {Player.CurrentAcceleration}X | {Player.FallAcceleration}Y",
                    Position = new Vector2(0, 15)
                },

                new Label(ContentManager.ButtonFont)
                {
                    Text = $"Speed: {Player.Speed}",
                    Position = new Vector2(0, 30)
                },

                new Label(ContentManager.ButtonFont)
                {
                    Text = $"In Air: {Player.IsInAir}",
                    Position = new Vector2(0, 45)
                }
            };
#endif
        }

        #endregion
    }
}
