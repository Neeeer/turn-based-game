using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemies : Character
{

    int HP;
    int mRange;
    int maxHP;
    bool dead;
    List<Vector2Int> ability1;
    List<Vector2Int> ability2;
    Vector3Int location;

    public Enemies()
    {
        HP = 100;
        maxHP = HP;
        mRange = 4;
        dead = false;
        ability1 = new List<Vector2Int>();
        ability2 = new List<Vector2Int>();

        ability1.Add(new Vector2Int(0, 0));

        ability2.Add(new Vector2Int(0, 0));
        ability2.Add(new Vector2Int(1, 1));
        ability2.Add(new Vector2Int(1, -1));

    }


    public override Vector3Int getLocation()
    {
        return location;
    }
    public override void setLocation(Vector3Int loc)
    {
        location = loc;
    }

    public override bool GetisPlayer()
    {
        return false;
    }

    public override int rangeAbility1()
    {
        return 1;
    }

    

    public override int rangeAbility2()
    {
        return 1;
    }

   



    public override int useAbility(Gridd gridd, Character c, int i)
    {
        System.Random rnd = new System.Random();
        int rand = 0;
        if (i == 1)
        {
             rand = rnd.Next(15, 20);
        }
        else if (i == 2)
        {
            rand = rnd.Next(12, 15);
        }
        gridd.damageDealt(c, rand);

        return rand;

    }

    public override int getHealth()
    {
        return HP;
    }

    public override int getMovementRange()
    {
        return mRange;
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