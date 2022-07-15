using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    private int selectedLevel = 0;
    private List<int> levelList;
    private Controls controls;
    public DragAndDrop dragAndDrop;

    private void Awake()
    {
        levelList = new List<int>();

        controls = new Controls();

        controls.clicks.Click.started += _ => press();
        controls.clicks.Click.canceled += _ => release();

        enabled = false;
    }

    private void release()
    {
        throw new NotImplementedException();
    }


    private void press()
    {
        Vector3 touch = new Vector3(controls.clicks.Position.ReadValue<Vector2>().x,
       controls.clicks.Position.ReadValue<Vector2>().y,
       0f);
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
        if (dragAndDrop.checkIfPartyReady())
        {
            Player.instance.CharacterList = (dragAndDrop.getCharacterSlots());
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + selectedLevel);
        }
    }

}
