using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SCR_VentInteraction : MonoBehaviour
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
    [SerializeField] private Animator doorAnim;
    [SerializeField] private GameObject explosive;
    [SerializeField] private Transform explosiveSpawn;
    [SerializeField] private AudioSource explosiveSFX;
    [SerializeField] private AudioSource doorOpening;
    [SerializeField] private AudioSource eventDialogue;

    [SerializeField] private string interactOne;
    [SerializeField] private string interactTwo;

    private bool bUsed = false;

    private bool firstTimeNotActive;
    private bool secondTimeNotActive;
    void Update()
    {
        distance = SCR_PlayerCasting.distanceFromTarget;
        distanceTwo = SCR_PlayerCastingTwo.distanceFromTarget;

        if (SCR_PlayerCasting.hitTarget.CompareTag("Vent") && distance < 2f && !SCR_InventoryOne.bHasExplosive)
        {
            firstTimeNotActive = true;
            idleCrosshairOne.SetActive(false);
            interactionUIOne.SetActive(true);
            textDisplayOne.text = "[Ventilation Hatch]\n Can allow objects to travel behind security door [COMPONENT MISSING]";
        }
        else if (SCR_PlayerCasting.hitTarget.CompareTag("Vent") && distance < 2f && SCR_InventoryOne.bHasExplosive)
        {
            firstTimeNotActive = true;
            idleCrosshairOne.SetActive(false);
            interactionUIOne.SetActive(true);
            textDisplayOne.text = "[Ventilation Hatch]\n Press 'X' To Place Explosive.";
        }
        else if(firstTimeNotActive)
        {
            idleCrosshairOne.SetActive(true);
            interactionUIOne.SetActive(false);
        }

        if (SCR_PlayerCastingTwo.hitTarget.CompareTag("Vent") && distanceTwo < 2f && SCR_InventoryTwo.bHasExplosive)
        {
            secondTimeNotActive = true;
            idleCrosshairTwo.SetActive(false);
            interactionUITwo.SetActive(true);
            textDisplayTwo.text = "[Ventilation Hatch]\n Press 'X' To Place Explosive.";
        }

        else if (SCR_PlayerCastingTwo.hitTarget.CompareTag("Vent") && distanceTwo < 2f && !SCR_InventoryTwo.bHasExplosive)
        {
            secondTimeNotActive = true;
            idleCrosshairTwo.SetActive(false);
            interactionUITwo.SetActive(true);
            textDisplayTwo.text = "[Ventilation Hatch]\n Can allow objects to travel behind security door [COMPONENT MISSING]";
        }
        else if(secondTimeNotActive)
        {
            secondTimeNotActive = false;
            idleCrosshairTwo.SetActive(true);
            interactionUITwo.SetActive(false);
        }

        if (distance < 2f && SCR_PlayerCasting.hitTarget.CompareTag("Vent") && (Input.GetButtonDown(interactOne) && SCR_InventoryOne.bHasExplosive && !bUsed))
        {
            StartCoroutine(OpenHatch());
        }
        else if (distanceTwo < 2f && SCR_PlayerCastingTwo.hitTarget.CompareTag("Vent") && Input.GetButtonDown(interactTwo) && SCR_InventoryTwo.bHasExplosive && !bUsed)
        {
            StartCoroutine(OpenHatch());
        }

    }

    IEnumerator OpenHatch()
    {
        anim.SetBool("bHasExplosive", true);
        bUsed = true;
        yield return new WaitForSeconds(3f);
        Instantiate(explosive, explosiveSpawn.transform.position, Quaternion.identity);
        anim.SetBool("bHasExplosive", false);
        yield return new WaitForSeconds(3f);
        explosiveSFX.Play();
        yield return new WaitForSeconds(3f);
        eventDialogue.Play();
        yield return new WaitForSeconds(10f);
        doorAnim.SetBool("bDoorCleared", true);
        doorOpening.Play();

        idleCrosshairOne.SetActive(true);
        idleCrosshairTwo.SetActive(true);

        interactionUIOne.SetActive(false);
        interactionUITwo.SetActive(false);

        textDisplayOne.text = null;
        textDisplayTwo.text = null;

        GetComponent<SCR_VentInteraction>().enabled = false;
    }
}