using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.UI.Image;

public class FirstPersonController : Interactor
{
    [SerializeField] private Transform cameraTransform;
    private Transform _transform;

    [Header("Interaction info")]
    [SerializeField] protected Image image;

    [Header("References")]
    [SerializeField] private CharacterController characterController;
    [SerializeField] public CinemachineCamera mainCamera;

    [Header("Movement")]
    [SerializeField] private float walkSpeed = 3.0f;
    [SerializeField] private float sprintMultiplier = 2.0f;

    [SerializeField] private float mouseSensitivity = 0.1f;
    [SerializeField] private float upDownLookRange = 80f;
   

    private Vector3 currentMovement;
    private float verticalRotation;
    private bool canMove = true;
    private float CurrentSpeed => walkSpeed * (playerInputHandler.SprintTriggered ? sprintMultiplier : 1);


    private void Awake()
    {
        _transform = cameraTransform;
    }

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        if (canMove) 
        { 
            HandleMovement();
            HandleRotation();
            HandleInventory();
        }
        HandleInteraction();
    }

    //movement
    private Vector3 CalculateWorldDirection()
    {

        Vector3 inputDirection = new Vector3(playerInputHandler.MovementInput.x, 0f, playerInputHandler.MovementInput.y);
        Vector3 worldDirection = transform.TransformDirection(inputDirection);

        return worldDirection.normalized;
    }

    private void HandleMovement()
    {
        Vector3 worldDirection = CalculateWorldDirection();
        currentMovement.x = worldDirection.x * CurrentSpeed;
        currentMovement.z = worldDirection.z * CurrentSpeed;

        characterController.Move(currentMovement * Time.deltaTime);
    }

    //rotation
    private void ApplyHorizontalRotation(float rotationAmount)
    {
        transform.Rotate(0, rotationAmount, 0);
    }

    private void ApplyVerticalRotation(float rotationAmount)
    {
        verticalRotation = Mathf.Clamp(verticalRotation - rotationAmount, -upDownLookRange, upDownLookRange);
        mainCamera.transform.localRotation = Quaternion.Euler(verticalRotation, 0, 0);
    }

    private void HandleRotation()
    {
        float mouseXRotation = playerInputHandler.RotationInput.x * mouseSensitivity;
        float mouseYRotation = playerInputHandler.RotationInput.y * mouseSensitivity;


        ApplyHorizontalRotation(mouseXRotation);
        ApplyVerticalRotation(mouseYRotation);
    }

    //interaction - base of this logic in Interactor.cs
    protected override bool CheckArea(out RaycastHit hit)
    {
        LayerMask mask = _interactableLayer | _obstacleLayer;
        return Physics.Raycast(
            _transform.position,
            _transform.forward,
            out hit,
            _interactionDistance,
            mask
        );
    }

    protected override void HandleUI(bool visible) 
    {
        if (image != null) image.gameObject.SetActive(visible);
    }

    protected override void HandleCanMove(bool canMove) 
    {
        this.canMove = canMove;
    }


    /*public void SetCanMove(bool canMove) 
    {
        this.canMove = canMove;
    }*/

}
