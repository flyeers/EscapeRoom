using UnityEngine;

public class Padlock : MonoBehaviour
{
    [SerializeField] private int combination;
    [Range(0, 9)]
    [SerializeField] private int[] numbers = {0, 0, 0};

   /* void OnValidate()
    {
        if (NumberOfEnemies > 5)
            NumberOfEnemies = 5; // Reset to 5 when it is tried to be set higher than 5
    }*/

    private bool CheckCombination() 
    {
        int currentCombination = int.Parse(string.Concat(numbers));
        
        Debug.Log(currentCombination);

        if (combination == currentCombination) return true;
        else return false;

    }

    public void SetNumber(int position, int newNumber) 
    {
        if (numbers.Length <= position) return; //position does not exist
        else 
        {
            numbers[position] = newNumber;
            if (CheckCombination())
            {
                Debug.Log("COMBINACIÓN ACERTADA :) ");
            }
        }
    }

    public int GetNumber(int position) 
    {
        if (numbers.Length <= position) return -1;
        return numbers[position]; 
    }
}
