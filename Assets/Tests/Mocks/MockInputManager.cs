using Interfaces;
using UnityEngine;
using UnityEngine.Events;

namespace Tests.Mocks
{
    public class MockInputManager : IInputManager
    {
        public UnityEvent<Vector2> OnMoveInput { get; } = new UnityEvent<Vector2>();

        public void SimulateInput(Vector2 dir) => OnMoveInput.Invoke(dir);
    }

}