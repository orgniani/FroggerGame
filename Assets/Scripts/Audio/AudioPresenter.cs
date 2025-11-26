using Interfaces;
using Config;

namespace Audio
{
    public class AudioPresenter
    {
        private readonly IAudioView _view;
        private readonly IAudioConfig _config;

        public AudioPresenter(IAudioView view, IAudioConfig config)
        {
            _view = view;
            _config = config;
        }

        public void PlayHit() => _view.PlaySfx(_config.SoundHit);
        public void PlayJump() => _view.PlaySfx(_config.SoundJump);
        public void PlayButton() => _view.PlaySfx(_config.SoundButton);
        public void PlayGameOver() => _view.PlaySfx(_config.SoundGameOver);
        public void PlayGameWin() => _view.PlaySfx(_config.SoundGameWin);

        public void PlayMusic() => _view.PlayMusic(_config.MusicClip);
        public void StopMusic() => _view.StopMusic();
    }

}