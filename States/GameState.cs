using brackeys_2020_2_jam.Component.Controls;
using brackeys_2020_2_jam.Component.Sprites;
using brackeys_2020_2_jam.Component.Sprites.Environment;
using brackeys_2020_2_jam.Component.Sprites.Obstacles;
using brackeys_2020_2_jam.Factory;
using brackeys_2020_2_jam.Manager;
using brackeys_2020_2_jam.Models;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Linq;
using Unity;

namespace brackeys_2020_2_jam.States
{
    public class GameState : State
    {
        [Dependency]
        public ObstacleFactory ObstacleFactory { get; set; }

        public float ConveyorSpeed { get; set; } = 0f;
        public bool GameStarted { get; set; } = false;
        public bool GameEnded { get; set; } = false;
        public double GameStartTimer { get; set; } = 0;
        private double SpawnTimer { get; set; }
        private double SpawnIntervall { get; set; } = 6;
        public int Level { get; private set; }
        public double LevelTimer { get; set; }

        private Player Player;
        private Progressbar Progressbar;
        private Sprite ConveyorHitBox;
        private Conveyor Conveyor;
        private Clock Clock;

        private SoundEffectInstance ConveyorSoundEffect;

#if DEBUG
        private List<Component.Component> debugComponents;
#endif
        public override void Load()
        {
            base.Load();

            ConveyorSoundEffect = ContentManager.ConveyorSoundEffect.CreateInstance();
            ConveyorSoundEffect.Volume = 0.4f;

            Player = new Player(new PlayerInput() { Jump = Keys.W, Left = Keys.A, Right = Keys.D, Windup = Keys.Space })
            {
                MaxSpeed = new Vector2(10, 10),
                MaxAcceleration = 3,
                Acceleration = 1f,
                ConveyorSpeed = ConveyorSpeed
            };

            Player.Position = new Vector2(1000, 470 - Player.Size.Height);

            Progressbar = new Progressbar(Player, new System.Drawing.Size(80, 20))
            {
                MaxValue = Player.ALIVE_MAX,
                Value = 0
            };

            ConveyorHitBox = new Sprite() { Texture = ContentManager.ProgressBarBackground, Position = new Vector2(JamGame.ScreenWidth - 1000, 470), Size = new System.Drawing.Size(2000, 80) };
            Conveyor = new Conveyor() { Position = new Vector2(JamGame.ScreenWidth - 1080, 400), Size = new System.Drawing.Size(2000, 100) };

            Clock = new Clock(ContentManager.Clock, 30) { Position = new Vector2(JamGame.ScreenWidth - 592, 115), Size = new System.Drawing.Size(175, 175) };
            Components.Add(Clock);
            // Häcksler
            Components.Add(new Chopper() { Position = new Vector2(25, 600), Size = new System.Drawing.Size(JamGame.ScreenWidth - 900, 600) });

            Components.Add(Progressbar);
            Components.Add(ConveyorHitBox);
            Components.Add(Conveyor);
            Components.Add(Player);

            GameStarted = false;
            GameStartTimer = 0;
            Conveyor.AnimationManager.Animation.FrameSpeed = 999999;

            Level = 1;

            AddDebugInfo();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            if (Player.Position.X > JamGame.ScreenWidth - Player.Rectangle.Width) Player.Position = new Vector2(JamGame.ScreenWidth - Player.Rectangle.Width, Player.Position.Y);

            PlayerUpdate();
            CollisionCheck(gameTime);
            if (GameStarted && !GameEnded)
            {
                Clock.Run = true;
                SpawnTimer += gameTime.ElapsedGameTime.TotalSeconds;
                LevelTimer += gameTime.ElapsedGameTime.TotalSeconds;
                HandleLevel();
                HandleSpawnTimer();
            }
            else HandleGameStart(gameTime);

            UpdateDebugInfo();
        }

        private void HandleLevel()
        {
            if (LevelTimer >= 0 && LevelTimer < 30)
                Level = 1;
            else if (LevelTimer >= 30 && LevelTimer < 60)
                Level = 2;
            else if (LevelTimer >= 60 && LevelTimer < 90)
                Level = 3;
            else if (LevelTimer >= 90 && LevelTimer < 120)
                Level = 4;
            else if (LevelTimer >= 120 && LevelTimer < 150)
                Level = 5;
            else if (LevelTimer >= 150 && LevelTimer < 180)
                Level = 6;
            else if (LevelTimer >= 180 && LevelTimer < 181)
            {
                Level = 7;
                ConveyorSpeed = 6;
                Clock.Run = false;
                Conveyor.AnimationManager.Animation.FrameSpeed = 0.05f;
            }
            else if (LevelTimer >= 182 && LevelTimer < 183)
            { ConveyorSpeed = 5; Conveyor.AnimationManager.Animation.FrameSpeed = 0.1f; }
            else if (LevelTimer >= 184 && LevelTimer < 185)
            { ConveyorSpeed = 4; Conveyor.AnimationManager.Animation.FrameSpeed = 0.15f; }
            else if (LevelTimer >= 186 && LevelTimer < 187)
            { ConveyorSpeed = 3; Conveyor.AnimationManager.Animation.FrameSpeed = 0.2f; }
            else if (LevelTimer >= 188 && LevelTimer < 189)
            { ConveyorSpeed = 2; Conveyor.AnimationManager.Animation.FrameSpeed = 0.25f; }
            else if (LevelTimer >= 190 && LevelTimer < 191)
            { ConveyorSpeed = 1; Conveyor.AnimationManager.Animation.FrameSpeed = 0.3f; }
            else if (LevelTimer >= 192 && LevelTimer < 193)
            { ConveyorSpeed = 0; Conveyor.AnimationManager.Animation.FrameSpeed = 999999f; }
            else if (LevelTimer >= 197)
            { HandleGameEnd(); StateManager.ChangeToEndGameWin(); return; }

            switch (Level)
            {
                case 1:
                    ConveyorSpeed = 2;
                    SpawnIntervall = 5;
                    Player.AliveDrain = 1.2f;
                    Conveyor.AnimationManager.Animation.FrameSpeed = 0.3f;
                    break;
                case 2:
                    ConveyorSpeed = 3;
                    SpawnIntervall = 4;
                    Player.AliveDrain = 1.4f;
                    Conveyor.AnimationManager.Animation.FrameSpeed = 0.25f;
                    break;
                case 3:
                    ConveyorSpeed = 4;
                    SpawnIntervall = 3;
                    Player.AliveDrain = 1.6f;
                    Conveyor.AnimationManager.Animation.FrameSpeed = 0.2f;
                    break;
                case 4:
                    ConveyorSpeed = 5;
                    SpawnIntervall = 2.5;
                    Player.AliveDrain = 1.8f;
                    Conveyor.AnimationManager.Animation.FrameSpeed = 0.15f;
                    break;
                case 5:
                    ConveyorSpeed = 6;
                    SpawnIntervall = 2;
                    Player.AliveDrain = 2f;
                    Conveyor.AnimationManager.Animation.FrameSpeed = 0.1f;
                    break;
                case 6:
                    ConveyorSpeed = 7;
                    SpawnIntervall = 2;
                    Player.AliveDrain = 2.2f;
                    Conveyor.AnimationManager.Animation.FrameSpeed = 0.5f;
                    break;
                default:
                    SpawnIntervall = 0;
                    Player.AliveDrain = 1;
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

        private void HandleGameStart(GameTime gameTime)
        {
            if (!GameStarted && Player.AliveTimer > 0 && !GameEnded)
            {
                GameStartTimer += gameTime.ElapsedGameTime.TotalSeconds;
                if (GameStartTimer > 3)
                {
                    GameStarted = true;
                    AudioManager.PlayEffect(ContentManager.MotorStartSoundEffect);
                    ConveyorSoundEffect.Play();
                    AudioManager.ChangeSong(ContentManager.GameMusic, volume: 0.3f);
                }
            }
        }

        private void HandleGameEnd()
        {
            GameEnded = false;
            AudioManager.StopMusic();
            ConveyorSoundEffect.Stop();
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
            if (SpawnIntervall == 0) return;

            StaticObstacle obstacle = new StaticObstacle();

            switch (Level)
            {
                case 1:
                    obstacle = ObstacleFactory.GetStaticObstacle();
                    break;

                case 2:
                    obstacle = ObstacleFactory.GetStickyObstacle();
                    break;

                case 3:
                    obstacle = ObstacleFactory.GetStaticOrStickyObstacle();
                    break;

                case 4:
                    obstacle = ObstacleFactory.GetRandomObstacle();
                    break;

                case 5:
                    obstacle = ObstacleFactory.GetRandomObstacle();
                    break;

                case 6:
                    obstacle = ObstacleFactory.GetMovingOrStickyObstacle();
                    break;
            }
            obstacle.Position = new Vector2(JamGame.ScreenWidth, ConveyorHitBox.Position.Y - ConveyorHitBox.Size.Height - obstacle.Hitbox.Height);
            Components.Add(obstacle);
        }

        public override void PostUpdate(GameTime gameTime)
        {
            foreach (Component.Component component in Components)
            {
                if (component is Chopper) continue;
                if (component.Position.X < 0 - 1000 || component.Position.Y > JamGame.ScreenHeight) component.IsRemoved = true;
            }

            if (Player.IsRemoved) { HandleGameEnd(); StateManager.ChangeToEndGameLose(); }

            base.PostUpdate(gameTime);
        }

        #region Debug

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(ContentManager.ProgressBarBackground, new Rectangle(0, 0, JamGame.ScreenWidth, JamGame.ScreenHeight), Color.White);

            spriteBatch.Draw(ContentManager.Background, new Rectangle(0, 0, JamGame.ScreenWidth, JamGame.ScreenHeight), Color.White);

            if (Components is null) return;
            foreach (Component.Component component in Components)
            {
#if DEBUG
                if (component is Sprite sprite)
                    spriteBatch.Draw(ContentManager.ProgressBarValue, sprite.Hitbox, Color.White);
#endif
                component.Draw(gameTime, spriteBatch);
            }

            Player.Draw(gameTime, spriteBatch);

            spriteBatch.Draw(ContentManager.Vignette, new Rectangle(0, 0, JamGame.ScreenWidth, JamGame.ScreenHeight), Color.White);
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
            ((Label)debugComponents[6]).Text = $"Alive Timer: {Player.AliveTimer}";
            ((Label)debugComponents[7]).Text = $"Level: {Level}";
            ((Label)debugComponents[8]).Text = $"Time: {LevelTimer}";
            ((Label)debugComponents[9]).Text = $"Next Spawn: {SpawnTimer}";
            ((Label)debugComponents[10]).Text = $"Conveyor Speed: {Player.ConveyorSpeed}";
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
                    Text = $"Alive Timer: {Player.AliveTimer}",
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
                },

                new Label(ContentManager.ButtonFont)
                {
                    Text = $"Converyor Speed: {Player.ConveyorSpeed}",
                    Position = new Vector2(0, 155)
                }
            };
#endif
        }

        #endregion
    }
}
