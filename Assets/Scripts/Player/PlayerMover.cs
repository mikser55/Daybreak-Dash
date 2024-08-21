using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMover : MonoBehaviour
{
    [SerializeField] private float _speed;

    private Rigidbody _rigidbody;
    private Vector3 _moveDirection;
    private Vector3 _move;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
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
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        _move = new Vector3(_moveDirection.x * _speed, 0f, _moveDirection.z * _speed);
        _rigidbody.velocity = _speed * Time.fixedDeltaTime * _move;
    }
}