using brackeys_2020_2_jam.Models;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Unity;

namespace brackeys_2020_2_jam
{
    public class JamGame : Game
    {
        private GraphicsDeviceManager Graphics;
        private SpriteBatch SpriteBatch;

        public StateManager StateManager { get; set; }

        public JamGame()
        {
            Graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            Window.Title = "Jam Game!";
            IsMouseVisible = false;

            StateManager = Program.UnityContainer.Resolve<StateManager>();
            StateManager.ChangeToMenu();

            Graphics.PreferredBackBufferHeight = 720;
            Graphics.PreferredBackBufferWidth = 1280;
            base.Initialize();
        }

        protected override void LoadContent()
        {
            SpriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            StateManager.Update(gameTime);

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
