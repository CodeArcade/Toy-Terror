﻿using brackeys_2020_2_jam.Component.Sprites;
using brackeys_2020_2_jam.Component.Sprites.Obstacles;
using brackeys_2020_2_jam.Manager;
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
            int pick = Random.Next(0, Enum.GetNames(typeof(StickyObstacles)).Length);
            return pick switch
            {
                (int)StickyObstacles.Shark => GetShark(),
                _ => GetHedgehog(),
            };
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
            // load animation
            // apply animation

            return obstacle;
        }

        public StaticObstacle GetSpiral()
        {
            MovingObstacle obstacle = new MovingObstacle()
            {
                AdditionalSpeed = 0.05f,
                Texture = ContentManager.ProgressBarBackground
            };

            // load animation
            // apply animation

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
            StaticObstacle obstacle = new StaticObstacle()
            {
                Texture = ContentManager.ProgressBarValue
            };
            // load texture
            return obstacle;
        }

        public StaticObstacle GetTeddy()
        {
            StaticObstacle obstacle = new StaticObstacle()
            {
                Texture = ContentManager.ProgressBarValue
            };
            // load texture
            return obstacle;
        }

        public StaticObstacle GetShark()
        {
            StickyObstacle obstacle = new StickyObstacle(Player.ALIVE_CHARGE)
            {
                Texture = ContentManager.ProgressBarOutline
            };
            // load texture
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
