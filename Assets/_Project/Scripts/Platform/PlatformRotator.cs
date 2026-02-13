using UnityEngine;

public class PlatformRotator : MonoBehaviour
{
    [SerializeField] private float _rotationSpeed = 60f;

    private bool _playerOnPlatform = false;
    private Rigidbody _playerRb;
    private Quaternion _lastRotation;

    void Start()
    {
        _lastRotation = transform.rotation;
    }

    void FixedUpdate()
    {
        transform.Rotate(Vector3.up * _rotationSpeed * Time.fixedDeltaTime, Space.World);

        if (_playerOnPlatform && _playerRb != null)
        {
            Quaternion deltaRot = transform.rotation * Quaternion.Inverse(_lastRotation);
            Vector3 direction = _playerRb.position - transform.position;
            direction = deltaRot * direction;
            _playerRb.MovePosition(transform.position + direction);
        }

        _lastRotation = transform.rotation;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            _playerOnPlatform = true;
            _playerRb = collision.collider.GetComponent<Rigidbody>();
            _lastRotation = transform.rotation;
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
