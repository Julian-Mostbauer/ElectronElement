﻿using UnityEngine;

public class PlayerData : MonoBehaviour
{
    public string Name;

    public Camera cam;

    public FirstPersonMovement Movement;
    public MouseLook Look;

    public void Activate()
    {
        Movement.enabled = true;
        Look.enabled = true;
        cam.gameObject.SetActive(true);
    }
    public void Deactivate()
    {
        Movement.enabled = false;
        Look.enabled = false;
        cam.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1)) CameraManager.Instance.allCams[0].TogglePosession(this);
    }
}