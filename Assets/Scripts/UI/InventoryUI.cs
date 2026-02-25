using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] private Image[] itemsImages;
    [SerializeField] private Image[] backgroundImages;
    [SerializeField] private Image backgroundPhone;
    [SerializeField] private Color selectedColor;
    [SerializeField] private Color notSelectedColor;
    [SerializeField] private Image messageImage;
    [SerializeField] private Color notifyColor;


    private int itemCount = -1;

    public void AddItemUI(Sprite itemSprite)
    {
        if (itemCount + 1 >= itemsImages.Length) return; //not enough space in UI  
        itemCount++;

        itemsImages[itemCount].sprite = itemSprite;
        itemsImages[itemCount].enabled = true;
    }
    public void RemoveItemUI(int index)
    {
        if (index >= itemsImages.Length) return; //not in UI  
        if (itemCount == -1) return; //allready empty 

        itemsImages[index].enabled = false;
        itemsImages[index].sprite = null;

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

    public void SetBackgroudSelected(int index, bool selected) 
    {
        if (index == -2) 
        { 
            SetBackgroudPnone(selected); 
            return;        
        }
        if (index >= backgroundImages.Length) return;
        
        //Background active / not active item
        backgroundImages[index].color = selected? selectedColor : notSelectedColor;
    }

    public void SetMessage(string messageText, bool show) 
    {
        if (!messageImage) return;

        if (!show || string.IsNullOrWhiteSpace(messageText))
        {
            messageImage.gameObject.SetActive(false);
        }
        else
        {
            messageImage.gameObject.SetActive(true);
            TextMeshProUGUI tmpText = messageImage.GetComponentInChildren<TextMeshProUGUI>();
            if (tmpText != null)
                tmpText.text = messageText;
        }
    }

    public void SetBackgroundNotify(int index) 
    {
        backgroundImages[index].color = notifyColor;
    }

    //PHONE
    public void SetBackgroudPnone(bool selected)
    {
        //Background active / not active item
        backgroundPhone.color = selected ? selectedColor : notSelectedColor;
    }
    public void SetBackgroundNotifyPhone()
    {
        backgroundPhone.color = notifyColor;
    }

}
