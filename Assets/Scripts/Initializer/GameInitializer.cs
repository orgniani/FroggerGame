using Game;
using Player;
using Health;
using Input;
using Helpers;
using UnityEngine;

namespace Initializer
{
    public class GameInitializer : MonoBehaviour
    {
        [Header("Views")]
        [SerializeField] private GameView gameView;
        [SerializeField] private PlayerView playerView;
        [SerializeField] private HealthView healthView;

        [Header("Managers")]
        [SerializeField] private InputManager inputManager;

        private void Awake()
        {
            ValidateReferences();
        }

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

            int maxLives = 3;

            var gameModel = new GameModel();
            var healthModel = new HealthModel(maxLives);
            var playerModel = new PlayerModel(startingX, startingY, laneWidth, laneHeight, maxY, minX, maxX, healthModel);

            var gamePresenter = new GamePresenter(gameModel, gameView);
            var healthPresenter = new HealthPresenter(healthModel, healthView);
            var playerPresenter = new PlayerPresenter(playerModel, playerView, healthModel, gamePresenter, inputManager);
        }

        private void ValidateReferences()
        {
            ReferenceValidator.Validate(gameView, nameof(gameView), this);
            ReferenceValidator.Validate(playerView, nameof(playerView), this);
            ReferenceValidator.Validate(healthView, nameof(healthView), this);
            ReferenceValidator.Validate(inputManager, nameof(inputManager), this);
        }
    }
}