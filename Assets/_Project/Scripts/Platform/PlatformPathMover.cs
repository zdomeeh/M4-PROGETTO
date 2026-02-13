using UnityEngine;

public class PlatformPathMover : MonoBehaviour
{
    [SerializeField] private Transform[] _points;
    [SerializeField] private float _speed = 2f;

    private int _currentIndex = 0;
    private Vector3 _lastPosition;

    private bool _playerOnPlatform = false;
    private Rigidbody _playerRb;

    void Start()
    {
        if (_points.Length > 0)
            _lastPosition = transform.position;
    }

    void FixedUpdate()
    {
        if (_points.Length == 0) return;

        transform.position = Vector3.MoveTowards(transform.position, _points[_currentIndex].position, _speed * Time.fixedDeltaTime);

        if (Vector3.Distance(transform.position, _points[_currentIndex].position) < 0.1f)
            _currentIndex = (_currentIndex + 1) % _points.Length;

        Vector3 deltaPos = transform.position - _lastPosition;

        if (_playerOnPlatform && _playerRb != null)
            _playerRb.MovePosition(_playerRb.position + deltaPos);

        _lastPosition = transform.position;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            _playerOnPlatform = true;
            _playerRb = collision.collider.GetComponent<Rigidbody>();
            _lastPosition = transform.position;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            _playerOnPlatform = false;
            _playerRb = null;
        }
    }
}
