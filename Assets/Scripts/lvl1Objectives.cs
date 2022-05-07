using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class lvl1Objectives : objectives
{
    int killObjective = 4;
    int kills = 0;
    int turnObjective = 10;
    int turns = 0;
    int xpGain = 150;
    bool gameOver = false;
    public Text killText;
    public Text objectiveText;
    public Gridd gridd;
    public Image endLevel;

    public Text LevelCompletetion;
    public Text optionalObjective;

    public Text druidLevel;
    public Text assasinLevel;
    public Text frogLevel;
    public Text paladinLevel;

    public Slider druidXP;
    public Slider assasinXP;
    public Slider frogXP;
    public Slider paladinXP;


    // Start is called before the first frame update
    protected override void Start()
    {

        foreach (Transform child in endLevel.transform)
        {
                child.gameObject.SetActive(false);
        }
        endLevel.gameObject.SetActive(false);
        

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

                if (kills == 4)
                {
                    gameOver = true;
                    endLevel.enabled = true;
                    gridd.endLevel();

                    foreach (Transform child in endLevel.transform)
                    {
                        child.gameObject.SetActive(true);
                    }
                    endLevel.gameObject.SetActive(true);

                    if (gridd.getTurn() > 10)
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

            if (Time.frameCount % 5 == 0)
            {
                druidXP.value = (float)(druidXP.value + 1.0 / 100.0);
                assasinXP.value = (float)(assasinXP.value + 1.0 / 100.0);
                frogXP.value = (float)(frogXP.value + 1.0 / 100.0);
                paladinXP.value = (float)(paladinXP.value + 1.0 / 100.0);
                xpGain--;

                if (druidXP.value >= 0.99)
                {
                    var i = Int32.Parse(druidLevel.text);
                    i++;
                    druidLevel.text = i.ToString();
                    druidXP.value = 0;
                }
                if (assasinXP.value >= 0.99)
                {
                    var i = Int32.Parse(assasinLevel.text);
                    i++;
                    assasinLevel.text = i.ToString();
                    assasinXP.value = 0;
                }
                if (frogXP.value >= 0.99)
                {
                    var i = Int32.Parse(frogLevel.text);
                    i++;
                    frogLevel.text = i.ToString();
                    frogXP.value = 0;
                }
                if (paladinXP.value >= 0.99)
                {
                    var i = Int32.Parse(paladinLevel.text);
                    i++;
                    paladinLevel.text = i.ToString();
                    paladinXP.value = 0;
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
