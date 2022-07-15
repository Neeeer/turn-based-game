using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Frog : Character
{

    public override CharacterInfo setCharacterInfo()
    {
        return Player.instance.FrogInfo;
    }


   




    public override void saveData(List<int> values)
    {
        Player.instance.frogLevel = values[0];
        Player.instance.frogXp = values[1];
        Player.instance.SavePlayer();
    }
    public override List<int> loadData()
    {
        List<int> list = new List<int>();
        list.Add(Player.instance.frogLevel);
        list.Add(Player.instance.frogXp);
        return list;
    }
}
