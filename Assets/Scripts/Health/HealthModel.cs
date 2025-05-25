using UnityEngine;
using UnityEngine.Events;

namespace Health
{
    public class HealthModel
    {
        public int MaxLives { get; }
        public int CurrentLives { get; private set; }

        public UnityEvent<int> OnLivesChanged = new UnityEvent<int>();

        public bool IsDepleted => CurrentLives <= 0;

        public HealthModel(int maxLives)
        {
            MaxLives = maxLives;
            CurrentLives = maxLives;
        }

        public void TakeDamage(int amount)
        {
            CurrentLives -= amount;
            CurrentLives = Mathf.Max(0, CurrentLives);
            OnLivesChanged?.Invoke(CurrentLives);
        }

        public void Reset()
        {
            CurrentLives = MaxLives;
            OnLivesChanged?.Invoke(CurrentLives);
        }
    }
}
