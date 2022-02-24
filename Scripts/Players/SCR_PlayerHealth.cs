using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SCR_PlayerHealth : MonoBehaviour
{
    public float currentHealth;
    public float maxHealth = 100;
    private SCR_PlayerMovement playerMovement;
    private SCR_GameManager manager;

    [SerializeField] private GameObject bloodVignette;
    [SerializeField] private GameObject gameManager;

    private bool bPlayerDowned = false;
    private bool bPlayerDamaged = false;

    [SerializeField] private TextMeshProUGUI healthNumText;
    void Start()
    {
        playerMovement = GetComponent<SCR_PlayerMovement>();
        manager = gameManager.GetComponent<SCR_GameManager>();
        currentHealth = maxHealth;
    }

    void Update()
    {
        healthNumText.text = currentHealth.ToString("0");

        if(currentHealth < 50)
        {
            bloodVignette.SetActive(true);
        }
        else
        {
            bloodVignette.SetActive(false);
        }

        if(bPlayerDamaged && !manager.bPlayerOneDead || !manager.bPlayerTwoDead)
        {
            StartCoroutine(RegenHealth());
            if(currentHealth == maxHealth)
            {
                StopCoroutine(RegenHealth());
            }
        }
    }

    public void TakeDamage(float damage)
    {
        bPlayerDamaged = true;
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            PlayerDowned();
        }
    }

    void PlayerDowned()
    {
        bPlayerDowned = true;
        playerMovement.enabled = false;
        StopCoroutine(RegenHealth());
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