using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SCR_Locker : MonoBehaviour
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
    [SerializeField] private GameObject body;

    [SerializeField] private string interactOne;
    [SerializeField] private string interactTwo;

    private bool firstTimeNotActive;
    private bool secondTimeNotActive;
    void Update()
    {
        distance = SCR_PlayerCasting.distanceFromTarget;
        distanceTwo = SCR_PlayerCastingTwo.distanceFromTarget;

        if (SCR_PlayerCasting.hitTarget.CompareTag("UnlockableLocker") && distance < 2f && !SCR_InventoryOne.bHasLockerKey)
        {
            firstTimeNotActive = true;
            idleCrosshairOne.SetActive(false);
            interactionUIOne.SetActive(true);
            textDisplayOne.text = "[Locker]\n Locked. Key Missing";
        }
        else if (SCR_PlayerCasting.hitTarget.CompareTag("UnlockableLocker") && distance < 2f && SCR_InventoryOne.bHasLockerKey)
        {
            firstTimeNotActive = true;
            idleCrosshairOne.SetActive(false);
            interactionUIOne.SetActive(true);
            textDisplayOne.text = "[Locker]\n Press 'X' To Unlock.";
        }
        else if (firstTimeNotActive)
        {
            idleCrosshairOne.SetActive(true);
            interactionUIOne.SetActive(false);
        }

        if (SCR_PlayerCastingTwo.hitTarget.CompareTag("UnlockableLocker") && distanceTwo < 2f && SCR_InventoryTwo.bHasLockerKey)
        {
            secondTimeNotActive = true;
            idleCrosshairTwo.SetActive(false);
            interactionUITwo.SetActive(true);
            textDisplayTwo.text = "[Locker]\n Press 'X' To Unlock.";
        }

        else if (SCR_PlayerCastingTwo.hitTarget.CompareTag("Vent") && distanceTwo < 2f && !SCR_InventoryTwo.bHasLockerKey)
        {
            secondTimeNotActive = true;
            idleCrosshairTwo.SetActive(false);
            interactionUITwo.SetActive(true);
            textDisplayTwo.text = "[Locker]\n Locked. Key Missing";
        }
        else if (secondTimeNotActive)
        {
            secondTimeNotActive = false;
            idleCrosshairTwo.SetActive(true);
            interactionUITwo.SetActive(false);
        }

        if (distance < 2f && SCR_PlayerCasting.hitTarget.CompareTag("UnlockableLocker") && (Input.GetButtonDown(interactOne) && SCR_InventoryOne.bHasLockerKey))
        {
            OpenLocker();
        }
        else if(distanceTwo < 2f && SCR_PlayerCastingTwo.hitTarget.CompareTag("UnlockableLocker") && Input.GetButtonDown(interactTwo) && SCR_InventoryTwo.bHasLockerKey)
        {
            OpenLocker();
        }
    }

    void OpenLocker()
    {
        anim.SetBool("bHasKey", true);
        body.layer = 2;

        idleCrosshairOne.SetActive(true);
        idleCrosshairTwo.SetActive(true);

        interactionUIOne.SetActive(false);
        interactionUITwo.SetActive(false);

        textDisplayOne.text = null;
        textDisplayTwo.text = null;

        GetComponent<SCR_Locker>().enabled = false;
    }
}
