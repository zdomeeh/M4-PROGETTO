using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VictoryUI : MonoBehaviour
{
    [SerializeField] private GameObject _panel;

    // Mostra il pannello vittoria
    public void ShowVictory()
    {
        if (_panel != null)
            _panel.SetActive(true);
    }

    // Bottone: torna al menu principale
    public void GoToMainMenu()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
    }
}
