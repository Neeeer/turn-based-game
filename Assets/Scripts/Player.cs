using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    private List<string> characterList;

    public static Player instance { get; private set; }

    void Start()
    {
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
        loadPlayer();
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
        if (SceneManager.GetActiveScene().buildIndex != 0)
        {
            List<string> charList = new List<string>();
            charList.Add("Druid");
            charList.Add( "Assasin");
            charList.Add( "Frog");
            charList.Add( "Paladin");
            setCharacterList(charList);
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

    public void setCharacterList(List<string> charList)
    {
        characterList = charList;
    }

    public List<string> getCharacterList()
    {
        return characterList;
    }
}
