using Interfaces;
using UnityEngine.Events;

namespace Tests.Mocks
{
    public class MockGameView : IGameView
    {
        public UnityEvent OnPlayButtonClicked { get; } = new UnityEvent();
        public UnityEvent OnRestartButtonClicked { get; } = new UnityEvent();

        public bool MainMenuHidden { get; private set; }

        public bool GameOverShown { get; private set; }
        public int TimesGameOverShown { get; private set; }

        public bool GameOverHidden { get; private set; }

        public void HideMainMenu()
        {
            MainMenuHidden = true;
        }

        public void ShowGameOver()
        {
            GameOverShown = true;
            TimesGameOverShown++;
        }

        public void HideGameOver()
        {
            GameOverHidden = true;
        }
    }
}