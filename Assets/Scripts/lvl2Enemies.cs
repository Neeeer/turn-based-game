using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lvl2Enemies : lvlEnemies
{

    public GameObject ene1;
    public GameObject ene2;
    public GameObject ene3;
    public GameObject ene4;
    public GameObject ene5;
    public GameObject ene6;

    private Character enemy1;
    private Character enemy2;
    private Character enemy3;
    private Character enemy4;
    private Character enemy5;
    private Character enemy6;

    List<Character> enemiess;

   
    public override void Awake()
    {
        enemy1 = new Enemies();
        enemy2 = new Enemies();
        enemy3 = new Enemies();
        enemy4 = new Enemies();
        enemy5 = new Enemies();
        enemy6 = new Enemies();

        enemy1.setGameobject(ene1);
        enemy2.setGameobject(ene2);
        enemy3.setGameobject(ene3);
        enemy4.setGameobject(ene4);
        enemy5.setGameobject(ene5);
        enemy6.setGameobject(ene6);

        enemiess = new List<Character>();
        enemiess.Add(enemy1);
        enemiess.Add(enemy2);
        enemiess.Add(enemy3);
        enemiess.Add(enemy4);
        enemiess.Add(enemy5);
        enemiess.Add(enemy6);

    }

    public override List<Character> getEnemies()
    {
        return enemiess;
    }
}
