using UnityEngine;
using UnityEngine.Events;

namespace Interfaces
{
    public interface IInputManager
    {
        UnityEvent<Vector2> OnMoveInput { get; }
    }
}
