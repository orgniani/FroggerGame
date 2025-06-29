using Interfaces;
using UnityEngine.Events;

namespace Tests.Mocks
{
    public class MockPlayerView : IPlayerView
    {
        public float LastX, LastY;
        public int LastFacing;
        public bool PlayedDead;
        
        public UnityEvent OnObstacleHit { get; } = new UnityEvent();
        public void UpdatePosition(float newX, float newY) { LastX = newX; LastY = newY; }
        public void SetFacingDirection(int xDir) { LastFacing = xDir; }
        public void ResetPosition() { LastX = 0f; LastY = 0f; }
        public void PlayGameOverAnimation(bool isDead) { PlayedDead = isDead; }
    }

}