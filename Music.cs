using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NAudio.Wave;
using NAudio.Vorbis;
using DungeonCrawler;

namespace Dungeon_Crawler
{
    public class Music
    {
        private VorbisWaveReader vorbisWaveReader;
        private WaveOutEvent waveOutEvent;
        private bool IsMusicPlaying;
        public void PlayMusic(string song, bool IsSongLooping = true)
        {
            StopMusic();
            string musicFile = GetMusicFileToPlay(song);

            vorbisWaveReader = new VorbisWaveReader(musicFile);
            waveOutEvent = new WaveOutEvent();
            IsMusicPlaying = true;
            waveOutEvent.Init(vorbisWaveReader);
            waveOutEvent.Play();
            if (IsSongLooping )
            {
                waveOutEvent.PlaybackStopped += OnPlayBackStopped;
            }
            else
            {
                waveOutEvent.PlaybackStopped -= OnPlayBackStopped;
            }
        }
        private void OnPlayBackStopped(object sender, StoppedEventArgs e)
        {
            if (IsMusicPlaying && waveOutEvent != null && vorbisWaveReader != null)
            {
                if (vorbisWaveReader.Position == vorbisWaveReader.Length)
                {
                    vorbisWaveReader.Position = 0;
                    waveOutEvent.Play();
                }
            }
        }

        private string GetMusicFileToPlay(string song)
        {
            switch (song)
            {
                case "Level":
                    return Path.Combine(Directory.GetCurrentDirectory(), @"Music\Level.ogg");
                case "Fanfare":
                    return Path.Combine(Directory.GetCurrentDirectory(), @"Music\Fanfare.ogg");
                case "GameOver":
                    return Path.Combine(Directory.GetCurrentDirectory(), @"Music\GameOver.ogg");
                case "TitleScreen":
                    return Path.Combine(Directory.GetCurrentDirectory(), @"Music\TitleScreen.ogg");
                default:
                    throw new ArgumentException("Invalid song specified");
            }
        }

        public void StopMusic()
        {
            IsMusicPlaying = false;
            if (waveOutEvent != null)
            {
                waveOutEvent.Stop();
                waveOutEvent.Dispose();
                waveOutEvent = null;
            }

            if (vorbisWaveReader != null)
            {
                vorbisWaveReader.Dispose();
                vorbisWaveReader = null;
            }
        }
    }
}