using Assets.Scripts.Interactions;
using UnityEngine;

public class MoveInteract : MonoBehaviour, IInteractable
{
    [SerializeField] private Vector3 _movementOffset = Vector3.zero;

    private bool _isClosed = true;
    private Vector3 _initialPosition;

    private void Awake()
    {
        _initialPosition = transform.position;
    }

    public void Interact(GameObject interactor)
    {
        if (_isClosed)
        {
            transform.position = _initialPosition + _movementOffset;
            _isClosed = false;
        }
        else
        {
            transform.position = _initialPosition;
            _isClosed = true;
        }
    }
}
