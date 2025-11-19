using UnityEngine;
using Helpers;
using Interfaces;

namespace Audio
{
    public class AudioView : MonoBehaviour, IAudioView
    {
        [SerializeField] private AudioSource sfxSource;
        [SerializeField] private AudioSource musicSource;

        private void Awake()
        {
            ValidateReferences();
        }

        public void PlaySfx(AudioClip clip)
        {
            if (clip == null) return;
            sfxSource.PlayOneShot(clip);
        }

        public void PlayMusic(AudioClip clip)
        {
            if (clip == null) return;

            musicSource.clip = clip;
            musicSource.loop = true;
            musicSource.Play();
        }

        public void StopMusic()
        {
            musicSource.Stop();
        }

        private void ValidateReferences()
        {
#if UNITY_INCLUDE_TESTS
            if (Application.isPlaying)
                return;
#endif
            ReferenceValidator.Validate(sfxSource, nameof(sfxSource), this);
            ReferenceValidator.Validate(musicSource, nameof(musicSource), this);
        }
    }
}
