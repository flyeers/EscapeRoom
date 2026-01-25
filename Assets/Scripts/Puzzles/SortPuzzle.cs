using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[System.Serializable]
public class IntList
{
    public List<int> values = new();
}

public class SortPuzzle : Puzzle
{
    [Header("Sort pieces")]
    [SerializeField] private List<int> piecesOrderIds = new List<int> { 1, 2, 3 };
    [SerializeField] private List<int> piecesAimOrderIds = new List<int> { 1, 2, 3 };

    [SerializeField] private List<IntList> equalIds;

    [Header("Movement (same for all peaces)")]
    [SerializeField] private Vector3 movementOnSelected = new Vector3(0, 0.1f, 0);


    private int pieceSelectedId = -1;
    private GameObject pieceSelected;

    private bool CheckCombination()
    {

        for (int i = 0; i < piecesAimOrderIds.Count; i++) 
        { 
            if(piecesAimOrderIds[i] != piecesOrderIds[i]) //diferent 
            { 
                if(equalIds.Count() == 0) return false;
                else //find if they are equals 
                { 
                    bool equalFound = false;
                    foreach(IntList list in equalIds) 
                    { 
                        if(list.values.Contains(piecesAimOrderIds[i]) && list.values.Contains(piecesOrderIds[i])) 
                        {
                            equalFound = true;
                            break;
                        }
                    }
                    if(!equalFound) return false;
                }
            }
        }

        //if(piecesAimIds.SequenceEqual(piecesOrderIds)) return true;
        return true;
    }

    public void PieceSelected(int newPieceId, GameObject newPiece) 
    {

        if(!piecesOrderIds.Contains(newPieceId)) return; //does not exist
        if(pieceSelectedId == newPieceId) //same as last selected
        {
            pieceSelected.transform.position -= movementOnSelected;
            pieceSelectedId = -1;
            pieceSelected = null;
        }
        else if (pieceSelectedId == -1) //not previos piece selected 
        {
            pieceSelectedId = newPieceId;
            pieceSelected = newPiece;
            pieceSelected.transform.position += movementOnSelected;
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
            Vector3 position1 = pieceSelected.transform.position - movementOnSelected;
            Vector3 position2 = newPiece.transform.position;
            pieceSelected.transform.position = position2;
            newPiece.transform.position = position1;

            if(newPiece.TryGetComponent<Outline>(out Outline otline)) otline.enabled = false;

            if (locked && CheckCombination()) 
            {
                locked = false;
                if (actionOnUnlocked) actionOnUnlocked.ExecuteAction();
                Debug.Log("CORECT COMBINATION");
            }

            pieceSelectedId = -1;
            pieceSelected = null;
        }
        return;
    }
}
