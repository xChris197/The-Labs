using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SCR_Button : MonoBehaviour
{
    private float distance;
    private float distanceTwo;

    [SerializeField] private GameObject interactionUIOne;
    [SerializeField] private GameObject idleCrosshairOne;
    [SerializeField] private TextMeshProUGUI textDisplayOne;

    [SerializeField] private GameObject interactionUITwo;
    [SerializeField] private GameObject idleCrosshairTwo;
    [SerializeField] private TextMeshProUGUI textDisplayTwo;

    [SerializeField] private Animator anim;
    [SerializeField] private AudioSource doorSound;

    [SerializeField] private string interactOne;
    [SerializeField] private string interactTwo;

    private bool firstTimeNotActive;
    private bool secondTimeNotActive;

    void Update()
    {
        distance = SCR_PlayerCasting.distanceFromTarget;
        distanceTwo = SCR_PlayerCastingTwo.distanceFromTarget;

        if (SCR_PlayerCasting.hitTarget.CompareTag("Button") && distance < 2f)
        {
            firstTimeNotActive = true;
            idleCrosshairOne.SetActive(false);
            interactionUIOne.SetActive(true);
            textDisplayOne.text = "[Main Door Button]\n Hold 'X' To Open The Door";
        }
        else if(firstTimeNotActive)
        {
            firstTimeNotActive = false;
            idleCrosshairOne.SetActive(true);
            interactionUIOne.SetActive(false);
        }
        if(distanceTwo < 2f && SCR_PlayerCastingTwo.hitTarget.CompareTag("Button"))
        {
            secondTimeNotActive = true;
            idleCrosshairTwo.SetActive(false);
            interactionUITwo.SetActive(true);
            textDisplayTwo.text = "[Main Door Button]\n Hold 'X' To Open The Door";
        }
        else if(secondTimeNotActive)
        {
            secondTimeNotActive = false;
            idleCrosshairTwo.SetActive(true);
            interactionUITwo.SetActive(false);
        }

        if (distance < 2f && SCR_PlayerCasting.hitTarget.CompareTag("Button") && (Input.GetButtonDown(interactOne)))
        {
            ActivateGame();
        }
        else if(Input.GetButtonDown(interactTwo) && distanceTwo < 2f && SCR_PlayerCastingTwo.hitTarget.CompareTag("Button"))
        {
            ActivateGame();
        }
    }
    void ActivateGame()
    {
        anim.SetBool("bDoorCleared", true);
        doorSound.Play();
        idleCrosshairOne.SetActive(true);
        idleCrosshairTwo.SetActive(true);
        interactionUIOne.SetActive(false);
        interactionUITwo.SetActive(false);
        textDisplayOne.text = null;
        textDisplayTwo.text = null;
        GetComponent<SCR_Button>().enabled = false;
    }
}
