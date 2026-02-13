using UnityEngine;

public class DangerZone : MonoBehaviour
{
    [SerializeField] private int _damagePerTick = 20;
    [SerializeField] private float _damageInterval = 0.3f; // più veloce di 1 secondo

    private float _timer = 0f;
    private LifeController _currentLife = null;

    private void OnTriggerEnter(Collider other)
    {
        LifeController life = other.GetComponent<LifeController>();
        if (life == null)
            return;

        // Danno immediato appena cade
        life.AddHP(-_damagePerTick);

        _currentLife = life;
        _timer = 0f;
    }

    private void OnTriggerStay(Collider other)
    {
        if (_currentLife == null)
            return;

        _timer += Time.deltaTime;

        if (_timer >= _damageInterval)
        {
            _currentLife.AddHP(-_damagePerTick);
            _timer = 0f;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<LifeController>() != null)
        {
            _currentLife = null;
            _timer = 0f;
        }
    }
}
