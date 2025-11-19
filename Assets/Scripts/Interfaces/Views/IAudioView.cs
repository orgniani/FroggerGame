using UnityEngine;

namespace Interfaces
{
    public interface IAudioView
    {
        void PlaySfx(AudioClip clip);
        void PlayMusic(AudioClip clip);
        void StopMusic();
    }

}