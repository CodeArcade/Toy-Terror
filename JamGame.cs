using brackeys_2020_2_jam.Manager;
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

        public int ScreenWidth => 1280;
        public int ScreenHeight => 720;

#if DEBUG
        bool AdvanceSlowly;
        KeyboardState PreviousKeyboard { get; set; }
        KeyboardState CurrentKeyboard { get; set; }
#endif

        public JamGame()
        {
            Graphics = new GraphicsDeviceManager(this)
            {
               PreferredBackBufferHeight = ScreenHeight,
               PreferredBackBufferWidth = ScreenWidth
            };
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            Window.Title = "Jam Game!";
            IsMouseVisible = true;
#if DEBUG
            AdvanceSlowly = false;
#endif

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

#if DEBUG
            PreviousKeyboard = CurrentKeyboard;
            CurrentKeyboard = Keyboard.GetState();
            if (CurrentKeyboard.IsKeyDown(Keys.R) && PreviousKeyboard.IsKeyUp(Keys.R))
            {
                StateManager.Reload();
                AdvanceSlowly = false;
            }

            if (CurrentKeyboard.IsKeyDown(Keys.Pause) && PreviousKeyboard.IsKeyUp(Keys.Pause))
            {
                AdvanceSlowly = !AdvanceSlowly;
            }

            if (!AdvanceSlowly) StateManager.Update(gameTime);
            else
            {
                if (CurrentKeyboard.IsKeyDown(Keys.Right) && PreviousKeyboard.IsKeyUp(Keys.Right))
                {
                    StateManager.Update(gameTime);
                }
            }
#else
            StateManager.Update(gameTime);
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
