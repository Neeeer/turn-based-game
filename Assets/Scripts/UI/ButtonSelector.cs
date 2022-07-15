using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



// class in charge of selecting buttons and getting character ability information associated with button to pass over to main grid class. Also in charge of enabling/disabling button interactibity
public class ButtonSelector : MonoBehaviour
{

    [SerializeField] private LevelLogic grid;
    [SerializeField] private int abilitySelected = 0;
    [SerializeField] private List<Vector2Int> highlightedAbility;

    [SerializeField] private List<Button> AbilityButtons;
    [SerializeField] private List<Text> AbilitiesDamageText;

    private int attackRange;
    private int attackAngle;

    [SerializeField] private Button ability1Button;
    [SerializeField] private Button ability2Button;
    [SerializeField] private Button ability3Button;
    [SerializeField] private Button ability4Button;

    public Button confirmAbility;

    [SerializeField] private Text ability1dmg;
    [SerializeField] private Text ability2dmg;
    [SerializeField] private Text ability3dmg;
    [SerializeField] private Text ability4dmg;

    public void Start()
    {
        AbilityButtons = new List<Button>();
        AbilityButtons.Add(ability1Button);
        AbilityButtons.Add(ability2Button);
        AbilityButtons.Add(ability3Button);
        AbilityButtons.Add(ability4Button);

        AbilitiesDamageText = new List<Text>();
        AbilitiesDamageText.Add(ability1dmg);
        AbilitiesDamageText.Add(ability2dmg);
        AbilitiesDamageText.Add(ability3dmg);
        AbilitiesDamageText.Add(ability4dmg);

        disableAbilities();
    }



    public void selectAbility1()
    {

        if (!grid.getCurrentTurn().GetisPlayer())
        {
            return;
        }
        if (grid.getAttackAction())
        {
            abilitySelected = 1;
            highlightedAbility = grid.getCurrentTurn().highlightAbility1();
            attackRange = grid.getCurrentTurn().rangeAbility1();
            attackAngle = grid.getCurrentTurn().angleAbility1();
            grid.selectAbility();
        }
    }
    public void selectAbility2()
    {
        if (!grid.getCurrentTurn().GetisPlayer())
        {
            return;
        }
        if (grid.getAttackAction())
        {
            abilitySelected = 2;
            highlightedAbility = grid.getCurrentTurn().highlightAbility2();
            attackRange = grid.getCurrentTurn().rangeAbility2();
            attackAngle = grid.getCurrentTurn().angleAbility2();
            grid.selectAbility();
        }
    }
    public void selectAbility3()
    {
        if (!grid.getCurrentTurn().GetisPlayer())
        {
            return;
        }

        if (grid.getAttackAction())
        {
            abilitySelected = 3;
            highlightedAbility = grid.getCurrentTurn().highlightAbility3();
            attackRange = grid.getCurrentTurn().rangeAbility3();
            attackAngle = grid.getCurrentTurn().angleAbility3();
            grid.selectAbility();
        }
    }
    public void selectAbility4()
    {
        if (!grid.getCurrentTurn().GetisPlayer())
        {
            return;
        }

        if (grid.getAttackAction())
        {
            abilitySelected = 4;
            highlightedAbility = grid.getCurrentTurn().highlightAbility4();
            attackRange = grid.getCurrentTurn().rangeAbility4();
            attackAngle = grid.getCurrentTurn().angleAbility4();
            grid.selectAbility();
        }
    }

    public void selectAButton(int i)
    {
        if (i == 0)
        {
            ability1Button.Select();
        }
        else if (i == 1)
        {
            ability2Button.Select();
        }
        else if (i == 2)
        {
            ability3Button.Select();
        }
        else if (i == 3)
        {
            ability4Button.Select();
        }
    }

    public int getAbilitySelected()
    {
        return abilitySelected;
    }

    public List<Vector2Int> getSelectedAbility()
    {
        return highlightedAbility;
    }

    public int getAttackRange()
    {
        return attackRange;
    }

    public int getAttackAngle()
    {
        return attackAngle;
    }

    public List<Vector2Int> getHighlightedAbility()
    {
        return highlightedAbility;
    }

    public void disableButtons()
    {
        confirmAbility.interactable = (false);
        disableAbilities();
    }

    public void enableButtons()
    {
        confirmAbility.interactable = (true);
        enableAbilities();
    }

    public void enableConfirmButton()
    {
        confirmAbility.interactable = (true);
    }


    public void disableAbilities()
    {
        foreach (Button button in AbilityButtons)
        {
            button.interactable = (false);
        }

    }

    public void enableAbilities()
    {
        foreach (Button button in AbilityButtons)
        {
            button.interactable = (true);
        }
    }



    public void setCurrentTurnAbilities()
    {
        Character currentTurn = grid.getCurrentTurn();

        for(int i = 0; i < AbilitiesDamageText.Count; i++)
        {
            Vector2Int dmg;
            if (i == 0)
            {
                dmg = currentTurn.damageAbility1();
            }
            else if (i == 1)
            {
                dmg = currentTurn.damageAbility2();
            }
            else if (i == 2)
            {
                dmg = currentTurn.damageAbility3();
            }
            else
            {
                dmg = currentTurn.damageAbility4();
            }

            if (dmg.x < 0 )
            {
                dmg *= -1;
                AbilitiesDamageText[i].text = "+" + dmg.x + " - " + dmg.y;
            }
            else
            {
                AbilitiesDamageText[i].text = dmg.x + " - " + dmg.y;
            }
        }
    }

    public void disableAbilityHighlights()
    {
        foreach (Transform child in this.gameObject.transform)
        {
            GameObject abilityText = child.Find("button text").gameObject;
            abilityText.SetActive(false);
        }
    }
}
