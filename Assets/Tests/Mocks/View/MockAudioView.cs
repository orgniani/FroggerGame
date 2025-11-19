using UnityEngine;
using Interfaces;

namespace Tests.Mocks
{
    public class MockAudioView : IAudioView
    {
        public AudioClip LastSfx;
        public AudioClip LastMusic;

        public bool MusicStopped;

        public void PlaySfx(AudioClip clip)
        {
            LastSfx = clip;
        }

        public void PlayMusic(AudioClip clip)
        {
            LastMusic = clip;
        }

        public void StopMusic()
        {
            MusicStopped = true;
        }
    }
}
