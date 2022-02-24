using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SCR_SecurityKeycard : MonoBehaviour
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

    private bool firstTimeNotActive;
    private bool secondTimeNotActive;

    void Update()
    {
        distance = SCR_PlayerCasting.distanceFromTarget;
        distanceTwo = SCR_PlayerCastingTwo.distanceFromTarget;

        if (distance < 2f && SCR_PlayerCasting.hitTarget.CompareTag("Security"))
        {
            firstTimeNotActive = true;
            idleCrosshairOne.SetActive(false);
            interactionUIOne.SetActive(true);
            textDisplayOne.text = "[Security Keycard]\n Press 'X' To Pickup";
        }
        else if (firstTimeNotActive)
        {
            firstTimeNotActive = false;
            idleCrosshairOne.SetActive(true);
            interactionUIOne.SetActive(false);
        }

        if (distanceTwo < 2f && SCR_PlayerCastingTwo.hitTarget.CompareTag("Security"))
        {
            secondTimeNotActive = true;
            idleCrosshairTwo.SetActive(true);
            interactionUITwo.SetActive(false);
            textDisplayOne.text = "[Security Keycard]\n Press 'X' To Pickup";
        }
        else if (secondTimeNotActive)
        {
            secondTimeNotActive = false;
            idleCrosshairTwo.SetActive(true);
            interactionUITwo.SetActive(false);
        }


        if (distance < 2f && SCR_PlayerCasting.hitTarget.CompareTag("Security") && (Input.GetButtonDown(interactOne)))
        {
            PickupKeycardOne();
        }
        else if (distanceTwo < 2f && SCR_PlayerCastingTwo.hitTarget.CompareTag("Security") && (Input.GetButtonDown(interactTwo)))
        {
            PickupKeycardTwo();
        }
    }
    void PickupKeycardOne()
    {
        SCR_InventoryOne.bHasSecurityCard = true;
        idleCrosshairOne.SetActive(true);
        interactionUIOne.SetActive(false);
        idleCrosshairTwo.SetActive(true);
        interactionUITwo.SetActive(false);
        textDisplayOne.text = null;
        textDisplayTwo.text = null;
        Destroy(gameObject);
    }
    void PickupKeycardTwo()
    {
        SCR_InventoryTwo.bHasSecurityCard = true;
        idleCrosshairOne.SetActive(true);
        interactionUIOne.SetActive(false);
        idleCrosshairTwo.SetActive(true);
        interactionUITwo.SetActive(false);
        textDisplayOne.text = null;
        textDisplayTwo.text = null;
        Destroy(gameObject);
    }
}
