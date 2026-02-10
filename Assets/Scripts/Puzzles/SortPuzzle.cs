using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using UnityEngine;
using UnityEngine.UIElements;

[System.Serializable]
public struct Piece 
{
    public int id;
    public int type;

    public Piece(int newId, int newType) 
    {
        id = newId;
        type = newType;
    }
}
public class SortPuzzle : Puzzle
{
    [Header("Sort pieces")]
    private List<Piece> piecesOrder;
    [SerializeField] private List<int> piecesAimOrderTypes = new List<int> { 1, 2, 3 }; //TYPES


    [Header("Movement (same for all peaces)")]
    [SerializeField] private Vector3 movementOnSelected = new Vector3(0, 0.1f, 0);

    private Piece pieceSelected = new Piece(-1, -1);
    private GameObject pieceSelectedObject;

    private void Awake()
    {
        int length = piecesAimOrderTypes.Count;
        piecesOrder = Enumerable.Repeat(new Piece(0, 0), length).ToList();
    }

    public void SetPiece(Piece piece,  int position) 
    {
        if (position >= piecesOrder.Count) return;
        piecesOrder[position] = piece;
    }

    private bool CheckCombination()
    {

        for (int i = 0; i < piecesAimOrderTypes.Count; i++) 
        { 
            if(piecesAimOrderTypes[i] != piecesOrder[i].type) 
            {
                return false;
            }
        }

        //if(piecesAimIds.SequenceEqual(piecesOrderIds)) return true;
        return true;
    }

    public void PieceSelected(Piece newPiece, GameObject newPieceObject) 
    {

        if(!piecesOrder.Contains(newPiece)) return; //does not exist
        if(pieceSelected.id == newPiece.id) //same as last selected
        {
            pieceSelectedObject.transform.position -= movementOnSelected;
            pieceSelected.id = -1;
            pieceSelectedObject = null;
        }
        else if (pieceSelected.id == -1) //not previos piece selected 
        {
            pieceSelected = newPiece;
            pieceSelectedObject = newPieceObject;
            pieceSelectedObject.transform.position += movementOnSelected;
        }
        else 
        {
            //change index in array 
            int index1 = piecesOrder.IndexOf(pieceSelected);
            int index2 = piecesOrder.IndexOf(newPiece);
            piecesOrder[index1] = newPiece;
            piecesOrder[index2] = pieceSelected;

            //TODO - smooth
            //change gameObejcts positions
            Vector3 position1 = pieceSelectedObject.transform.position - movementOnSelected;
            Vector3 position2 = newPieceObject.transform.position;
            pieceSelectedObject.transform.position = position2;
            newPieceObject.transform.position = position1;

            if(newPieceObject.TryGetComponent<Outline>(out Outline otline)) otline.enabled = false;

            if (locked && CheckCombination()) 
            {
                locked = false;
                if (actionOnUnlocked) actionOnUnlocked.ExecuteAction(null);
                Debug.Log("CORECT COMBINATION");
            }

            pieceSelected.id= -1;
            pieceSelectedObject = null;
        }
        return;
    }
}
