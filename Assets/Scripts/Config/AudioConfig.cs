using Interfaces;
using UnityEngine;

namespace Config
{
    [CreateAssetMenu(menuName = "Config/Audio", fileName = "AudioCfg", order = 0)]
    public class AudioConfig : ScriptableObject, IAudioConfig
    {
        [field: SerializeField] public AudioClip MusicClip { get; private set; }
        [field: SerializeField] public AudioClip SoundHit { get; private set; }
        [field: SerializeField] public AudioClip SoundJump { get; private set; }
        [field: SerializeField] public AudioClip SoundButton { get; private set; }
        [field: SerializeField] public AudioClip SoundGameOver { get; private set; }
        [field: SerializeField] public AudioClip SoundGameWin { get; private set; }
    }
}
