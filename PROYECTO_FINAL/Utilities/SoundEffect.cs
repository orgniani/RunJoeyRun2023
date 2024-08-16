using System;
using SFML.Audio;

    class SoundEffect
    {
        private SoundBuffer buffer;
        private Sound sound;

        public SoundEffect(string fileName)
        {
            buffer = new SoundBuffer(fileName);
            sound = new Sound(buffer);
        }

        public void Play() => sound.Play();

        public void SetVolume(float value)
        {
            sound.Volume = value;
        }
    }