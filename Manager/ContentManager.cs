﻿using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
using System.Collections.Generic;
using Unity;

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
        public SoundEffect SelectSoundEffect => JamGame.Content.Load<SoundEffect>("SoundEffects/Select");
        public SoundEffect MotorStartSoundEffect => JamGame.Content.Load<SoundEffect>("SoundEffects/MotorStart");

        public Song MenuMusic => JamGame.Content.Load<Song>("Music/MenuMusic");
        public Song GameMusic => JamGame.Content.Load<Song>("Music/GameMusic");

        public List<Texture2D> HurtParticles
        {
            get
            {
                List<Texture2D> hurtParticles = new List<Texture2D>
                {
                    JamGame.Content.Load<Texture2D>("Particle/HurtParticle1"),
                    JamGame.Content.Load<Texture2D>("Particle/HurtParticle1"),
                    JamGame.Content.Load<Texture2D>("Particle/HurtParticle2"),
                    JamGame.Content.Load<Texture2D>("Particle/HurtParticle2"),
                    JamGame.Content.Load<Texture2D>("Particle/HurtParticle3"),
                    JamGame.Content.Load<Texture2D>("Particle/HurtParticle3"),
                    JamGame.Content.Load<Texture2D>("Particle/HurtParticle4")
                };
                return hurtParticles;
            }
        }

        public Texture2D WalkingAnimation => JamGame.Content.Load<Texture2D>("Animations/Walking");
        public Texture2D StandingAnimation => JamGame.Content.Load<Texture2D>("Animations/Standing");
        public Texture2D JumpAnimation => JamGame.Content.Load<Texture2D>("Animations/Jump");

        public Texture2D Background => JamGame.Content.Load<Texture2D>("Background");
    }
}
