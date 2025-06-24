using Interfaces;
using UnityEngine.Events;

namespace Tests.Mocks
{
    public class MockGameView : IGameView
    {
        public bool GameOverShown { get; private set; }
        public bool GameOverHidden { get; private set; }
        public int TimesGameOverShown { get; private set; }
        public UnityEvent OnRestartButtonClicked { get; } = new UnityEvent();

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