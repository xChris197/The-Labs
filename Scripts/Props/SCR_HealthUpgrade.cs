using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SCR_HealthUpgrade : MonoBehaviour
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

    [SerializeField] private GameObject playerOne;
    [SerializeField] private GameObject playerTwo;

    [SerializeField] private AudioSource injectionSFX;

    private SCR_PlayerHealth healthOne;
    private SCR_PlayerHealth healthTwo;

    private bool firstTimeNotActive;
    private bool secondTimeNotActive;

    void Start()
    {
        healthOne = playerOne.GetComponent<SCR_PlayerHealth>();
        healthTwo = playerTwo.GetComponent<SCR_PlayerHealth>();
    }
    void Update()
    {
        distance = SCR_PlayerCasting.distanceFromTarget;
        distanceTwo = SCR_PlayerCastingTwo.distanceFromTarget;

        if (distance < 2f && SCR_PlayerCasting.hitTarget.CompareTag("HealthUpgrade"))
        {
            firstTimeNotActive = true;
            idleCrosshairOne.SetActive(false);
            interactionUIOne.SetActive(true);
            textDisplayOne.text = "[Health Syringe]\n Press 'X' To Pickup";
        }
        else if (firstTimeNotActive)
        {
            firstTimeNotActive = false;
            idleCrosshairOne.SetActive(true);
            interactionUIOne.SetActive(false);
        }

        if (distanceTwo < 2f && SCR_PlayerCastingTwo.hitTarget.CompareTag("HealthUpgrade"))
        {
            secondTimeNotActive = true;
            idleCrosshairTwo.SetActive(true);
            interactionUITwo.SetActive(false);
            textDisplayTwo.text = "[Health Syringe]\n Press 'X' To Pickup";
        }
        else if (secondTimeNotActive)
        {
            secondTimeNotActive = false;
            idleCrosshairTwo.SetActive(true);
            interactionUITwo.SetActive(false);
        }


        if (distance < 2f && SCR_PlayerCasting.hitTarget.CompareTag("HealthUpgrade") && (Input.GetButtonDown(interactOne)))
        {
            PickupHealthOne();
        }
        else if (distanceTwo < 2f && SCR_PlayerCastingTwo.hitTarget.CompareTag("HealthUpgrade") && (Input.GetButtonDown(interactTwo)))
        {
            PickupHealthTwo();
        }
    }
    void PickupHealthOne()
    {
        injectionSFX.Play();
        healthOne.maxHealth = 150f;
        healthOne.currentHealth = healthOne.maxHealth;

        idleCrosshairOne.SetActive(true);
        interactionUIOne.SetActive(false);
        idleCrosshairTwo.SetActive(true);
        interactionUITwo.SetActive(false);
        textDisplayOne.text = null;
        textDisplayTwo.text = null;
        Destroy(gameObject);
    }
    void PickupHealthTwo()
    {
        injectionSFX.Play();
        healthTwo.maxHealth = 150f;
        healthTwo.currentHealth = healthTwo.maxHealth;

        idleCrosshairOne.SetActive(true);
        interactionUIOne.SetActive(false);
        idleCrosshairTwo.SetActive(true);
        interactionUITwo.SetActive(false);
        textDisplayOne.text = null;
        textDisplayTwo.text = null;
        Destroy(gameObject);
    }
}
