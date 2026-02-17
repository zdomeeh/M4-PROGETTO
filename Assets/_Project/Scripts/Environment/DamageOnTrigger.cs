using UnityEngine;

public class DamageOnTrigger : MonoBehaviour
{
    [SerializeField] private int _damage = 10;
    [SerializeField] private float _damageInterval = 1f;

    private float _timer = 0f;
    private LifeController _currentLife = null;

    private void OnTriggerEnter(Collider other)
    {
        LifeController life = other.GetComponent<LifeController>(); // Controlla se l'oggetto che entra ha un LifeController
        if (life == null)
            return;

        // Danno immediato
        life.AddHP(-_damage);

        _currentLife = life;
        _timer = 0f;
    }

    private void OnTriggerStay(Collider other)
    {
        if (_currentLife == null) // Se non c'è un player attivo nella zona, non fare nulla
            return;

        _timer += Time.deltaTime;

        if (_timer >= _damageInterval) // Applica danno periodico quando il timer supera l'intervallo
        {
            _currentLife.AddHP(-_damage);
            _timer = 0f;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<LifeController>() != null) // Quando il player esce dalla zona, resetta riferimenti e timer
        {
            _currentLife = null;
            _timer = 0f;
        }
    }
}
