using UnityEngine;

namespace Game
{
    public class GameModel
    {
        public bool IsGameOver { get; private set; }

        public void EndGame()
        {
            IsGameOver = true;
        }

        public void ResetGame()
        {
            IsGameOver = false;
        }
    }
}
