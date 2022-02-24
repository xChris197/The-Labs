using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SCR_LightFlicker : MonoBehaviour
{
    [SerializeField] private GameObject roofPanel;
    [SerializeField] private Renderer rend;
    [SerializeField] private Material lightOff;
    [SerializeField] private Material lightOn;

    private bool bIsFlickering = false;
    private float delay = 3f;

    [SerializeField] private Light wallLight;

    void Start()
    {
        rend = roofPanel.GetComponent<Renderer>();
        wallLight = GetComponent<Light>();
    }

    void Update()
    {
        if(!bIsFlickering)
        {
            StartCoroutine(Flicker());
        }
    }

    IEnumerator Flicker()
    {
        bIsFlickering = true;
        wallLight.enabled = false;
        rend.material = lightOff;
        delay = Random.Range(0.01f, 0.2f);
        yield return new WaitForSeconds(delay);
        wallLight.enabled = true;
        rend.material = lightOn;
        delay = Random.Range(0.01f, 0.2f);
        yield return new WaitForSeconds(delay);
        bIsFlickering = false;
    }
}
