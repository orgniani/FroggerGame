using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using Helpers;

namespace Input
{
    public class InputManager : MonoBehaviour
    {
        [Header("Inputs")]
        [SerializeField] private InputActionAsset inputActions;
        [SerializeField] private string moveAction = "Move";

        private InputAction _moveAction;
        public UnityEvent<Vector2> OnMoveInput = new UnityEvent<Vector2>();

        private void Awake()
        {
            ReferenceValidator.Validate(inputActions, nameof(inputActions), this);
        }

        private void OnEnable()
        {
            _moveAction = inputActions.FindAction(moveAction);
            if (_moveAction != null)
            {
                _moveAction.performed += HandleMovementInput;
                _moveAction.canceled += HandleMovementInput;
                _moveAction.Enable();
            }
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

        private void HandleMovementInput(InputAction.CallbackContext ctx)
        {
            Vector2 movementInput = ctx.ReadValue<Vector2>();
            OnMoveInput.Invoke(movementInput);
        }
    }
}
