using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace brackeys_2020_2_jam.Models
{
    public abstract class State
    {
        #region Fields

        [Dependency]
        public ContentManager Content;

        protected GraphicsDevice GraphicsDevice;

        [Dependency]
        public JamGame Game { get; set; }

        #endregion

        #region Methods
        public State( GraphicsDevice graphicsDevice, ContentManager content)
        {
            GraphicsDevice = graphicsDevice;

            Content = content;
        }

        public abstract void Draw(GameTime gameTime, SpriteBatch spriteBatch);

        public abstract void PostUpdate(GameTime gameTime);

        public abstract void Update(GameTime gameTime);

        #endregion
    }
}