using Interfaces;
using UnityEngine.Events;

namespace Tests.Mocks
{
    public class MockPlayerPresenter : IPlayerPresenter
    {
        public UnityEvent OnGameOverTriggered { get; } = new UnityEvent();
        public bool WasReset { get; private set; }

        public void ResetPlayer() => WasReset = true;
    }
}