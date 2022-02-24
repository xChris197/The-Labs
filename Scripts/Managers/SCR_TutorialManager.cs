using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class SCR_TutorialManager : MonoBehaviour
{
    [SerializeField] private GameObject playerOne;
    [SerializeField] private GameObject playerTwo;
    private SCR_TutorialHealth playerHealthOne;
    private SCR_TutorialHealth playerHealthTwo;

    [SerializeField] private GameObject playerOneUI;
    [SerializeField] private GameObject playerTwoUI;
    [SerializeField] private GameObject weaponOne;
    [SerializeField] private GameObject weaponTwo;

    [SerializeField] private TextMeshProUGUI popUpOne;
    [SerializeField] private TextMeshProUGUI popUpTwo;
    [SerializeField] private int index = 0;

    [SerializeField] private AudioSource[] dialogues;

    [SerializeField] private GameObject fadeOut;
    [SerializeField] private GameObject fadeOutTwo;
    private Animator anim;
    private Animator animTwo;

    private float interval = 2f;
    private bool bLookCommandTold = false;
    private bool bWeaponCommandTold = false;
    private bool bGrenadeCommandTold = false;
    private bool bHurtCommandTold = false;

    private bool bLookExec = false;
    private bool bWeaponExec = false;
    private bool bGrenadeExec = false;
    private bool bHurtExec = false;
    private bool bEndingExec = false;

    void Start()
    {
        playerHealthOne = playerOne.GetComponent<SCR_TutorialHealth>();
        playerHealthTwo = playerTwo.GetComponent<SCR_TutorialHealth>();
        anim = fadeOut.GetComponent<Animator>();
        animTwo = fadeOutTwo.GetComponent<Animator>();
    }
    void Update()
    {
        if(index == 0 && !bLookCommandTold && !bLookExec)
        {
            StartCoroutine(LookCommand());
            dialogues[index].Play();
            bLookExec = true;
        }
        if(index == 0 && bLookCommandTold && Input.GetAxis("R_YAxis_1") > 0)
        {
            index++;
            bLookCommandTold = false;
        }
        else if(index == 0 && bLookCommandTold && Input.GetAxis("R_YAxis_1") < 0)
        {
            index++;
            bLookCommandTold = false;
        }

        else if (index == 0 && bLookCommandTold && Input.GetAxis("R_YAxis_2") < 0)
        {
            index++;
            bLookCommandTold = false;
        }

        else if (index == 0 && bLookCommandTold && Input.GetAxis("R_YAxis_2") < 0)
        {
            index++;
            bLookCommandTold = false;
        }

        if (index == 1 && !bWeaponCommandTold && !bWeaponExec)
        {
            StartCoroutine(WeaponCommand());
            dialogues[index].Play();
            bWeaponExec = true;
        }

        if(index == 1 && bWeaponCommandTold && Input.GetAxis("TriggersR_1") > 0f)
        {
            index++;
            bWeaponCommandTold = false;
        }
        else if(index == 1 && bWeaponCommandTold && Input.GetAxis("TriggersR_2") > 0f)
        {
            index++;
            bWeaponCommandTold = false;
        }

        if(index == 2 && !bGrenadeCommandTold && !bGrenadeExec)
        {
            StartCoroutine(GrenadeCommand());
            dialogues[index].Play();
            bGrenadeExec = true;
        }
        if(index == 2 && bGrenadeCommandTold && Input.GetButtonDown("RB_1"))
        {
            index++;
            bGrenadeCommandTold = false;
        }
        else if (index == 2 && bGrenadeCommandTold && Input.GetButtonDown("RB_2"))
        {
            index++;
            bGrenadeCommandTold = false;
        }

        if(index == 3 && !bHurtCommandTold && !bHurtExec)
        {
            StartCoroutine(HurtCommand());
            dialogues[index].Play();
            bHurtExec = true;
        }
        if(index == 3 && bHurtCommandTold)
        {
            index++;
            bHurtCommandTold = false;
        }

        if(index == 4 && !bEndingExec)
        {
            StartCoroutine(FinishTutorial());
            dialogues[index].Play();
            bEndingExec = true;
        }
    }

    IEnumerator LookCommand()
    {
        yield return new WaitForSeconds(13.5f);
        popUpOne.text = "Use RS To Look Around";
        popUpTwo.text = "Use RS To Look Around";
        yield return new WaitForSeconds(interval);
        bLookCommandTold = true;
    }
    IEnumerator WeaponCommand()
    {
        popUpOne.text = " ";
        popUpTwo.text = " ";
        yield return new WaitForSeconds(9f);
        weaponOne.SetActive(true);
        weaponTwo.SetActive(true);
        playerOneUI.SetActive(true);
        playerTwoUI.SetActive(true);
        bWeaponCommandTold = true;
        popUpOne.text = "Press RT To Fire Your Weapon";
        popUpTwo.text = "Press RT To Fire Your Weapon";
        yield return new WaitForSeconds(interval);
    }

    IEnumerator GrenadeCommand()
    {
        popUpOne.text = " ";
        popUpTwo.text = " ";
        yield return new WaitForSeconds(9f);
        bGrenadeCommandTold = true;
        popUpOne.text = "Press RB To Throw A Grenade";
        popUpTwo.text = "Press RB To Throw A Grenade";
        yield return new WaitForSeconds(interval);
    }

    IEnumerator HurtCommand()
    {
        popUpOne.text = " ";
        popUpTwo.text = " ";
        yield return new WaitForSeconds(2f);
        playerHealthOne.currentHealth = 10;
        playerHealthTwo.currentHealth = 10;
        playerHealthOne.bPlayerDamaged = true;
        playerHealthTwo.bPlayerDamaged = true;
        yield return new WaitForSeconds(10.5f);
        bHurtCommandTold = true;
        yield return new WaitForSeconds(interval);
    }

    IEnumerator FinishTutorial()
    {
        yield return new WaitForSeconds(15f);
        anim.SetBool("bTutorialFinished", true);
        animTwo.SetBool("bTutorialFinished", true);
        yield return new WaitForSeconds(2.2f);
        SceneManager.LoadScene(2);
    }
}
