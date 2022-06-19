using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;



// each ability button has a on pointer hover to display a grid of the affected area of that ability
public class abilityButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Gridd gridd;
    List<Vector2Int> list;
    List<Vector2Int> alteredList;
    public Image image;
    private Vector2Int startSlot;
    private Vector2Int slot;
    private string childName;

    void Start()
    {
        // center of grid is 3,3
        startSlot = new Vector2Int(3,3);
        childName = "square";
        alteredList = new List<Vector2Int>();
    }

   
    // on pointer enter game object display ability 
    public void OnPointerEnter(PointerEventData eventData)
    {
        Character currentTurn = gridd.getCurrentTurn();
       
        if (this.transform.parent.name == "ability 1")
        {
            list = currentTurn.highlightAbility1();
        }
        else if (this.transform.parent.name == "ability 2")
        {
            list = currentTurn.highlightAbility2();
        }
        else if (this.transform.parent.name == "ability 3")
        {
            list = currentTurn.highlightAbility3();
        }
        else if (this.transform.parent.name == "ability 4")
        {
            list = currentTurn.highlightAbility4();
        }

        // some abilities are empty cause they affect all tiels withing ability range
        if (list.Count == 0)
        {
            // to be updated
        }
        else
        {
            // for each tile affected display color of each square ui from center position
            foreach (Vector2Int v in list)
            {
                slot = new Vector2Int(startSlot.x - v.x, startSlot.y - v.y);
                Transform child = image.transform.Find(childName + slot.x + slot.y);
                child.GetComponent<Image>().color = Color.green;
                alteredList.Add(slot);
            }
        }
        // set ui display active
        image.transform.parent.gameObject.SetActive(true);
        image.transform.parent.position = new Vector3(this.transform.position.x, this.transform.position.y + 0.65f , image.transform.position.z);
        
    }

    // upon pointer exiting button reset square grid and disable ui
    public void OnPointerExit(PointerEventData eventData)
    {
        foreach (Vector2Int alList in alteredList)
        {
            Transform child = image.transform.Find(childName + alList.x + alList.y);
            child.GetComponent<Image>().color = Color.white;
        }
        alteredList.Clear();
        image.transform.parent.gameObject.SetActive(false);
    }

    

}
