using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverUI : MonoBehaviour
{
    [SerializeField] private CameraOrbit _cameraOrbit; // riferimento per la camera

    // Mostra il pannello di Game Over
    public void Show()
    {
        gameObject.SetActive(true);
        
        if (_cameraOrbit != null) // blocca la telecamera
            _cameraOrbit.enabled = false;
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
