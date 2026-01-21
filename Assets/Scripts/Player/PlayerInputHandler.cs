using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{    
    [SerializeField] private InputActionAsset playerControls;

    [Header("Input FirstPerson")]
    [SerializeField] private string actionMapName = "Player";

    [Header("Action names")]
    [SerializeField] private string movement = "Movement";
    [SerializeField] private string rotation = "Rotation";
    [SerializeField] private string sprint = "Sprint";
    [SerializeField] private string interact = "Interact";

    [Header("Input CloseUp")]
    [SerializeField] private string actionMapNameColoseUp = "CloseUp";

    [Header("Action names")]
    [SerializeField] private string back = "Back";
    [SerializeField] private string interactClose = "Interact";

    private InputAction movementAction;
    private InputAction rotationAction;
    private InputAction sprintAction;
    private InputAction interactAction;

    private InputAction backAction;
    private InputAction interactCloseAction;

    public Vector2 MovementInput { get; private set; }
    public Vector2 RotationInput { get; private set; }
    public bool SprintTriggered { get; private set; }
    public bool InteractTriggered { get; private set; }


    public bool BackTriggered { get; private set; }
    public bool InteractCloseTriggered { get; private set; }


    private void Awake()
    {
        InputActionMap mapReference = playerControls.FindActionMap(actionMapName);
        movementAction = mapReference.FindAction(movement);
        rotationAction = mapReference.FindAction(rotation);
        sprintAction = mapReference.FindAction(sprint);
        interactAction = mapReference.FindAction(interact);

        //CloseUp
        InputActionMap mapReferenceCloseUp = playerControls.FindActionMap(actionMapNameColoseUp);
        backAction = mapReferenceCloseUp.FindAction(back);
        interactCloseAction = mapReferenceCloseUp.FindAction(interact);

        SubscribeActionValuesToInputEvents();
    }

    private void SubscribeActionValuesToInputEvents() 
    {
        movementAction.performed += inputInfo => MovementInput = inputInfo.ReadValue<Vector2>();
        movementAction.canceled += inputInfo => MovementInput = Vector2.zero;

        rotationAction.performed += inputInfo => RotationInput = inputInfo.ReadValue<Vector2>();
        rotationAction.canceled += inputInfo => RotationInput = Vector2.zero;

        sprintAction.performed += inputInfo => SprintTriggered = true;
        sprintAction.canceled += inputInfo => SprintTriggered = false;

        interactAction.performed += inputInfo => InteractTriggered = true;
        interactAction.canceled += inputInfo => InteractTriggered = false;


        //CloseUp
        backAction.performed += inputInfo => BackTriggered = true;
        backAction.canceled += inputInfo => BackTriggered = false;

        interactCloseAction.performed += inputInfo => InteractCloseTriggered = true;
        interactCloseAction.canceled += inputInfo => InteractCloseTriggered = false;
    }


    public void SetPlayerMap(bool enable) 
    { 
        if(enable) playerControls.FindActionMap(actionMapName).Enable();
        else playerControls.FindActionMap(actionMapName).Disable();

    }

    public void SetCloseUpMap(bool enable)
    {
        if (enable) playerControls.FindActionMap(actionMapNameColoseUp).Enable();
        else playerControls.FindActionMap(actionMapNameColoseUp).Disable();

    }

    private void OnEnable()
    {
        playerControls.FindActionMap(actionMapName).Enable();
    }

    private void OnDisable()
    {
        playerControls.FindActionMap(actionMapName).Disable();
        playerControls.FindActionMap(actionMapNameColoseUp).Disable();
    }

}
