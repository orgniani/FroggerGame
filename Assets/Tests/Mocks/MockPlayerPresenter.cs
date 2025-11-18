using Interfaces;
using UnityEngine.Events;

namespace Tests.Mocks
{
    public class MockPlayerPresenter : IPlayerPresenter
    {
        public UnityEvent OnGameOverTriggered { get; } = new UnityEvent();

        public bool WasReset { get; private set; }
        public int ResetCount { get; private set; }

        public bool WasMovementAllowed { get; private set; }
        public int AllowMovementCount { get; private set; }

        public bool WasMovementBlocked { get; private set; }
        public int BlockMovementCount { get; private set; }

        public void ResetPlayer()
        {
            WasReset = true;
            ResetCount++;
        }

        public void AllowMovement()
        {
            WasMovementAllowed = true;
            AllowMovementCount++;
        }

        public void BlockMovement()
        {
            WasMovementBlocked = true;
            BlockMovementCount++;
        }
    }
}
