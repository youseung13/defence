using UnityEngine;

[CreateAssetMenu(fileName = "New Item Data", menuName = "Inventory/Item Data")]
public class ItemData : ScriptableObject
{
    public enum ItemType { Weapon, Armor, Consumable } // Example item types
    public ItemType itemType;
    public Sprite itemSprite;
    public int someStat;
    public string textDescription;
}