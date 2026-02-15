using Assets.Scripts.Interactions;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Windows;


public class Interactor : MonoBehaviour
{
    [Header("Interaction parameters")]

    [SerializeField] protected PlayerInputHandler playerInputHandler;
    [SerializeField] protected InventoryManager inventoryManager;

    [SerializeField] protected float _interactionDistance = 1.5f;
    [SerializeField] protected LayerMask _interactableLayer;
    [SerializeField] protected LayerMask _obstacleLayer;
    [SerializeField] protected float cooldown = 0.5f;

    protected bool _canInteract = true;
    protected Outline _otlineLastSeen;
    protected RaycastHit _lastHit;

    private void OnDisable()
    {
        HandleInteractionInfo(false);
    }

    protected void HandleInteraction() 
    {

        if (CheckMessageActive()) return;

        if (CheckArea(out RaycastHit hit) && ((1 << hit.collider.gameObject.layer) & _obstacleLayer) == 0) //hit + no obstacle
        {
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

    //IMPORTANTE child should implement this function  
    protected virtual bool CheckArea(out RaycastHit hit)
    {
        hit = new RaycastHit();
        return false;
    }

    public void HandleInteractionInfo(bool visible) 
    {
        if (visible) 
        {
            //UI
            HandleUI(visible);

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
            HandleUI(visible);

            //set outline
            if (_otlineLastSeen)
            {
                _otlineLastSeen.enabled = false;
                _otlineLastSeen = null;
            }
        }
    }

    //IMPORTANTE child should implement this function  
    protected virtual void HandleUI(bool visible) { }

    protected bool CheckMessageActive() 
    {
        GameObject messageUI = GameObject.FindGameObjectWithTag("MessageUI");
        if (messageUI != null)
        {
            //Block movement 
            HandleCanMove(false);

            if (_canInteract && playerInputHandler.InteractTriggered)
            {
                //Close menu if oppen
                Destroy(messageUI);
                StartCoroutine(Cooldown());
                //Unblock movement
                HandleCanMove(true);
            }
            return true;
        }
        return false;
    }

    //IMPORTANTE child should implement this function  (if needed)
    protected virtual void HandleCanMove(bool canMove) { }

    IEnumerator Cooldown()
    {
        _canInteract = false;
        yield return new WaitForSeconds(cooldown);
        _canInteract = true;
    }


    //INVENTORY 
    protected void HandleInventory() 
    {
        if (_canInteract && inventoryManager) 
        { 
            if (playerInputHandler.ActiveItemActionTriggered) 
            {
                inventoryManager.SetItemActive();
                StartCoroutine(Cooldown());
            }
            else if (playerInputHandler.PreveItemActionTriggered) 
            {
                inventoryManager.ActivateItem(true);
                StartCoroutine(Cooldown());
            }
            else if (playerInputHandler.NextItemActionTriggered) 
            {
                inventoryManager.ActivateItem(false);
                StartCoroutine(Cooldown());
            }
        }
    }

}
