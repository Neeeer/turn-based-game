using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData 
{
    public int druidXp;
    public int assasinXp;
    public int paladinXp;
    public int frogXp;

    public int druidLevel;
    public int assasinLevel;
    public int paladinLevel;
    public int frogLevel;

    public PlayerData (Player player)
    {
        druidXp = player.druidXp;
        assasinXp = player.assasinXp;
        paladinXp = player.paladinXp;
        frogXp = player.frogXp;


        druidLevel = player.druidLevel;
        assasinLevel = player.assasinLevel;
        paladinLevel = player.paladinLevel;
        frogLevel = player.frogLevel;

    }
}
