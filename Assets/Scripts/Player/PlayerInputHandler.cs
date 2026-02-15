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

    [Header("Input CloseUp")]
    [SerializeField] private string actionMapNameColoseUp = "CloseUp";

    [Header("Action names")]
    [SerializeField] private string back = "Back";

    [Header("Shared action names")]
    [SerializeField] private string interact = "Interact";
    [SerializeField] private string activeItem = "ActivateItem";
    [SerializeField] private string prevItem = "PreviousItem";
    [SerializeField] private string nextItem = "NextItem";


    private InputAction movementAction;
    private InputAction rotationAction;
    private InputAction sprintAction;
    private InputAction interactAction;

    private InputAction backAction;
    private InputAction interactCloseAction;

    private InputAction activeItemAction;
    private InputAction prevItemAction;
    private InputAction nextItemAction;

    private InputAction activeItemCloseAction;
    private InputAction prevItemCloseAction;
    private InputAction nextItemCloseAction;

    public Vector2 MovementInput { get; private set; }
    public Vector2 RotationInput { get; private set; }
    public bool SprintTriggered { get; private set; }
    public bool InteractTriggered { get; private set; }


    public bool BackTriggered { get; private set; }
    public bool ActiveItemActionTriggered { get; private set; }
    public bool PreveItemActionTriggered { get; private set; }
    public bool NextItemActionTriggered { get; private set; }


    private void Awake()
    {
        InputActionMap mapReference = playerControls.FindActionMap(actionMapName);
        movementAction = mapReference.FindAction(movement);
        rotationAction = mapReference.FindAction(rotation);
        sprintAction = mapReference.FindAction(sprint);

        //CloseUp
        InputActionMap mapReferenceCloseUp = playerControls.FindActionMap(actionMapNameColoseUp);
        backAction = mapReferenceCloseUp.FindAction(back);

        //Shared
        interactAction = mapReference.FindAction(interact);
        interactCloseAction = mapReferenceCloseUp.FindAction(interact);
        activeItemAction = mapReference.FindAction(activeItem);
        activeItemCloseAction = mapReferenceCloseUp.FindAction(activeItem);
        prevItemAction = mapReference.FindAction(prevItem);
        prevItemCloseAction = mapReferenceCloseUp.FindAction(prevItem);
        nextItemAction = mapReference.FindAction(nextItem);
        nextItemCloseAction = mapReferenceCloseUp.FindAction(nextItem);


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

        interactCloseAction.performed += inputInfo => InteractTriggered = true;
        interactCloseAction.canceled += inputInfo => InteractTriggered = false;

        //Shared
        activeItemAction.performed += inputInfo => ActiveItemActionTriggered = true;
        activeItemAction.canceled += inputInfo => ActiveItemActionTriggered = false;
        activeItemCloseAction.performed += inputInfo => ActiveItemActionTriggered = true;
        activeItemCloseAction.canceled += inputInfo => ActiveItemActionTriggered = false;

        prevItemAction.performed += inputInfo => PreveItemActionTriggered = true;
        prevItemAction.canceled += inputInfo => PreveItemActionTriggered = false;
        prevItemCloseAction.performed += inputInfo => PreveItemActionTriggered = true;
        prevItemCloseAction.canceled += inputInfo => PreveItemActionTriggered = false;

        nextItemAction.performed += inputInfo => NextItemActionTriggered = true;
        nextItemAction.canceled += inputInfo => NextItemActionTriggered = false;
        nextItemCloseAction.performed += inputInfo => NextItemActionTriggered = true;
        nextItemCloseAction.canceled += inputInfo => NextItemActionTriggered = false;

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
