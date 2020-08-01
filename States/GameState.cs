using brackeys_2020_2_jam.Component.Sprites;
using brackeys_2020_2_jam.Models;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System.Linq;

namespace brackeys_2020_2_jam.States
{
    public class GameState : State
    {

        private Player Player;

        public override void Load()
        {
            base.Load();
            Player = new Player(new PlayerInput() { Jump = Keys.W, Left = Keys.A, Right = Keys.D, Windup = Keys.Space })
            {
                MaxSpeed = new Vector2(10, 10),
                MaxAcceleration = 5,
                Acceleration = 0.5f,
                Position = new Vector2(250, 250)
            };

            Components.Add(Player);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            foreach(Sprite sprite in Components.Where(x => x is Sprite).Select(x=> x))
            {
                if (Player.Rectangle.Intersects(sprite.Rectangle)) Player.OnCollision(sprite);
            }
        }

    }
}
