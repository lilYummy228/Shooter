using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput), typeof(CharacterMovementController))]
public class Player : MonoBehaviour
{
    [SerializeField] private Camera _camera;
 
    private PlayerInput _playerInput;
    private CharacterMovementController _movementController;

    private void Awake()
    {       
        _movementController = GetComponent<CharacterMovementController>();
        _playerInput = new PlayerInput();
    }

    private void OnEnable()
    {
        _playerInput.Enable();
        _playerInput.Player.Jump.performed += OnJump;
    }

    private void OnDisable()
    {
        _playerInput.Disable();
        _playerInput.Player.Jump.performed -= OnJump;
    }

    private void Update()
    {
        _movementController.Rotate(_playerInput.Player.Rotate.ReadValue<Vector2>(), _camera);
        _movementController.Move(_camera, _playerInput.Player.Move.ReadValue<Vector3>());        
    }

    private void OnJump(InputAction.CallbackContext context)
    {
        _movementController.Jump();
    }
}
