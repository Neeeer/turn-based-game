using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class objectives : MonoBehaviour
{

    [SerializeField] private Text killText;
    [SerializeField] private Text objectiveText;
    [SerializeField] private LevelLogic grid;
    [SerializeField] private Image endLevel;

    [SerializeField] private Text LevelCompletetion;
    [SerializeField] private Text optionalObjective;

    [SerializeField] private Text char1;
    [SerializeField] private Text char2;
    [SerializeField] private Text char3;
    [SerializeField] private Text char4;

    [SerializeField] private Slider char1xp;
    [SerializeField] private Slider char2xp;
    [SerializeField] private Slider char3xp;
    [SerializeField] private Slider char4xp;

    [SerializeField] private int kills = 0;
    [SerializeField] private int turnObjective = 10;

    [SerializeField] private int killObjective = 4;
    [SerializeField] private int turns = 0;
    [SerializeField] private int xpGain = 150;

    [SerializeField] private bool gameOver = false;

    // Start is called before the first frame update
    protected  void Start()
    {

        foreach (Transform child in endLevel.transform)
        {
            child.gameObject.SetActive(false);
        }
        endLevel.gameObject.SetActive(false);

        List<int> list = new List<int>();
        list = grid.character1.loadData();
        char1.text = list[0].ToString();
        char1xp.value = list[1] / 100;

        list.Clear();
        list = grid.character2.loadData();
        char2.text = list[0].ToString();
        char2xp.value = list[1] / 100;

        list.Clear();
        list = grid.character3.loadData();
        char3.text = list[0].ToString();
        char3xp.value = list[1] / 100;

        list.Clear();
        list = grid.character4.loadData();
        char4.text = list[0].ToString();
        char4xp.value = list[1] / 100;

    }

    // Update is called once per frame
    protected void Update()
    {
        if (!gameOver)
        {
            kills = grid.getKills();
            killText.text = kills + "/" + killObjective;

            if (kills >= killObjective)
            {
                gameOver = true;
                endLevel.enabled = true;
                grid.endLevel();


                foreach (Transform child in endLevel.transform)
                {
                    child.gameObject.SetActive(true);
                }
                endLevel.gameObject.SetActive(true);

                if (grid.getTurn() > turnObjective)
                {
                    foreach (Transform child in optionalObjective.transform)
                    {
                        child.gameObject.SetActive(false);
                    }
                    optionalObjective.enabled = false;

                }
            }


            if (grid.getTurn() != turns)
            {
                turns = grid.getTurn();

                objectiveText.text = turns + "/" + turnObjective;
                if (turns > turnObjective)
                {
                    xpGain -= 50;
                }
            }
        }
        else
        {

            if (Time.frameCount % 5 == 0)
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

                    List<int> list = new List<int>();
                    list.Add(Int32.Parse(char1.text));
                    list.Add(Mathf.RoundToInt(char1xp.value * 100));

                    grid.character1.saveData(list);

                    list.Clear();
                    list.Add(Int32.Parse(char2.text));
                    list.Add(Mathf.RoundToInt(char2xp.value * 100));

                    grid.character2.saveData(list);

                    list.Clear();
                    list.Add(Int32.Parse(char3.text));
                    list.Add(Mathf.RoundToInt(char3xp.value * 100));

                    grid.character3.saveData(list);

                    list.Clear();

                    list.Add(Int32.Parse(char4.text));
                    list.Add(Mathf.RoundToInt(char4xp.value * 100));

                    grid.character4.saveData(list);


                }
            }
        }
    }

    public void levelFailed()
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
