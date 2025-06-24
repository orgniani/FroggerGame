using NUnit.Framework;
using Game;
using Tests.Mocks;

namespace Tests.Editor.Game
{
    public class GamePresenterTests
    {
        private GameModel _model;
        private GamePresenter _presenter;
        
        private MockGameView _mockView;
        private MockPlayerPresenter _mockPlayer;

        [SetUp]
        public void Setup()
        {
            _model = new GameModel();
            _mockView = new MockGameView();
            _mockPlayer = new MockPlayerPresenter();

            _presenter = new GamePresenter(_model, _mockView, _mockPlayer);
        }

        [Test]
        public void TriggerGameOver_UpdatesModelAndView()
        {
            _presenter.TriggerGameOver();

            Assert.IsTrue(_model.IsGameOver);
            Assert.IsTrue(_mockView.GameOverShown);
        }

        [Test]
        public void ResetGame_ResetsEverything()
        {
            _model.EndGame();
            _presenter.ResetGame();

            Assert.IsFalse(_model.IsGameOver);
            Assert.IsTrue(_mockView.GameOverHidden);
            Assert.IsTrue(_mockPlayer.WasReset);
        }

        [Test]
        public void OnRestartButtonClicked_ResetsGame()
        {
            _model.EndGame();
            _mockView.OnRestartButtonClicked.Invoke();

            Assert.IsFalse(_model.IsGameOver);
            Assert.IsTrue(_mockView.GameOverHidden);
            Assert.IsTrue(_mockPlayer.WasReset);
        }

        [Test]
        public void OnGameOverTriggered_CallsTriggerGameOver()
        {
            _mockPlayer.OnGameOverTriggered.Invoke();

            Assert.IsTrue(_model.IsGameOver);
            Assert.IsTrue(_mockView.GameOverShown);
        }

        [Test]
        public void Presenter_SubscribesToPlayerGameOverEvent()
        {
            _mockPlayer.OnGameOverTriggered.Invoke();
            Assert.IsTrue(_model.IsGameOver);
        }

        [Test]
        public void ResetGame_WithoutGameOver_StillCallsViewAndPlayerReset()
        {
            _presenter.ResetGame();

            Assert.IsFalse(_model.IsGameOver);
            Assert.IsTrue(_mockView.GameOverHidden);
            Assert.IsTrue(_mockPlayer.WasReset);
        }

        [Test]
        public void TriggerGameOver_WhenGameAlreadyOver_DoesNothing()
        {
            _model.EndGame();
            _presenter.TriggerGameOver();

            Assert.AreEqual(0, _mockView.TimesGameOverShown);
        }

        [Test]
        public void TriggerGameOver_DoesNotTriggerTwice()
        {
            _presenter.TriggerGameOver();
            _presenter.TriggerGameOver();

            Assert.IsTrue(_model.IsGameOver);
            Assert.AreEqual(1, _mockView.TimesGameOverShown);
        }

        [Test]
        public void Presenter_HidesGameOverOnStart()
        {
            Assert.IsTrue(_mockView.GameOverHidden);
        }
    }
}