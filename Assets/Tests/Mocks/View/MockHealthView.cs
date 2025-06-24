using Interfaces;

namespace Tests.Mocks
{
    public class MockHealthView : IHealthView
    {
        public int SetupCalledWith { get; private set; }
        public int LastUpdatedValue { get; private set; }

        public void Setup(int maxLives)
        {
            SetupCalledWith = maxLives;
        }

        public void UpdateHearts(int currentLives)
        {
            LastUpdatedValue = currentLives;
        }
    }
}