using brackeys_2020_2_jam.Component.Sprites;
using brackeys_2020_2_jam.Component.Sprites.Obstacles;
using brackeys_2020_2_jam.Manager;
using brackeys_2020_2_jam.Models;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Drawing;
using Unity;

namespace brackeys_2020_2_jam.Factory
{
    public class ObstacleFactory
    {
        [Dependency]
        public ContentManager ContentManager { get; set; }

        private Random Random { get; } = new Random();

        public StaticObstacle GetMovingObstacle()
        {
            int pick = Random.Next(0, Enum.GetNames(typeof(MovingObstacles)).Length);
            return pick switch
            {
                (int)MovingObstacles.Train => GetTrain(),
                _ => GetSpiral(),
            };
        }
        public StaticObstacle GetStaticObstacle()
        {
            int pick = Random.Next(0, Enum.GetNames(typeof(StaticObstacles)).Length);
            return pick switch
            {
                (int)StaticObstacles.Block => GetBlock(),
                (int)StaticObstacles.Pez => GetPez(),
                (int)StaticObstacles.Furby => GetFurby(),
                _ => GetTeddy(),
            };
        }
        public StaticObstacle GetStickyObstacle()
        {
            return GetShark();
            //int pick = Random.Next(0, Enum.GetNames(typeof(StickyObstacles)).Length);
            //return pick switch
            //{
            //    (int)StickyObstacles.Shark => GetShark(),
            //    _ => GetHedgehog(),
            //};
        }
        public StaticObstacle GetStaticOrStickyObstacle()
        {
            List<StaticObstacle> obstacles = new List<StaticObstacle>
            {
                GetStaticObstacle(),
                GetStickyObstacle()
            };

            return obstacles[Random.Next(0, 2)];
        }
        public StaticObstacle GetMovingOrStickyObstacle()
        {
            List<StaticObstacle> obstacles = new List<StaticObstacle>
            {
                GetMovingObstacle(),
                GetStickyObstacle()
            };

            return obstacles[Random.Next(0, 2)];
        }
        public StaticObstacle GetRandomObstacle()
        {
            List<StaticObstacle> obstacles = new List<StaticObstacle>
            {
                GetStaticObstacle(),
                GetStickyObstacle(),
                GetMovingObstacle()
            };

            return obstacles[Random.Next(0, 3)];
        }

        public StaticObstacle GetTrain()
        {
            MovingObstacle obstacle = new MovingObstacle()
            {
                AdditionalSpeed = 0.1f,
                Texture = ContentManager.ProgressBarBackground
            };

            obstacle.AnimationManager.Scale = 0.6f;
            obstacle.AnimationManager.Play(new Animation(ContentManager.TrainAnimation, 8) { FrameSpeed = 0.01f });
            obstacle.Size = new Size(obstacle.AnimationManager.AnimationRectangle.Width, obstacle.AnimationManager.AnimationRectangle.Height);
            obstacle.HitboxSize = new Size(obstacle.Size.Width, (int)(obstacle.Size.Height * 0.8));
            obstacle.HitBoxYOffSet = 40;

            return obstacle;
        }

        public StaticObstacle GetSpiral()
        {
            MovingObstacle obstacle = new MovingObstacle()
            {
                AdditionalSpeed = 0.05f,
            };

            obstacle.AnimationManager.Scale = 0.2f;
            obstacle.AnimationManager.Play(new Animation(ContentManager.Walking2Animation, 29) { FrameSpeed = 0.01f });
            obstacle.Size = new Size(obstacle.AnimationManager.AnimationRectangle.Width, obstacle.AnimationManager.AnimationRectangle.Height);
            obstacle.HitboxSize = new Size(obstacle.Size.Width,obstacle.Size.Height);

            return obstacle;
        }

        public StaticObstacle GetBlock()
        {
            int width = 200;
            int height = 210;
            int hitboxHeight = 125;

            StaticObstacle obstacle = new StaticObstacle()
            {
                Texture = ContentManager.CubeTexture,
                Size = new Size(width, height),
                HitboxSize = new Size(width, hitboxHeight),
                HitBoxYOffSet = (height - hitboxHeight) / 2
            };

            return obstacle;
        }

        public StaticObstacle GetPez()
        {
            int width = 135;
            int height = 250;
            int hitboxHeight = 230;
            int hitboxWidth = 72;

            StaticObstacle obstacle = new StaticObstacle()
            {
                Texture = ContentManager.PetzTexture,
                Size = new Size(width, height),
                HitboxSize = new Size(hitboxWidth, hitboxHeight),
                HitBoxXOffSet = (int)((width - hitboxWidth) / 1.75),
                HitBoxYOffSet = height - hitboxHeight
            };

            return obstacle;
        }

        public StaticObstacle GetFurby()
        {

            StaticObstacle obstacle = new StaticObstacle();

            obstacle.AnimationManager.Scale = 0.3f;
            obstacle.AnimationManager.Play(new Animation(ContentManager.RobotTexture, 1) { FrameSpeed = 1 });
            obstacle.Size = new Size(obstacle.AnimationManager.AnimationRectangle.Width, obstacle.AnimationManager.AnimationRectangle.Height);
            obstacle.HitboxSize = new Size((int)(obstacle.Size.Width * 0.9), (int)(obstacle.Size.Height * 0.8));
            obstacle.HitBoxYOffSet = 20;

            // load texture
            return obstacle;
        }

        public StaticObstacle GetTeddy()
        {
            int width = 250;
            int height = 135;
            int hitboxHeight = 72;
            int hitboxWidth = 230;

            StaticObstacle obstacle = new StaticObstacle()
            {
                Texture = ContentManager.Petz2Texture,
                Size = new Size(width, height),
                HitboxSize = new Size(hitboxWidth, hitboxHeight),
                HitBoxXOffSet = (int)((width - hitboxWidth) / 1.75),
                HitBoxYOffSet = 50
            };

            return obstacle;
        }

        public StaticObstacle GetShark()
        {
            int hitboxHeight = 70;
            int hitboxWidth = 90;

            StickyObstacle obstacle = new StickyObstacle(Player.ALIVE_CHARGE);
            obstacle.AnimationManager.Scale = 0.18f;
            obstacle.AnimationManager.Play(new Animation(ContentManager.SharkAnimation, 10) { FrameSpeed = 0.25f });
            obstacle.HitboxSize = new Size(hitboxWidth, hitboxHeight);
            obstacle.HitBoxXOffSet = 25;
            obstacle.HitBoxYOffSet = 50;

            return obstacle;
        }

        public StaticObstacle GetHedgehog()
        {
            StickyObstacle obstacle = new StickyObstacle(Player.ALIVE_CHARGE / 2)
            {
                Texture = ContentManager.ProgressBarOutline
            };
            // load texture
            return obstacle;
        }

    }

    enum MovingObstacles
    {
        Train,
        Spiral
    }

    enum StaticObstacles
    {
        Block,
        Pez,
        Furby,
        Teddy
    }

    enum StickyObstacles
    {
        Shark,
        Hedgehog
    }
}
