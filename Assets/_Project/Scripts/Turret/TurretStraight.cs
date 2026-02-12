using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretStraight : MonoBehaviour
{
    [SerializeField] private GameObject _projectilePrefab;
    [SerializeField] private Transform _firePoint;
    [SerializeField] private float _fireRate = 1f;
    [SerializeField] private float _projectileSpeed = 5f; // più basso per test visibile

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
            GameObject proj = Instantiate(_projectilePrefab, _firePoint.position, _firePoint.rotation);
            Rigidbody rb = proj.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.velocity = _firePoint.forward * _projectileSpeed;
            }
        }
    }
}
