using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class DragAndDrop : MonoBehaviour 
{
    public Camera mainCamera;
    bool clicked;
    private Controls controls;


    public Image draggableCharacter;

    List<Image> characterSlots;

    public Text partyNotReadyText;

    public Image characterSlot1;
    public Image characterSlot2;
    public Image characterSlot3;
    public Image characterSlot4;

    bool message = false;

    public void OnEnable()
    {
        controls.Enable();
    }

    public void OnDisable()
    {
        controls.Disable();
    }


    private void Awake()
    {
        clicked = false;
        controls = new Controls();

        characterSlots = new List<Image>();

        characterSlots.Add(characterSlot1);
        characterSlots.Add(characterSlot2);
        characterSlots.Add(characterSlot3);
        characterSlots.Add(characterSlot4);

        controls.clicks.Click.started += _ => press();
        controls.clicks.Click.canceled += _ => release();


    }

    private void press()
    {

        Vector2 position = controls.clicks.Position.ReadValue<Vector2>();

        RaycastHit2D hit2D = Physics2D.Raycast(position, Vector2.zero);


        if (hit2D.collider != null && hit2D.transform.gameObject.layer == LayerMask.NameToLayer("Draggable"))
        {
            clicked = true;

            draggableCharacter.gameObject.SetActive(true);
            draggableCharacter.GetComponentInChildren<Text>().text = hit2D.collider.GetComponentInChildren<Text>().text;

            Image r = hit2D.collider.gameObject.GetComponent<Image>();
            Color newColor = r.color;
            newColor.a = 0.5f;
            r.color = newColor;

            StartCoroutine(DragUpdate(hit2D.collider.gameObject));
        }

    }

    private IEnumerator DragUpdate(GameObject clickedObject)
    {
        
        while (clicked)
        {

            draggableCharacter.gameObject.transform.position = controls.clicks.Position.ReadValue<Vector2>();

            yield return new WaitForEndOfFrame();

        }
    }

    private void release()
    {
        if (clicked)
        {
            Vector2 position = controls.clicks.Position.ReadValue<Vector2>();

            RaycastHit2D hit2D = Physics2D.Raycast(position, Vector2.zero);
            

            if (hit2D.collider != null && hit2D.transform.gameObject.layer == LayerMask.NameToLayer("Slot"))
            {
                String text = draggableCharacter.GetComponentInChildren<Text>().text;

                foreach (Image charSlots in characterSlots)
                {
                    if(charSlots.GetComponentInChildren<Text>().text == text)
                    {
                        charSlots.GetComponentInChildren<Text>().text = "Empty";
                    }
                }


                hit2D.collider.GetComponentInChildren<Text>().text = text;
            }
            draggableCharacter.gameObject.SetActive(false);
        }


        clicked = false;
    }

    public bool checkIfPartyReady()
    {


        foreach (Image charSlots in characterSlots)
        {
            if (charSlots.GetComponentInChildren<Text>().text == "Empty")
            {
                partyNotReady();
                
                return false;
            }
        }
        return true;
    }

    private void partyNotReady()
    {
        partyNotReadyText.gameObject.SetActive(true);

        Color c = partyNotReadyText.color;
        c.a = 1f;
        partyNotReadyText.color = c;
        message = true;

    }

    public List<string> getCharacterSlots()
    {

        List<string> charList = new List<string>();

        foreach (Image charSlots in characterSlots)
        {
            charList.Add(charSlots.GetComponentInChildren<Text>().text);
        }

        return charList;
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (message)
        {
            Color c = partyNotReadyText.color;
            c.a -= Time.deltaTime;
            partyNotReadyText.color = c;

            if (c.a < 0)
            {
                partyNotReadyText.gameObject.SetActive(false);
                message = false;
            }
        }
    }
}
