using UnityEngine;

public class Shuriken : MonoBehaviour
{
    [SerializeField] private Transform _player;
    [SerializeField] private float _rotationSpeed = 30f;
    [SerializeField] private float _radius = 5f;
    [SerializeField] private float _damage = 10f;
    [SerializeField] private float _height = 1f;

    private Vector3 _initialOffset;
    private float _currentAngle;

    private void Start()
    {
        _initialOffset = new Vector3(_radius, _height, 0);
        _currentAngle = 0;
        UpdatePosition();
    }

    private void Update()
    {
        _currentAngle += _rotationSpeed * Time.deltaTime;

        if (_currentAngle >= 360f)
            _currentAngle -= 360f;

        UpdatePosition();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Health health))
        {
            if (health.TryGetComponent(out Enemy _))
                health.TakeDamage(_damage);
            else if (health.TryGetComponent(out ScrapMine _))
                health.TakeDamage(1);
        }
    }

    private void UpdatePosition()
    {
        Quaternion rotation = Quaternion.Euler(0, _currentAngle, 0);
        Vector3 offsetPosition = rotation * _initialOffset;
        transform.position = _player.position + offsetPosition;
    }
}