using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMover : MonoBehaviour
{
    [SerializeField] private float _currentSpeed;
    [SerializeField] private PlayerAnimator _playerAnimator;
    [SerializeField] private float _rotationSpeed = 10f;
    [SerializeField] private CircleFillController _circleFillController;

    private readonly float _minSpeed = 20f;
    private readonly float _maxSpeed = 25f;
    private Rigidbody _rigidbody;
    private Vector3 _moveDirection;
    private Vector3 _move;

    private bool IsMoving => _moveDirection != Vector3.zero;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _currentSpeed = _minSpeed;
    }

    private void OnEnable()
    {
        PlayerInput.Get.Enable();
    }

    private void OnDisable()
    {
        PlayerInput.Get.Disable();
    }

    private void Update()
    {
        _moveDirection = PlayerInput.Get.Player.Move.ReadValue<Vector3>();
        _playerAnimator.UpdateRun(IsMoving);
    }

    private void FixedUpdate()
    {
        Move();
    }

    public void IncreaseMovespeed(float value)
    {
        if (value > 0)
        {
            _currentSpeed += value;
            _currentSpeed = Mathf.Clamp(_currentSpeed, _minSpeed, _maxSpeed);
        }
    }

    private void Move()
    {
        _move = new Vector3(_moveDirection.x * _currentSpeed, 0f, _moveDirection.z * _currentSpeed);
        _rigidbody.velocity = _currentSpeed * Time.fixedDeltaTime * _move;

        if (_moveDirection != Vector3.zero)
        {
            Rotation(_moveDirection);
            _circleFillController.ExperienceCircleRotation();
        }
    }

    private void Rotation(Vector3 movementDirection)
    {
        Quaternion toRotation = Quaternion.LookRotation(movementDirection, Vector3.up);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, _rotationSpeed);
    }
}