using UnityEngine;
using UnityEngine.InputSystem;

namespace S_Characters.S_Player
{
    public class InputManager : IInput
    {
        private readonly Inputs _inputs;
        private Vector2 _moveInput;
        
        public InputManager(Inputs inputs)
        {
            _inputs = inputs;
        }

        public void OnEnable()
        {
            _inputs.Enable();
            _inputs.Player.Mov.performed += OnMove;
            _inputs.Player.Mov.canceled += OnMove;
        }

        public void OnDisable()
        {
            _inputs.Disable();
            _inputs.Player.Mov.performed -= OnMove;
            _inputs.Player.Mov.canceled -= OnMove;
        }

        public void OnMove(InputAction.CallbackContext context)
        {
            _moveInput = context.ReadValue<Vector2>();
        }

        public Vector2 GetPlayerMovement()
        {
            return _moveInput;
        }
    }
}