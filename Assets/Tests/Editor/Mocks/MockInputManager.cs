using Input;
using UnityEngine;
using UnityEngine.Events;

namespace Tests.Editor.Mocks
{
    public class MockInputManager : IInputManager
    {
        public UnityEvent<Vector2> OnMoveInput { get; } = new UnityEvent<Vector2>();

        public void SimulateInput(Vector2 dir) => OnMoveInput.Invoke(dir);
    }

}