using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SCR_KeypadButton8 : MonoBehaviour
{
    private float distance;
    private float distanceTwo;

    [SerializeField] private GameObject keypad;
    [SerializeField] private AudioSource inputSound;
    private SCR_Keypad keypadScript;

    [SerializeField] private GameObject interactionUIOne;
    [SerializeField] private GameObject idleCrosshairOne;
    [SerializeField] private TextMeshProUGUI textDisplayOne;

    [SerializeField] private GameObject interactionUITwo;
    [SerializeField] private GameObject idleCrosshairTwo;
    [SerializeField] private TextMeshProUGUI textDisplayTwo;

    [SerializeField] private string interactOne;
    [SerializeField] private string interactTwo;

    [SerializeField] private int keypadValue;
    [SerializeField] private string buttonNum;

    private bool firstTimeNotActive;
    private bool secondTimeNotActive;
    void Start()
    {
        keypadScript = keypad.GetComponent<SCR_Keypad>();
    }

    void Update()
    {
        distance = SCR_PlayerCasting.distanceFromTarget;
        distanceTwo = SCR_PlayerCastingTwo.distanceFromTarget;

        if (distance < 2f && SCR_PlayerCasting.hitTarget.CompareTag("Keypad Button 8"))
        {
            firstTimeNotActive = true;
            idleCrosshairOne.SetActive(false);
            interactionUIOne.SetActive(true);
            textDisplayOne.text = buttonNum;
        }
        else if (firstTimeNotActive)
        {
            firstTimeNotActive = false;
            idleCrosshairOne.SetActive(true);
            interactionUIOne.SetActive(false);
        }

        if (distanceTwo < 2f && SCR_PlayerCastingTwo.hitTarget.CompareTag("Keypad Button 8"))
        {
            secondTimeNotActive = true;
            idleCrosshairTwo.SetActive(true);
            interactionUITwo.SetActive(false);
            textDisplayTwo.text = buttonNum;
        }
        else if (secondTimeNotActive)
        {
            secondTimeNotActive = false;
            idleCrosshairTwo.SetActive(true);
            interactionUITwo.SetActive(false);
        }

        if (distance < 2f && SCR_PlayerCasting.hitTarget.CompareTag("Keypad Button 8") && (Input.GetButtonDown(interactOne)))
        {
            inputSound.Play();
            keypadScript.AddPlayerInput(keypadValue);
        }
        else if (distanceTwo < 2f && SCR_PlayerCastingTwo.hitTarget.CompareTag("Keypad Button 8") && Input.GetButtonDown(interactTwo))
        {
            inputSound.Play();
            keypadScript.AddPlayerInput(keypadValue);
        }
    }
}
