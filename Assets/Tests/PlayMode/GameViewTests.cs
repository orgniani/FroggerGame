using NUnit.Framework;
using System.Collections;
using UnityEngine;
using UnityEngine.TestTools;
using Game;
using UnityEngine.UI;

namespace Tests.PlayMode
{
    public class GameViewTests
    {
        private GameObject _go;
        private GameView _view;

        private GameObject _mainMenuScreen;
        private GameObject _gameOverScreen;

        private Button _playButton;
        private Button _restartButton;

        [UnitySetUp]
        public IEnumerator SetUp()
        {
            _go = new GameObject("GameViewTestObject");
            _view = _go.AddComponent<GameView>();

            _mainMenuScreen = new GameObject("MainMenuScreen");
            _mainMenuScreen.transform.SetParent(_go.transform);

            _gameOverScreen = new GameObject("GameOverScreen");
            _gameOverScreen.transform.SetParent(_go.transform);

            _playButton = _mainMenuScreen.AddComponent<Button>();
            _restartButton = _gameOverScreen.AddComponent<Button>();

            _view.GetType().GetField("mainMenuScreen",
                System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
                ?.SetValue(_view, _mainMenuScreen);

            _view.GetType().GetField("gameOverScreen",
                System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
                ?.SetValue(_view, _gameOverScreen);

            _view.GetType().GetField("playButton",
                System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
                ?.SetValue(_view, _playButton);

            _view.GetType().GetField("restartButton",
                System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
                ?.SetValue(_view, _restartButton);

            _view.Invoke("Start", 0f);

            yield return null;
        }

        [UnityTest]
        public IEnumerator Start_ShowsMainMenu_AndHidesGameOver()
        {
            yield return null;

            Assert.IsTrue(_mainMenuScreen.activeSelf);
            Assert.IsFalse(_gameOverScreen.activeSelf);
        }

        [UnityTest]
        public IEnumerator ShowGameOver_EnablesGameOverScreen()
        {
            _view.ShowGameOver();
            yield return null;

            Assert.IsTrue(_gameOverScreen.activeSelf);
        }

        [UnityTest]
        public IEnumerator HideGameOver_DisablesGameOverScreen()
        {
            _gameOverScreen.SetActive(true);
            _view.HideGameOver();
            yield return null;

            Assert.IsFalse(_gameOverScreen.activeSelf);
        }

        [UnityTest]
        public IEnumerator PlayButton_TriggersEvent()
        {
            bool clicked = false;
            _view.OnPlayButtonClicked.AddListener(() => clicked = true);

            _playButton.onClick.Invoke();
            yield return null;

            Assert.IsTrue(clicked);
        }

        [UnityTest]
        public IEnumerator RestartButton_TriggersEvent()
        {
            bool clicked = false;
            _view.OnRestartButtonClicked.AddListener(() => clicked = true);

            _restartButton.onClick.Invoke();
            yield return null;

            Assert.IsTrue(clicked);
        }

        [UnityTearDown]
        public IEnumerator TearDown()
        {
            Object.Destroy(_go);
            yield return null;
        }
    }
}