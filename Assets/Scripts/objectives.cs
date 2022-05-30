using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class objectives : MonoBehaviour
{




    public Text killText;
    public Text objectiveText;
    public Gridd gridd;
    public Image endLevel;

    public Text LevelCompletetion;
    public Text optionalObjective;

    public Text char1;
    public Text char2;
    public Text char3;
    public Text char4;

    public Slider char1xp;
    public Slider char2xp;
    public Slider char3xp;
    public Slider char4xp;

    public int kills = 0;
    public int turnObjective = 10;

    public int killObjective = 4;
    public int turns = 0;
    public int xpGain = 150;

    public bool gameOver = false;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        
    }

    public virtual void levelFailed()
    {
    }

}
