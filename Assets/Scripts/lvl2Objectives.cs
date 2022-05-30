using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class lvl2Objectives : objectives
{


    // Start is called before the first frame update
    protected override void Start()
    {
        foreach (Transform child in endLevel.transform)
        {
            child.gameObject.SetActive(false);
        }
        endLevel.gameObject.SetActive(false);

        char4xp.value = 1 / 2;
    }

    // Update is called once per frame
    protected override void Update()
    {


        if (!gameOver)
        {

            if (gridd.getKills() != kills)
            {
                kills = gridd.getKills();
                killText.text = kills + "/" + killObjective;

                if (kills == killObjective)
                {
                    gameOver = true;
                    endLevel.enabled = true;
                    gridd.endLevel();

                    foreach (Transform child in endLevel.transform)
                    {
                        child.gameObject.SetActive(true);
                    }
                    endLevel.gameObject.SetActive(true);

                    if (gridd.getTurn() > turnObjective)
                    {
                        foreach (Transform child in optionalObjective.transform)
                        {
                            child.gameObject.SetActive(false);
                        }
                        optionalObjective.enabled = false;
                        xpGain = 100;
                    }
                    else
                    {
                        xpGain = 150;
                    }
                }

            }

            if (gridd.getTurn() != turns)
            {
                turns = gridd.getTurn();
                objectiveText.text = turns + "/" + turnObjective;
            }
        }
        else
        {

            if (Time.frameCount % 3 == 0)
            {
                char1xp.value = (float)(char1xp.value + 1.0 / 100.0);
                char2xp.value = (float)(char2xp.value + 1.0 / 100.0);
                char3xp.value = (float)(char3xp.value + 1.0 / 100.0);
                char4xp.value = (float)(char4xp.value + 1.0 / 100.0);
                xpGain--;

                if (char1xp.value >= 0.99)
                {
                    var i = Int32.Parse(char1.text);
                    i++;
                    char1.text = i.ToString();
                    char1xp.value = 0;
                }
                if (char2xp.value >= 0.99)
                {
                    var i = Int32.Parse(char2.text);
                    i++;
                    char2.text = i.ToString();
                    char2xp.value = 0;
                }
                if (char3xp.value >= 0.99)
                {
                    var i = Int32.Parse(char3.text);
                    i++;
                    char3.text = i.ToString();
                    char3xp.value = 0;
                }
                if (char4xp.value >= 0.99)
                {
                    var i = Int32.Parse(char4.text);
                    i++;
                    char4.text = i.ToString();
                    char4xp.value = 0;
                }

                if (xpGain == 0)
                {
                    enabled = false;
                }
            }
        }
    }

    public override void levelFailed()
    {
        foreach (Transform child in endLevel.transform)
        {
            child.gameObject.SetActive(true);
        }
        endLevel.gameObject.SetActive(true);

        foreach (Transform child in LevelCompletetion.transform)
        {
            child.gameObject.SetActive(false);
        }

        LevelCompletetion.text = "Level Failed";
    }

}
