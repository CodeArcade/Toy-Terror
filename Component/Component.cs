using brackeys_2020_2_jam.Manager;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Unity;

namespace brackeys_2020_2_jam.Component
{
    public abstract class Component
    {
        #region Dependencies

        public AnimationManager AnimationManager => Program.UnityContainer.Resolve<AnimationManager>();
        public ContentManager ContentManager => Program.UnityContainer.Resolve<ContentManager>();
        public AudioManager AudioManager => Program.UnityContainer.Resolve<AudioManager>();
        public ParticleManager ParticleManager { get; } = new ParticleManager();

        #endregion

        #region Private Properties

        private Vector2 InternalPosition { get; set; }

        #endregion

        #region Public Properties
        
        public Vector2 Position
        {
            get { return InternalPosition; }
            set
            {
                InternalPosition = value;
                if (AnimationManager != null) AnimationManager.Position = InternalPosition;
            }
        }

        public bool IsRemoved { get; set; }

        public Component Parent { get; set; }

        #endregion

        public abstract void Draw(GameTime gameTime, SpriteBatch spriteBatch);
        public abstract void Update(GameTime gameTime);
    }
}