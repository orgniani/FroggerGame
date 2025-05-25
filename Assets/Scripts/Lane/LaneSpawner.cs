using Game;
using Helpers;
using System.Collections.Generic;
using UnityEngine;

namespace Lane
{
    public class LaneSpawner : MonoBehaviour
    {
        [SerializeField] private LaneView lanePrefab;

        [SerializeField] private List<LaneConfig> laneConfigs;
        [SerializeField] private GameConfig gameConfig;

        private void Awake()
        {
            ValidateReferences();
        }

        private void Start()
        {
            for (int i = 0; i < laneConfigs.Count; i++)
            {
                float spawnPosY = gameConfig.BottomBoundary + (i * gameConfig.LaneHeight);
                Vector3 lanePos = new Vector3(0, spawnPosY, 0);
                LaneDirection direction = GetLaneDirection(i);

                LaneConfig config = laneConfigs[i];
                LaneModel laneModel = new LaneModel(config, direction, gameConfig.LeftBoundary, gameConfig.RightBoundary);
                LaneView laneView = Instantiate(lanePrefab, lanePos, Quaternion.identity, transform);
                LanePresenter lanePresenter = new LanePresenter(laneModel, laneView);
                laneView.SetPresenter(lanePresenter);
            }
        }

        private LaneDirection GetLaneDirection(int laneIndex)
        {
            return laneIndex % 2 == 0 ? LaneDirection.Right : LaneDirection.Left;
        }

        private void ValidateReferences()
        {
            ReferenceValidator.Validate(lanePrefab, nameof(lanePrefab), this);
            ReferenceValidator.Validate(gameConfig, nameof(gameConfig), this);

            if (laneConfigs.Count == 0)
                Debug.LogError("LaneConfigs list is empty!", this);
        }
    }
}