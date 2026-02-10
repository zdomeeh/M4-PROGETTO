using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerCoinCollector : MonoBehaviour
{
    public UnityEvent<int> OnCoinsChanged; // Evento per UI

    private int _coins = 0;

    public void AddCoins(int amount)
    {
        _coins += amount;
        OnCoinsChanged.Invoke(_coins); // Aggiorna UI
    }

    public int GetCoins() => _coins;
}
