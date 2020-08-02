namespace brackeys_2020_2_jam.Component.Sprites.Environment
{
    public class Chopper: Sprite
    {
        public Chopper()
        {
            Texture = ContentManager.ProgressBarBackground;
            AnimationManager.Scale = 0.2f;
            AnimationManager.Parent = this;
            AnimationManager.Play(new Models.Animation(ContentManager.JumpAnimation, 12));
        }
    }
}
