using UnityEngine.Events;

namespace Interfaces
{
    public interface IGameView
    {
        UnityEvent OnRestartButtonClicked { get; }

        void ShowGameOver();
        void HideGameOver();
    }
}