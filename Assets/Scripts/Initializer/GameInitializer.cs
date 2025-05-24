using Game;
using Player;
using Input;
using UnityEngine;

namespace Initializer
{
    public class GameInitializer : MonoBehaviour
    {
        [Header("Views")]
        [SerializeField] private GameView gameView;
        [SerializeField] private PlayerView playerView;
        [SerializeField] private InputManager inputManager;

        private void Start()
        {
            float startingX = 0;
            float startingY = playerView.transform.position.y;

            //TODO: Replace hardcoded values!!!
            float laneWidth = 1f;
            float laneHeight = 1f;

            float maxY = 4.5f;
            float minX = -8.5f;
            float maxX = 8.5f;
            var gameModel = new GameModel();
            var playerModel = new PlayerModel(startingX, startingY, laneWidth, laneHeight, maxY, minX, maxX);

            var gamePresenter = new GamePresenter(gameModel, gameView);
            var playerPresenter = new PlayerPresenter(playerModel, playerView, gamePresenter, inputManager);
        }
    }
}