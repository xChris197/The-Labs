using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SCR_GrenadeThrower : MonoBehaviour
{
    [SerializeField] private GameObject grenade;
    [SerializeField] private float throwForce = 10f;

    [SerializeField] private int grenadeCount;
    [SerializeField] private int maxAmmo = 4;
    [SerializeField] private TextMeshProUGUI currentGrenadeCount;

    [SerializeField] private string throwName;
    void Start()
    {
        grenadeCount = maxAmmo;
    }
    void Update()
    {
        currentGrenadeCount.text = grenadeCount.ToString();

        if(Input.GetButtonDown(throwName) && grenadeCount > 0)
        {
            grenadeCount--;
            GameObject grenadeGO = Instantiate(grenade, transform.position, transform.rotation);
            Rigidbody rb = grenadeGO.GetComponentInChildren<Rigidbody>();
            rb.AddForce(transform.forward * throwForce, ForceMode.VelocityChange);
        }
    }
}
