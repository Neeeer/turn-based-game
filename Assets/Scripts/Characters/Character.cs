using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character 
{

    int health;

    List<Vector2Int> ability1;
    List<Vector2Int> ability2;
    List<Vector2Int> ability3;
    List<Vector2Int> ability4;

    Vector2Int ability1Damage;
    Vector2Int ability2Damage;
    Vector2Int ability3Damage;
    Vector2Int ability4Damage;

    int ability1Range;
    int ability2Range;
    int ability3Range;
    int ability4Range;

    int ability1Angle;
    int ability2Angle;
    int ability3Angle;
    int ability4Angle;

    int ability1Effect;
    int ability2Effect;
    int ability3Effect;
    int ability4Effect;

    List<int> equippedAbilities;

    CharacterInfo characterInfo;

    public Character()
    {
        characterInfo = setCharacterInfo();

        equippedAbilities = new List<int>();
        equippedAbilities.Add(1);
        equippedAbilities.Add(2);
        equippedAbilities.Add(3);
        equippedAbilities.Add(4);

        MaxHeath = characterInfo.BaseMaxHeath;
        Health = MaxHeath;
        MovementRange = characterInfo.MovementRange;

        ability1 = characterInfo.Ability1;
        ability2 = characterInfo.Ability2;
        ability3 = characterInfo.Ability3;
        ability4 = characterInfo.Ability4;

        ability1Damage = characterInfo.Ability1BaseDamage;
        ability2Damage = characterInfo.Ability2BaseDamage;
        ability3Damage = characterInfo.Ability3BaseDamage;
        ability4Damage = characterInfo.Ability4BaseDamage;

        ability1Range = characterInfo.Ability1Range;
        ability2Range = characterInfo.Ability2Range;
        ability3Range = characterInfo.Ability3Range;
        ability4Range = characterInfo.Ability4Range;

        ability1Angle = characterInfo.Ability1Angle;
        ability2Angle = characterInfo.Ability2Angle;
        ability3Angle = characterInfo.Ability3Angle;
        ability4Angle = characterInfo.Ability4Angle;

        ability1Effect = characterInfo.Ability1Effect;
        ability2Effect = characterInfo.Ability2Effect;
        ability3Effect = characterInfo.Ability3Effect;
        ability4Effect = characterInfo.Ability4Effect;

    }

    public void  equipAbilities(List<int> list)
    {
        equippedAbilities = list;
    }

    public virtual CharacterInfo setCharacterInfo()
    {
        return null;
    }

    public List<Vector2Int> highlightAbility1()
    {
        return ability1;
    }

    public int rangeAbility1()
    {
        return ability1Range;
    }

    public int angleAbility1()
    {
        return ability1Angle;
    }

    public int effectAbility1()
    {
        return ability1Effect;
    }

    public List<Vector2Int> highlightAbility2()
    {
        return ability2;
    }

    public int rangeAbility2()
    {
        return ability2Range;
    }

    public int angleAbility2()
    {
        return ability2Angle;
    }

    public int effectAbility2()
    {
        return ability2Effect;
    }

    public List<Vector2Int> highlightAbility3()
    {
        return ability3;
    }

    public int rangeAbility3()
    {
        return ability3Range;
    }

    public int angleAbility3()
    {
        return ability3Angle;
    }

    public int effectAbility3()
    {
        return ability3Effect;
    }

    public List<Vector2Int> highlightAbility4()
    {
        return ability4;
    }

    public int rangeAbility4()
    {
        return ability4Range;
    }

    public int angleAbility4()
    {
        return ability4Angle;
    }

    public int effectAbility4()
    {
        return ability4Effect;
    }




    public void  useAbility(LevelLogic gridd, Character c , int i)
    {

        char[] delimiterChars = { '-', '+' };
        Vector2Int dmg;
        int effect;

        System.Random rnd = new System.Random();
        int rand = 0;
        if (i == 1)
        {
            dmg = this.damageAbility1();
            effect = this.effectAbility1();
        }
        else if (i == 2)
        {
            dmg = this.damageAbility2();
            effect = this.effectAbility2();
        }
        else if (i == 3)
        {
            dmg = this.damageAbility3();
            effect = this.effectAbility3();
        }
        else 
        {
            dmg = this.damageAbility4();
            effect = this.effectAbility4();
        }
        rand = rnd.Next(dmg[0], dmg[1]);
        gridd.damageDealt(c, rand);
        if (effect != 0)
        {
            if (effect == 1)
            {
                this.Health += rand / 2;
            }
        }

    }


    public Vector2Int damageAbility1()
    {
        return ability1Damage;
    }
    public Vector2Int  damageAbility2()
    {
        return ability2Damage;
    }
    public Vector2Int  damageAbility3()
    {
        return ability3Damage;
    }
    public Vector2Int damageAbility4()
    {
        return ability4Damage;
    }


    public bool GetisPlayer()
    {
        return characterInfo.PlayerCharacter;
    }
    public Vector3Int Location { get; set; }
    public GameObject CharGameobject { get;  set; }
    
   
    public int MovementRange { get; protected set; }

    public bool Dead { get; set; }


    public virtual void saveData(List<int> values)
    {

    }
    public virtual List<int> loadData()
    {
        return null;
    }

    public int Health
    {
        get
        {
            return health;
        }
        set
        {
            health = value;
            if (health > MaxHeath)
            {
                health = MaxHeath;
            }
        }
    }


    public int MaxHeath { get; protected set; }

}
