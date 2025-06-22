using NUnit.Framework;
using Game;

namespace Tests.Editor.Game
{
    public class GameModelTests
    {
        private GameModel _model;

        [SetUp]
        public void Setup()
        {
            _model = new GameModel();
        }

        [Test]
        public void EndGame_SetsIsGameOverTrue()
        {
            _model.EndGame();
            Assert.IsTrue(_model.IsGameOver);
        }

        [Test]
        public void ResetGame_SetsIsGameOverFalse()
        {
            _model.EndGame();
            _model.ResetGame();

            Assert.IsFalse(_model.IsGameOver);
        }
    }
}