using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SCR_CardManager : MonoBehaviour
{
    [SerializeField] private Animator anim;
    [SerializeField] private AudioSource doorSound;
    [SerializeField] private AudioSource dialogue;

    private int goal = 3;
    public int currentValue = 0;

    void Update()
    {
        if(currentValue >= goal)
        {
            StartCoroutine(OpenFinalDoor());
        }
    }

    IEnumerator OpenFinalDoor()
    {
        yield return new WaitForSeconds(1.2f);
        dialogue.Play();
        yield return new WaitForSeconds(5.5f);
        anim.SetBool("bSwipedAll", true);
        doorSound.Play();
    }
}
