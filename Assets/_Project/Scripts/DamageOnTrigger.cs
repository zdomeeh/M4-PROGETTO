using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageOnTrigger : MonoBehaviour
{
    [SerializeField] private int _damage = 20;

    private void OnTriggerEnter(Collider other)
    {
        LifeController life = other.GetComponent<LifeController>();
        if (life != null)
        {
            life.AddHP(-_damage);
        }
    }
}
