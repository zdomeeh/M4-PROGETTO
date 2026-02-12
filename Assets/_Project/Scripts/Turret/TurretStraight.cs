using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretStraight : MonoBehaviour
{
    [SerializeField] private GameObject _projectilePrefab; // prefab del proiettile
    [SerializeField] private Transform _firePoint;         // punto da cui sparare
    [SerializeField] private float _fireRate = 1f;         // colpi al secondo
    [SerializeField] private float _projectileSpeed = 10f;

    private float _nextFireTime = 0f;

    void Update()
    {
        if (Time.time >= _nextFireTime)
        {
            Fire();
            _nextFireTime = Time.time + 1f / _fireRate;
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
                rb.velocity = _firePoint.forward * _projectileSpeed;
            }
        }
    }
}
