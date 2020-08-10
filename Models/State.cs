using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Unity;
using System.Collections.Generic;
using brackeys_2020_2_jam.Manager;
using brackeys_2020_2_jam.Component.Sprites;

namespace brackeys_2020_2_jam.Models
{
    public abstract class State
    {
        #region Fields
        protected List<Component.Component> Components { get; set; }

        [Dependency]
        public ContentManager ContentManager { get; set; }

        public StateManager StateManager => Program.UnityContainer.Resolve<StateManager>();

        [Dependency]
        public JamGame JamGame { get; set; }

        [Dependency]
        public AudioManager AudioManager { get; set; }

        public bool HasLoaded { get; protected set; }
        
        #endregion

        #region Methods

        public virtual void Load() { Components = new List<Component.Component>(); HasLoaded = true; }

        public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {         
            if (Components is null) return;
            foreach (Component.Component component in Components)
            {
                component.Draw(gameTime, spriteBatch);
            }

        }

        public virtual void PostUpdate(GameTime gameTime)
        {
            if (Components is null) return;
            for (int i = Components.Count - 1; i >= 0; i--)
            {
                if (Components[i].IsRemoved) Components.RemoveAt(i);
            }
        }

        public virtual void Update(GameTime gameTime)
        {
            AudioManager.Update();
            if (Components is null) return;
            foreach (Component.Component component in Components)
            {
                component.Update(gameTime);
            }
        }

        #endregion
    }
}