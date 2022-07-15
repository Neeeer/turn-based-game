using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paladin : Character 
{

   

    public override CharacterInfo setCharacterInfo()
    {
        return Player.instance.PaladinInfo;
    }





    public override void saveData(List<int> values)
    {
        Player.instance.paladinLevel = values[0];
        Player.instance.paladinXp = values[1];
        Player.instance.SavePlayer();
    }
    public override List<int> loadData()
    {
        List<int> list = new List<int>();
        list.Add(Player.instance.paladinLevel);
        list.Add(Player.instance.paladinXp);
        return list;
    }

}
