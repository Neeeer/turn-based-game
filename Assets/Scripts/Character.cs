using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character 
{
    Vector3Int location;
    GameObject gameO;
    int  mRange = 4;
    bool dead = false;
    int health;

    // Start is called before the first frame update


    public Character()
    {
    }

    public virtual List<Vector2Int> highlightAbility1()
    {
        return null;
    }

    public virtual int rangeAbility1()
    {
        return 0;
    }

    public virtual int angleAbility1()
    {
        return 0;
    }


    public virtual List<Vector2Int> highlightAbility2()
    {
        return null;
    }

    public virtual int rangeAbility2()
    {
        return 0;
    }

    public virtual int angleAbility2()
    {
        return 0;
    }



    public virtual List<Vector2Int> highlightAbility3()
    {
        return null;
    }

    public virtual int rangeAbility3()
    {
        return 0;
    }

    public virtual int angleAbility3()
    {
        return 0;
    }

    public virtual int useAbility3()
    {
        return 0;
    }

    public virtual List<Vector2Int> highlightAbility4()
    {
        return null;
    }

    public virtual int rangeAbility4()
    {
        return 0;
    }

    public virtual int angleAbility4()
    {
        return 0;
    }

    public virtual int useAbility(Gridd gridd, Character c , int i)
    {
        char[] delimiterChars = { '-', '+' };

        System.Random rnd = new System.Random();
        Debug.Log(i);
        int rand = 0;
        if (i == 1)
        {
            string[] range = this.damageAbility1().Split(delimiterChars);
            rand = rnd.Next(Int32.Parse(range[0]), Int32.Parse(range[range.Length - 1]));
        }
        else if (i == 2)
        {
            string[] range = this.damageAbility2().Split(delimiterChars);
            rand = rnd.Next(Int32.Parse(range[0]), Int32.Parse(range[range.Length - 1]));

        }
        else if (i == 3)
        {
            string[] range = this.damageAbility3().Split(delimiterChars);
            if (range.Length == 3)
            {
                rand = - rnd.Next(Int32.Parse(range[1]), Int32.Parse(range[range.Length - 1]));
            }
            else
            {
                rand = rnd.Next(Int32.Parse(range[0]), Int32.Parse(range[range.Length - 1]));
            }
        }
        else if (i == 4)
        {
            string[] range = this.damageAbility4().Split(delimiterChars);
            rand = rnd.Next(Int32.Parse(range[0]), Int32.Parse(range[range.Length - 1]));

        }

        gridd.damageDealt(c, rand);

        return rand;
    }


public virtual String damageAbility1()
    {
        return "0";
    }
    public virtual String damageAbility2()
    {
        return "0";
    }
    public virtual String damageAbility3()
    {
        return "0";
    }
    public virtual String damageAbility4()
    {
        return "0";
    }


    public virtual bool GetisPlayer()
    {
        return false;
    }
    public virtual Vector3Int getLocation()
    {
        return location;
    }
    public virtual void setLocation(Vector3Int loc)
    {
        location = loc;
    }
    public virtual void setGameobject(GameObject i)
    {
        gameO = i;
    }
    public virtual GameObject getGameobject()
    {
        return gameO;
    }
    public virtual int getMovementRange()
    {
        return mRange;
    }
   

    public virtual void death()
    {
        dead = true;
    }


    public virtual void saveData(List<int> values)
    {

    }
    public virtual List<int> loadData()
    {
        return null;
    }

    public virtual int Health
    {
        get
        {
            return health;
        }
        set
        {
            health = value;
            if (health > maxHeath)
            {
                health = maxHeath;
            }
        }
    }


    public virtual int maxHeath { get; protected set; }

}
