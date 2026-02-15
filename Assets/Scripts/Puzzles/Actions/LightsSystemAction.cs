using UnityEngine;

public class LightsSystemAction : Action
{

    [SerializeField] private LightsSystem lightsSystem;
    [SerializeField] private PlaceItemIntercat lampPlaceItem;
    [SerializeField] private Transform bulbParent;
    [TextArea(2, 10)]
    [SerializeField] string newMessagetext = "";

    private bool lightOn = false;
    private LayerMask LayerMask;
    private string originalMessageText;

    private void Awake()
    {
        LayerMask = lampPlaceItem.gameObject.layer;
        originalMessageText = lampPlaceItem.GetMessageText();
    }

    public override void ExecuteAction(GameObject obejct) 
    {
        lightOn = !lightOn; //switch light state

        //Get bulb (if there is one)
        GameObject bulb = new GameObject();
        if (bulbParent.childCount > 0) 
        {
            bulb = bulbParent.GetChild(0).gameObject;
        }

        //Set light
        if (lightOn) //on 
        {
            lampPlaceItem.SetForceAlternativeMessage(true);
            lampPlaceItem.SetMessageText(newMessagetext);
            //lampPlaceItem.gameObject.layer = LayerMask.GetMask("Default");
            if (bulb) bulb.layer = LayerMask.GetMask("Default");
        }
        else //off
        {
            lampPlaceItem.SetForceAlternativeMessage(false);
            lampPlaceItem.SetMessageText(originalMessageText);
            //lampPlaceItem.gameObject.layer = LayerMask;
            if (bulb) bulb.layer = LayerMask;

        }

        lightsSystem.SwitchLight(lightOn);
    }
}
