using UnityEngine;



[CreateAssetMenu(fileName = "New Hero Data", menuName = "Hero Data")]
public class HeroData : ScriptableObject
{
    public Hero_type type;

    public int d_level;
    public int d_damage;
    public float d_critChance;
    public float d_critPower;
    public float d_attackRange;
    public float d_attackDelay;

    // Add more Stat fields for other hero-specific data as needed

    // Optionally, you can add methods or properties to this scriptable object to further customize its behavior.
}