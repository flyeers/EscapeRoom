using Assets.Scripts.Interactions;
using UnityEngine;

public class SortPieceInteract : MonoBehaviour, IInteractable
{
    [SerializeField] private SortPuzzle sortPuzzle;
    /*[SerializeField] private int pieceId;
    [SerializeField] private int pieceType;*/
    [SerializeField] private Piece piece = new Piece(1,1);

    public void Interact(GameObject interactor)
    {
        sortPuzzle.PieceSelected(piece, gameObject);
    }
}
