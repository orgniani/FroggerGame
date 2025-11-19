using NUnit.Framework;
using System.Collections;
using UnityEngine;
using UnityEngine.TestTools;
using Audio;

namespace Tests.PlayMode
{
    public class AudioViewTests
    {
        private GameObject _go;
        private AudioView _view;

        private AudioSource _music;

        private AudioClip _clip;

        [UnitySetUp]
        public IEnumerator SetUp()
        {
            _go = new GameObject("AudioViewTestObject");
            _view = _go.AddComponent<AudioView>();

            _music = _go.AddComponent<AudioSource>();

            _view.GetType().GetField("musicSource",
                System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
                ?.SetValue(_view, _music);

            _clip = AudioClip.Create("testClip", 1, 1, 44100, false);

            yield return null;
        }

        [UnityTest]
        public IEnumerator PlayMusic_SetsClipAndPlays()
        {
            _view.PlayMusic(_clip);
            yield return null;

            Assert.AreEqual(_clip, _music.clip);

            Assert.IsTrue(_music.loop);
            Assert.IsTrue(_music.isPlaying);
        }

        [UnityTest]
        public IEnumerator StopMusic_StopsPlayback()
        {
            _view.PlayMusic(_clip);
            yield return null;

            _view.StopMusic();
            yield return null;

            Assert.IsFalse(_music.isPlaying);
        }

        [UnityTearDown]
        public IEnumerator TearDown()
        {
            Object.Destroy(_go);
            yield return null;
        }
    }
}
