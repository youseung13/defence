using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//make a skill class that manage each hero's skills

public enum SkillType
{
    Normal,
    Active,
    Passive
}

[CreateAssetMenu(fileName = "New Skill", menuName = "Skill")]
public class Skill : ScriptableObject
{
    public string skillName;
    [TextArea(3, 5)]
    public string skillDescription;
    public Sprite skillIcon;
    public SkillType skillType;
    public bool skillUnlocked;
    public int skillLevel;
    public int skillMaxLevel;
    public int numArrows;
    public float baseDamage;
    public float skillRange;
    public float skillCooldown;
    public float skillDuration;
    public Vector3 targetPosition;
    public GameObject projectilePrefab;
    public GameObject addiotnalEffectPrefab;
    
   
}

