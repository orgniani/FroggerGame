using Game;
using Player;
using Health;
using Input;
using Helpers;
using Config;
using UnityEngine;
using Audio;

namespace Initializer
{
    public class GameInitializer : MonoBehaviour
    {
        [Header("Views")]
        [SerializeField] private GameView gameView;
        [SerializeField] private PlayerView playerView;
        [SerializeField] private HealthView healthView;
        [SerializeField] private AudioView audioView;

        [Header("Managers")]
        [SerializeField] private InputManager inputManager;

        [Header("Configs")]
        [SerializeField] private GameConfig gameConfig;
        [SerializeField] private AudioConfig audioConfig;

        private void Awake()
        {
            ValidateReferences();
        }

        private void Start()
        {
            Vector3 startingPosition = playerView.transform.position;

            var gameModel = new GameModel();
            var healthModel = new HealthModel(gameConfig.MaxLives);

            var playerModel = new PlayerModel(startingPosition, gameConfig);

            var healthPresenter = new HealthPresenter(healthModel, healthView);

            var inputThreshold = gameConfig.InputThreshold;
            var playerPresenter = new PlayerPresenter(playerModel, playerView, healthModel, inputManager, inputThreshold);
            var gamePresenter = new GamePresenter(gameModel, gameView, playerPresenter);

            HookAudio(playerPresenter);
        }

        private void HookAudio(PlayerPresenter playerPresenter)
        {
            var audioPresenter = new AudioPresenter(audioView, audioConfig);

            audioPresenter.PlayMusic();

            gameView.OnPlayButtonClicked.AddListener(audioPresenter.PlayButton);
            gameView.OnRestartButtonClicked.AddListener(audioPresenter.PlayButton);

            playerPresenter.OnHit?.AddListener(audioPresenter.PlayHit);
            playerPresenter.OnJump?.AddListener(audioPresenter.PlayJump);

            playerPresenter.OnGameFinished.AddListener(isWin =>
            {
                if (isWin)
                    audioPresenter.PlayGameWin();
                else
                    audioPresenter.PlayGameOver();
            });
        }

        private void ValidateReferences()
        {
            ReferenceValidator.Validate(gameView, nameof(gameView), this);
            ReferenceValidator.Validate(playerView, nameof(playerView), this);
            ReferenceValidator.Validate(healthView, nameof(healthView), this);
            ReferenceValidator.Validate(audioView, nameof(audioView), this);

            ReferenceValidator.Validate(inputManager, nameof(inputManager), this);

            ReferenceValidator.Validate(gameConfig, nameof(gameConfig), this);
            ReferenceValidator.Validate(audioConfig, nameof(audioConfig), this);
        }
    }
}