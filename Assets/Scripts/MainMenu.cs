using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    private int selectedLevel = 0;
    private List<int> levelList;


    private void Awake()
    {
        levelList = new List<int>();
    }

    public void selectLevel1()
    {
        selectedLevel = 1;
    }
    public void menus()
    {
        SceneManager.LoadScene(0);
    }

    public void selectLevel2()
    {
        selectedLevel = 2;
    }
    public void QuitGame()
    {
        Application.Quit();
    }
   
    

    public void playLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + selectedLevel);

    }

}
