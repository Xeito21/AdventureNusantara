using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fall : MonoBehaviour
{
    PlayerManager playerManager;
    CameraManager cameraManager;
    [SerializeField] private CinemachineVirtualCamera vCam;

    private void Awake()
    {
        playerManager = FindObjectOfType<PlayerManager>();
        cameraManager = FindObjectOfType<CameraManager>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        playerManager.TerkenaDamage();
        cameraManager.DisableVirtualCamera();
        StartCoroutine(playerManager.HidupKembali(0.5f));
    }
}
