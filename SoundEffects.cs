using NAudio.Wave;
using System;
using System.IO;

namespace Dungeon_Crawler
{
    public class SoundEffects
    {
        private WaveFileReader waveFileReader;
        private WaveOutEvent waveOutEvent;

        public void PlaySoundEffect(string sound)
        {
            string soundFile = GetSoundEffectFileToPlay(sound);

            waveFileReader = new WaveFileReader(soundFile);
            waveOutEvent = new WaveOutEvent();
            waveOutEvent.Init(waveFileReader);
            waveOutEvent.Play();
            //waveOutEvent.PlaybackStopped += OnPlaybackStopped;
        }

        public void OnPlaybackStopped(object sender, EventArgs e)
        {
            StopSoundEffect();
        }

        private string GetSoundEffectFileToPlay(string sound)
        {
            switch (sound)
            {
                case "Wall":
                    return Path.Combine(Directory.GetCurrentDirectory(), @"Sound Effects\Wall.wav");
                case "Warp":
                    return Path.Combine(Directory.GetCurrentDirectory(), @"Sound Effects\Warp.wav");
                case "Arrow":
                    return Path.Combine(Directory.GetCurrentDirectory(), @"Sound Effects\Arrow.wav");
                case "FakeWall":
                    return Path.Combine(Directory.GetCurrentDirectory(), @"Sound Effects\FakeWall.wav");
                case "Coins":
                    return Path.Combine(Directory.GetCurrentDirectory(), @"Sound Effects\Coins.wav");
                case "DoorOpen":
                    return Path.Combine(Directory.GetCurrentDirectory(), @"Sound Effects\DoorOpen.wav");
                case "DoorLocked":
                    return Path.Combine(Directory.GetCurrentDirectory(), @"Sound Effects\DoorLocked.wav");
                case "Key":
                    return Path.Combine(Directory.GetCurrentDirectory(), @"Sound Effects\Key.wav");
                case "Heart":
                    return Path.Combine(Directory.GetCurrentDirectory(), @"Sound Effects\Heart.wav");
                case "GoldHeart":
                    return Path.Combine(Directory.GetCurrentDirectory(), @"Sound Effects\GoldHeart.wav");
                case "MagicalKey":
                    return Path.Combine(Directory.GetCurrentDirectory(), @"Sound Effects\MagicalKey.wav");
                case "BarrierBreak":
                    return Path.Combine(Directory.GetCurrentDirectory(), @"Sound Effects\BarrierBreak.wav");
                case "BarrierLocked":
                    return Path.Combine(Directory.GetCurrentDirectory(), @"Sound Effects\BarrierLocked.wav");
                default:
                    throw new ArgumentException("Invalid sound specified");
            }
        }

        public void StopSoundEffect()
        {
            if (waveOutEvent != null)
            {
                waveOutEvent.Stop();
                waveOutEvent.Dispose();
                waveOutEvent = null;
            }

            if (waveFileReader != null)
            {
                waveFileReader.Dispose();
                waveFileReader = null;
            }
        }
    }
}