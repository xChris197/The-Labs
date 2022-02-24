using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SCR_SodiumHypochlorite : MonoBehaviour
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

        if (distance < 2f && SCR_PlayerCasting.hitTarget.CompareTag("Bleach"))
        {
            firstTimeNotActive = true;
            idleCrosshairOne.SetActive(false);
            interactionUIOne.SetActive(true);
            textDisplayOne.text = "[Sodium Hypochlorite]\n Press 'X' To Pickup";
        }
        else if (firstTimeNotActive)
        {
            firstTimeNotActive = false;
            idleCrosshairOne.SetActive(true);
            interactionUIOne.SetActive(false);
        }
        if (distanceTwo < 2f && SCR_PlayerCastingTwo.hitTarget.CompareTag("Bleach"))
        {
            secondTimeNotActive = true;
            idleCrosshairTwo.SetActive(true);
            interactionUITwo.SetActive(false);
            textDisplayTwo.text = "[Sodium Hypochlorite]\n Press 'X' To Pickup";
        }
        else if (secondTimeNotActive)
        {
            secondTimeNotActive = false;
            idleCrosshairTwo.SetActive(true);
            interactionUITwo.SetActive(false);
        }
        if (distance < 2f && SCR_PlayerCasting.hitTarget.CompareTag("Bleach") && (Input.GetButtonDown(interactOne)))
        {
            PickupBleachOne();
        }
        else if (distanceTwo < 2f && SCR_PlayerCastingTwo.hitTarget.CompareTag("Bleach") && (Input.GetButtonDown(interactTwo)))
        {
            PickupBleachTwo();
        }
    }
    void PickupBleachOne()
    {
        SCR_InventoryOne.bHasBleach = true;
        idleCrosshairOne.SetActive(true);
        interactionUIOne.SetActive(false);
        textDisplayOne.text = null;
        Destroy(gameObject);
    }
    void PickupBleachTwo()
    {
        SCR_InventoryTwo.bHasBleach = true;
        idleCrosshairTwo.SetActive(true);
        interactionUITwo.SetActive(false);
        textDisplayTwo.text = null;
        Destroy(gameObject);
    }
}
