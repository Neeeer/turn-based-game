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

    public CharacterInfo AssasinInfo;
    public CharacterInfo DruidInfo;
    public CharacterInfo FrogInfo;
    public CharacterInfo PaladinInfo;
    public CharacterInfo Enemy1Info;

    private List<int> equippedAbilities;

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
            CharacterList = (charList);
        }
    }

    public int PlayerAbilities { get; set; }
    public List<string> CharacterList { get;  set; }
    
}
