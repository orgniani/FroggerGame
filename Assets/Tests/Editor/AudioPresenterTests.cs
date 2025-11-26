using NUnit.Framework;
using UnityEngine;
using Audio;
using Tests.Mocks;

namespace Tests.Editor.Audio
{
    public class AudioPresenterTests
    {
        private MockAudioView _mockView;
        private MockAudioConfig _config;
        private AudioPresenter _presenter;

        [SetUp]
        public void Setup()
        {
            _mockView = new MockAudioView();
            _config = new MockAudioConfig();

            _config.MusicClip = AudioClip.Create("music", 1, 1, 44100, false);
            _config.SoundHit = AudioClip.Create("hit", 1, 1, 44100, false);
            _config.SoundJump = AudioClip.Create("jump", 1, 1, 44100, false);
            _config.SoundButton = AudioClip.Create("button", 1, 1, 44100, false);
            _config.SoundGameOver = AudioClip.Create("gameover", 1, 1, 44100, false);
            _config.SoundGameWin = AudioClip.Create("win", 1, 1, 44100, false);

            _presenter = new AudioPresenter(_mockView, _config);
        }

        [Test]
        public void PlayHit_UsesCorrectClip()
        {
            _presenter.PlayHit();
            Assert.AreEqual(_config.SoundHit, _mockView.LastSfx);
        }

        [Test]
        public void PlayJump_UsesCorrectClip()
        {
            _presenter.PlayJump();
            Assert.AreEqual(_config.SoundJump, _mockView.LastSfx);
        }

        [Test]
        public void PlayButton_UsesCorrectClip()
        {
            _presenter.PlayButton();
            Assert.AreEqual(_config.SoundButton, _mockView.LastSfx);
        }

        [Test]
        public void PlayGameOver_UsesCorrectClip()
        {
            _presenter.PlayGameOver();
            Assert.AreEqual(_config.SoundGameOver, _mockView.LastSfx);
        }

        [Test]
        public void PlayGameWin_UsesCorrectClip()
        {
            _presenter.PlayGameWin();
            Assert.AreEqual(_config.SoundGameWin, _mockView.LastSfx);
        }

        [Test]
        public void PlayMusic_UsesCorrectClip()
        {
            _presenter.PlayMusic();
            Assert.AreEqual(_config.MusicClip, _mockView.LastMusic);
        }

        [Test]
        public void StopMusic_StopsMusic()
        {
            _presenter.StopMusic();
            Assert.IsTrue(_mockView.MusicStopped);
        }
    }
}
