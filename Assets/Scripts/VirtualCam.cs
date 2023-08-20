using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class VirtualCam : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera fPSCam;
    [SerializeField] private CinemachineVirtualCamera lookAtTargetCam;
    public PlayerCamera playerCamera;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void LookAtTarget()
    {
        playerCamera.allowCamToMove = false;
        GetComponent<PlayerMovement>().canPlayerMove = false;
        lookAtTargetCam.Priority = 11;
    }
    public void ResumeAction()
    {
        lookAtTargetCam.Priority = 9;
        playerCamera.allowCamToMove = true;
        GetComponent<PlayerMovement>().canPlayerMove = true;
    }
}
