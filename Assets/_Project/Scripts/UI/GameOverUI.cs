using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverUI : MonoBehaviour
{
    [SerializeField] private CameraOrbit _cameraOrbit; // riferimento per la camera
    [SerializeField] private LevelTimer _levelTimer; // riferimento per il timer

    // Mostra il pannello di Game Over
    public void Show()
    {
        gameObject.SetActive(true);
        
        if (_cameraOrbit != null) // blocca la telecamera
            _cameraOrbit.enabled = false;

        if (_levelTimer != null)
            _levelTimer.StopTimer(); // blocca il timer quando muori

    }

    // Riavvia il livello corrente
    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // Torna al menu principale
    public void GoToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
