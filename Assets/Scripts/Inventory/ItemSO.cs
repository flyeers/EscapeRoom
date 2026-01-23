using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Scriptable Objects/ItemSO")]
public class ItemSO : ScriptableObject
{
    public string Name;
    public string Description;
    public bool consumable;
    public Sprite ItemSprite;
}