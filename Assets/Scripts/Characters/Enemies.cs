using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemies : Character
{

    public override CharacterInfo setCharacterInfo()
    {
        return Player.instance.Enemy1Info;
    }



}
