﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NAudio.Wave;
using NAudio.Vorbis;

namespace Dungeon_Crawler
{
    public class Music
    {
        private VorbisWaveReader vorbisWaveReader;
        private WaveOutEvent waveOutEvent;

        public void PlayMusic(string song)
        {

            StopMusic();

            string musicFile = GetMusicFileToPlay(song);

            vorbisWaveReader = new VorbisWaveReader(musicFile);
            waveOutEvent = new WaveOutEvent();
            waveOutEvent.Init(vorbisWaveReader);
            waveOutEvent.Play();
            waveOutEvent.PlaybackStopped += OnPlayBackStopped;
        }
        private void OnPlayBackStopped(object sender, EventArgs e)
        {
            if (vorbisWaveReader != null)
            {
                vorbisWaveReader.Position = 0;
                waveOutEvent.Play(); 
            }
        }

        private string GetMusicFileToPlay(string song)
        {
            switch (song)
            {
                case "Level":
                    return Path.Combine(Directory.GetCurrentDirectory(), @"Music\Level.ogg");
                default:
                    throw new ArgumentException("Invalid level specified");
            }
        }

        public void StopMusic()
        {
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