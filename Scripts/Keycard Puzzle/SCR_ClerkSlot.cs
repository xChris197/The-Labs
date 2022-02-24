using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SCR_ClerkSlot : MonoBehaviour
{
    private float distance;
    private float distanceTwo;

    [SerializeField] private GameObject scannerBody;
    private SCR_CardManager cardManager;
    [SerializeField] private Animator anim;

    [SerializeField] private GameObject interactionUIOne;
    [SerializeField] private GameObject idleCrosshairOne;
    [SerializeField] private TextMeshProUGUI textDisplayOne;

    [SerializeField] private GameObject interactionUITwo;
    [SerializeField] private GameObject idleCrosshairTwo;
    [SerializeField] private TextMeshProUGUI textDisplayTwo;

    [SerializeField] private string interactOne;
    [SerializeField] private string interactTwo;

    private bool firstTimeNotActive;
    private bool secondTimeNotActive;

    void Start()
    {
        cardManager = scannerBody.GetComponent<SCR_CardManager>();
    }
    void Update()
    {
        distance = SCR_PlayerCasting.distanceFromTarget;
        distance = SCR_PlayerCastingTwo.distanceFromTarget;

        if (distance < 2f && SCR_PlayerCasting.hitTarget.CompareTag("Clerk Slot") && SCR_InventoryOne.bHasClerkCard)
        {
            firstTimeNotActive = true;
            idleCrosshairOne.SetActive(false);
            interactionUIOne.SetActive(true);
            textDisplayOne.text = "[Clerk Slot]\n Press 'X' To Interact";
        }
        else if (distance < 2f && SCR_PlayerCasting.hitTarget.CompareTag("Clerk Slot") && !SCR_InventoryOne.bHasClerkCard)
        {
            firstTimeNotActive = true;
            idleCrosshairOne.SetActive(false);
            interactionUIOne.SetActive(true);
            textDisplayOne.text = "[Clerk Slot]\n Keycard MISSING";
        }
        else if (firstTimeNotActive)
        {
            firstTimeNotActive = false;
            idleCrosshairOne.SetActive(true);
            interactionUIOne.SetActive(false);
        }

        if (distanceTwo < 2f && SCR_PlayerCastingTwo.hitTarget.CompareTag("Clerk Slot") && SCR_InventoryTwo.bHasClerkCard)
        {
            firstTimeNotActive = true;
            idleCrosshairTwo.SetActive(false);
            interactionUITwo.SetActive(true);
            textDisplayTwo.text = "[Clerk Slot]\n Press 'X' To Interact";
        }
        else if (distanceTwo < 2f && SCR_PlayerCastingTwo.hitTarget.CompareTag("Clerk Slot") && !SCR_InventoryTwo.bHasClerkCard)
        {
            secondTimeNotActive = true;
            idleCrosshairTwo.SetActive(false);
            interactionUITwo.SetActive(true);
            textDisplayTwo.text = "[Clerk Slot]\n Keycard MISSING";
        }
        else if (secondTimeNotActive)
        {
            secondTimeNotActive = false;
            idleCrosshairTwo.SetActive(true);
            interactionUITwo.SetActive(false);
        }


        if (distance < 2f && SCR_PlayerCasting.hitTarget.CompareTag("Clerk Slot") && (Input.GetButtonDown(interactOne) && SCR_InventoryOne.bHasClerkCard))
        {
            UseKeycardOne();
        }
        else if (distanceTwo < 2f && SCR_PlayerCastingTwo.hitTarget.CompareTag("Clerk Slot") && (Input.GetButtonDown(interactTwo) && SCR_InventoryTwo.bHasClerkCard))
        {
            UseKeycardTwo();
        }
    }

    void UseKeycardOne()
    {
        cardManager.currentValue++;
        anim.SetBool("bUsedClerk", true);
        idleCrosshairOne.SetActive(true);
        interactionUIOne.SetActive(false);
        idleCrosshairTwo.SetActive(true);
        interactionUITwo.SetActive(false);
        textDisplayOne.text = null;
        textDisplayTwo.text = null;
        GetComponent<SCR_ClerkSlot>().enabled = false;
    }

    void UseKeycardTwo()
    {
        cardManager.currentValue++;
        anim.SetBool("bUsedClerk", true);
        idleCrosshairOne.SetActive(true);
        interactionUIOne.SetActive(false);
        idleCrosshairTwo.SetActive(true);
        interactionUITwo.SetActive(false);
        textDisplayOne.text = null;
        textDisplayTwo.text = null;
        GetComponent<SCR_ClerkSlot>().enabled = false;
    }
}
