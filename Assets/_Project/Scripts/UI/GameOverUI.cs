using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverUI : MonoBehaviour
{
    // Mostra il pannello di Game Over
    public void Show()
    {
        gameObject.SetActive(true);
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
