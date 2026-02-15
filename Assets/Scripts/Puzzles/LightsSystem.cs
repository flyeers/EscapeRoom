using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LightBulbInfo
{
    public ItemSO itemSO;
    public string tagName;
    public Color lihtColor;
}

public class LightsSystem : MonoBehaviour
{
    [SerializeField] private Light lightbulb;
    [SerializeField] private Transform bulbParent;
    [SerializeField] private List<LightBulbInfo> Lightbulbs= new List<LightBulbInfo>();
   
    private Dictionary<ItemSO, GameObject[]> obejctToAppearDictionary = new Dictionary<ItemSO, GameObject[]>();
    private Dictionary<ItemSO, LightBulbInfo> lightbulbInfoDictionary = new Dictionary<ItemSO, LightBulbInfo>();

    private ItemSO currentItemSO;

    void Awake()
    {
        foreach (LightBulbInfo l in Lightbulbs)
        {
            lightbulbInfoDictionary.Add(l.itemSO, l);

            //GameObject[] objs = GameObject.FindGameObjectsWithTag(l.tagName);
            //obejctToAppearDictionary.Add(l.itemSO, objs);
        }
    }


    public void SwitchLight(bool on) 
    {

        if (!on) //off
        {
            SetLight(currentItemSO, false);
        }
        else//on
        {

            if (bulbParent.childCount > 0) //is ther a bulb placed
            {
                if (bulbParent.GetChild(0).gameObject.TryGetComponent<PickUpInteract>(out PickUpInteract pickUp))
                {
                    currentItemSO = pickUp.itemSO;
                    SetLight(currentItemSO, true);
                }
            }
        }   
    }

    public void SetLight(ItemSO itemSO, bool on)
    {
        if (!itemSO) return; //no light bolb
        
        //light
        if(on) lightbulb.color = lightbulbInfoDictionary[itemSO].lihtColor;
        lightbulb.enabled = on;

        //object to appear 
        if (obejctToAppearDictionary.Count == 0 || !obejctToAppearDictionary.ContainsKey(itemSO)) return;
        foreach (GameObject obj in obejctToAppearDictionary[itemSO])
        {
            var rend = obj.GetComponentInChildren<Renderer>();
            if (rend)
                rend.enabled = on;
        }

    }

}
