using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SCR_Isopropanyl : MonoBehaviour
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

        if (distance < 2f && SCR_PlayerCasting.hitTarget.CompareTag("Isopropanyl"))
        {
            firstTimeNotActive = true;
            idleCrosshairOne.SetActive(false);
            interactionUIOne.SetActive(true);
            textDisplayOne.text = "[Isopropanyl]\n Press 'X' To Pickup";
        }
        else if (firstTimeNotActive)
        {
            firstTimeNotActive = false;
            idleCrosshairOne.SetActive(true);
            interactionUIOne.SetActive(false);
        }
        if (distanceTwo < 2f && SCR_PlayerCastingTwo.hitTarget.CompareTag("Isopropanyl"))
        {
            secondTimeNotActive = true;
            idleCrosshairTwo.SetActive(true);
            interactionUITwo.SetActive(false);
            textDisplayTwo.text = "[Isopropanyl]\n Press 'X' To Pickup";
        }
        else if (secondTimeNotActive)
        {
            secondTimeNotActive = false;
            idleCrosshairTwo.SetActive(true);
            interactionUITwo.SetActive(false);
        }
        if (distance < 2f && SCR_PlayerCasting.hitTarget.CompareTag("Isopropanyl") && (Input.GetButtonDown(interactOne)))
        {
            PickupIsopropanylOne();
        }
        else if (distanceTwo < 2f && SCR_PlayerCastingTwo.hitTarget.CompareTag("Isopropanyl") && (Input.GetButtonDown(interactTwo)))
        {
            PickupIsopropanylTwo();
        }
    }
    void PickupIsopropanylOne()
    {
        SCR_InventoryOne.bHasIsopropanyl = true;
        idleCrosshairOne.SetActive(true);
        interactionUIOne.SetActive(false);
        textDisplayOne.text = null;
        Destroy(gameObject);
    }
    void PickupIsopropanylTwo()
    {
        SCR_InventoryTwo.bHasIsopropanyl = true;
        idleCrosshairTwo.SetActive(true);
        interactionUITwo.SetActive(false);
        textDisplayTwo.text = null;
        Destroy(gameObject);
    }
}
