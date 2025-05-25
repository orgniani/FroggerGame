using UnityEngine;

namespace Player
{
    public class PlayerInputHandler
    {

        private readonly float _inputThreshold;

        public PlayerInputHandler(float inputThreshold)
        {
            _inputThreshold = inputThreshold;
        }

        public Vector2Int GetMoveDirection(Vector2 input)
        {
            int yDir = input.y > _inputThreshold ? 1 : (input.y < -_inputThreshold ? -1 : 0);
            int xDir = input.x > _inputThreshold ? 1 : (input.x < -_inputThreshold ? -1 : 0);

            return new Vector2Int(xDir, yDir);
        }
    }
}
