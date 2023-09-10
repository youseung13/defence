using UnityEngine;

[CreateAssetMenu(fileName = "New SkillData", menuName = "Skill System/Skill Data")]
public class SkillData : ScriptableObject
{
    public int skillLevel;
    public float baseDamage;
    public float damageMultiplier;
}