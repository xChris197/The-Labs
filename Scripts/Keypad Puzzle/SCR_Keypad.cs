using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SCR_Keypad : MonoBehaviour
{
    [SerializeField] private int[] correctSequence = { 4, 9, 2, 1 };
    [SerializeField] private Animator anim;
    [SerializeField] private AudioSource incorrectSound;
    [SerializeField] private AudioSource successSound;
    [SerializeField] private int playerInputIndex = 0;

    [SerializeField] private GameObject crateBox;

    public void AddPlayerInput(int value)
    {
        // Reset if incorrect value provided
        if (value != correctSequence[playerInputIndex])
        {
            playerInputIndex = 0;
            incorrectSound.Play();
            return;
        }
        else
        {
            playerInputIndex++;
        }

        // Last input reached, meaning all the values are correct
        if (playerInputIndex == correctSequence.Length)
        {
            playerInputIndex = 0;
            StartCoroutine(SuccessSequence());
            GetComponent<SCR_Keypad>().enabled = false;
        }
    }
    IEnumerator SuccessSequence()
    {
        anim.SetBool("bCorrectInput", true);
        crateBox.layer = 2;
        successSound.Play();
        yield return new WaitForSeconds(1f);
        successSound.Stop();
    }
}
