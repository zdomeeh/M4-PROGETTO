using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretAim : MonoBehaviour
{
    [SerializeField] private GameObject _projectilePrefab; // proiettile
    [SerializeField] private Transform _firePoint;         // punto da cui sparare
    [SerializeField] private float _fireRate = 1f;         // colpi al secondo
    [SerializeField] private float _projectileSpeed = 10f;

    [SerializeField] private Transform _player;            // player da seguire
    [SerializeField] private float _rotationSpeed = 5f;    // velocità rotazione verso player

    private bool _playerInRange = false;
    private float _nextFireTime = 0f;

    void Update()
    {
        if (_playerInRange && _player != null)
        {
            // Ruota verso il player
            Vector3 targetDir = _player.position - transform.position;
            targetDir.y = 0; // mantiene l'orientamento orizzontale
            if (targetDir != Vector3.zero)
            {
                Quaternion targetRot = Quaternion.LookRotation(targetDir);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, _rotationSpeed * Time.deltaTime);
            }

            // Spara se è tempo
            if (Time.time >= _nextFireTime)
            {
                Fire();
                _nextFireTime = Time.time + 1f / _fireRate;
            }
        }
    }

    void Fire()
    {
        if (_projectilePrefab != null && _firePoint != null)
        {
            GameObject projectile = Instantiate(_projectilePrefab, _firePoint.position, _firePoint.rotation);
            Rigidbody rb = projectile.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.velocity = (_player.position - _firePoint.position).normalized * _projectileSpeed;
            }
        }
    }

    // Trigger di attivazione
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _playerInRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _playerInRange = false;
        }
    }
}
