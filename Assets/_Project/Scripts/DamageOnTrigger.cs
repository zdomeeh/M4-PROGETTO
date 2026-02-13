using UnityEngine;

public class DamageOnTrigger : MonoBehaviour
{
    [SerializeField] private int _damage = 10;
    [SerializeField] private float _damageInterval = 1f;

    private float _timer = 0f;
    private LifeController _currentLife = null;

    private void OnTriggerEnter(Collider other)
    {
        LifeController life = other.GetComponent<LifeController>();
        if (life == null)
            return;

        // Danno immediato
        life.AddHP(-_damage);

        // Inizia il danno nel tempo
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
            _currentLife.AddHP(-_damage);
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
