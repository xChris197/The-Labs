using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SCR_PicricAcid : MonoBehaviour
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

        if (distance < 2f && SCR_PlayerCasting.hitTarget.CompareTag("Picric"))
        {
            firstTimeNotActive = true;
            idleCrosshairOne.SetActive(false);
            interactionUIOne.SetActive(true);
            textDisplayOne.text = "[Picric Acid]\n Press 'X' To Pickup";
        }
        else if (firstTimeNotActive)
        {
            firstTimeNotActive = false;
            idleCrosshairOne.SetActive(true);
            interactionUIOne.SetActive(false);
        }
        if (distanceTwo < 2f && SCR_PlayerCastingTwo.hitTarget.CompareTag("Picric"))
        {
            secondTimeNotActive = true;
            idleCrosshairTwo.SetActive(true);
            interactionUITwo.SetActive(false);
            textDisplayTwo.text = "[Picric Acid]\n Press 'X' To Pickup";
        }
        else if (secondTimeNotActive)
        {
            secondTimeNotActive = false;
            idleCrosshairTwo.SetActive(true);
            interactionUITwo.SetActive(false);
        }
        if (distance < 2f && SCR_PlayerCasting.hitTarget.CompareTag("Picric") && (Input.GetButtonDown(interactOne)))
        {
            PickupPicricOne();
        }
        else if (distanceTwo < 2f && SCR_PlayerCastingTwo.hitTarget.CompareTag("Picric") && (Input.GetButtonDown(interactTwo)))
        {
            PickupPicricTwo();
        }
    }
    void PickupPicricOne()
    {
        SCR_InventoryOne.bHasPicricAcid = true;
        idleCrosshairOne.SetActive(true);
        interactionUIOne.SetActive(false);
        textDisplayOne.text = null;
        Destroy(gameObject);
    }
    void PickupPicricTwo()
    {
        SCR_InventoryTwo.bHasPicricAcid = true;
        idleCrosshairTwo.SetActive(true);
        interactionUITwo.SetActive(false);
        textDisplayTwo.text = null;
        Destroy(gameObject);
    }
}
