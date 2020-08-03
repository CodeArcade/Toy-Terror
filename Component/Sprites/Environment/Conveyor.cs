using brackeys_2020_2_jam.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace brackeys_2020_2_jam.Component.Sprites.Environment
{
    public class Conveyor : Sprite
    {
        
        public Conveyor()
        {
            Texture = ContentManager.ButtonTexture;
           // AnimationManager.Play(new Animation(ContentManager.WalkingAnimation, 29));
        }

    }
}
