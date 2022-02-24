using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class SCR_ChemicalAnalyser : MonoBehaviour
{
    private float distance;
    private float distanceTwo;

    [SerializeField] private AudioSource componentMissing;

    [SerializeField] private GameObject playerOne;
    [SerializeField] private GameObject playerTwo;
    [SerializeField] private EventSystem es;
    [SerializeField] private GameObject hydrogenButton;
    [SerializeField] private GameObject hydrogenButtonTwo;

    [SerializeField] private GameObject interactionUIOne;
    [SerializeField] private GameObject idleCrosshairOne;
    [SerializeField] private TextMeshProUGUI textDisplayOne;

    [SerializeField] private GameObject interactionUITwo;
    [SerializeField] private GameObject idleCrosshairTwo;
    [SerializeField] private TextMeshProUGUI textDisplayTwo;

    [SerializeField] private string interactOne;
    [SerializeField] private string interactTwo;

    [SerializeField] private string cancelOne;
    [SerializeField] private string cancelTwo;

    private bool firstTimeNotActive;
    private bool secondTimeNotActive;

    [SerializeField] private GameObject consoleUI;
    [SerializeField] private GameObject consoleUITwo;

    [SerializeField] private GameObject[] fakeChemicals;
    [SerializeField] private Material[] consoleMaterials;
    [SerializeField] private Material noScreenMaterial;
    [SerializeField] private Light laser;

    private bool bIsInteracting = false;
    private bool bPlayerOneUsing = false;
    private bool bPlayerTwoUsing = false;

    void Update()
    {
        distance = SCR_PlayerCasting.distanceFromTarget;
        distanceTwo = SCR_PlayerCastingTwo.distanceFromTarget;

        if (distance < 2f && SCR_PlayerCasting.hitTarget.CompareTag("Analyser"))
        {
            firstTimeNotActive = true;
            idleCrosshairOne.SetActive(false);
            interactionUIOne.SetActive(true);
            textDisplayOne.text = "[Chemical Analyser]\n Press 'X' To Interact";
        }
        else if (firstTimeNotActive)
        {
            firstTimeNotActive = false;
            idleCrosshairOne.SetActive(true);
            interactionUIOne.SetActive(false);
        }

        if (distanceTwo < 2f && SCR_PlayerCastingTwo.hitTarget.CompareTag("Analyser"))
        {
            secondTimeNotActive = true;
            idleCrosshairTwo.SetActive(true);
            interactionUITwo.SetActive(false);
            textDisplayTwo.text = "[Chemical Analyser]\n Press 'X' To Interact";
        }
        else if (secondTimeNotActive)
        {
            secondTimeNotActive = false;
            idleCrosshairTwo.SetActive(true);
            interactionUITwo.SetActive(false);
        }

        if (distance < 2f && SCR_PlayerCasting.hitTarget.CompareTag("Analyser") && !bIsInteracting && (Input.GetButtonDown(interactOne)))
        {
            bPlayerOneUsing = true;
            UseMachineOne();
        }
        else if (distanceTwo < 2f && SCR_PlayerCastingTwo.hitTarget.CompareTag("Analyser") && !bIsInteracting && (Input.GetButtonDown(interactTwo)))
        {
            bPlayerTwoUsing = true;
            UseMachineTwo();
        }

        if (Input.GetButtonDown(cancelOne) && bIsInteracting)
        {
            bPlayerOneUsing = false;
            ExitMachineOne();
        }
        else if(Input.GetButtonDown(cancelTwo) && bIsInteracting)
        {
            bPlayerTwoUsing = false;
            ExitMachineTwo();
        }
    }

    void UseMachineOne()
    {
        es.SetSelectedGameObject(hydrogenButton);
        bIsInteracting = true;
        consoleUI.SetActive(true);
        playerOne.GetComponent<SCR_PlayerMovement>().enabled = false;
        playerOne.GetComponentInChildren<SCR_PlayerLook>().enabled = false;
        idleCrosshairTwo.SetActive(false);
        interactionUITwo.SetActive(false);
    }

    void UseMachineTwo()
    {
        es.SetSelectedGameObject(hydrogenButtonTwo);
        bIsInteracting = true;
        consoleUITwo.SetActive(true);
        playerTwo.GetComponent<SCR_PlayerMovement>().enabled = false;
        playerTwo.GetComponentInChildren<SCR_PlayerLook>().enabled = false;
        idleCrosshairTwo.SetActive(false);
        interactionUITwo.SetActive(false);
    }

    void ExitMachineOne()
    {
        for (int i = 0; i < fakeChemicals.Length; i++)
        {
            fakeChemicals[i].SetActive(false);
        }
        GetComponent<MeshRenderer>().material = noScreenMaterial;
        consoleUI.SetActive(false);
        bIsInteracting = false;
        consoleUI.SetActive(false);
        playerOne.GetComponent<SCR_PlayerMovement>().enabled = true;
        playerOne.GetComponentInChildren<SCR_PlayerLook>().enabled = true;
        idleCrosshairTwo.SetActive(false);
        interactionUITwo.SetActive(false);
        laser.enabled = false;
    }

    void ExitMachineTwo()
    {
        for(int i = 0; i < fakeChemicals.Length; i++)
        {
            fakeChemicals[i].SetActive(false);
        }
        GetComponent<MeshRenderer>().material = noScreenMaterial;
        consoleUITwo.SetActive(false);
        bIsInteracting = false;
        consoleUITwo.SetActive(false);
        playerTwo.GetComponent<SCR_PlayerMovement>().enabled = true;
        playerTwo.GetComponentInChildren<SCR_PlayerLook>().enabled = true;
        idleCrosshairTwo.SetActive(false);
        interactionUITwo.SetActive(false);
        laser.enabled = false;
    }

    public void HydrogenPeroxide()
    {
        if (consoleUI.activeInHierarchy|| consoleUITwo.activeInHierarchy)
        {
            ExitMachineOne();
            ExitMachineTwo();
        }
        for (int i = 0; i < fakeChemicals.Length; i++)
        {
            fakeChemicals[i].SetActive(false);
        }
        if (bPlayerOneUsing)
        {
            if (SCR_InventoryOne.bHasHydrogenPeroxide)
            {
                fakeChemicals[0].SetActive(true);
                laser.enabled = true;
                GetComponent<MeshRenderer>().material = consoleMaterials[0];
            }
            else if (!SCR_InventoryOne.bHasHydrogenPeroxide)
            {
                componentMissing.Play();
            }
        }
        if (bPlayerTwoUsing)
        {
            if (SCR_InventoryTwo.bHasHydrogenPeroxide)
            {
                fakeChemicals[0].SetActive(true);
                laser.enabled = true;
                GetComponent<MeshRenderer>().material = consoleMaterials[0];
            }
            else if (!SCR_InventoryTwo.bHasHydrogenPeroxide)
            {
                componentMissing.Play();
            }
        }
    }
    public void SulphuricAcid()
    {
        if(consoleUI.activeInHierarchy || consoleUITwo.activeInHierarchy)
        {
            ExitMachineOne();
            ExitMachineTwo();
        }
        for (int i = 0; i < fakeChemicals.Length; i++)
        {
            fakeChemicals[i].SetActive(false);
        }
        if (bPlayerOneUsing)
        {
            if (SCR_InventoryOne.bHasSulphuricAcid)
            {
                fakeChemicals[0].SetActive(true);
                laser.enabled = true;
                GetComponent<MeshRenderer>().material = consoleMaterials[1];
            }
            else if (!SCR_InventoryOne.bHasSulphuricAcid)
            {
                componentMissing.Play();
            }
        }
        if (bPlayerTwoUsing)
        {
            if (SCR_InventoryTwo.bHasSulphuricAcid)
            {
                fakeChemicals[1].SetActive(true);
                laser.enabled = true;
                GetComponent<MeshRenderer>().material = consoleMaterials[1];
            }
            else if (!SCR_InventoryTwo.bHasSulphuricAcid)
            {
                componentMissing.Play();
            }
        }
    }

    public void Acetone()
    {
        if (consoleUI.activeInHierarchy || consoleUITwo.activeInHierarchy)
        {
            ExitMachineOne();
            ExitMachineTwo();
        }
        for (int i = 0; i < fakeChemicals.Length; i++)
        {
            fakeChemicals[i].SetActive(false);
        }
        if (bPlayerOneUsing)
        {
            if (SCR_InventoryOne.bHasAcetone)
            {
                fakeChemicals[2].SetActive(true);
                laser.enabled = true;
                GetComponent<MeshRenderer>().material = consoleMaterials[2];
            }
            else if (!SCR_InventoryOne.bHasAcetone)
            {
                componentMissing.Play();
            }
        }
        if (bPlayerTwoUsing)
        {
            if (SCR_InventoryTwo.bHasAcetone)
            {
                fakeChemicals[2].SetActive(true);
                laser.enabled = true;
                GetComponent<MeshRenderer>().material = consoleMaterials[2];
            }
            else if (!SCR_InventoryTwo.bHasAcetone)
            {
                componentMissing.Play();
            }
        }
    }

    public void Isopropanyl()
    {
        if (consoleUI.activeInHierarchy || consoleUITwo.activeInHierarchy)
        {
            ExitMachineOne();
            ExitMachineTwo();
        }
        for (int i = 0; i < fakeChemicals.Length; i++)
        {
            fakeChemicals[i].SetActive(false);
        }
        if (bPlayerOneUsing)
        {
            if (SCR_InventoryOne.bHasIsopropanyl)
            {
                fakeChemicals[3].SetActive(true);
                laser.enabled = true;
                GetComponent<MeshRenderer>().material = consoleMaterials[3];
            }
            else if (!SCR_InventoryOne.bHasIsopropanyl)
            {
                componentMissing.Play();
            }
        }
        if (bPlayerTwoUsing)
        {
            if (SCR_InventoryTwo.bHasIsopropanyl)
            {
                fakeChemicals[3].SetActive(true);
                laser.enabled = true;
                GetComponent<MeshRenderer>().material = consoleMaterials[3];
            }
            else if (!SCR_InventoryTwo.bHasIsopropanyl)
            {
                componentMissing.Play();
            }
        }
    }

    public void SodiumHypochlorite()
    {
        if (consoleUI.activeInHierarchy || consoleUITwo.activeInHierarchy)
        {
            ExitMachineOne();
            ExitMachineTwo();
        }
        for (int i = 0; i < fakeChemicals.Length; i++)
        {
            fakeChemicals[i].SetActive(false);
        }
        if (bPlayerOneUsing)
        {
            if (SCR_InventoryOne.bHasBleach)
            {
                fakeChemicals[4].SetActive(true);
                laser.enabled = true;
                GetComponent<MeshRenderer>().material = consoleMaterials[4];
            }
            else if (!SCR_InventoryOne.bHasBleach)
            {
                componentMissing.Play();
            }
        }
        if (bPlayerTwoUsing)
        {
            if (SCR_InventoryTwo.bHasBleach)
            {
                fakeChemicals[4].SetActive(true);
                laser.enabled = true;
                GetComponent<MeshRenderer>().material = consoleMaterials[4];
            }
            else if (!SCR_InventoryTwo.bHasBleach)
            {
                componentMissing.Play();
            }
        }
    }
    public void PicricAcid()
    {
        if (consoleUI.activeInHierarchy || consoleUITwo.activeInHierarchy)
        {
            ExitMachineOne();
            ExitMachineTwo();
        }
        for (int i = 0; i < fakeChemicals.Length; i++)
        {
            fakeChemicals[i].SetActive(false);
        }
        if (bPlayerOneUsing)
        {
            if (SCR_InventoryOne.bHasPicricAcid)
            {
                fakeChemicals[5].SetActive(true);
                laser.enabled = true;
                GetComponent<MeshRenderer>().material = consoleMaterials[5];
            }
            else if (!SCR_InventoryOne.bHasPicricAcid)
            {
                componentMissing.Play();
            }
        }
        if (bPlayerTwoUsing)
        {
            if (SCR_InventoryTwo.bHasPicricAcid)
            {
                fakeChemicals[5].SetActive(true);
                laser.enabled = true;
                GetComponent<MeshRenderer>().material = consoleMaterials[5];
            }
            else if (!SCR_InventoryTwo.bHasPicricAcid)
            {
                componentMissing.Play();
            }
        }
    }
}