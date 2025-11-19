using UnityEngine;

namespace Audio
{
    [CreateAssetMenu(menuName = "Config/Audio", fileName = "AudioCfg", order = 0)]
    public class AudioConfig : ScriptableObject
    {
        public AudioClip musicClip;

        public AudioClip soundHit;
        public AudioClip soundJump;
        public AudioClip soundButton;
        public AudioClip soundGameOver;
        public AudioClip soundGameWin;
    }

}