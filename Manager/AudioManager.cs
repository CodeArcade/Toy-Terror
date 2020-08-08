using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;

namespace brackeys_2020_2_jam.Manager
{
    public class AudioManager
    {
        private Song CurrentSong;
        private Song NextSong;
        private bool Loop;

        public void PlayEffect(SoundEffect effect, float volume = 1, float pitch = 0)
        {
            effect.Play(volume, pitch, 0);
        }

        public void Update()
        {
            ChangeSong();
        }

        private void ChangeSong()
        {
            if (NextSong is null) return;
            CurrentSong = NextSong;
            NextSong = null;

            MediaPlayer.IsRepeating = Loop;
            MediaPlayer.Stop();
            MediaPlayer.Volume = 0.3f;
            MediaPlayer.Play(CurrentSong);
        }

        public void ChangeSong(Song song, bool loop = false) { NextSong = song; Loop = loop; }

        public void StopMusic() => MediaPlayer.Stop();
    }
}
