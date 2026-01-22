using Assets.Scripts.Interactions;
using Assets.Scripts.Interactions.Clickable;
using System.Collections;
using UnityEditor.Timeline.Actions;
using UnityEngine;
using UnityEngine.InputSystem;

public class CloseUpController : MonoBehaviour
{
    [SerializeField] private CharacterController characterController;
    [SerializeField] private FirstPersonController firstPersonController;
    [SerializeField] private Camera mainCamera;
    [SerializeField] private PlayerInputHandler playerInputHandler;
    [SerializeField] private ControllerManager controllerManager;

    private Camera currentCamera;

    [Header("Interaction info")]
    [SerializeField] private Texture2D imageCursor;

    [Header("Interaction parameters")]
    [SerializeField] private float interactDistance = 5f;
    [SerializeField] private LayerMask interactableLayer;
    [SerializeField] private float cooldown = 0.5f;

    private bool _canInteract = true;
    private Outline _otlineLastSeen;
    private RaycastHit _lastHit;

    private void OnEnable()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    void OnDisable()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        HandleInteractionInfo(false);
    }

    void Update()
    {
        HandleClick();
    }

    private void HandleClick()
    {
        if (playerInputHandler.BackTriggered) //Exit CloseUp mode
        {
            controllerManager.ChangeControllers(true, mainCamera);
            return;
        }


        // Object to interact

        Vector2 mousePosition = Mouse.current.position.ReadValue();
        Ray ray = currentCamera.ScreenPointToRay(mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, interactDistance, interactableLayer))
        {
            //Debug.DrawLine(ray.origin, hit.point, Color.green);

            if (hit.collider.TryGetComponent<IClickable>(out IClickable interactable))
            {
                _lastHit = hit;
                HandleInteractionInfo(true);

                if (_canInteract && playerInputHandler.InteractCloseTriggered)
                {
                    interactable.Interact(gameObject);
                    StartCoroutine(Cooldown());
                }
            }
        }
        else 
        {
            _lastHit = hit;
            HandleInteractionInfo(false);
        }
        
    }

    public void HandleInteractionInfo(bool visible)
    {
        if (visible)
        {
            //UI
            Cursor.SetCursor(imageCursor, Vector2.zero, CursorMode.Auto);

            //set outline
            Outline _aux = _otlineLastSeen;
            _otlineLastSeen = _lastHit.transform.GetComponent<Outline>() ??
                                _lastHit.transform.GetComponentInParent<Outline>() ??
                                _lastHit.transform.GetComponentInChildren<Outline>();
            if (_otlineLastSeen)
            {
                _otlineLastSeen.enabled = true;
                if (_aux && _aux.transform.root != _otlineLastSeen.transform.root)
                {
                    _aux.enabled = false;
                }
            }
        }
        else
        {
            //UI
            Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);

            //set outline
             if (_otlineLastSeen)
             {
                 _otlineLastSeen.enabled = false;
                 _otlineLastSeen = null;
             }
        }


    }

    IEnumerator Cooldown()
    {
        _canInteract = false;
        yield return new WaitForSeconds(cooldown);
        _canInteract = true;
    }


    public void SetCurrentCamera(Camera camera) 
    {
        currentCamera = camera;
    }

}
