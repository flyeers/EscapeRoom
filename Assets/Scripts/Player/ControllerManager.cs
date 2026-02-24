using System;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.AI;

public class ControllerManager : MonoBehaviour
{
    [SerializeField] private PlayerInputHandler playerInputHandler;
    [SerializeField] private FirstPersonController firstPersoncontroller;
    [SerializeField] private CloseUpController closeUpController;
    [SerializeField] private CinemachineCamera mainCamera;

    private CinemachineCamera currentCamera;


    [SerializeField] private bool isFirstPersonController = true;

    private void Awake()
    {
        SetControllers();
        currentCamera = mainCamera;
    }

    private void SetControllers() 
    {
        playerInputHandler.SetPlayerMap(isFirstPersonController);
        firstPersoncontroller.enabled = isFirstPersonController;

        playerInputHandler.SetCloseUpMap(!isFirstPersonController);
        closeUpController.enabled = !isFirstPersonController;
    } 

    public void ChangeControllers(bool _firstPersonController, CinemachineCamera newCamera) 
    {
        isFirstPersonController = _firstPersonController;
        if (!newCamera) newCamera = mainCamera; //if not newCamera we assume player's camera

        currentCamera.enabled = false;
        newCamera.enabled = true;
        currentCamera = newCamera;

        SetControllers();
    }

    public bool GetIsFirstPersonController() 
    { 
        return isFirstPersonController;
    }
}
