using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private int _totalCoins = 0; // quante monete ci sono nel livello
    public UnityEvent OnLevelWon;

    private PlayerCoinCollector _playerCollector;

    void Start()
    {
        // Trova il player nella scena
        _playerCollector = FindObjectOfType<PlayerCoinCollector>();
        if (_playerCollector != null)
        {
            _playerCollector.OnCoinsChanged.AddListener(CheckVictory);
        }
    }

    void CheckVictory(int coinsCollected)
    {
        if (coinsCollected >= _totalCoins)
        {
            Debug.Log("Hai raccolto tutte le monete! Vittoria!");
            OnLevelWon.Invoke();
        }
    }
}
