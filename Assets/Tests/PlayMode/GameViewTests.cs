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
        private GameObject _gameOverScreen;
        private Button _restartButton;

        [UnitySetUp]
        public IEnumerator SetUp()
        {
            _go = new GameObject("GameViewTestObject");
            _view = _go.AddComponent<GameView>();

            _gameOverScreen = new GameObject("GameOverScreen");
            _gameOverScreen.transform.SetParent(_go.transform);
            _view.GetType().GetField("gameOverScreen", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
                ?.SetValue(_view, _gameOverScreen);

            _restartButton = _gameOverScreen.AddComponent<Button>();
            _view.GetType().GetField("restartButton", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
                ?.SetValue(_view, _restartButton);

            yield return null;
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