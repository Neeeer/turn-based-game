using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// information reagarding a tilemap tile cell
public class Cell 
{
    private Character character;

    // Start is called before the first frame update
    public Cell()
    {
        Occupied = false;
        Passable = true;

    }

    public int zAxis { get; set; }


    public bool Passable { get; set; }


    public bool Occupied { get; set; }

    public void removeCharacter()
    {
        character = null;
        Occupied = false;
    }

    public  bool Fog { get; set; }


    public Character Character {

        get { return character; }

        set {
            character = value;
            Occupied = true;
        }
    }

}
