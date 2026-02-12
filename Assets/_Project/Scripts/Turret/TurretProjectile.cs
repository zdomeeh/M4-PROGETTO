using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretProjectile : MonoBehaviour
{
    [SerializeField] private int _damage = 10;
    [SerializeField] private float _lifetime = 5f;

    void Start()
    {
        Destroy(gameObject, _lifetime);
    }

    private void OnTriggerEnter(Collider other)
    {
        LifeController life = other.GetComponent<LifeController>();
        if (life != null)
        {
            life.AddHP(-_damage);
        }
        Destroy(gameObject);
    }
}
