using NUnit.Framework;
using Obstacle;

namespace Tests.Editor.Obstacle
{
    public class ObstacleModelTests
    {
        [Test]
        public void UpdatePosition_MovesRight_WhenSpeedPositive()
        {
            var model = new ObstacleModel(0f, -10f, 10f, 5f);
            model.UpdatePosition(1f);
            Assert.AreEqual(5f, model.CurrentX, 0.01f);
        }

        [Test]
        public void UpdatePosition_MovesLeft_WhenSpeedNegative()
        {
            var model = new ObstacleModel(0f, -10f, 10f, -3f);
            model.UpdatePosition(1f);
            Assert.AreEqual(-3f, model.CurrentX, 0.01f);
        }

        [Test]
        public void UpdatePosition_WrapsAroundRightToLeft_WhenOutOfBounds()
        {
            var model = new ObstacleModel(9f, -10f, 10f, 2f);
            model.UpdatePosition(1f);
            Assert.AreEqual(-10f, model.CurrentX, 0.01f);
        }

        [Test]
        public void UpdatePosition_WrapsAroundLeftToRight_WhenOutOfBounds()
        {
            var model = new ObstacleModel(-9f, -10f, 10f, -2f);
            model.UpdatePosition(1f);
            Assert.AreEqual(10f, model.CurrentX, 0.01f);
        }

        [Test]
        public void SetPosition_UpdatesCorrectly()
        {
            var model = new ObstacleModel(0f, -10f, 10f, 1f);
            model.SetPosition(42f);
            Assert.AreEqual(42f, model.CurrentX);
        }
    }
}