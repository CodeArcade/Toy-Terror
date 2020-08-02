using brackeys_2020_2_jam.Component.Sprites;
using brackeys_2020_2_jam.Component.Sprites.Obstacles;
using System;

namespace brackeys_2020_2_jam.Factory
{
    public class ObstacleFactory
    {
        private Random Random { get; } = new Random();

        public Sprite GetMovingObstacle()
        {
            int pick = Random.Next(0, Enum.GetNames(typeof(MovingObstacles)).Length);
            return pick switch
            {
                (int)MovingObstacles.Train => GetTrain(),
                _ => GetSpiral(),
            };
        }

        public Sprite GetTrain()
        {
            MovingObstacle obstacle = new MovingObstacle()
            {
                AdditionalSpeed = 4f
            };
            // load animation
            // apply animation

            return obstacle;
        }

        public Sprite GetSpiral()
        {
            MovingObstacle obstacle = new MovingObstacle()
            {
                AdditionalSpeed = 2f
            };

            // load animation
            // apply animation

            return obstacle;
        }

        public Sprite GetStaticObstacle()
        {
            int pick = Random.Next(0, Enum.GetNames(typeof(StaticObstacles)).Length);
            return pick switch
            {
                (int)StaticObstacles.Block => GetBlock(),
                (int)StaticObstacles.Furby => GetFurby(),
                (int)StaticObstacles.Pez => GetPez(),
                _ => GetTeddy(),
            };
        }

        public Sprite GetBlock()
        {
            StaticObstacle obstacle = new StaticObstacle();
            // load texture
            return obstacle;
        }

        public Sprite GetPez()
        {
            StaticObstacle obstacle = new StaticObstacle();
            // load texture
            return obstacle;
        }

        public Sprite GetFurby()
        {
            StaticObstacle obstacle = new StaticObstacle();
            // load texture
            return obstacle;
        }

        public Sprite GetTeddy()
        {
            StaticObstacle obstacle = new StaticObstacle();
            // load texture
            return obstacle;
        }

        public Sprite GetStickyObstacle()
        {
            int pick = Random.Next(0, Enum.GetNames(typeof(StickyObstacle)).Length);
            return pick switch
            {
                (int)StickyObstacles.Shark => GetShark(),
                _ => GetHedgehog(),
            };
        }

        public Sprite GetShark()
        {
            StickyObstacle obstacle = new StickyObstacle(Player.ALIVE_CHARGE);
            // load texture
            return obstacle;
        }

        public Sprite GetHedgehog()
        {
            StickyObstacle obstacle = new StickyObstacle(Player.ALIVE_CHARGE / 2);
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
