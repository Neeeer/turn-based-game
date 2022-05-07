using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Druid  : Character
{
    int HP;
    int maxHP;
    List<Vector2Int> ability1;
    List<Vector2Int> ability2;
    List<Vector2Int> ability3;
    List<Vector2Int> ability4;
    bool dead;





    // Start is called before the first frame update
    public Druid()
    {
        HP= 90;
        maxHP = HP;
        dead = false;
        ability1 = new List<Vector2Int>();
        ability2 = new List<Vector2Int>();
        ability3 = new List<Vector2Int>();
        ability4 = new List<Vector2Int>();

        ability1.Add(new Vector2Int(0, 0));

        ability2.Add(new Vector2Int(0, 0));
        ability2.Add(new Vector2Int(1, 0));
        ability2.Add(new Vector2Int(2, 0));

        ability3.Add(new Vector2Int(0, 0));
        ability3.Add(new Vector2Int(1, 0));
        ability3.Add(new Vector2Int(1, 1));
        ability3.Add(new Vector2Int(1, -1));

    }

    public override List<Vector2Int> highlightAbility1()
    {
        return ability1;
    }

    public override int rangeAbility1()
    {
        return 1;
    }



    public override List<Vector2Int> highlightAbility2()
    {
        return ability2;
    }

    public override int rangeAbility2()
    {
        return 1;
    }



    public override List<Vector2Int> highlightAbility3()
    {
        return ability3;
    }

    public override int rangeAbility3()
    {
        return 1;
    }


    public override List<Vector2Int> highlightAbility4()
    {
        return ability4;
    }

    public override int rangeAbility4()
    {
        return 1;
    }

    public override String damageAbility1()
    {
        return "15";
    }
    public override String damageAbility2()
    {
        return "13-16";
    }
    public override String damageAbility3()
    {
        return "10-13";
    }
    public override String damageAbility4()
    {
        return "8-11";
    }


    public override bool GetisPlayer()
    {
        return true;
    }

    public override int getHealth()
    {
        return HP;
    }
    public override void death()
    {
        dead = true;
    }

    public override int getMaxHealth()
    {
        return maxHP;
    }

    public override void setHealth(int h)
    {
        HP = HP - h;
    }
}