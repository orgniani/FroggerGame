using UnityEngine;
using Interfaces;

namespace Tests.Mocks
{
    public class MockAudioConfig : IAudioConfig
    {
        public AudioClip MusicClip { get; set; }
        public AudioClip SoundHit { get; set; }
        public AudioClip SoundJump { get; set; }
        public AudioClip SoundButton { get; set; }
        public AudioClip SoundGameOver { get; set; }
        public AudioClip SoundGameWin { get; set; }
    }
}
