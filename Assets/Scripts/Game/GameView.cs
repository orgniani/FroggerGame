using Helpers;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Interfaces;

namespace Game
{
    public class GameView : MonoBehaviour, IGameView
    {
        [Header("Screens")]
        [SerializeField] private GameObject mainMenuScreen;
        [SerializeField] private GameObject gameOverScreen;

        [Header("Buttons")]
        [SerializeField] private Button playButton;
        [SerializeField] private Button restartButton;

        public UnityEvent OnRestartButtonClicked { get; } = new UnityEvent();
        public UnityEvent OnPlayButtonClicked { get; } = new UnityEvent();

        private void Awake()
        {
            ValidateReferences();
        }

        private void Start()
        {
            ShowMainMenu();
            HideGameOver();

            playButton.onClick.AddListener(() => OnPlayButtonClicked?.Invoke());
            restartButton.onClick.AddListener(() => OnRestartButtonClicked?.Invoke());
        }

        private void ShowMainMenu()
        {
            mainMenuScreen.SetActive(true);
        }

        public void HideMainMenu()
        {
            mainMenuScreen.SetActive(false);
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

            ReferenceValidator.Validate(mainMenuScreen, nameof(mainMenuScreen), this);
            ReferenceValidator.Validate(gameOverScreen, nameof(gameOverScreen), this);
            ReferenceValidator.Validate(playButton, nameof(playButton), this);
            ReferenceValidator.Validate(restartButton, nameof(restartButton), this);
        }
    }
}
