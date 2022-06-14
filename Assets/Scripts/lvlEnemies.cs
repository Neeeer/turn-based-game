using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lvlEnemies : MonoBehaviour
{
   

    public virtual void Awake()
    {

    }

    public virtual List<Character> getEnemies()
    {
        return null;
    }
    public virtual void initializeCharacterLocations()
    {

    }

    public virtual List<Vector3> getCharacterStartingPositions()
    {
        return null;
    }
    public virtual List<Vector2> getCameraBoundries()
    {
        return null;
    }
}
