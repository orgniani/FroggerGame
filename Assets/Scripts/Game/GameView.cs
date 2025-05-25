using Helpers;
using UnityEngine;

namespace Game
{
    public class GameView : MonoBehaviour
    {
        [SerializeField] private GameObject gameOverScreen;

        private void Awake()
        {
            ReferenceValidator.Validate(gameOverScreen, nameof(gameOverScreen), this);
        }

        private void Start()
        {
            HideGameOver();
        }

        public void ShowGameOver()
        {
            gameOverScreen.SetActive(true);
        }

        public void HideGameOver()
        {
            gameOverScreen.SetActive(false);
        }
    }
}
