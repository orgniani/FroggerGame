using UnityEngine;

namespace Game
{
    public class GameModel
    {
        public bool HasStarted { get; private set; } = false;
        public bool IsGameOver { get; private set; } = false;

        public void StartGame()
        {
            HasStarted = true;
        }

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
