using UnityEngine;

public class Padlock : MonoBehaviour
{
    [Header("Padlock")]
    [SerializeField] private int combination;
    [Range(0, 9)]
    [SerializeField] private int[] numbers = {0, 0, 0};

    [Header("On unlocked")]
    [SerializeField] private Action actionOnUnlocked;

    private bool locked = true;

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
            if (locked && CheckCombination())
            {
                //TODO - change when stablished
                locked = false;
                if(actionOnUnlocked != null) actionOnUnlocked.ExecuteAction();
            }
        }
    }

    public int GetNumber(int position) 
    {
        if (numbers.Length <= position) return -1;
        return numbers[position]; 
    }
}
