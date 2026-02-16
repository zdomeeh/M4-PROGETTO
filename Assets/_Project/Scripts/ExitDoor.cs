using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ExitDoor : MonoBehaviour
{
    [SerializeField] private int _requiredCoins = 100;
    [SerializeField] private VictoryUI _victoryUI;

    private void OnTriggerEnter(Collider other)
    {
        PlayerCoinCollector collector = other.GetComponent<PlayerCoinCollector>();
        if (collector == null)
            return;

        if (collector.GetCoins() >= _requiredCoins)
        {
            _victoryUI.ShowVictory(collector, _requiredCoins);
        }
        else
        {
            Debug.Log("Non hai abbastanza monete");
        }
    }
}
