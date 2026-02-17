using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VictoryUI : MonoBehaviour
{
    [SerializeField] private GameObject _panelVictory;
    [SerializeField] private GameObject _panelPerfectVictory;

    [SerializeField] private TextMeshProUGUI _coinsText;
    [SerializeField] private TextMeshProUGUI _completionText; 

    [SerializeField] private GameObject _normalStar;   // stella grigia
    [SerializeField] private GameObject _perfectStar;  // stella gialla

    [SerializeField] private CameraOrbit _cameraOrbit; // riferimento per la camera

    public void ShowVictory(PlayerCoinCollector collector, int requiredCoins) // Mostra il pannello di vittoria corretto in base al numero di monete raccolte
    {
        if (collector == null) return;

        int collected = collector.GetCoins(); // Monete raccolte dal player
        int total = collector.TotalCoinsInLevel; // Monete totali nel livello

        bool perfectRun = collected >= total; // Determina se la run è perfetta o normale
        bool normalVictory = collected >= requiredCoins && collected < total;

        if (_coinsText != null)
            _coinsText.text = collected + " / " + total + " coins";

        // PERFECT RUN
        if (perfectRun)
        {
            _panelVictory?.SetActive(false);
            _panelPerfectVictory?.SetActive(true);

            if (_completionText != null)
                _completionText.text = "110% Completato";

            _perfectStar?.SetActive(true);
            _normalStar?.SetActive(false);
        }
        // VITTORIA NORMALE
        else if (normalVictory)
        {
            _panelVictory?.SetActive(true);
            _panelPerfectVictory?.SetActive(false);

            if (_completionText != null)
                _completionText.text = "100% Completato";

            _normalStar?.SetActive(true);
            _perfectStar?.SetActive(false);
        }
        // Caso in cui il player non ha il minimo delle monete richiesto
        else
        {
            Debug.Log("Non hai abbastanza monete per aprire la porta!");
            return;
        }

        // Blocca la camera
        if (_cameraOrbit != null)
            _cameraOrbit.enabled = false;

        Time.timeScale = 0f; // pausa il gioco
    }

    public void GoToMainMenu() // Torna al menu principale ripristinando il tempo
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }
}