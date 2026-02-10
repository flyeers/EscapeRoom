using Assets.Scripts.Interactions;
using UnityEngine;

public class SortPieceInteract : MonoBehaviour, IInteractable
{
    [SerializeField] private SortPuzzle sortPuzzle;
    [SerializeField] private int positionInPuzzle;
    [SerializeField] private Piece piece = new Piece(1,1);

    private void Start()
    {
        sortPuzzle.SetPiece(piece, positionInPuzzle);
    }

    public void Interact(GameObject interactor)
    {
        sortPuzzle.PieceSelected(piece, gameObject);
    }
}
