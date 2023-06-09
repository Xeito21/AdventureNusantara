using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fall : MonoBehaviour
{
    [Header("Virtual Camera")]
    [SerializeField] private CinemachineVirtualCamera vCam;

    [Header("References")]
    PlayerManager playerManager;
    CameraManager cameraManager;

    private void Awake()
    {
        playerManager = FindObjectOfType<PlayerManager>();
        cameraManager = FindObjectOfType<CameraManager>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        playerManager.DamageJatuh();
        cameraManager.DisableVirtualCamera();
        StartCoroutine(playerManager.HidupKembali(0.5f));
    }
}
