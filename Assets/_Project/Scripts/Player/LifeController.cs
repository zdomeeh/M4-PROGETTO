using UnityEngine;
using UnityEngine.Events;

public class LifeController : MonoBehaviour
{
    [SerializeField] private int _currentHP = 100;
    [SerializeField] private int _maxHP = 100;
    [SerializeField] private bool _fullHPOnStart = true;

    [SerializeField] private UnityEvent<int, int> _onHPChanged;
    [SerializeField] private UnityEvent _onDefeated;

    private void Start()
    {
        // Inizializza vita piena se richiesto
        if (_fullHPOnStart)
        {
            SetHP(_maxHP);
        }
    }

    public int GetHP() => _currentHP;
    public int GetMaxHP() => _maxHP;

    public void AddHP(int amount) => SetHP(_currentHP + amount);

    public void SetHP(int hp)
    {
        // Limita hp tra 0 e maxHP
        hp = Mathf.Clamp(hp, 0, _maxHP);

        // Se la vita cambia, aggiorna e invoca eventi
        if (hp != _currentHP)
        {
            _currentHP = hp;
            _onHPChanged.Invoke(_currentHP, _maxHP);

            // Se vita a 0, invoca evento di sconfitta
            if (_currentHP == 0)
            {
                _onDefeated.Invoke();
            }
        }
    }

    // Metodo di test rapido per infliggere danno
    public void Inflict20Damage()
    {
        AddHP(-20);
    }
}