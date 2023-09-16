using UnityEngine;
using static WaveManager;

public enum EnemyType
{
    Normal,
    Epic,
    Boss
}

[CreateAssetMenu(fileName = "SpawnData", menuName = "Data/Spawn")]
public class SpawnData : ScriptableObject
{
    public int world;
    public int stage;
    public GameObject[] enemyprefab;

    public SpawnType[] spawntype;
    public int[] enemycount;

    public float[] enemyspawnrate;

    public int[] MaxEnemy;

   
    
}
/*
[CreateAssetMenu(fileName = "Item", menuName = "Scriptable Object/ItemData")]
public class ItemData : ScriptableObject
{
   
    public enum ItemType { Melee, Range, Glove, Shoe, Heal}

    [Header("# Main Info")]
    public ItemType itemType;
    public int itemId;
    public string itemName;

    public int num;
    [TextArea]
    public string itemDesc;
    public Sprite itemIcon;

    [Header("# Level Data")]
    public float baseDamage;
    public int baseCount;
    public float[] damages;
    public int[] counts;

    [Header("# Weapon")] 
    public GameObject projectile;
    public Sprite hand;
}
*/