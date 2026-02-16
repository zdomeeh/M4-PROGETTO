using UnityEngine;
using UnityEngine.Events;

public class PlayerCoinCollector : MonoBehaviour
{
    [SerializeField] private int _totalCoinsInLevel = 110;

    private int _coins = 0;

    // Evento per UI / LevelManager
    // Passa il numero aggiornato di monete
    public UnityEvent<int> OnCoinsChanged;

    // Aggiunge monete al contatore
    public void AddCoins(int value)
    {
        _coins += value;
        OnCoinsChanged?.Invoke(_coins);
    }

    // Ritorna quante monete ha raccolto il player
    public int GetCoins() => _coins;

    // Ritorna il numero totale di monete nel livello
    // Serve per PERFECT RUN
    public int TotalCoinsInLevel => _totalCoinsInLevel;

    // Percentuale di completamento (può superare il 100%)
    public float GetCompletionPercentage()
    {
        if (_totalCoinsInLevel <= 0)
            return 0f;

        return (_coins / (float)_totalCoinsInLevel) * 100f;
    }
}
