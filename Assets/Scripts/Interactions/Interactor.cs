using Assets.Scripts.Interactions;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;


public class Interactor : MonoBehaviour
{
    [SerializeField] private PlayerInputHandler playerInputHandler;
    [SerializeField] private Transform cameraTransform;

    [Header("Interaction info")]
    [SerializeField] private Image image;

    [Header("Interaction parameters")]
    [SerializeField] private float _interactionRadius = 1.5f;
    [SerializeField] private LayerMask _interactableLayer;
    [SerializeField] private LayerMask _obstacleLayer;

    private Transform _transform;

    private void Awake()
    {
        _transform = cameraTransform;
    }


    void Update()
    {
        
        HandleInteraction();
       
    }

    private void HandleInteraction() 
    {

        if (Physics.Raycast(_transform.position, _transform.forward, out var hit, _interactionRadius, _interactableLayer))
        {
            Debug.DrawRay(_transform.position, _transform.forward, Color.red); 
            
            if (hit.transform.TryGetComponent(out IInteractable interactableObject))
            {
                if (image != null) image.gameObject.SetActive(true);

                if (playerInputHandler.InteractTriggered)
                {
                    interactableObject.Interact(gameObject);
                    Debug.Log("interactableObject reached");
                }
            }
        }
        else 
        { 
             if (image != null) image.gameObject.SetActive(false);
        }
    }
}
