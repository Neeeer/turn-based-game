using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSetUp
{
    List<String> fabList;
    public CharacterSetUp()
    {
        fabList = new List<string>();
    }

    public List<Character> setUpCharacter(List<string> list)
    {
        List<Character> charList = new List<Character>();

        foreach (string s in list)
        {
            Character c = instantiateCharacter(s);
            charList.Add(c);
        }

        return charList;
    }

    private Character instantiateCharacter(string s)
    {
        Character c;

        if (s == "Druid")
        {
            c = new Druid();
            fabList.Add("prefabs/DruidFab");
        }
        else if (s == "Assasin")
        {
            c = new Assasin();
            fabList.Add("prefabs/AssasinFab");
        }
        else if (s == "Frog")
        {
            c = new Frog();
            fabList.Add("prefabs/FrogFab");
        }
        else if (s == "Paladin")
        {
            c = new Paladin();
            fabList.Add("prefabs/PaladinFab");
        }
        else
        {
            c = new Druid();
            fabList.Add("prefabs/DruidFab");
        }
        return c;
    }


    public List<string> getCharacterPrefabs()
    {
        
        return fabList;
    }

}
