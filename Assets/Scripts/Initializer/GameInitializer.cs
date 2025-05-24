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

            float startingY = playerView.transform.position.y;
            float laneHeight = 1f;
            float maxY = 2f;

            var gameModel = new GameModel();
            var playerModel = new PlayerModel(startingY, laneHeight, maxY);

            var gamePresenter = new GamePresenter(gameModel, gameView);
            var playerPresenter = new PlayerPresenter(playerModel, playerView, gamePresenter, inputManager);
        }
    }
}