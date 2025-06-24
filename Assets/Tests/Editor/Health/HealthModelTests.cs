using NUnit.Framework;
using Health;

namespace Tests.Editor.Health
{
    public class HealthModelTests
    {
        [Test]
        public void Initializes_With_Max_Lives()
        {
            var model = new HealthModel(3);
            Assert.AreEqual(3, model.CurrentLives);
        }

        [Test]
        public void Takes_Damage_Reduces_Lives()
        {
            var model = new HealthModel(3);
            model.TakeDamage(1);
            Assert.AreEqual(2, model.CurrentLives);
        }

        [Test]
        public void Lives_Do_Not_Go_Below_Zero()
        {
            var model = new HealthModel(2);
            model.TakeDamage(5);
            Assert.AreEqual(0, model.CurrentLives);
        }

        [Test]
        public void Reset_Restores_Lives_To_Max()
        {
            var model = new HealthModel(4);
            model.TakeDamage(3);
            model.Reset();
            Assert.AreEqual(4, model.CurrentLives);
        }

        [Test]
        public void OnLivesChanged_Is_Invoked()
        {
            var model = new HealthModel(3);
            int calledWith = -1;
            model.OnLivesChanged.AddListener(val => calledWith = val);

            model.TakeDamage(1);

            Assert.AreEqual(2, calledWith);
        }
    }
}