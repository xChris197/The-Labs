using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class SCR_FinalSyringe : MonoBehaviour
{
    private float distance;
    private float distanceTwo;

    [SerializeField] private GameObject playerOne;
    [SerializeField] private GameObject playerTwo;
    [SerializeField] private GameObject playerCamOne;
    [SerializeField] private GameObject playerCamTwo;

    [SerializeField] private GameObject pistolOne;
    [SerializeField] private GameObject pistolTwo;
    private SCR_Pistol pistolOneScript;
    private SCR_Pistol pistolTwoScript;
    private SCR_PlayerHealth playerOneHealth;
    private SCR_PlayerHealth playerTwoHealth;

    [SerializeField] private GameObject goodEndingButtonOne;

    [SerializeField] private GameObject goodEndingButtonTwo;

    [SerializeField] private GameObject playerWonUI;
    [SerializeField] private GameObject playerWonUITwo;
    [SerializeField] private TextMeshProUGUI playerText;
    [SerializeField] private TextMeshProUGUI playerTextTwo;

    [SerializeField] private GameObject interactionUIOne;
    [SerializeField] private GameObject idleCrosshairOne;
    [SerializeField] private TextMeshProUGUI textDisplayOne;

    [SerializeField] private GameObject interactionUITwo;
    [SerializeField] private GameObject idleCrosshairTwo;
    [SerializeField] private TextMeshProUGUI textDisplayTwo;

    [SerializeField] private string interactOne;
    [SerializeField] private string interactTwo;
    [SerializeField] private EventSystem es;
    [SerializeField] private GameObject finalDecisionUI;
    [SerializeField] private GameObject finalDecisionUITwo;

    private bool firstTimeNotActive;
    private bool secondTimeNotActive;

    private bool bPlayerOneActivated = false;
    private bool bPlayerTwoActivated = false;

    void Start()
    {
        pistolOneScript = pistolOne.GetComponent<SCR_Pistol>();
        pistolTwoScript = pistolTwo.GetComponent<SCR_Pistol>();
        playerOneHealth = playerOne.GetComponent<SCR_PlayerHealth>();
        playerTwoHealth = playerTwo.GetComponent<SCR_PlayerHealth>();
    }
    void Update()
    {
        distance = SCR_PlayerCasting.distanceFromTarget;
        distanceTwo = SCR_PlayerCastingTwo.distanceFromTarget;

        if (distance < 2f && SCR_PlayerCasting.hitTarget.CompareTag("Cure"))
        {
            firstTimeNotActive = true;
            idleCrosshairOne.SetActive(false);
            interactionUIOne.SetActive(true);
            textDisplayOne.text = "[Mysterious Serum]\n Press 'X' To Pickup";
        }
        else if (firstTimeNotActive)
        {
            firstTimeNotActive = false;
            idleCrosshairOne.SetActive(true);
            interactionUIOne.SetActive(false);
        }
        if (distanceTwo < 2f && SCR_PlayerCastingTwo.hitTarget.CompareTag("Cure"))
        {
            secondTimeNotActive = true;
            idleCrosshairTwo.SetActive(true);
            interactionUITwo.SetActive(false);
            textDisplayTwo.text = "[Mysterious Serum]\n Press 'X' To Pickup";
        }
        else if (secondTimeNotActive)
        {
            secondTimeNotActive = false;
            idleCrosshairTwo.SetActive(true);
            interactionUITwo.SetActive(false);
        }
        if (distance < 2f && SCR_PlayerCasting.hitTarget.CompareTag("Cure") && (Input.GetButtonDown(interactOne) && !bPlayerOneActivated))
        {
            PickupCureOne();
        }
        else if (distanceTwo < 2f && SCR_PlayerCastingTwo.hitTarget.CompareTag("Cure") && (Input.GetButtonDown(interactTwo) && !bPlayerTwoActivated))
        {
            PickupCureTwo();
        }

        if(pistolOneScript.bEndingChosen || pistolTwoScript.bEndingChosen)
        {
            if(playerOneHealth.currentHealth <= 0)
            {
                StartCoroutine(PlayerTwoWon());
            }
            else if(playerTwoHealth.currentHealth <= 0)
            {
                StartCoroutine(PlayerOneWon());
            }
        }
    }
    void PickupCureOne()
    {
        finalDecisionUI.SetActive(true);
        idleCrosshairOne.SetActive(true);
        interactionUIOne.SetActive(false);
        textDisplayOne.text = null;
        playerOne.GetComponent<SCR_PlayerMovement>().enabled = false;
        playerCamOne.GetComponent<SCR_PlayerLook>().enabled = false;
        es.SetSelectedGameObject(goodEndingButtonOne);
    }

    void PickupCureTwo()
    {
        finalDecisionUITwo.SetActive(true);
        idleCrosshairTwo.SetActive(true);
        interactionUITwo.SetActive(false);
        textDisplayTwo.text = null;
        playerTwo.GetComponent<SCR_PlayerMovement>().enabled = false;
        playerCamTwo.GetComponent<SCR_PlayerLook>().enabled = false;
        es.SetSelectedGameObject(goodEndingButtonTwo);
    }

    IEnumerator PlayerOneWon()
    {
        playerWonUI.SetActive(true);
        playerWonUITwo.SetActive(true);

        playerText.text = "Player One has stolen the serum";
        playerTextTwo.text = "Player One has stolen the serum";

        playerOne.GetComponent<SCR_PlayerMovement>().enabled = false;
        playerTwo.GetComponent<SCR_PlayerMovement>().enabled = false;
        playerCamOne.GetComponent<SCR_PlayerLook>().enabled = false;
        playerCamTwo.GetComponent<SCR_PlayerLook>().enabled = false;
        yield return new WaitForSeconds(5f);
        SceneManager.LoadScene(3);
    }

    IEnumerator PlayerTwoWon()
    {
        playerWonUI.SetActive(true);
        playerWonUITwo.SetActive(true);

        playerText.text = "Player Two has stolen the serum";
        playerTextTwo.text = "Player Two has stolen the serum";

        playerOne.GetComponent<SCR_PlayerMovement>().enabled = false;
        playerTwo.GetComponent<SCR_PlayerMovement>().enabled = false;
        playerCamOne.GetComponent<SCR_PlayerLook>().enabled = false;
        playerCamTwo.GetComponent<SCR_PlayerLook>().enabled = false;
        yield return new WaitForSeconds(5f);
        SceneManager.LoadScene(3);
    }

    public void GoodEnding()
    {
        textDisplayOne.text = null;
        textDisplayTwo.text = null;
        SceneManager.LoadScene(3);
    }

    public void EvilEnding()
    {
        gameObject.layer = 2;
        textDisplayOne.text = null;
        textDisplayTwo.text = null;
        idleCrosshairOne.SetActive(true);
        interactionUIOne.SetActive(false);
        idleCrosshairTwo.SetActive(true);
        interactionUITwo.SetActive(false);
        finalDecisionUI.SetActive(false);
        finalDecisionUITwo.SetActive(false);
        es.SetSelectedGameObject(null);
        playerOne.GetComponent<SCR_PlayerMovement>().enabled = true;
        playerCamOne.GetComponent<SCR_PlayerLook>().enabled = true;
        playerTwo.GetComponent<SCR_PlayerMovement>().enabled = true;
        playerCamTwo.GetComponent<SCR_PlayerLook>().enabled = true;
        pistolOneScript.bEndingChosen = true;
        pistolTwoScript.bEndingChosen = true;
    }
}
