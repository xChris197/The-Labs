using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SCR_TutorialHealth : MonoBehaviour
{
    public float currentHealth;
    public float maxHealth = 100;

    [SerializeField] private GameObject bloodVignette;

    public bool bPlayerDamaged = false;

    [SerializeField] private TextMeshProUGUI healthNumText;
    void Start()
    {
        currentHealth = maxHealth;
    }

    void Update()
    {
        healthNumText.text = currentHealth.ToString("0");

        if (currentHealth < 50)
        {
            bloodVignette.SetActive(true);
        }
        else
        {
            bloodVignette.SetActive(false);
        }

        if (bPlayerDamaged)
        {
            StartCoroutine(RegenHealth());
            if (currentHealth == maxHealth)
            {
                StopCoroutine(RegenHealth());
                bPlayerDamaged = false;
            }
        }
    }

    IEnumerator RegenHealth()
    {
        if (currentHealth < maxHealth)
        {
            yield return new WaitForSeconds(5f);
            currentHealth += 1 * Time.deltaTime;
        }
        else
        {
            yield return null;
        }
    }
}
