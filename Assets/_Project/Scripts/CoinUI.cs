using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CoinUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _coinText;

    public void UpdateCoinText(int coins)
    {
        if (_coinText != null)
            _coinText.text = "Coins: " + coins;
    }
}
