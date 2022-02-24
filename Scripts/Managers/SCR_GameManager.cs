using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SCR_GameManager : MonoBehaviour
{
    [SerializeField] private AudioSource introDialogue;
    [SerializeField] private GameObject button;

    [SerializeField] private Camera playerOneCam;
    [SerializeField] private Camera playerTwoCam;

    [SerializeField] private GameObject playerOne;
    [SerializeField] private GameObject playerTwo;

    [SerializeField] private GameObject pistolOne;
    [SerializeField] private GameObject pistolTwo;

    [SerializeField] private AudioSource explosionSFX;
    [SerializeField] private SCR_CameraShake cameraShakeOne;
    [SerializeField] private SCR_CameraShake cameraShakeTwo;

    [SerializeField] private GameObject deadUIOne;
    [SerializeField] private GameObject deadUITwo;
    [SerializeField] private EventSystem es;
    [SerializeField] private GameObject tryAgainButton;
    private SCR_PlayerHealth playerOneHealth;
    private SCR_PlayerHealth playerTwoHealth;
    private SCR_PlayerMovement playerOneMovement;
    private SCR_PlayerMovement playerTwoMovement;
    private SCR_Pistol pistolOneScript;
    private SCR_Pistol pistolTwoScript;
    public bool bPlayerOneDead = false;
    public bool bPlayerTwoDead = false;

    [SerializeField] private GameObject pauseMenuOne;
    [SerializeField] private GameObject pauseMenuTwo;
    [SerializeField] private GameObject blankPauseOne;
    [SerializeField] private GameObject blankPauseTwo;
    [SerializeField] private GameObject resumeButton;
    [SerializeField] private GameObject resumeButtonTwo;
    public bool bGamePaused = false;

    [SerializeField] private Animator anim;
    [SerializeField] private Animator animtwo;

    void Start()
    {
        playerOneMovement = playerOne.GetComponent<SCR_PlayerMovement>();
        playerTwoMovement = playerTwo.GetComponent<SCR_PlayerMovement>();
        playerOneHealth = playerOne.GetComponent<SCR_PlayerHealth>();
        playerTwoHealth = playerTwo.GetComponent<SCR_PlayerHealth>();
        pistolOneScript = pistolOne.GetComponent<SCR_Pistol>();
        pistolTwoScript = pistolTwo.GetComponent<SCR_Pistol>();
        StartCoroutine(IntroScene());
        anim.SetBool("bGameStarted", true);     //Activates fade-in screen
        animtwo.SetBool("bGameStarted", true);
    }

    void Update()
    {
        if (playerOneHealth.currentHealth <= 0f && pistolOneScript.bEndingChosen || pistolTwoScript.bEndingChosen)
        {
            bPlayerOneDead = true;
            playerOneMovement.enabled = false;
        }
        else if (playerTwoHealth.currentHealth <= 0f && pistolOneScript.bEndingChosen || pistolTwoScript.bEndingChosen)
        {
            bPlayerTwoDead = true;
            playerTwoMovement.enabled = false;
        }

        if(bPlayerOneDead && bPlayerTwoDead && pistolOneScript.bEndingChosen || pistolTwoScript.bEndingChosen)
        {
            deadUIOne.SetActive(true);
            deadUITwo.SetActive(true);
            es.SetSelectedGameObject(tryAgainButton);
        }

        if(Input.GetButtonDown("Start_1") && !bGamePaused)
        {
            PlayerOnePaused();
        }
        else if(Input.GetButtonDown("Start_1") && bGamePaused)
        {
            PlayerOneResume();
        }

        if(Input.GetButtonDown("Start_2") && !bGamePaused)
        {
            PlayerTwoPaused();
        }
        else if(Input.GetButtonDown("Start_2") && bGamePaused)
        {
            PlayerTwoResume();
        }
    }

    IEnumerator IntroScene()
    {
        yield return new WaitForSeconds(2f);

        explosionSFX.Play();
        yield return new WaitForSeconds(1f);
        cameraShakeOne.shouldShake = true;
        cameraShakeTwo.shouldShake = true;
        yield return new WaitForSeconds(3f);

        playerOne.GetComponent<SCR_PlayerMovement>().enabled = true;
        playerTwo.GetComponent<SCR_PlayerMovement>().enabled = true;

        introDialogue.Play();
        yield return new WaitForSeconds(38.5f);
        button.layer = 2;
    }

    void PlayerOnePaused()
    {
        es.SetSelectedGameObject(resumeButton);
        Time.timeScale = 0;
        pauseMenuOne.SetActive(true);
        blankPauseTwo.SetActive(true);
    }

    void PlayerTwoPaused()
    {
        es.SetSelectedGameObject(resumeButtonTwo);
        Time.timeScale = 0;
        pauseMenuTwo.SetActive(true);
        blankPauseOne.SetActive(true);
    }

    void PlayerOneResume()
    {
        es.SetSelectedGameObject(null);
        Time.timeScale = 1;
        pauseMenuOne.SetActive(false);
        blankPauseTwo.SetActive(false);
    }
    void PlayerTwoResume()
    {
        es.SetSelectedGameObject(null);
        Time.timeScale = 1;
        pauseMenuTwo.SetActive(false);
        blankPauseOne.SetActive(false);
    }
}
