using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class SCR_PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenuOne;
    [SerializeField] private GameObject blankPauseOne;
    [SerializeField] private GameObject pauseMenuTwo;
    [SerializeField] private GameObject blankPauseTwo;
    [SerializeField] private EventSystem es;

    [SerializeField] private GameObject controlsMenuOne;
    [SerializeField] private GameObject controlsMenuTwo;
    [SerializeField] private GameObject controlsButtonOne;
    [SerializeField] private GameObject controlsButtonTwo;
    [SerializeField] private GameObject controlsBackButtonOne;
    [SerializeField] private GameObject controlsBackButtonTwo;

    public void PlayerOneResume()
    {
        es.SetSelectedGameObject(null);
        Time.timeScale = 1;
        pauseMenuOne.SetActive(false);
        blankPauseTwo.SetActive(false);
    }
    public void PlayerTwoResume()
    {
        es.SetSelectedGameObject(null);
        Time.timeScale = 1;
        pauseMenuTwo.SetActive(false);
        blankPauseOne.SetActive(false);
    }

    public void PlayerOneControls()
    {
        controlsMenuOne.SetActive(true);
        pauseMenuOne.SetActive(false);
        es.SetSelectedGameObject(controlsBackButtonOne);
    }

    public void PlayerTwoControls()
    {
        controlsMenuTwo.SetActive(true);
        pauseMenuTwo.SetActive(false);
        es.SetSelectedGameObject(controlsBackButtonTwo);
    }

    public void PlayerOneControlsBackButton()
    {
        controlsMenuOne.SetActive(false);
        pauseMenuOne.SetActive(true);
        es.SetSelectedGameObject(controlsButtonOne);
    }

    public void PlayerTwoControlsBackButton()
    {
        controlsMenuTwo.SetActive(false);
        pauseMenuTwo.SetActive(true);
        es.SetSelectedGameObject(controlsButtonTwo);
    }

    public void QuitToMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void QuitToDesktop()
    {
        Application.Quit();
    }
}
