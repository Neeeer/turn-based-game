using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class  Player : MonoBehaviour
{
    public int druidXp;
    public int assasinXp;
    public int paladinXp;
    public int frogXp;

    public int druidLevel;
    public int assasinLevel;
    public int paladinLevel;
    public int frogLevel;

    private List<int> equippedAbilities;


    public static Player instance { get; private set; }

    void Start()
    {
        loadPlayer();
    }

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public void SavePlayer()
    {
        SaveSystem.SavePlayer(this);
    }

    public void loadPlayer()
    {
        PlayerData data = SaveSystem.loadPlayer();
        if(data == null)
        {

        }
        else
        {
            druidXp = data.druidXp;
            assasinXp = data.assasinXp;
            paladinXp = data.paladinXp;
            frogXp = data.frogXp;


            druidLevel = data.druidLevel;
            assasinLevel = data.assasinLevel;
            paladinLevel = data.paladinLevel;
            frogLevel = data.frogLevel;
        }

    }

    public void setPlayerAbilities(List<int> loadout)
    {
        equippedAbilities = loadout;
    }

    public List<int> getPlayerAbilities()
    {
        return equippedAbilities;
    }
}
