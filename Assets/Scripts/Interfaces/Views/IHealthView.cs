namespace Interfaces
{
    public interface IHealthView
    {
        void Setup(int maxLives);
        void UpdateHearts(int currentLives);
    }

}