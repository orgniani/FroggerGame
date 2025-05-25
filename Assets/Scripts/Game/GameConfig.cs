using UnityEngine;

namespace Game
{
    [CreateAssetMenu(menuName = "Config/Game", fileName = "GameCfg", order = 0)]
    public class GameConfig : ScriptableObject
    {
        [field: SerializeField] public float LaneWidth { get; private set; } = 1f;
        [field: SerializeField] public float LaneHeight { get; private set; } = 1f;
        [field: SerializeField] public int LaneAmount { get; private set; } = 6;
        [field: SerializeField] public float TopBoundary { get; private set; } = 4.5f;
        [field: SerializeField] public float BottomBoundary { get; private set; } = -2.5f;
        [field: SerializeField] public float LeftBoundary { get; private set; } = -8.5f;
        [field: SerializeField] public float RightBoundary { get; private set; } = 8.5f;
        [field: SerializeField] public int MaxLives { get; private set; } = 3;
        [field: SerializeField] public float InputThreshold { get; private set; } = 0.5f;
    }
}