using UnityEngine;

namespace Lane
{
    [CreateAssetMenu(menuName = "Config/Lane", fileName = "LaneCfg", order = 0)]
    public class LaneConfig : ScriptableObject
    {
        [field: SerializeField] public int ObstacleWidth { get; private set; } = 1;

        [field: SerializeField] public float Speed { get; private set; }

        [field: SerializeField] public int Distance { get; private set; }

        [field: SerializeField] public Sprite Sprite { get; private set; }
    }
}
