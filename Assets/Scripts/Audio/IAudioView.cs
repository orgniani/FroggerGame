using UnityEngine;

namespace Audio
{
    public interface IAudioView
    {
        void PlaySfx(AudioClip clip);
        void PlayMusic(AudioClip clip);
        void StopMusic();
    }

}