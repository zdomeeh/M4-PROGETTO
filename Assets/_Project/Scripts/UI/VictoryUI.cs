using TMPro;
using UnityEngine;

public class VictoryUI : MonoBehaviour
{
    [SerializeField] private GameObject _panelVictory;
    [SerializeField] private GameObject _panelPerfectVictory;

    [SerializeField] private TextMeshProUGUI _coinsText;
    [SerializeField] private TextMeshProUGUI _completionText;

    [SerializeField] private GameObject _extraStar; // stella grigia / bonus

    public void ShowVictory(PlayerCoinCollector collector, int requiredCoins)
    {
        if (collector == null) return;

        int collected = collector.GetCoins();
        int total = collector.TotalCoinsInLevel;

        // COMPLETION BASE SEMPRE 100%
        if (_completionText != null)
            _completionText.text = "Completion: 100%";

        if (_coinsText != null)
            _coinsText.text = "Coins: " + collected;

        // PERFECT RUN tutte le monete
        bool perfectRun = collected >= total;

        if (perfectRun)
        {
            if (_panelVictory != null)
                _panelVictory.SetActive(false);

            if (_panelPerfectVictory != null)
                _panelPerfectVictory.SetActive(true);
        }
        else
        {
            if (_panelVictory != null)
                _panelVictory.SetActive(true);

            if (_panelPerfectVictory != null)
                _panelPerfectVictory.SetActive(false);

            // mostra stella grigia / extra
            if (_extraStar != null)
                _extraStar.SetActive(true);
        }

        Time.timeScale = 0f;
    }

    public void GoToMainMenu()
    {
        Time.timeScale = 1f;
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
    }
}
