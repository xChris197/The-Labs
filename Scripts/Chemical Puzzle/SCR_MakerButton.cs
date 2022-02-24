using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SCR_MakerButton : MonoBehaviour
{
    [SerializeField] private GameObject makerBody;

    private SCR_MakerManager manager;

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

    void Start()
    {
        manager = makerBody.GetComponent<SCR_MakerManager>();
    }

    void Update()
    {
        distance = SCR_PlayerCasting.distanceFromTarget;
        distanceTwo = SCR_PlayerCastingTwo.distanceFromTarget;

        if (SCR_PlayerCasting.hitTarget.CompareTag("Maker Button") && distance < 2f)
        {
            firstTimeNotActive = true;
            idleCrosshairOne.SetActive(false);
            interactionUIOne.SetActive(true);
            textDisplayOne.text = "[Synthesis Button]\n Press 'X' To Interact";
        }
        else if (firstTimeNotActive)
        {
            firstTimeNotActive = false;
            idleCrosshairOne.SetActive(true);
            interactionUIOne.SetActive(false);
        }
        if (distanceTwo < 2f && SCR_PlayerCastingTwo.hitTarget.CompareTag("Maker Button"))
        {
            secondTimeNotActive = true;
            idleCrosshairTwo.SetActive(false);
            interactionUITwo.SetActive(true);
            textDisplayTwo.text = "[Synthesis Button]\n Hold 'X' To Open The Door";
        }
        else if (secondTimeNotActive)
        {
            secondTimeNotActive = false;
            idleCrosshairTwo.SetActive(true);
            interactionUITwo.SetActive(false);
        }

        if (distance < 2f && SCR_PlayerCasting.hitTarget.CompareTag("Maker Button") && (Input.GetButtonDown(interactOne)))
        {
            ActivateMachine();
        }
        else if(Input.GetButtonDown(interactTwo) && distanceTwo < 2f && SCR_PlayerCastingTwo.hitTarget.CompareTag("Maker Button"))
        {
            ActivateMachine();
        }
    }

    void ActivateMachine()
    {
        manager.bButtonPressed = true;
    }
}
