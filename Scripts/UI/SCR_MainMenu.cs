using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class SCR_MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject mainMenuFirstButton;
    [SerializeField] private EventSystem es;
    [SerializeField] private GameObject backButton;
    [SerializeField] private GameObject controlsButton;

    void Start()
    {
        StartCoroutine(ShowMenu());
    }

    
    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void Controls()
    {
        es.SetSelectedGameObject(backButton);
    }

    public void Back()
    {
        es.SetSelectedGameObject(null);
        es.SetSelectedGameObject(controlsButton);
    }
    IEnumerator ShowMenu()
    {
        yield return new WaitForSeconds(2f);
        mainMenu.SetActive(true);
    }
}