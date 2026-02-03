using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] private Image[] itemsImages;

    private int itemCount = -1;

    public void AddItemUI(Sprite itemSprite)
    {
        if (itemCount + 1 >= itemsImages.Length) return; //not enough space in UI  
        itemCount++;

        itemsImages[itemCount].sprite = itemSprite;
        itemsImages[itemCount].enabled = true;
    }
    public void AddStackUI(int index)
    {
        Debug.Log("UI FOR MULTIPLE EQUAL ELEMENTS NOT IMPLEMENTED");
    }


    public void RemoveItemUI(int index)
    {
        if (index >= itemsImages.Length) return; //not in UI  
        if (itemCount == -1) return; //allready empty 

        itemsImages[itemCount].enabled = false;
        itemsImages[itemCount].sprite = null;

        if (index != itemCount) //if not last item 
        {
            for (int i = 1; i <= itemCount; i++)
            {
                if (!itemsImages[i-1].enabled) 
                {
                    itemsImages[i - 1].sprite = itemsImages[i].sprite;
                    itemsImages[i - 1].enabled = true;

                    itemsImages[i].sprite = null;
                    itemsImages[i].enabled = false;
                }
            }
        }

        itemCount--;
    }
    public void RemoveStackUI(int index)
    {
        Debug.Log("UI FOR MULTIPLE EQUAL ELEMENTS NOT IMPLEMENTED");
    }



}
