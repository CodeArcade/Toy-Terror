using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Unity;
using System.Collections.Generic;

namespace brackeys_2020_2_jam.Models
{
    public abstract class State
    {
        #region Fields

        protected List<Component> Components { get; set; } = new List<Component>();

        [Dependency]
        public Manager.ContentManager Content;

        [Dependency]
        public JamGame Game { get; set; }

        #endregion

        #region Methods

        public abstract void Draw(GameTime gameTime, SpriteBatch spriteBatch);

        public abstract void PostUpdate(GameTime gameTime);

        public abstract void Update(GameTime gameTime);

        #endregion
    }
}