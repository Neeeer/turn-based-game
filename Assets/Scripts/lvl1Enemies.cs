using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lvl1Enemies : lvlEnemies
{

    public GameObject ene1;
    public GameObject ene2;
    public GameObject ene3;
    public GameObject ene4;

    private Character enemy1;
    private Character enemy2;
    private Character enemy3;
    private Character enemy4;

    public Gridd gridd;
    List<Character> enemiess;

   

    public override void Awake()
    {
        enemy1 = new Enemies();
        enemy2 = new Enemies();
        enemy3 = new Enemies();
        enemy4 = new Enemies();

       

        enemy1.setGameobject(ene1);
        enemy2.setGameobject(ene2);
        enemy3.setGameobject(ene3);
        enemy4.setGameobject(ene4);

        enemiess = new List<Character>();
        enemiess.Add(enemy1);
        enemiess.Add(enemy2);
        enemiess.Add(enemy3);
        enemiess.Add(enemy4);

    }

    public override List<Character> getEnemies()
    {
        return enemiess;
    }


}
