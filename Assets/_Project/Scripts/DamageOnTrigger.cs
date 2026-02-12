using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageOnTrigger : MonoBehaviour
{
    [SerializeField] private int _damage = 10;          // danno per tick
    [SerializeField] private float _damageInterval = 1f; // ogni quanti secondi

    private float _timer = 0f;

    private void OnTriggerStay(Collider other)
    {
        LifeController life = other.GetComponent<LifeController>();
        if (life == null)
            return;

        // Accumula il tempo
        _timer += Time.deltaTime;

        // Applica danno a intervalli regolari
        if (_timer >= _damageInterval)
        {
            life.AddHP(-_damage);
            _timer = 0f;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Reset quando il player esce
        if (other.GetComponent<LifeController>() != null)
        {
            _timer = 0f;
        }
    }
}
