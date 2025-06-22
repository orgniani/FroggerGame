using Helpers;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Interfaces;

namespace Game
{
    public class GameView : MonoBehaviour, IGameView
    {
        [Header("Buttons")]
        [SerializeField] private GameObject gameOverScreen;
        [SerializeField] private Button restartButton;

        public UnityEvent OnRestartButtonClicked { get; } = new UnityEvent();

        private void Awake()
        {
            ValidateReferences();
        }

        private void Start()
        {
            HideGameOver();
            restartButton.onClick.AddListener(() => OnRestartButtonClicked?.Invoke());
        }

        public void ShowGameOver()
        {
            gameOverScreen.SetActive(true);
        }

        public void HideGameOver()
        {
            gameOverScreen.SetActive(false);
        }

        private void ValidateReferences()
        {
#if UNITY_INCLUDE_TESTS
            if (Application.isPlaying)
                return;
#endif

            ReferenceValidator.Validate(gameOverScreen, nameof(gameOverScreen), this);
            ReferenceValidator.Validate(restartButton, nameof(restartButton), this);
        }
    }
}
