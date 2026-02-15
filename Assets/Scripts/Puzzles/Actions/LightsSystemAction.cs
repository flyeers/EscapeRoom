using UnityEngine;

public class LightsSystemAction : Action
{

    [SerializeField] private LightsSystem lightsSystem;
    [SerializeField] private GameObject lamp;
    [SerializeField] private Transform bulbParent;


    private bool lightOn = false;
    private LayerMask LayerMask;

    private void Awake()
    {
        LayerMask = lamp.layer;
    }

    public override void ExecuteAction(GameObject obejct) 
    {
        lightOn = !lightOn; //switch light state

        GameObject bulb = new GameObject();
        if (bulbParent.childCount > 0) //is ther a bulb placed
        {
            bulb = bulbParent.GetChild(0).gameObject;
        }

        if (lightOn) //on 
        {
            lamp.layer = LayerMask.GetMask("Default");
            if(bulb) bulb.layer = LayerMask.GetMask("Default");
        }
        else //off
        {
            lamp.layer = LayerMask;
            if (bulb) bulb.layer = LayerMask;

        }

        lightsSystem.SwitchLight(lightOn);
    }
}
