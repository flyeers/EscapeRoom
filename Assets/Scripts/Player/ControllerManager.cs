using System;
using UnityEngine;
using UnityEngine.AI;

public class ControllerManager : MonoBehaviour
{
    [SerializeField] private PlayerInputHandler playerInputHandler;
    [SerializeField] private FirstPersonController firstPersoncontroller;
    [SerializeField] private Interactor interactor;
    [SerializeField] private CloseUpController closeUpController;
    [SerializeField] private Camera mainCamera;

    private Camera currentCamera;


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
        interactor.enabled = isFirstPersonController;

        playerInputHandler.SetCloseUpMap(!isFirstPersonController);
        closeUpController.enabled = !isFirstPersonController;

        if (!isFirstPersonController) closeUpController.SetCurrentCamera(currentCamera);
    } 

    public void ChangeControllers(bool _firstPersonController, Camera newCamera) 
    {
        isFirstPersonController = _firstPersonController;

        currentCamera.enabled = false;
        newCamera.enabled = true;
        currentCamera = newCamera;

        SetControllers();
    }

}
