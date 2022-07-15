using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Druid  : Character
{
    public override CharacterInfo setCharacterInfo()
    {
        return Player.instance.DruidInfo;
    }

    



    public override void saveData(List<int> values)
    {
        Player.instance.druidLevel = values[0];
        Player.instance.druidXp = values[1];
        Player.instance.SavePlayer();
    }
    public override List<int> loadData()
    {
        List<int> list = new List<int>();
        list.Add(Player.instance.druidLevel);
        list.Add(Player.instance.druidXp);
        return list;
    }

}
