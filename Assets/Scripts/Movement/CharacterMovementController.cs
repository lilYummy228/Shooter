using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class CharacterMovementController : MonoBehaviour
{
    [SerializeField] private float _speed = 7f;
    [SerializeField] private float _strafeSpeed = 7f;
    [SerializeField] private float _verticalTurnSensitivity = 0.6f;
    [SerializeField] private float _horizontalTurnSensitivity = 0.8f;
    [SerializeField] private float _jumpSpeed;

    private CharacterController _characterController;
    private Vector3 _verticalVelocity;
    private float _cameraAngle;
    private float _maxRotationAngle = 89f;
    private float _minRotationAngle = -89f;

    private void Awake()
    {
        _characterController = GetComponent<CharacterController>();
    }

    public Vector3 GetDirection(Camera camera, Vector3 direction)
    {
        float angle = 90f;

        Vector3 forward = Vector3.ProjectOnPlane(camera.transform.forward, Vector3.up).normalized;
        Vector3 right = Quaternion.AngleAxis(angle, Vector3.up) * forward;

        return forward * direction.z * _speed + right * direction.x * _strafeSpeed;
    }

    public void Rotate(Vector2 lookDirection, Camera camera)
    {
        if (lookDirection.sqrMagnitude < 0.1f)
            return;

        _cameraAngle -= lookDirection.y * _verticalTurnSensitivity;
        _cameraAngle = Mathf.Clamp(_cameraAngle, _minRotationAngle, _maxRotationAngle);
        camera.transform.localEulerAngles = Vector3.right * _cameraAngle;

        transform.Rotate(lookDirection.x * _horizontalTurnSensitivity * Vector3.up);
    }

    public void Move(Camera camera, Vector3 direction)
    {
        if (_characterController.isGrounded)
        {
            _characterController.Move((_verticalVelocity + GetDirection(camera, direction)) * Time.deltaTime);
        }
        else
        {
            Vector3 horizontalVelocity = _characterController.velocity;
            horizontalVelocity.y = 0f;
            _verticalVelocity += Physics.gravity * Time.deltaTime;
            _characterController.Move((horizontalVelocity + _verticalVelocity) * Time.deltaTime);
        }
    }

    public void Jump()
    {
        _verticalVelocity = Vector3.up * _jumpSpeed;
    }
}
