using brackeys_2020_2_jam.Manager;
using brackeys_2020_2_jam.Models;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity;

namespace brackeys_2020_2_jam.Component
{
    public abstract class Component
    {
        #region Dependencies

        public AnimationManager AnimationManager => Program.UnityContainer.Resolve<AnimationManager>();
        public AnimationManager ContentManager => Program.UnityContainer.Resolve<AnimationManager>();
        public AnimationManager StateManager => Program.UnityContainer.Resolve<AnimationManager>();

        #endregion

        #region Private Properties

        private Vector2 position { get; set; }

        #endregion

        #region Public Properties
        
        public Vector2 Position
        {
            get { return position; }
            set
            {
                position = value;
                if (AnimationManager != null) AnimationManager.Position = position;
            }
        }

        public Texture2D Texture { get; set; }

        public bool IsRemoved { get; set; }

        #endregion

        public abstract void Draw(GameTime gameTime, SpriteBatch spriteBatch);
        public abstract void Update(GameTime gameTime);
    }
}