using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuUI : MonoBehaviour
{
    // Avvia il gioco caricando il primo livello
    public void StartGame()
    {
        SceneManager.LoadScene("LevelOne"); // cambia con il nome esatto della scena
    }

    // Esce dall’applicazione
    public void ExitGame()
    {
        Application.Quit();
        Debug.Log("Hai premuto Exit Game"); // utile per test in editor
    }
}
