using UnityEngine.Events;

namespace Interfaces
{
    public interface IPlayerView
    {
        UnityEvent OnObstacleHit { get; }

        void UpdatePosition(float newX, float newY);
        void SetFacingDirection(int xDir);
        void ResetPosition();
        void PlayGameOverAnimation(bool isDead);
    }
}
