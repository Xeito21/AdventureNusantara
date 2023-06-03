using UnityEngine;
using Cinemachine;

public class CameraManager : MonoBehaviour
{
    [SerializeField] public CinemachineVirtualCamera Vcam;
    private CinemachineBrain cinemachineBrain;

    private void Awake()
    {
        cinemachineBrain = FindObjectOfType<CinemachineBrain>();
    }

    public void DisableVirtualCamera()
    {
        cinemachineBrain.enabled = false;
        Vcam.gameObject.SetActive(false);
    }

    public void EnableVirtualCamera()
    {
        Vcam.gameObject.SetActive(true);
        cinemachineBrain.enabled = true;
    }
}
