using Microsoft.Xna.Framework.Audio;
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
        public SoundEffect GrindingSoundEffect => JamGame.Content.Load<SoundEffect>("SoundEffects/Schredder");
        public SoundEffect ClockSoundEffect => JamGame.Content.Load<SoundEffect>("SoundEffects/Clock");
        public SoundEffect ConveyorSoundEffect => JamGame.Content.Load<SoundEffect>("SoundEffects/Conveyor");
        public SoundEffect JumpSoundEffect => JamGame.Content.Load<SoundEffect>("SoundEffects/Jump");
        public SoundEffect LandSoundEffect => JamGame.Content.Load<SoundEffect>("SoundEffects/Land");
        public SoundEffect StepSoundEffect => JamGame.Content.Load<SoundEffect>("SoundEffects/Step");
        public SoundEffect WindupSoundEffect => JamGame.Content.Load<SoundEffect>("SoundEffects/Windup");
        public SoundEffect WinddownSoundEffect => JamGame.Content.Load<SoundEffect>("SoundEffects/Winddown");
        public SoundEffect ChopperSoundEffect => JamGame.Content.Load<SoundEffect>("SoundEffects/Chopper");

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
        public List<Texture2D> DustParticles
        {
            get
            {
                List<Texture2D> dustParticles = new List<Texture2D>
                {
                    JamGame.Content.Load<Texture2D>("Particle/DustParticle1"),
                    JamGame.Content.Load<Texture2D>("Particle/DustParticle2"),
                    JamGame.Content.Load<Texture2D>("Particle/DustParticle3"),
                    JamGame.Content.Load<Texture2D>("Particle/DustParticle4"),
                };
                return dustParticles;
            }
        }

        public List<Texture2D> Clock
        {
            get
            {
                List<Texture2D> hurtParticles = new List<Texture2D>
                {
                    JamGame.Content.Load<Texture2D>("Textures/12_Uhr"),
                    JamGame.Content.Load<Texture2D>("Textures/13_Uhr"),
                    JamGame.Content.Load<Texture2D>("Textures/14_Uhr"),
                    JamGame.Content.Load<Texture2D>("Textures/15_Uhr"),
                    JamGame.Content.Load<Texture2D>("Textures/16_Uhr"),
                    JamGame.Content.Load<Texture2D>("Textures/17_Uhr"),
                    JamGame.Content.Load<Texture2D>("Textures/18_Uhr"),
                };
                return hurtParticles;
            }
        }
        public Texture2D Logo => JamGame.Content.Load<Texture2D>("Textures/logo_KEKW");

        public Texture2D WalkingAnimation => JamGame.Content.Load<Texture2D>("Animations/Walking");
        public Texture2D Walking2Animation => JamGame.Content.Load<Texture2D>("Animations/Walking2");
        public Texture2D StandingAnimation => JamGame.Content.Load<Texture2D>("Animations/Standing");
        public Texture2D JumpAnimation => JamGame.Content.Load<Texture2D>("Animations/Jump");
        public Texture2D WindupAnimation => JamGame.Content.Load<Texture2D>("Animations/Windup");
        public Texture2D ChopperAnimation => JamGame.Content.Load<Texture2D>("Animations/Chopper");
        public Texture2D ConveyorAnimation => JamGame.Content.Load<Texture2D>("Animations/Conveyor");
        public Texture2D SharkAnimation => JamGame.Content.Load<Texture2D>("Animations/Shark");
        public Texture2D TrainAnimation => JamGame.Content.Load<Texture2D>("Animations/Train");

        public Texture2D Background => JamGame.Content.Load<Texture2D>("Background");
        public Texture2D MenuBackground => JamGame.Content.Load<Texture2D>("MenuBackground");
        public Texture2D Vignette => JamGame.Content.Load<Texture2D>("Textures/Vignette");
        public Texture2D EndGameBackground => JamGame.Content.Load<Texture2D>("QR");

        public Texture2D PetzTexture => JamGame.Content.Load<Texture2D>("Sprites/Petz");
        public Texture2D Petz2Texture => JamGame.Content.Load<Texture2D>("Sprites/Petz2");
        public Texture2D CubeTexture => JamGame.Content.Load<Texture2D>("Sprites/Rubik_Cube");
        public Texture2D RobotTexture => JamGame.Content.Load<Texture2D>("Sprites/Robot");
        public Texture2D TransparentTexture => JamGame.Content.Load<Texture2D>("Textures/Transparent");
        public Texture2D ControlsTexture => JamGame.Content.Load<Texture2D>("Textures/Controls");
        public Texture2D RewindTexture => JamGame.Content.Load<Texture2D>("Textures/Rewind");
    }
}
