using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SCR_Pistol : MonoBehaviour
{
    private float weaponDamage = 25f;
    private float weaponRange = 100f;
    private float throwForce = 50f;

    [SerializeField] private Camera mainCam;
    [SerializeField] private ParticleSystem muzzleFlash;
    [SerializeField] private GameObject impactEffect;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform bulletSpawn;
    [SerializeField] private AudioSource gunshot;

    private bool bIsReloading = false;
    public bool bEndingChosen = false;

    private float maxAmmo = 60f;
    private float magSize = 15f;
    private float currentAmmo;
    private float reloadTime = 1.5f;

    [SerializeField] private GameObject reloadPrompt;
    [SerializeField] private float timeBetweenShots = 0.2f;

    [SerializeField] private string fireName;
    [SerializeField] private string reloadName;

    private bool bAxisInUse = false;
    private Animator animFiring;

    private float timer = 0f;
    [SerializeField] private TextMeshProUGUI currentAmmoCount;
    [SerializeField] private TextMeshProUGUI maxAmmoCount;

    void Start()
    {
        currentAmmo = magSize;
        animFiring = GetComponent<Animator>();
    }

    void Update()
    {
        timer += Time.deltaTime;

        currentAmmoCount.text = currentAmmo.ToString();
        maxAmmoCount.text = maxAmmo.ToString();

        if(Input.GetAxisRaw(fireName) > 0 && !bAxisInUse && !bIsReloading && timer > timeBetweenShots)
        {
            timer = 0;
            Fire();
            bAxisInUse = true;
        }
        else
        {
            bAxisInUse = false;
        }

        if(Input.GetButtonDown(reloadName) && !bIsReloading && currentAmmo < magSize)
        {
            StartCoroutine(Reload());
        }

        else if(currentAmmo <= 0)
        {
            StartCoroutine(Reload());
        }
    }

    void Fire()
    {
        gunshot.Play();

        animFiring.SetBool("bIsFiring", true);
        animFiring.SetBool("bIsFiring", false);

        muzzleFlash.Play();
        currentAmmo--;

        GameObject bulletGO = Instantiate(bulletPrefab, bulletSpawn.position, bulletSpawn.rotation);
        Rigidbody rb = bulletGO.GetComponent<Rigidbody>();

        if(rb != null)
        {
            rb.AddForce(Vector3.right * throwForce);
            Destroy(bulletGO, 10f);
        }

        RaycastHit hit;
        if(Physics.Raycast(mainCam.transform.position, mainCam.transform.forward, out hit, weaponRange))
        {
            SCR_BaseEnemy target = hit.transform.GetComponent<SCR_BaseEnemy>();
            SCR_PlayerHealth playerHealth = hit.transform.GetComponent<SCR_PlayerHealth>();
            if(target != null)
            {
                target.TakeDamage(weaponDamage);
            }
            else if(bEndingChosen && playerHealth != null)
            {
                playerHealth.TakeDamage(weaponDamage);
            }

            GameObject impactGO = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
            Destroy(impactGO, 10f);
        }
        
    }

    IEnumerator Reload()
    {
        bIsReloading = true;
        reloadPrompt.SetActive(true);
        yield return new WaitForSeconds(reloadTime);
        reloadPrompt.SetActive(false);
        float tempAmmo = magSize - currentAmmo;
        maxAmmo -= tempAmmo;       
        currentAmmo = magSize;
        
        bIsReloading = false;
    }
}
