using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Unity;
using DependencyAttribute = Unity.DependencyAttribute;

namespace brackeys_2020_2_jam.Manager
{
    public class ContentManager
    {
        public JamGame JamGame => Program.UnityContainer.Resolve<JamGame>();

        public Texture2D ButtonTexture => JamGame.Content.Load<Texture2D>("Sprites/Button");
        public SpriteFont ButtonFont => JamGame.Content.Load<SpriteFont>("Fonts/ButtonFont");

        public Texture2D ProgressBarOutline => JamGame.Content.Load<Texture2D>("Sprites/ProgressBarOutline");
        public Texture2D ProgressBarValue => JamGame.Content.Load<Texture2D>("Sprites/ProgressBarValue");
        public Texture2D ProgressBarBackground => JamGame.Content.Load<Texture2D>("Sprites/ProgressBarBackground");

        public SoundEffect HurtSoundEffect => JamGame.Content.Load<SoundEffect>("SoundEffects/Hurt");

    }
}
