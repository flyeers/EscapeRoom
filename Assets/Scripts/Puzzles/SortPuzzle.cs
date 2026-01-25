using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SortPuzzle : Puzzle
{
    [SerializeField] private List<int> piecesOrderIds = new List<int> { 1, 2, 3 };
    [SerializeField] private List<int> piecesCombinationIds = new List<int> { 1, 2, 3 };

    private int pieceSelectedId = -1;
    private GameObject pieceSelected;

    private bool CheckCombination()
    {
        if(piecesCombinationIds.SequenceEqual(piecesOrderIds)) return true;
        return false;
    }

    public void PieceSelected(int newPieceId, GameObject newPiece) 
    {

        if(!piecesOrderIds.Contains(newPieceId)) return;

        if (pieceSelectedId == -1) //not previos piece selected 
        {
            pieceSelectedId = newPieceId;
            pieceSelected = newPiece;
        }
        else 
        {
            //change index in array 
            int index1 = piecesOrderIds.IndexOf(pieceSelectedId);
            int index2 = piecesOrderIds.IndexOf(newPieceId);
            piecesOrderIds[index1] = newPieceId;
            piecesOrderIds[index2] = pieceSelectedId;

            //TODO - smooth
            //change gameObejcts positions
            Vector3 position1 = pieceSelected.transform.position;
            Vector3 position2 = newPiece.transform.position;
            pieceSelected.transform.position = position2;
            newPiece.transform.position = position1;

            if(newPiece.TryGetComponent<Outline>(out Outline otline)) otline.enabled = false;

            if (CheckCombination()) 
            {
                if (actionOnUnlocked) actionOnUnlocked.ExecuteAction();
                Debug.Log("CORECT COMBINATION");

            }

            pieceSelectedId = -1;
            pieceSelected = null;

        }
    }
}
