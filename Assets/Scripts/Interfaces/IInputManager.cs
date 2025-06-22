using UnityEngine;
using UnityEngine.Events;

namespace Input
{
    public interface IInputManager
    {
        UnityEvent<Vector2> OnMoveInput { get; }
    }
}
