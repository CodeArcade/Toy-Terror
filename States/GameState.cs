using brackeys_2020_2_jam.Component.Controls;
using brackeys_2020_2_jam.Component.Sprites;
using brackeys_2020_2_jam.Component.Sprites.Environment;
using brackeys_2020_2_jam.Component.Sprites.Obstacles;
using brackeys_2020_2_jam.Factory;
using brackeys_2020_2_jam.Manager;
using brackeys_2020_2_jam.Models;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using Unity;

namespace brackeys_2020_2_jam.States
{
    public class GameState : State
    {
        [Dependency]
        public ObstacleFactory ObstacleFactory { get; set; }

        private double SpawnTimer { get; set; }
        private double SpawnIntervall { get; set; } = 6;
        private float ConveyorSpeed { get; set; } = 2f;
        private int Level { get; set; }
        private double LevelTimer { get; set; }

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
                Position = new Vector2(500, 250),
                ConveyorSpeed = ConveyorSpeed
            };

            Progressbar = new Progressbar(Player, new System.Drawing.Size(80, 20))
            {
                MaxValue = Player.ALIVE_MAX,
                Value = 0
            };

            Conveyor = new Sprite() { Texture = ContentManager.ButtonTexture, Position = new Vector2(JamGame.ScreenWidth - 1000, 450), Size = new System.Drawing.Size(2000, 100) };

            Components.Add(new Clock(ContentManager.HurtParticles, 1) { Position = new Vector2(JamGame.ScreenWidth - 500, 150), Size = new System.Drawing.Size(100, 100) });
            // Häcksler
            Components.Add(new Chopper() { Position = new Vector2(0, 600), Size = new System.Drawing.Size(JamGame.ScreenWidth - 900, 600) });
            // Window
            Components.Add(new Clock(ContentManager.HurtParticles, 1) { Position = new Vector2(200, 100), Size = new System.Drawing.Size(200, 250) });

            Components.Add(Progressbar);
            Components.Add(Player);
            Components.Add(Conveyor);

            Level = 1;

            AddDebugInfo();
        }

        public override void Update(GameTime gameTime)
        {
            SpawnTimer += gameTime.ElapsedGameTime.TotalSeconds;
            LevelTimer += gameTime.ElapsedGameTime.TotalSeconds;

            base.Update(gameTime);

            PlayerUpdate();
            CollisionCheck(gameTime);
            HandleLevel();
            HandleSpawnTimer();
            UpdateDebugInfo();
        }

        private void HandleLevel()
        {
            if (LevelTimer >= 0 && LevelTimer < 30)
                Level = 1;
            else if (LevelTimer >= 30 && LevelTimer < 90)
                Level = 2;
            else if (LevelTimer >= 90 && LevelTimer < 180)
                Level = 3;

            switch (Level)
            {
                case 1:
                    ConveyorSpeed = 2;
                    SpawnIntervall = 10;
                    break;
                case 2:
                    ConveyorSpeed = 3;
                    SpawnIntervall = 7;
                    break;
                case 3:
                    ConveyorSpeed = 4;
                    SpawnIntervall = 5;
                    break;
                default:
                    ConveyorSpeed = 2;
                    SpawnIntervall = 10;
                    break;
            }
        }

        private void PlayerUpdate()
        {
            Progressbar.Value = Player.AliveTimer;
            Player.ConveyorSpeed = ConveyorSpeed;
        }

        private void CollisionCheck(GameTime gameTime)
        {
            IEnumerable<Sprite> sprites = Components.Where(x => x is Sprite).Select(x => x as Sprite);
            foreach (Sprite sprite in sprites)
            {
                if (sprite is StaticObstacle obstacle) obstacle.ConveyorSpeed = ConveyorSpeed;
                foreach (Sprite sprite2 in sprites)
                {
                    if (sprite == sprite2) continue;
                    if (sprite.Rectangle.Intersects(sprite2.Rectangle))
                    {
                        sprite.OnCollision(sprite2, gameTime);
                        sprite2.OnCollision(sprite, gameTime);
                    }
                }
            }
        }

        private void HandleSpawnTimer()
        {
            if (SpawnTimer > SpawnIntervall)
            {
                SpawnTimer = 0;
                Spawn();
            }
        }

        private void Spawn()
        {
            StaticObstacle obstacle = new StaticObstacle();

            switch (Level)
            {
                case 1:
                    obstacle = ObstacleFactory.GetStaticObstacle();
                    break;

                case 2:
                    obstacle = ObstacleFactory.GetStaticOrStickyObstacle();
                    break;

                case 3:
                    obstacle = ObstacleFactory.GetRandomObstacle();
                    break;

                default:
                    break;
            }
            obstacle.Position = new Vector2(1100, Conveyor.Position.Y - Conveyor.Size.Height);
            Components.Add(obstacle);
        }

        public override void PostUpdate(GameTime gameTime)
        {
            foreach (Component.Component component in Components)
            {
                if (component.Position.X < 0 || component.Position.Y > JamGame.ScreenHeight) component.IsRemoved = true;
            }

            if (Player.IsRemoved) StateManager.ChangeToMenu();

            base.PostUpdate(gameTime);
        }

        #region Debug

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            base.Draw(gameTime, spriteBatch);

#if DEBUG
            foreach (Component.Component c in debugComponents)
            {
                c.Draw(gameTime, spriteBatch);
            }
#endif
        }

        private void UpdateDebugInfo()
        {
#if DEBUG
            ((Label)debugComponents[0]).Text = $"Position: {Player.Position}";
            ((Label)debugComponents[1]).Text = $"Acceleration: {Player.CurrentAcceleration}X | {Player.FallAcceleration}Y";
            ((Label)debugComponents[2]).Text = $"Speed: {Player.Speed}";
            ((Label)debugComponents[3]).Text = $"In Air: {Player.IsInAir}";
            ((Label)debugComponents[4]).Text = $"Winding Up: {Player.IsWindingUp}";
            ((Label)debugComponents[5]).Text = $"On Conveyor: {Player.IsOnConveyor}";
            ((Label)debugComponents[6]).Text = $"On Conveyor: {Player.IsOnConveyor}";
            ((Label)debugComponents[7]).Text = $"Level: {Level}";
            ((Label)debugComponents[8]).Text = $"Time: {LevelTimer}";
            ((Label)debugComponents[9]).Text = $"Next Spawn: {SpawnTimer}";
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
                },

                new Label(ContentManager.ButtonFont)
                {
                    Text = $"Winding Up: {Player.IsWindingUp}",
                    Position = new Vector2(0, 60)
                },

                new Label(ContentManager.ButtonFont)
                {
                    Text = $"On Conveyor: {Player.IsOnConveyor}",
                    Position = new Vector2(0, 75)
                },

                new Label(ContentManager.ButtonFont)
                {
                    Text = $"Level: {Level}",
                    Position = new Vector2(0, 95)
                },

                new Label(ContentManager.ButtonFont)
                {
                    Text = $"Level: {Level}",
                    Position = new Vector2(0, 110)
                },

                new Label(ContentManager.ButtonFont)
                {
                    Text = $"Time: {LevelTimer}",
                    Position = new Vector2(0, 125)
                },

                new Label(ContentManager.ButtonFont)
                {
                    Text = $"Next Spawn: {SpawnTimer}",
                    Position = new Vector2(0, 140)
                }
            };
#endif
        }

        #endregion
    }
}
