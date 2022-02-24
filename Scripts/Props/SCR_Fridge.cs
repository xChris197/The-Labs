using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SCR_Fridge : MonoBehaviour
{
    private float distance;
    private float distanceTwo;

    [SerializeField] private GameObject interactionUIOne;
    [SerializeField] private GameObject idleCrosshairOne;
    [SerializeField] private TextMeshProUGUI textDisplayOne;

    [SerializeField] private GameObject interactionUITwo;
    [SerializeField] private GameObject idleCrosshairTwo;
    [SerializeField] private TextMeshProUGUI textDisplayTwo;

    [SerializeField] private string interactOne;
    [SerializeField] private string interactTwo;
    [SerializeField] private Animator anim;

    private bool firstTimeNotActive;
    private bool secondTimeNotActive;
    void Update()
    {
        distance = SCR_PlayerCasting.distanceFromTarget;
        distanceTwo = SCR_PlayerCastingTwo.distanceFromTarget;

        if (SCR_PlayerCasting.hitTarget.CompareTag("Fridge") && distance < 2f)
        {
            firstTimeNotActive = true;
            idleCrosshairOne.SetActive(false);
            interactionUIOne.SetActive(true);
            textDisplayOne.text = "[Chemical Fridge]\n Press 'X' To Open Door";
        }
        else if (firstTimeNotActive)
        {
            idleCrosshairOne.SetActive(true);
            interactionUIOne.SetActive(false);
        }

        if (SCR_PlayerCastingTwo.hitTarget.CompareTag("Fridge") && distanceTwo < 2f)
        {
            secondTimeNotActive = true;
            idleCrosshairTwo.SetActive(false);
            interactionUITwo.SetActive(true);
            textDisplayTwo.text = "[Chemical Fridge]\n Press 'X' To Open Door";
        }
        else if (secondTimeNotActive)
        {
            idleCrosshairTwo.SetActive(true);
            interactionUITwo.SetActive(false);
        }

        if (distance < 2f && SCR_PlayerCasting.hitTarget.CompareTag("Fridge") && (Input.GetButtonDown(interactOne)))
        {
            OpenFridgeOne();
        }
        else if(distanceTwo < 2f && SCR_PlayerCastingTwo.hitTarget.CompareTag("Fridge") && Input.GetButtonDown(interactTwo))
        {
            OpenFridgeTwo();
        }
    }

    void OpenFridgeOne()
    {
        anim.SetBool("bDoorOpen", true);
        idleCrosshairOne.SetActive(true);
        interactionUIOne.SetActive(false);
        textDisplayOne.text = null;
        gameObject.layer = 2;
        GetComponent<SCR_Fridge>().enabled = false;
    }
    void OpenFridgeTwo()
    {
        anim.SetBool("bDoorOpen", true);
        idleCrosshairTwo.SetActive(true);
        interactionUITwo.SetActive(false);
        textDisplayTwo.text = null;
        GetComponent<SCR_Fridge>().enabled = false;
        gameObject.layer = 2;
    }
}