using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private PlayerCoinCollector _player;
    [SerializeField] private VictoryUI _victoryUI;
    [SerializeField] private int _requiredCoins = 100; // numero minimo per vittoria

    public void FinishLevel()
    {
        if (_victoryUI != null && _player != null)
        {
            _victoryUI.ShowVictory(_player, _requiredCoins);
        }
    }
}
