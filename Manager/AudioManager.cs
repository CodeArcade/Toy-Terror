using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;
using NAudio;

namespace brackeys_2020_2_jam.Manager
{
    class AudioManager
    {
        private Song CurrentSong;
        private Song NextSong;
        public void PlayEffect(SoundEffect effect)
        {
            effect.Play();
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
            
            MediaPlayer.Stop();
            MediaPlayer.Play(CurrentSong);
        }

        public void ChangeSong(Song song) => NextSong = song;

        public void StopMusic() => MediaPlayer.Stop();
    }
}
