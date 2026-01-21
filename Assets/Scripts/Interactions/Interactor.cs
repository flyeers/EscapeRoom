using Assets.Scripts.Interactions;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class Interactor : MonoBehaviour
{
    [SerializeField] private PlayerInputHandler playerInputHandler;
    [SerializeField] private Transform cameraTransform;


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

            if (playerInputHandler.InteractTriggered)
            {
                if (hit.transform.TryGetComponent(out IInteractable interactableObject))
                {
                    interactableObject.Interact(gameObject);
                    Debug.Log("interactableObject reached");
                }
            }
            
        }
    }
}
