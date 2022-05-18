using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class Inputs: MonoBehaviour 
{
    public Tilemap tilemap;
    private List<Vector3Int> movementPositions;

    private List<int> movementDirections;
    private Controls controls;
    public Gridd grid;
    public TileHighlighter tileHighlighter;

    private bool clickHeldDown = false;
    private int abilitySelected;
    public ButtonSelector abilitiesButtons;
    public Image abilityHud;
    public Image actionHud;
    private TileSelect tileSelector;
    private Vector3Int movingFrom;
    private Cell[,] cells;
    private int movementRange;

    private Vector3Int selectedPosition;

    private bool moving = false;

    private Vector3 touchEndPosition;

    private Vector3 touchLastPosition;

    private int abilityHudx = 570;
    private int abilityHudy = 180;

    private int actionHudx = 300;
    private int actionHudy = 180;

    private int cellxoffset;
    private int cellyoffset;

    public Healthbar healthbar;


    private Tile arrowup;
    private Tile arrowdown;
    private Tile arrowleft;
    private Tile arrowright;
    private Tile arrowupr;
    private Tile arrowupl;
    private Tile arrowleftr;
    private Tile arrowleftl;
    private Tile arrowdownr;
    private Tile arrowdownl;
    private Tile arrowrightr;
    private Tile arrowrightl;
    private Tile selectedp;


    private void OnEnable()
    {
        controls.Enable();
    }

    private void OnDisable()
    {
        controls.Disable();
    }


    private void Awake()
    {
        controls = new Controls();

        controls.clicks.Click.started += _ => press();
        controls.clicks.Click.canceled += _ => release();
        movementPositions = new List<Vector3Int>();
        movementDirections = new List<int>();

        arrowup = Resources.Load<Tile>("isometric tilemap/arrows/0,1");
        arrowdown = Resources.Load<Tile>("isometric tilemap/arrows/0,-1");
        arrowleft = Resources.Load<Tile>("isometric tilemap/arrows/-1,0");
        arrowright = Resources.Load<Tile>("isometric tilemap/arrows/1,0");
        arrowupr = Resources.Load<Tile>("isometric tilemap/arrows/t,r,1,0");
        arrowupl = Resources.Load<Tile>("isometric tilemap/arrows/b,l,0,1");
        arrowleftr = Resources.Load<Tile>("isometric tilemap/arrows/t,l,-1,0");
        arrowleftl = Resources.Load<Tile>("isometric tilemap/arrows/b,r,-1,0");
        arrowdownr = Resources.Load<Tile>("isometric tilemap/arrows/b,l,0,-1");
        arrowdownl = Resources.Load<Tile>("isometric tilemap/arrows/t,r,0,-1");
        arrowrightr = Resources.Load<Tile>("isometric tilemap/arrows/b,r,1,0");
        arrowrightl = Resources.Load<Tile>("isometric tilemap/arrows/t,l,1,0");
        selectedp = Resources.Load<Tile>("isometric tilemap/arrows/selectedPosition");
        selectedPosition = new Vector3Int(1,1,5);
    }

    private void Update()
    {
        if (clickHeldDown)
        {
            hold();
        }
    }

    private void Start()
    {
        tileSelector = grid.getTileSelector();
        cells = grid.getCellGrid();
        cellxoffset = grid.getXoffset();
        cellyoffset = grid.getXoffset();
    }

    public void press()
    {

        if (grid.getAttackAction())
        {
            if (abilitySelected == 1)
            {
                abilitiesButtons.ability1Button.Select();
            }
            else if (abilitySelected == 2)
            {
                abilitiesButtons.ability2Button.Select();
            }
            else if (abilitySelected == 3)
            {
                abilitiesButtons.ability3Button.Select();
            }
            else if (abilitySelected == 4)
            {
                abilitiesButtons.ability4Button.Select();
            }
        }


        Vector3 touch = new Vector3(controls.clicks.Position.ReadValue<Vector2>().x,
        controls.clicks.Position.ReadValue<Vector2>().y,
        0f);

        if (touch.x < abilityHud.rectTransform.rect.width && touch.y < abilityHud.rectTransform.rect.height)
        {
            return;

        }
        else if (touch.x > (Screen.width - actionHud.rectTransform.rect.width) && touch.y < actionHud.rectTransform.rect.width)
        {
            return;
        }

        touchLastPosition = touch;
        movingFrom = grid.selectATile(touchLastPosition);

        if (grid.getMovementAction())
        {
            if (grid.checkBounds(movingFrom))
            {
                if (cells[movingFrom.x + grid.getXoffset(), movingFrom.y + grid.getYoffset()].getCharacter() == grid.getCurrentTurn())
                {
                    emptyMovementList();
                    emptyDirectionList();
                    grid.setMovingToo(grid.getCurrentTurn().getLocation());
                    moving = true;
                    movementRange = grid.getCurrentTurn().getMovementRange();
                }
            }
        }
        clickHeldDown = true;
    }


    public void hold()
    {
        touchEndPosition = new Vector3(controls.clicks.Position.ReadValue<Vector2>().x,
            controls.clicks.Position.ReadValue<Vector2>().y,
            0f);

        if (moving)
        {
            characterMovement(touchLastPosition, touchEndPosition);

        }
        else if (false)
        {

        }
        else
        {
            Vector3 direction = touchLastPosition - touchEndPosition;

            Camera.main.transform.position += direction / 100;
            touchLastPosition = touchEndPosition;
        }

        //Debug.Log("last pos:" + touchLastPosition);
        // Debug.Log("end pos:" + touchEndPosition);
    }


    public void release()
    {
        touchEndPosition = new Vector3(controls.clicks.Position.ReadValue<Vector2>().x,
            controls.clicks.Position.ReadValue<Vector2>().y,
            0f);

        if (touchEndPosition.x < abilityHudx && touchEndPosition.y < abilityHudy)
        {
            return;

        }
        else if (touchEndPosition.x > (Screen.width - actionHudx) && touchEndPosition.y < actionHudy)
        {
            return;
        }


        moving = false;
        clickHeldDown = false;
        Character currentTurn = grid.getCurrentTurn();


        if (cells[getSelectedPosition().x + cellxoffset, getSelectedPosition().y + cellxoffset].getCharacter() != currentTurn)
        {
            tilemap.SetTile(getSelectedPosition(), null);
        }

        var p = tileSelector.getCorrectSelectedPosition(touchEndPosition);


        if (grid.checkBounds(p))
        {
            p.z = cells[p.x + cellxoffset, p.y + cellyoffset].getzAxis();

            if (tilemap.HasTile(p))
            {
                selectedPosition = p;
                selectedPosition.z += 3;

                if (cells[getSelectedPosition().x + cellxoffset, getSelectedPosition().y + cellyoffset].getCharacter() != currentTurn)
                {
                    tilemap.SetTile(getSelectedPosition(), selectedp);
                }


                if (cells[getSelectedPosition().x + cellxoffset, getSelectedPosition().y + cellyoffset].getCharacter() != null && cells[selectedPosition.x + cellxoffset, selectedPosition.y + cellyoffset].getFog() == false)
                {
                    healthbar.updateHealthBar(cells[getSelectedPosition().x + cellxoffset, getSelectedPosition().y + cellyoffset].getCharacter());

                }
                else
                {
                    healthbar.removeHealthBar();
                }


                if (grid.getAttackAction())
                {
                    if (abilitiesButtons.getHighlightedAbility() == null)
                    {
                        return;
                    }

                    if (abilitiesButtons.getHighlightedAbility().Count == 0)
                    {
                        return;
                    }

                    var found = false;

                    foreach (Vector3Int v in tileHighlighter.getHighlightedPositions())
                    {
                        if (v.x == getSelectedPosition().x && v.y == getSelectedPosition().y)
                        {
                            found = true;
                            break;
                        }
                    }

                    if (found)
                    {
                        if (tileHighlighter.getAffectedPositions().Count != 0)
                        {
                            tileHighlighter.emptyHighlightedList();
                            tileHighlighter.emptyAffectedList();
                            tileHighlighter.highlightTiles(abilitiesButtons.getAttackRange(), currentTurn.getLocation());
                            tileHighlighter.unHighlightStandingPosition(currentTurn.getLocation());
                        }

                        List<Vector2Int> d = tileHighlighter.getDisplayAbilityDirection(currentTurn.getLocation(), getSelectedPosition(), abilitiesButtons.getHighlightedAbility(), abilitiesButtons.getAttackAngle());


                        tileHighlighter.displayAbility(d);

                    }
                }
            }
        }
    }

    public void characterMovement(Vector3 touchLastP, Vector3 touchEndP)
    {


        Vector3 old = Camera.main.ScreenToWorldPoint(touchLastP);
        int oldLocation = 0;
        Vector3 neew = Camera.main.ScreenToWorldPoint(touchEndP);


        Vector3Int from = tileSelector.getCorrectSelectedPosition(touchLastP);
        Vector3Int too = tileSelector.getCorrectSelectedPosition(touchEndP);


        if (!grid.checkBounds(too))
        {
            return;
        }
        else
        {
            if (cells[too.x + cellxoffset, too.y + cellyoffset].getCharacter() != grid.getCurrentTurn())
            {
                if (!grid.checkIfCanPass(too))
                {
                    return;
                }
            }
        }

        if (old != neew)
        {
            int difx = ((int)(too.x - from.x));
            int dify = ((int)(too.y - from.y));

            if (difx != 0 || dify != 0)
            {
                if (Mathf.Abs(difx) == 1 && Mathf.Abs(dify) == 0)
                {
                    int z = cells[from.x + cellxoffset, from.y + cellyoffset].getzAxis();

                    from.z = z + 2;

                    if (difx == 1)
                    {
                        if (oldLocation == 1)
                        {
                            movingBack(touchEndP, oldLocation);
                        }
                        else if (movementRange == 0)
                        {
                            return;
                        }
                        else
                        {
                            if (oldLocation == 2)
                            {
                                tilemap.SetTile(from, arrowrightl);
                            }
                            else if (oldLocation == 3)
                            {
                                tilemap.SetTile(from, arrowright);
                            }
                            else if (oldLocation == 4)
                            {
                                tilemap.SetTile(from, arrowrightr);
                            }
                            else
                            {
                                tilemap.SetTile(from, arrowright);
                            }
                            oldLocation = 3;
                            movementPositions.Add(from);
                            movementDirections.Add(oldLocation);
                            movementRange--;
                        }
                    }
                    else if (difx == -1)
                    {

                        if (oldLocation == 3)
                        {
                            movingBack(touchEndP, oldLocation);
                        }
                        else if (movementRange == 0)
                        {
                            return;
                        }
                        else
                        {

                            if (oldLocation == 1)
                            {
                                tilemap.SetTile(from, arrowleft);
                            }
                            else if (oldLocation == 2)
                            {
                                tilemap.SetTile(from, arrowleftr);
                            }
                            else if (oldLocation == 4)
                            {
                                tilemap.SetTile(from, arrowleftl);
                            }
                            else
                            {
                                tilemap.SetTile(from, arrowleft);
                            }
                            oldLocation = 1;
                            movementPositions.Add(from);
                            movementDirections.Add(oldLocation);
                            movementRange--;
                        }
                    }
                    touchLastPosition = touchEndP;
                    grid.setMovingToo(too);
                }

                else if (Mathf.Abs(dify) == 1 && Mathf.Abs(difx) == 0)
                {

                    int z = cells[from.x + cellxoffset, from.y + cellyoffset].getzAxis();

                    from.z = z + 2;

                    if (dify == 1)
                    {
                        if (oldLocation == 2)
                        {
                            movingBack(touchEndP, oldLocation);
                        }
                        else if (movementRange == 0)
                        {
                            return;
                        }
                        else
                        {
                            if (oldLocation == 1)
                            {
                                tilemap.SetTile(from, arrowupr);
                            }
                            else if (oldLocation == 3)
                            {
                                tilemap.SetTile(from, arrowupl);
                            }
                            else if (oldLocation == 4)
                            {

                                tilemap.SetTile(from, arrowup);
                            }
                            else
                            {
                                tilemap.SetTile(from, arrowup);
                            }
                            oldLocation = 4;
                            movementPositions.Add(from);
                            movementDirections.Add(oldLocation);
                            movementRange--;
                        }
                    }
                    else if (dify == -1)
                    {

                        if (oldLocation == 4)
                        {
                            movingBack(touchEndP, oldLocation);
                        }
                        else if (movementRange == 0)
                        {
                            return;
                        }
                        else
                        {
                            if (oldLocation == 1)
                            {
                                tilemap.SetTile(from, arrowdownl);
                            }
                            else if (oldLocation == 2)
                            {
                                tilemap.SetTile(from, arrowdown);
                            }
                            else if (oldLocation == 3)
                            {
                                tilemap.SetTile(from, arrowdownr);
                            }
                            else
                            {
                                tilemap.SetTile(from, arrowdown);
                            }
                            oldLocation = 2;
                            movementPositions.Add(from);
                            movementDirections.Add(oldLocation);
                            movementRange--;
                        }
                    }
                    touchLastPosition = touchEndP;
                    grid.setMovingToo(too);
                }

            }
        }
    }

    

    void movingBack(Vector3 touchEndP, int oldLocation)
    {

        movementDirections.RemoveAt(movementDirections.Count - 1);

        if (movementDirections.Count == 0)
        {
            oldLocation = 0;
        }
        else
        {
            movementDirections.RemoveAt(movementDirections.Count - 1);

            oldLocation = movementDirections.Last();
        }

        if (movementPositions.Count > 0)
        {
            movementRange++;
            Vector3Int last = movementPositions.Last();
            tilemap.SetTile(last, null);
            movementPositions.RemoveAt(movementPositions.Count - 1);
        }
    }

    public void emptyMovementList()
    {
        for (int i = 0; i < movementPositions.Count;)
        {
            tilemap.SetTile(movementPositions[0], null);
            movementPositions.RemoveAt(0);
        }
    }

    public void emptyDirectionList()
    {
        for (int i = 0; i < movementDirections.Count;)
        {
            movementDirections.RemoveAt(0);
        }
    }


    public Vector3Int getSelectedPosition()
    {
        return selectedPosition;
    }
}
