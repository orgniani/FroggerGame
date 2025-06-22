using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using Helpers;
using Interfaces;

namespace Input
{
    public class InputManager : MonoBehaviour, IInputManager
    {
        [Header("Inputs")]
        [SerializeField] private InputActionAsset inputActions;
        [SerializeField] private string moveAction = "Move";

        private InputAction _moveAction;
        public UnityEvent<Vector2> OnMoveInput { get; } = new UnityEvent<Vector2>();

        private void Awake()
        {
            ReferenceValidator.Validate(inputActions, nameof(inputActions), this);
        }

        private void OnEnable()
        {
            SetupInput();
        }

        private void OnDisable()
        {
            if (_moveAction != null)
            {
                _moveAction.performed -= HandleMovementInput;
                _moveAction.canceled -= HandleMovementInput;
                _moveAction.Disable();
            }
        }

        public void SetupInput()
        {
            if (inputActions == null) return;

            _moveAction = inputActions.FindAction(moveAction);
            if (_moveAction != null)
            {
                _moveAction.performed += HandleMovementInput;
                _moveAction.canceled += HandleMovementInput;
                _moveAction.Enable();
            }
        }

        private void HandleMovementInput(InputAction.CallbackContext ctx)
        {
            Vector2 movementInput = ctx.ReadValue<Vector2>();
            OnMoveInput?.Invoke(movementInput);
        }
    }
}
