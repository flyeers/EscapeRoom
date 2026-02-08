using Assets.Scripts.Interactions;
using System.Collections;
using Unity.Cinemachine;
using UnityEditor.Timeline.Actions;
using UnityEngine;
using UnityEngine.InputSystem;

public class CloseUpController : Interactor
{
    [Header("Interaction info")]
    [SerializeField] private Texture2D imageCursor;

    [Header("References")]
    [SerializeField] private CharacterController characterController;
    [SerializeField] private FirstPersonController firstPersonController;
    [SerializeField] private Camera mainCamera;
    [SerializeField] private CinemachineCamera mainCameraVirtual;
    [SerializeField] private ControllerManager controllerManager;

    private void OnEnable()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    void OnDisable()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        HandleBack();
        HandleInteraction();
    }

    //back
    private void HandleBack()
    {
        if (CheckMessageActive()) return;

        //Exit CloseUp mode
        if (playerInputHandler.BackTriggered) 
        {
            controllerManager.ChangeControllers(true, mainCameraVirtual);
            return;
        }
    }

    //interaction - base of this logic in Interactor.cs
    protected override bool CheckArea(out RaycastHit hit)
    {
        /* Vector2 mousePosition = Mouse.current.position.ReadValue();
         Ray ray = mainCamera.ScreenPointToRay(mousePosition);
         if (Physics.Raycast(ray, out hit, _interactionDistance, _obstacleLayer))
         {
             return false; //obstacle
         }
         return Physics.Raycast(ray, out hit, _interactionDistance, _interactableLayer);*/

        LayerMask mask = _interactableLayer | _obstacleLayer;
        Vector2 mousePosition = Mouse.current.position.ReadValue();
        Ray ray = mainCamera.ScreenPointToRay(mousePosition);

        return Physics.Raycast(ray, out hit, _interactionDistance, mask);

    }
    protected override void HandleUI(bool visible) 
    {

        if (visible) 
        {
            Cursor.SetCursor(imageCursor, Vector2.zero, CursorMode.Auto);
        }
        else 
        {
            Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
        }
    }

}
