using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell 
{
    private bool passable;
    private bool occupied;
    private int zaxis;
    private Character character;
    private bool fog;

    // Start is called before the first frame update
    public Cell()
    {
        occupied = false;
        passable = true;

    }

    public void setPassable(bool t)
    {
        passable = t;
    }

    public void setzAxis(int z)
    {
        zaxis = z;
    }

    public int getzAxis()
    {
        return zaxis;
    }

    public bool getPassable()
    {
        return passable;
    }

    public bool getOcupied()
    {
        return occupied;
    }

    public void setOccupied(bool t)
    {
       occupied = t;
    }

    public void removeCharacter()
    {
        character = null;
        occupied = false;

    }
    public void setCharacter(Character chara)
    {
        character = chara;
        occupied = true;
    }
    public void setFog(bool f)
    {
        fog = f;
    }

    
    public bool getFog()
    {
         return fog;
    }

    public Character getCharacter()
    {
        return  character;
    }



}
