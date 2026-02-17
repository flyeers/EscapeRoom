using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Scriptable Objects/ItemSO")]
public class ItemSO : ScriptableObject
{
    public string Name;
    public bool consumable;
    public bool showMessage;
    public string message;
    public Sprite ItemSprite;
    public GameObject ItemPrefab;
}