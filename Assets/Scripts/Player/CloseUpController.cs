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
    [SerializeField] private ControllerManager controllerManager;

    private void OnEnable()
    {
        _enabled = true;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    void OnDisable()
    {
        _enabled = false;
        HandleInteractionInfo(false);
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
            controllerManager.ChangeControllers(true, null);
            return;
        }

        HandleInventory();
    }

    //interaction - base of this logic in Interactor.cs
    protected override bool CheckArea(out RaycastHit hit)
    {
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
