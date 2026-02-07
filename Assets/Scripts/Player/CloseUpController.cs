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


   /* [Header("Interaction parameters")]
    [SerializeField] private float interactDistance = 5f;
    [SerializeField] private LayerMask interactableLayer;
    [SerializeField] private float cooldown = 0.5f;

    private bool _canInteract = true;
    private Outline _otlineLastSeen;
    private RaycastHit _lastHit;*/

    private void OnEnable()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    void OnDisable()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

       // HandleInteractionInfo(false);
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
        Vector2 mousePosition = Mouse.current.position.ReadValue();
        Ray ray = mainCamera.ScreenPointToRay(mousePosition);
        if (Physics.Raycast(ray, out hit, _interactionDistance, _interactableLayer))
            return true;
        return false;
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
