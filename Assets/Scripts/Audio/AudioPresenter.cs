namespace Audio
{
    public class AudioPresenter
    {
        private readonly IAudioView _view;
        private readonly AudioConfig _config;

        public AudioPresenter(IAudioView view, AudioConfig config)
        {
            _view = view;
            _config = config;
        }

        public void PlayHit() => _view.PlaySfx(_config.soundHit);
        public void PlayJump() => _view.PlaySfx(_config.soundJump);
        public void PlayButton() => _view.PlaySfx(_config.soundButton);
        public void PlayGameOver() => _view.PlaySfx(_config.soundGameOver);
        public void PlayGameWin() => _view.PlaySfx(_config.soundGameWin);

        public void PlayMusic() => _view.PlayMusic(_config.musicClip);
        public void StopMusic() => _view.StopMusic();
    }

}