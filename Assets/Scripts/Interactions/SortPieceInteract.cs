using Assets.Scripts.Interactions;
using UnityEngine;

public class SortPieceInteract : MonoBehaviour, IInteractable
{
    [SerializeField] private SortPuzzle sortPuzzle;
    [SerializeField] private int pieceId;

    public void Interact(GameObject interactor)
    {
        sortPuzzle.PieceSelected(pieceId, gameObject);
    }
}
