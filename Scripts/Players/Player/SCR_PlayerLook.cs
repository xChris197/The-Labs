using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SCR_PlayerLook : MonoBehaviour
{
    [SerializeField] private float lookSensitivity = 500f;
    [SerializeField] private Transform player;

    [SerializeField] private string mouseX;
    [SerializeField] private string mouseY;

    private float xRotation = 0f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        float horizontal = Input.GetAxis(mouseX) * lookSensitivity * Time.deltaTime;
        float vertical = Input.GetAxis(mouseY) * lookSensitivity * Time.deltaTime;

        xRotation -= vertical;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        player.Rotate(Vector3.up * horizontal);
    }
}
