using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Assasin : Character
{

    public override CharacterInfo setCharacterInfo()
    {
        return Player.instance.AssasinInfo;
    }


    public override void saveData(List<int> values)
    {
        Player.instance.assasinLevel = values[0];
        Player.instance.assasinXp = values[1];
        Player.instance.SavePlayer();
    }
    public override List<int> loadData()
    {
        List<int> list = new List<int>();
        list.Add(Player.instance.assasinLevel);
        list.Add(Player.instance.assasinXp);
        return list;
    }

}
