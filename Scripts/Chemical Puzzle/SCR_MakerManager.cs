using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SCR_MakerManager : MonoBehaviour
{
    [SerializeField] private int[] correctSequence = { 1, 3, 6 };
    [SerializeField] private int[] playerSequence;

    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private GameObject zombiePrefab;
    [SerializeField] private GameObject zombie;
    private int zombieCount = 5;
    public int currentKillCount;

    [SerializeField] private Animator anim;

    [SerializeField] private AudioSource machineDialogue;
    [SerializeField] private AudioSource completedTaskDialogue;
    [SerializeField] private AudioSource wrongSequence;
    private int numsCorrect = 0;
    private int numsRequired = 3;
    public bool bButtonPressed = false;
    void Update()
    {
        if(bButtonPressed)
        {
            for(int i = 0; i < correctSequence.Length; i++)
            {
                if(playerSequence[i] == correctSequence[i])
                {
                    numsCorrect++;
                }
                else
                {
                    wrongSequence.Play();
                    bButtonPressed = false;
                }
            }
            if(numsCorrect == numsRequired)
            {
                StartCoroutine(SpawnZombieWave());
                bButtonPressed = false;
            }
            else
            {
                bButtonPressed = false;
                return;
            }
        }

        if (currentKillCount >= zombieCount)
        {
            completedTaskDialogue.Play();
            zombiePrefab = null;
            anim.SetBool("bAllDead", true);
        }
    }

    public void AddFirstChemical(int value)
    {
        playerSequence[0] = value;
    }

    public void AddSecondChemical(int value)
    {
        playerSequence[1] = value;
    }

    public void AddThirdChemical(int value)
    {
        playerSequence[2] = value;
    }

    IEnumerator SpawnZombieWave()
    {
        yield return new WaitForSeconds(1f);
        machineDialogue.Play();
        yield return new WaitForSeconds(11f);
        foreach (Transform point in spawnPoints)
        {
            zombiePrefab = Instantiate(zombie, point.position, point.rotation);
        }
    }

}
