using UnityEngine;

namespace Game
{
    public class GameView : MonoBehaviour
    {
        [SerializeField] private GameObject gameOverScreen;

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
