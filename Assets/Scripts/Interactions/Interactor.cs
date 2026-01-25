using Assets.Scripts.Interactions;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;


public class Interactor : MonoBehaviour
{
    [SerializeField] private PlayerInputHandler playerInputHandler;
    [SerializeField] private FirstPersonController firstPersonController;
    [SerializeField] private Transform cameraTransform;

    [Header("Interaction info")]
    [SerializeField] private Image image;

    [Header("Interaction parameters")]
    [SerializeField] private float _interactionRadius = 1.5f;
    [SerializeField] private LayerMask _interactableLayer;
    //[SerializeField] private LayerMask _obstacleLayer;
    [SerializeField] private float cooldown = 0.5f;

    private bool _canInteract = true;
    private Transform _transform;
    private Outline _otlineLastSeen;
    private RaycastHit _lastHit;

    private void Awake()
    {
        _transform = cameraTransform;
    }

    void Update()
    {
        HandleInteraction();
    }

    private void OnDisable()
    {
        HandleInteractionInfo(false);
    }

    private void HandleInteraction() 
    {

        if (CheckMessageActive()) return;


        if (Physics.Raycast(_transform.position, _transform.forward, out var hit, _interactionRadius, _interactableLayer))
        {
            //Debug.DrawRay(_transform.position, _transform.forward, Color.red); 
            
            if (hit.transform.TryGetComponent(out IInteractable interactableObject))
            {
                _lastHit = hit;
                HandleInteractionInfo(true);

                if (_canInteract && playerInputHandler.InteractTriggered)
                {
                    interactableObject.Interact(gameObject);
                    StartCoroutine(Cooldown());
                    //Debug.Log("interactableObject reached");
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
            if (image != null) image.gameObject.SetActive(true);

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
            if (image != null) image.gameObject.SetActive(false);

            //set outline
            if (_otlineLastSeen)
            {
                _otlineLastSeen.enabled = false;
                _otlineLastSeen = null;
            }
        }
    }

    private bool CheckMessageActive() 
    {
        GameObject messageUI = GameObject.FindGameObjectWithTag("MessageUI");
        if (messageUI != null)
        {
            //Block movement
            firstPersonController.SetCanMove(false);

            if (_canInteract && playerInputHandler.InteractTriggered)
            {
                //Close menu if oppen
                Destroy(messageUI);
                StartCoroutine(Cooldown());
                //Unblock movement
                firstPersonController.SetCanMove(true);
            }
            return true;
        }
        return false;
    }

    IEnumerator Cooldown()
    {
        _canInteract = false;
        yield return new WaitForSeconds(cooldown);
        _canInteract = true;
    }

}
