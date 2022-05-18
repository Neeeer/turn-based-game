using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonSelector : MonoBehaviour
{

    public Gridd grid;
    private int abilitySelected = 0;
    private List<Vector2Int> highlightedAbility;
    private int attackRange;
    private int attackAngle;

    public Button ability1Button;
    public Button ability2Button;
    public Button ability3Button;
    public Button ability4Button;

    public Button confirmAbility;

    public Text ability1dmg;
    public Text ability2dmg;
    public Text ability3dmg;
    public Text ability4dmg;

    public void Start()
    {
        ability1Button.interactable = (false);
        ability2Button.interactable = (false);
        ability3Button.interactable = (false);
        ability4Button.interactable = (false);
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
        ability1Button.interactable = (false);
        ability2Button.interactable = (false);
        ability3Button.interactable = (false);
        ability4Button.interactable = (false);


    }

    public void setCurrentTurnAbilities()
    {
        Character currentTurn = grid.getCurrentTurn();

        ability1dmg.text = currentTurn.damageAbility1();
        ability2dmg.text = currentTurn.damageAbility2();
        ability3dmg.text = currentTurn.damageAbility3();
        ability4dmg.text = currentTurn.damageAbility4();

    }

}
