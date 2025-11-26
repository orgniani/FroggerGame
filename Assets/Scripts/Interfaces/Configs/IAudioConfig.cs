using UnityEngine;

namespace Interfaces
{
    public interface IAudioConfig
    {
        AudioClip MusicClip { get; }
        AudioClip SoundHit { get; }
        AudioClip SoundJump { get; }
        AudioClip SoundButton { get; }
        AudioClip SoundGameOver { get; }
        AudioClip SoundGameWin { get; }
    }
}
