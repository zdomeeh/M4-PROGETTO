using UnityEngine;
using UnityEngine.Events;

public class PlayerCoinCollector : MonoBehaviour
{
    private int _coins = 0;

    // Evento che passa il numero totale di monete raccolte
    public UnityEvent<int> OnCoinsChanged;

    public void AddCoins(int value)
    {
        _coins += value;
        Debug.Log("Monete raccolte: " + _coins); // <-- per test
        if (OnCoinsChanged != null)
            OnCoinsChanged.Invoke(_coins); // Passa il numero aggiornato
    }

    public int GetCoins() => _coins;
}
