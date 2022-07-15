using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CharacterInfo", menuName = "Character/Info")]

public class CharacterInfo : ScriptableObject
{
    [field: SerializeField] public int BaseMaxHeath { get; private set; }
    [field: SerializeField] public float HealthIncreasePerLevel { get; private set; }
    [field: SerializeField] public int BaseDamage { get; private set; }
    [field: SerializeField] public int MovementRange { get; private set; }

    [field: SerializeField] public bool PlayerCharacter { get; private set; }

    public List<Vector2Int> Ability1;
    public List<Vector2Int> Ability2;
    public List<Vector2Int> Ability3;
    public List<Vector2Int> Ability4;

    [field: SerializeField] public int Ability1Range { get; private set; }
    [field: SerializeField] public int Ability2Range { get; private set; }
    [field: SerializeField] public int Ability3Range { get; private set; }
    [field: SerializeField] public int Ability4Range { get; private set; }

    [field: SerializeField] public int Ability1Angle { get; private set; }
    [field: SerializeField] public int Ability2Angle { get; private set; }
    [field: SerializeField] public int Ability3Angle { get; private set; }
    [field: SerializeField] public int Ability4Angle { get; private set; }

    public Vector2Int Ability1BaseDamage;
    public Vector2Int Ability2BaseDamage;
    public Vector2Int Ability3BaseDamage;
    public Vector2Int Ability4BaseDamage;

    public Vector2 Ability1DamageIncreasePerLevel;
    public Vector2 Ability2DamageIncreasePerLevel;
    public Vector2 Ability3DamageIncreasePerLevel;
    public Vector2 Ability4DamageIncreasePerLevel;

    public int Ability1Effect;
    public int Ability2Effect;
    public int Ability3Effect;
    public int Ability4Effect;

}
