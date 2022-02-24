using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SCR_Grenade : MonoBehaviour
{
    [SerializeField] private float blastRadius = 5f;
    [SerializeField] private float countdown = 3f;
    [SerializeField] private float blastForce = 300f;
    [SerializeField] private float blastDamage = 80f;

    [SerializeField] private GameObject explosionEffect;
    [SerializeField] private AudioSource explosionSFX;

    void Update()
    {
        StartCoroutine(Explode());
    }

    IEnumerator Explode()
    {
        yield return new WaitForSeconds(countdown);
        Instantiate(explosionEffect, transform.position, transform.rotation);
        explosionSFX.Play();

        Collider[] colliders = Physics.OverlapSphere(transform.position, blastRadius);
        foreach(Collider objects in colliders)
        {
            Rigidbody rb = objects.GetComponent<Rigidbody>();
            SCR_Target target = objects.GetComponent<SCR_Target>();
            SCR_BaseEnemy enemy = objects.GetComponent<SCR_BaseEnemy>();
            SCR_PlayerHealth player = objects.GetComponent<SCR_PlayerHealth>();
            if(rb != null)
            {
                rb.AddExplosionForce(blastForce, transform.position, blastRadius);
            }

            if(target != null)
            {
                target.TakeDamage(blastDamage);
            }
            else if(enemy != null)
            {
                enemy.TakeDamage(blastDamage);
            }
        }

        Destroy(gameObject);
    }
}
