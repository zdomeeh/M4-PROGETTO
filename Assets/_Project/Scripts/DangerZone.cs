using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DangerZone : MonoBehaviour
{
    [SerializeField] private int _damagePerSecond = 20;
    private float _timer;

    private void OnTriggerStay(Collider other)
    {
        LifeController life = other.GetComponent<LifeController>();
        if (life == null) return;

        _timer += Time.deltaTime;

        if (_timer >= 1f)
        {
            life.AddHP(-_damagePerSecond);
            _timer = 0f;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        _timer = 0f;
    }
}
