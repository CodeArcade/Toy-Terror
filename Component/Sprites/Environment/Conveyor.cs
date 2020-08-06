using brackeys_2020_2_jam.Models;

namespace brackeys_2020_2_jam.Component.Sprites.Environment
{
    public class Conveyor : Sprite
    {
        
        public Conveyor()
        {
            AnimationManager.Play(new Animation(ContentManager.ConveyorAnimation, 3) {  FrameSpeed = 0.1f } );
            AnimationManager.Scale = 0.5f;
        }

    }
}
