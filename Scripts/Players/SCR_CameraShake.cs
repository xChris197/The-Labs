using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SCR_CameraShake : MonoBehaviour
{
    //Adapted from Renaissance Coders, 2017
    [SerializeField] private float magnitude = 0.7f;
    [SerializeField] private float duration = 1f;
    [SerializeField] private float slowDownAmount = 1f;

    [SerializeField] private Transform cam;

    public bool shouldShake = true;
    private Vector3 currentPos;
    private float initalDuration;

    void Start()
    {
        cam = GetComponent<Transform>();
        currentPos = transform.localPosition;
        initalDuration = duration;
    }

    void Update()
    {
        if(shouldShake)
        {
            if(duration > 0)
            {
                cam.localPosition = currentPos + Random.insideUnitSphere * magnitude;
                duration -= Time.deltaTime * slowDownAmount;
            }
            else
            {
                shouldShake = false;
                duration = initalDuration;
                cam.localPosition = currentPos;
            }
        }
    }
    //End of adapted code
}
