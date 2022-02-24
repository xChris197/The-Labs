using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

public class SCR_ThirdSlot : MonoBehaviour
{
    private float distance;
    private float distanceTwo;

    [SerializeField] private AudioSource componentMissing;

    [SerializeField] private GameObject playerOne;
    [SerializeField] private GameObject playerTwo;
    [SerializeField] private EventSystem es;
    [SerializeField] private GameObject hydrogenButtonOne;
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

    [SerializeField] private GameObject makerUI;
    [SerializeField] private GameObject makerUITwo;

    [SerializeField] private GameObject[] fakeChemicals;
    [SerializeField] private GameObject makerMachine;
    private SCR_MakerManager makerManager;

    private bool bIsInteracting = false;
    private bool bPlayerOneUsing = false;
    private bool bPlayerTwoUsing = false;

    void Start()
    {
        makerManager = makerMachine.GetComponent<SCR_MakerManager>();
    }
    void Update()
    {
        distance = SCR_PlayerCasting.distanceFromTarget;
        distanceTwo = SCR_PlayerCastingTwo.distanceFromTarget;

        if (distance < 2f && SCR_PlayerCasting.hitTarget.CompareTag("Third Slot"))
        {
            firstTimeNotActive = true;
            idleCrosshairOne.SetActive(false);
            interactionUIOne.SetActive(true);
            textDisplayOne.text = "[Third Slot]\n Press 'X' To Interact";
        }
        else if (firstTimeNotActive)
        {
            firstTimeNotActive = false;
            idleCrosshairOne.SetActive(true);
            interactionUIOne.SetActive(false);
        }

        if (distanceTwo < 2f && SCR_PlayerCastingTwo.hitTarget.CompareTag("Third Slot"))
        {
            secondTimeNotActive = true;
            idleCrosshairTwo.SetActive(true);
            interactionUITwo.SetActive(false);
            textDisplayOne.text = "[Third Slot]\n Press 'X' To Interact";
        }
        else if (secondTimeNotActive)
        {
            secondTimeNotActive = false;
            idleCrosshairTwo.SetActive(true);
            interactionUITwo.SetActive(false);
        }

        if (distance < 2f && SCR_PlayerCasting.hitTarget.CompareTag("Third Slot") && !bIsInteracting && (Input.GetButtonDown(interactOne)))
        {
            bPlayerOneUsing = true;
            UseSlotOne();
        }
        else if (distanceTwo < 2f && SCR_PlayerCastingTwo.hitTarget.CompareTag("Third Slot") && !bIsInteracting && (Input.GetButtonDown(interactTwo)))
        {
            bPlayerTwoUsing = true;
            UseSlotTwo();
        }

        if (Input.GetButtonDown(cancelOne) && bIsInteracting)
        {
            bPlayerOneUsing = false;
            ExitSlotOne();
        }
        else if (Input.GetButtonDown(cancelTwo) && bIsInteracting)
        {
            bPlayerTwoUsing = false;
            ExitSlotTwo();
        }
    }

    void UseSlotOne()
    {
        es.SetSelectedGameObject(hydrogenButtonOne);
        bIsInteracting = true;
        makerUI.SetActive(true);
        playerOne.GetComponent<SCR_PlayerMovement>().enabled = false;
        playerOne.GetComponentInChildren<SCR_PlayerLook>().enabled = false;
        idleCrosshairTwo.SetActive(false);
        interactionUITwo.SetActive(false);
    }

    void UseSlotTwo()
    {
        es.SetSelectedGameObject(hydrogenButtonTwo);
        bIsInteracting = true;
        makerUITwo.SetActive(true);
        playerTwo.GetComponent<SCR_PlayerMovement>().enabled = false;
        playerTwo.GetComponentInChildren<SCR_PlayerLook>().enabled = false;
        idleCrosshairTwo.SetActive(false);
        interactionUITwo.SetActive(false);
    }

    void ExitSlotOne()
    {
        for (int i = 0; i < fakeChemicals.Length; i++)
        {
            fakeChemicals[i].SetActive(false);
        }
        makerUI.SetActive(false);
        bIsInteracting = false;
        makerUI.SetActive(false);
        playerOne.GetComponent<SCR_PlayerMovement>().enabled = true;
        playerOne.GetComponentInChildren<SCR_PlayerLook>().enabled = true;
        idleCrosshairTwo.SetActive(false);
        interactionUITwo.SetActive(false);
    }

    void ExitSlotTwo()
    {
        for (int i = 0; i < fakeChemicals.Length; i++)
        {
            fakeChemicals[i].SetActive(false);
        }
        makerUITwo.SetActive(false);
        bIsInteracting = false;
        makerUITwo.SetActive(false);
        playerTwo.GetComponent<SCR_PlayerMovement>().enabled = true;
        playerTwo.GetComponentInChildren<SCR_PlayerLook>().enabled = true;
        idleCrosshairTwo.SetActive(false);
        interactionUITwo.SetActive(false);
    }

    public void AddHydrogenPeroxide()
    {
        if (makerUI.activeInHierarchy || makerUITwo.activeInHierarchy)
        {
            ExitSlotOne();
            ExitSlotTwo();
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
                makerManager.AddThirdChemical(1);

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
                makerManager.AddThirdChemical(1);
            }
            else if (!SCR_InventoryTwo.bHasHydrogenPeroxide)
            {
                componentMissing.Play();
            }
        }
    }
    public void AddSulphuricAcid()
    {
        if (makerUI.activeInHierarchy || makerUITwo.activeInHierarchy)
        {
            ExitSlotOne();
            ExitSlotTwo();
        }
        for (int i = 0; i < fakeChemicals.Length; i++)
        {
            fakeChemicals[i].SetActive(false);
        }
        if (bPlayerOneUsing)
        {
            if (SCR_InventoryOne.bHasSulphuricAcid)
            {
                fakeChemicals[1].SetActive(true);
                makerManager.AddThirdChemical(2);

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
                makerManager.AddThirdChemical(2);
            }
            else if (!SCR_InventoryTwo.bHasSulphuricAcid)
            {
                componentMissing.Play();
            }
        }
    }
    public void AddAcetone()
    {
        if (makerUI.activeInHierarchy || makerUITwo.activeInHierarchy)
        {
            ExitSlotOne();
            ExitSlotTwo();
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
                makerManager.AddThirdChemical(3);

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
                makerManager.AddThirdChemical(3);
            }
            else if (!SCR_InventoryTwo.bHasAcetone)
            {
                componentMissing.Play();
            }
        }
    }
    public void AddIsopropanyl()
    {
        if (makerUI.activeInHierarchy || makerUITwo.activeInHierarchy)
        {
            ExitSlotOne();
            ExitSlotTwo();
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
                makerManager.AddThirdChemical(4);

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
                makerManager.AddThirdChemical(4);
            }
            else if (!SCR_InventoryTwo.bHasIsopropanyl)
            {
                componentMissing.Play();
            }
        }
    }
    public void AddSodiumHypochlorite()
    {
        if (makerUI.activeInHierarchy || makerUITwo.activeInHierarchy)
        {
            ExitSlotOne();
            ExitSlotTwo();
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
                makerManager.AddThirdChemical(5);

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
                makerManager.AddThirdChemical(5);
            }
            else if (!SCR_InventoryTwo.bHasBleach)
            {
                componentMissing.Play();
            }
        }
    }
    public void AddPicricAcid()
    {
        if (makerUI.activeInHierarchy || makerUITwo.activeInHierarchy)
        {
            ExitSlotOne();
            ExitSlotTwo();
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
                makerManager.AddThirdChemical(6);

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
                makerManager.AddThirdChemical(6);
            }
            else if (!SCR_InventoryTwo.bHasPicricAcid)
            {
                componentMissing.Play();
            }
        }
    }
}
