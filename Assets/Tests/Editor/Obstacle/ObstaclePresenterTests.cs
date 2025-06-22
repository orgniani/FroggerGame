using NUnit.Framework;
using Obstacle;
using Tests.Mocks;

namespace Tests.Editor.Obstacle
{
    public class ObstaclePresenterTests
    {
        private ObstacleModel _model;
        private MockObstacleView _mockView;
        private ObstaclePresenter _presenter;

        [SetUp]
        public void Setup()
        {
            _model = new ObstacleModel(0f, -10f, 10f, 2f);
            var viewGO = new UnityEngine.GameObject("MockObstacleView");
            _mockView = viewGO.AddComponent<MockObstacleView>();
            _presenter = new ObstaclePresenter(_model, _mockView);
        }

        [Test]
        public void Update_CallsSetXPosition_WithUpdatedX()
        {
            _presenter.Update(1f); // 0 + 2 = 2
            Assert.AreEqual(2f, _mockView.transform.position.x, 0.01f);
        }

        [Test]
        public void Reset_SetsModelAndViewXToGivenValue()
        {
            _presenter.Reset(42f);
            Assert.AreEqual(42f, _model.CurrentX, 0.01f);
            Assert.AreEqual(42f, _mockView.transform.position.x, 0.01f);
        }

        [TearDown]
        public void Teardown()
        {
            UnityEngine.Object.DestroyImmediate(_mockView.gameObject);
        }
    }
}