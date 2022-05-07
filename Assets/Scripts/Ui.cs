using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ui : MonoBehaviour
{
    bool ability1;
    bool ability2;
    bool ability3;
    bool ability4;


    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(" ys" );
        ability1 = false;
    }

    public void ability1Click()
    {
        refreshSelectedAbility();
        ability1 = true;
        Debug.Log(" yes" + ability1);

    }

    public void ability2Click()
    {
        refreshSelectedAbility();
        ability2 = true;
        Debug.Log(" yes" + ability2);
    }

    public void ability3Click()
    {
        refreshSelectedAbility();
        ability3 = true;
        Debug.Log(" yes" + ability3);
    }

    public void ability4Click()
    {
        refreshSelectedAbility();
        ability4 = true;
        Debug.Log(" yes" + ability4);
    }

    public void refreshSelectedAbility()
    {
        ability1 = false;
        ability2 = false;
        ability3 = false;
        ability4 = false;

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
