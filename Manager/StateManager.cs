using brackeys_2020_2_jam.Models;
using brackeys_2020_2_jam.States;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using DependencyAttribute = Unity.DependencyAttribute;

namespace brackeys_2020_2_jam.Manager
{
    public class StateManager
    {
        private State CurrentState { get; set; }
        private State NextState { get; set; }
        
        [Dependency]
        public MenuState MenuState { get; set; }

        [Dependency]
        public GameState GameState { get; set; }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch) => CurrentState.Draw(gameTime, spriteBatch);

        public void Update(GameTime gameTime)
        {
            ChangeState();
            CurrentState.Update(gameTime);
            CurrentState.PostUpdate(gameTime);
        }
       
        private void ChangeState()
        {
            if (NextState is null) return;

            CurrentState = NextState;
            NextState = null;
        }

        public void ChangeToMenu() => NextState = MenuState;

        public void ChangeToGame() => NextState = GameState;

    }
}