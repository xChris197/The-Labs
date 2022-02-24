using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SCR_PlayerCasting : MonoBehaviour
{
    public static float distanceFromTarget;
    [SerializeField] private float toTarget;

    public static GameObject hitTarget;

    void Update()
    {
        RaycastHit hit;
        if(Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit))
        {
            toTarget = hit.distance;
            distanceFromTarget = toTarget;
            hitTarget = hit.transform.gameObject;
        }
    }
}
