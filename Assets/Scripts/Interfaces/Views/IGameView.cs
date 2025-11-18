using UnityEngine.Events;

namespace Interfaces
{
    public interface IGameView
    {
        UnityEvent OnPlayButtonClicked { get; }
        UnityEvent OnRestartButtonClicked { get; }
        void HideMainMenu();
        void ShowGameOver();
        void HideGameOver();
    }
}