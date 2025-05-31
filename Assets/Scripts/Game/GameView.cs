using Helpers;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Game
{
    public class GameView : MonoBehaviour
    {
        [Header("Buttons")]
        [SerializeField] private GameObject gameOverScreen;
        [SerializeField] private Button restartButton;

        public UnityEvent OnRestartButtonClicked = new UnityEvent();
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
            ReferenceValidator.Validate(gameOverScreen, nameof(gameOverScreen), this);
            ReferenceValidator.Validate(restartButton, nameof(restartButton), this);
        }
    }
}
