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

    List<Character> enemiess;

    List<Vector3> charLocations;
    List<Vector2> cameraBoundries;



    public override void Awake()
    {
        enemy1 = new Enemies();
        enemy2 = new Enemies();
        enemy3 = new Enemies();
        enemy4 = new Enemies();

        enemy1.CharGameobject = ene1;
        enemy2.CharGameobject = ene2;
        enemy3.CharGameobject = ene3;
        enemy4.CharGameobject = ene4;

        enemiess = new List<Character>();
        enemiess.Add(enemy1);
        enemiess.Add(enemy2);
        enemiess.Add(enemy3);
        enemiess.Add(enemy4);

        cameraBoundries = new List<Vector2>();

        cameraBoundries.Add(new Vector2(6,5));
        cameraBoundries.Add(new Vector2(-6, -5));
    }

    public override List<Character> getEnemies()
    {
        return enemiess;
    }

    public override void initializeCharacterLocations()
    {

        charLocations = new List<Vector3>();
        charLocations.Add(new Vector3(0.5f,-0.25f,0f));
        charLocations.Add(new Vector3(1.5f, -0.75f, 0f));
        charLocations.Add(new Vector3(0.5f, -1.25f, 0f));
        charLocations.Add(new Vector3(-0.5f, -1.25f, 0f));
    }

    public override List<Vector3> getCharacterStartingPositions()
    {
        return charLocations;
    }

    public override List<Vector2> getCameraBoundries()
    {
         return cameraBoundries;
    }

}
