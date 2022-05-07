using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class buttonsClicked : MonoBehaviour
{
    public Button button1;
    public Button button2;
    public Button button3;
    public Gridd grid;

    // Start is called before the first frame update
    void Start()
    {
     
    }
    void OnEnable()
    {
        //Register Button Events
        button1.onClick.AddListener(() => buttonCallBack(button1));
        button2.onClick.AddListener(() => buttonCallBack(button2));
        button3.onClick.AddListener(() => buttonCallBack(button3));

    }

    private void buttonCallBack(Button buttonPressed)
    {
        grid.confirmAction();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
