using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TheEnd : MainMenu
{
    [SerializeField] GameObject endMenu;
    [SerializeField] GameObject playMenu, managerOffice;

    private void OnEnable()
    {
        Health.OnGameEndedEvent += GameEnds;
       EndRound.OnGameEndedEvent += GameEnds;
    }
    private void OnDisable()
    {
        Health.OnGameEndedEvent -= GameEnds;
        EndRound.OnGameEndedEvent -= GameEnds;
    }

    public void PlayAgain() => SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    public void Quit() => Application.Quit();

    private void GameEnds()
    {
        playMenu.SetActive(false);
        managerOffice.SetActive(false);
        endMenu.SetActive(true);
      
    }
}
