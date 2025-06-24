using NUnit.Framework;
using Health;
using Tests.Mocks;

namespace Tests.Editor.Health
{
    public class HealthPresenterTests
    {
        [Test]
        public void Setup_And_Initial_Heart_Update_Are_Called()
        {
            var model = new HealthModel(3);
            var mockView = new MockHealthView();
            var presenter = new HealthPresenter(model, mockView);

            Assert.AreEqual(3, mockView.SetupCalledWith);
            Assert.AreEqual(3, mockView.LastUpdatedValue);
        }

        [Test]
        public void Presenter_Updates_View_When_Lives_Change()
        {
            var model = new HealthModel(2);
            var mockView = new MockHealthView();
            var presenter = new HealthPresenter(model, mockView);

            model.TakeDamage(1);

            Assert.AreEqual(1, mockView.LastUpdatedValue);
        }
    }
}