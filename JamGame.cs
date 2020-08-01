using brackeys_2020_2_jam.Manager;
using brackeys_2020_2_jam.Models;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Unity;

namespace brackeys_2020_2_jam
{
    public class JamGame : Game
    {
        private readonly GraphicsDeviceManager Graphics;
        private SpriteBatch SpriteBatch;

        public StateManager StateManager { get; set; }

        public JamGame()
        {
            Graphics = new GraphicsDeviceManager(this)
            {
               PreferredBackBufferHeight = 720,
                PreferredBackBufferWidth = 1280
            };
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            Window.Title = "Jam Game!";
            IsMouseVisible = true;
            
            base.Initialize();
        }

        protected override void LoadContent()
        {
            SpriteBatch = new SpriteBatch(GraphicsDevice);

            StateManager = Program.UnityContainer.Resolve<StateManager>();
            StateManager.ChangeToMenu();
        }

        protected override void Update(GameTime gameTime)
        {
            StateManager.Update(gameTime);
#if DEBUG
            if (Keyboard.GetState().IsKeyDown(Keys.R))
            {
                StateManager.Reload();
            }
#endif
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            SpriteBatch.Begin();

            StateManager.Draw(gameTime, SpriteBatch);

            SpriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
