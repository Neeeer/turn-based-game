using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;


public class Gridd : MonoBehaviour
{
    private List<Vector3Int> highlightedPositions;
    private Vector3Int selectedPosition;
    private Vector3Int selectedTurn;
    private List<Vector3Int> movementPositions;
    private List<Vector3Int> affectedPositions;
    private List<Vector2Int> highlightedAbility;

    private List<int> movementDirections;

    public Tilemap tilemap;

    public Cell cell;
    private Cell[,] cells;
    public lvlEnemies lvlenemies;
    public objectives objectives;

    public new Camera camera; 

    private bool moving;

    private Character currentTurn;

    private Vector3 touchEndPosition;

    private Vector3 touchLastPosition;
    private Vector3Int movingFrom;

    private int abilityHudx = 570;
    private int abilityHudy = 180;

    private int actionHudx = 300;
    private int actionHudy = 180;
    int cellxOffset;
    int cellyOffset;

    private float zAxisyIncrease = (float)0.36;
    private int oldLocation;

    private int kills;
    private int turns;

    private Druid character1;
    private Frog character2;
    private Assasin character3;
    private Paladin character4;


    public GameObject char1;
    public GameObject char2;
    public GameObject char3;
    public GameObject char4;



    public UnityEngine.UI.Slider healthSlider;
    public UnityEngine.UI.Image healthSlide;
    public UnityEngine.UI.Image healthBar;
    public UnityEngine.UI.Image healthUI;
    public Text healthNumber;
    public Text phase;

    public Button ability1Button;
    public Button ability2Button;
    public Button ability3Button;
    public Button ability4Button;

    public Text ability1dmg;
    public Text ability2dmg;
    public Text ability3dmg;
    public Text ability4dmg;

    public Button confirmAbility;

    List<UnityEngine.UI.Image> HealthListUI;

    List<Character> characterOrder;
    List<Character> entityOrder;
    List<Character> players;
    List<Character> enemie;

    bool movementAction = false;
    bool attackAction = false;

    private int Turn;
    private int movementRange;
    private int attackRange;
    private int attackAngle;

    private BoundsInt bounds;

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
    private Tile highlightedp;
    private Tile fog;
    private Tile lava;

    private int abilitySelected = 0;
    private Vector3Int movingToo;

    private Vector3 tileWidth;
    private Vector3 tileHeight;

    private int useAbility;





    [SerializeField] public Transform damagePopup;



    // Start is called before the first frame update
    void Start()

    {
        Turn = 0;
        character1 = new Druid() ;
        character2 = new Frog();
        character3 = new Assasin();
        character4 = new Paladin(); 

        character1.setGameobject(char1);
        character2.setGameobject(char2);
        character3.setGameobject(char3);
        character4.setGameobject(char4);


        characterOrder = new List<Character>();
        entityOrder = new List<Character>();
        players = new List<Character>();
        

        players.Add(character1);
        players.Add(character2);
        players.Add(character3);
        players.Add(character4);


        setUpParty(players);

        selectedPosition = new Vector3Int(0, 0, 10);
        movingToo = new Vector3Int(0, 0, 0);

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
        highlightedp = Resources.Load<Tile>("isometric tilemap/arrows/highlightedTile");
        fog = Resources.Load<Tile>("isometric tilemap/arrows/fog");
        lava = Resources.Load<Tile>("isometric tilemap/25-ground-blocks/lava");
        oldLocation = 0;


        tilemap.CompressBounds();
        bounds = tilemap.cellBounds;
        cells = new Cell[bounds.size.x, bounds.size.y];
        
        int tilez = 0;
        moving = false;

        cellxOffset = Mathf.Abs(bounds.xMin);
        cellyOffset = Mathf.Abs(bounds.yMin);

        for (int x = bounds.xMin; x < bounds.xMax; x++)
        {

            for (int y = bounds.yMin; y < bounds.yMax; y++)
            {
                 var px = cellxOffset + x;
                 var py = cellyOffset + y;

                 cell = new Cell();

                 cells[px, py] = cell;

                for (int i = 0; i < bounds.size.z ; i++)
                {

                    if (tilemap.HasTile(new Vector3Int(x, y, i)))
                    {

                        tilez = i;
                        cells[px, py].setzAxis(tilez);
                        var tilemapTile = tilemap.GetTile(new Vector3Int(x, y, i));
                        if (tilemapTile == lava)
                        {
                            cells[px, py].setPassable(false);
                        }
                        else
                        {
                            cells[px, py].setPassable(true);
                        }
                    }
                }
            }
        }

        HealthListUI = new List<UnityEngine.UI.Image>();
        movementPositions = new List<Vector3Int>();
        affectedPositions = new List<Vector3Int>();
        movementDirections = new List<int>();
        highlightedPositions = new List<Vector3Int>();

        movementRange = 0;
        attackRange = 0;
        attackAngle = 0;

        setUpUnit(character1);
        setUpUnit(character2);
        setUpUnit(character3);
        setUpUnit(character4);


        HealthListUI.Add(healthSlide);
        HealthListUI.Add(healthBar);
        HealthListUI.Add(healthUI);

       

        ability1Button.interactable = (false);
        ability2Button.interactable = (false);
        ability3Button.interactable = (false);
        ability4Button.interactable = (false);


        selectedTurn = new Vector3Int(0,0,3);
        selectedTurn.z = cells[selectedTurn.x , selectedTurn.y].getzAxis() + 3;

       

        addEnemy(lvlenemies.getEnemies());


        charactersReady();

    }

    public void charactersReady()
    {
        setUpEntities(enemie);
        startGameDefog();
        removeHealthBar();

        nextTurn();

    }


    private void unHighlightStandingPosition(Vector3Int loc)
    {
        loc.z = cells[loc.x + cellxOffset, loc.y + cellyOffset].getzAxis() + 1;
        tilemap.SetTile(loc, null);
        highlightedPositions.RemoveAt(0);
    }

    public void selectAbility1()
    {

        if (!currentTurn.GetisPlayer())
        {
            return;
        }
        if (attackAction)
        {
            abilitySelected = 1;
            highlightedAbility = currentTurn.highlightAbility1();
            attackRange = currentTurn.rangeAbility1();
            attackAngle = currentTurn.angleAbility1();
            selectAbility();
        }
    }
    public void selectAbility2()
    {
        if (!currentTurn.GetisPlayer())
        {
            return;
        }
        if (attackAction)
        {
            abilitySelected = 2;
            highlightedAbility = currentTurn.highlightAbility2();
            attackRange = currentTurn.rangeAbility2();
            attackAngle = currentTurn.angleAbility2();
            selectAbility();
        }
    }
    public void selectAbility3()
    {
        if (!currentTurn.GetisPlayer())
        {
            return;
        }

        if (attackAction)
        {
            abilitySelected = 3;
            highlightedAbility = currentTurn.highlightAbility3();
            attackRange = currentTurn.rangeAbility3();
            attackAngle = currentTurn.angleAbility3();
            selectAbility();
        }
    }
    public void selectAbility4()
    {
        if (!currentTurn.GetisPlayer())
        {
            return;
        }

        if (attackAction)
        {
            abilitySelected = 4;
            highlightedAbility = currentTurn.highlightAbility4();
            attackRange = currentTurn.rangeAbility4();
            attackAngle = currentTurn.angleAbility4();
            selectAbility();
        }
    }

    public void selectAbility()
    {
        emptyHighlightedList();
        emptyAffectedList();
        Vector3Int loc = currentTurn.getLocation();
        highlightRangeLoop(attackRange, loc, new Vector2Int(0, 0));
        unHighlightStandingPosition(loc);


        if (highlightedAbility.Count == 0)
         {
            affectedPositions = new List<Vector3Int>(highlightedPositions);


            foreach (Vector3Int s in affectedPositions)
            {
                
                var position = s;
                position.z = cells[position.x + cellxOffset, position.y + cellyOffset].getzAxis();

                if (checkBounds(position))
                {
                    if (tilemap.HasTile(position))
                    {
                        position.z += 1;
                        tilemap.SetTile(position, highlightedp);
                        tilemap.SetTileFlags(position, TileFlags.None);
                        Color color = new Color(1.0f, 1.0f, 0.0f, 0.5f);
                        tilemap.SetColor(position, color);
                    }
                }


            }
        }
    }
    public void endTurn()
    {
        if (movementAction)
        {
            emptyMovementList();
            emptyDirectionList();
            emptyHighlightedList();
            attackAction = true;
            abilitySelected = 1;
            ability1Button.Select();

            ability1Button.interactable = (true);
            ability2Button.interactable = (true);
            ability3Button.interactable = (true);
            ability4Button.interactable = (true);

            movementAction = false;
            selectAbility1();
            phase.text = "Action Phase";
        }
        else if (attackAction)
        {
            emptyHighlightedList();
            emptyAffectedList();

            attackAngle = 0;
            movementAction = false;
            attackAction = false;
            ability1Button.interactable = (false);
            ability2Button.interactable = (false);
            ability3Button.interactable = (false);
            ability4Button.interactable = (false);

            Turn++;
            nextTurn();
        }

    }

    public void confirmAction()
    {
        if (movementAction)
        {
           

            var currentCharacterLocation = currentTurn.getLocation();

            Vector3 v = getNonIsometricCoordinatesGo(movingToo);

            v.z = cells[movingToo.x + cellxOffset, movingToo.y + cellyOffset].getzAxis();

            v.y +=  zAxisyIncrease * v.z ;
            v.y = Mathf.Round(v.y * 100f) / 100f;


            checkIfSelected(currentCharacterLocation, movingToo);

            cells[currentCharacterLocation.x + cellxOffset, currentCharacterLocation.y + cellyOffset].removeCharacter();
            cells[movingToo.x + cellxOffset, movingToo.y + cellyOffset].setCharacter(currentTurn);



            currentTurn.setLocation(movingToo);
            currentTurn.getGameobject().transform.position = v;

            tilemap.SetTile(selectedTurn, null);

            var loc = currentTurn.getLocation();
            loc.z = cells[loc.x + cellxOffset, loc.y + cellyOffset].getzAxis() + 3;

            selectedTurn = loc;
            tilemap.SetTile(loc, selectedp);


            playerMoveRefog(currentCharacterLocation, movingToo);

            endTurn();
        }
        else if (attackAction)
        {
            foreach (Vector3Int s in affectedPositions)
            {

                var position = s;
                position.z = cells[position.x + cellxOffset, position.y + cellyOffset].getzAxis();

                if (cells[position.x + cellxOffset, position.y + cellyOffset].getOcupied() == true)
                {

                    
                    Character currentChara = cells[position.x + cellxOffset, position.y + cellyOffset].getCharacter();
                    
                    
                    currentTurn.useAbility(this, currentChara, abilitySelected);

                    
                    if (currentChara.getHealth() <= 0)
                    {
                        currentChara.death();
                        currentChara.getGameobject().SetActive(false);
                        if (currentChara.GetisPlayer())
                        {
                            players.Remove(currentChara);
                        }
                        else
                        {
                            enemie.Remove(currentChara);
                            kills++;
                        }
                        entityOrder.Remove(currentChara);
                        cells[currentChara.getLocation().x + cellxOffset, currentChara.getLocation().y + cellyOffset].removeCharacter();
                    }
                }
            }

            endTurn();
        }

    }


    // Update is called once per frame

    void Update()
    {
        
        if (Input.touchCount > 0)
        {

            if (attackAction)
            {
                if (abilitySelected == 1)
                {
                    ability1Button.Select();
                }
                else if(abilitySelected == 2)
                {
                    ability2Button.Select();
                }
                else if (abilitySelected == 3)
                {
                    ability3Button.Select();
                }
                else if(abilitySelected == 4)
                {
                    ability4Button.Select();
                }
            }




            Touch touch = Input.GetTouch(0);
            if (touch.position.x < abilityHudx && touch.position.y < abilityHudy )
            {
                return;

            }
            else if (touch.position.x >  (Screen.width - actionHudx)  && touch.position.y < actionHudy)
            {
                return;
            }
            if (touch.phase == TouchPhase.Began)
            {

               
                touchLastPosition = touch.position;
                movingFrom = getSelectedPosition(touchLastPosition);
                

                if (movementAction)
                {
                    if (checkBounds(movingFrom))
                    {
                        if (cells[movingFrom.x + cellxOffset, movingFrom.y + cellyOffset].getCharacter() == currentTurn)
                        {
                            emptyMovementList();
                            emptyDirectionList();
                            movingToo = currentTurn.getLocation();
                            moving = true;
                            movementRange = currentTurn.getMovementRange();
                        }
                    }
                }
               
            }

            else if(touch.phase == TouchPhase.Moved)
            {

                touchEndPosition = touch.position;
                
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

           else if(touch.phase == TouchPhase.Ended)
           {
                
                touchEndPosition = touch.position;
                moving = false;
                oldLocation = 0;

                


                if (cells[selectedPosition.x + cellxOffset, selectedPosition.y + cellyOffset].getCharacter() != currentTurn)
                {
                    tilemap.SetTile(selectedPosition, null);
                }

                var p = getSelectedPosition(touchEndPosition);


                if (checkBounds(p))
                {
                    p.z = cells[p.x + cellxOffset, p.y + cellyOffset].getzAxis();

                    if (tilemap.HasTile(p))
                    {
                        selectedPosition = p;
                        selectedPosition.z += 3;

                        if (cells[selectedPosition.x + cellxOffset, selectedPosition.y + cellyOffset].getCharacter() != currentTurn)
                        {
                            tilemap.SetTile(selectedPosition, selectedp);
                        }


                        if (cells[selectedPosition.x + cellxOffset, selectedPosition.y + cellyOffset].getCharacter() != null )
                        {
                            updateHealthBar(cells[selectedPosition.x + cellxOffset, selectedPosition.y + cellyOffset].getCharacter());
                            
                        }
                        else
                        {
                            removeHealthBar();
                        }


                        if (attackAction)
                        {
                            if (highlightedAbility == null)
                            {
                                return;
                            }

                            if (highlightedAbility.Count == 0)
                            {
                                return;
                            }

                            var found = false;

                            foreach (Vector3Int v in highlightedPositions)
                            {
                                if (v.x == selectedPosition.x && v.y == selectedPosition.y)
                                {
                                    found = true;
                                    break;
                                }
                            }

                            if (found)
                            {
                                if (affectedPositions.Count != 0)
                                {
                                    emptyHighlightedList();
                                    emptyAffectedList();
                                    highlightRangeLoop(attackRange, currentTurn.getLocation(), new Vector2Int(0, 0));
                                    unHighlightStandingPosition(currentTurn.getLocation());
                                }

                                List<Vector2Int> d = getDisplayAbility(currentTurn.getLocation(), selectedPosition, highlightedAbility);

                                
                                displayAbility(d);

                            }

                        }


                    }
                }

            }
        }

        

    }

    private void displayAbility(List<Vector2Int> d)
    {
        Vector3Int selectPos = selectedPosition;

        if (attackAngle > 10)
        {
            bool found = false;
            Vector3Int direction = selectedPosition - currentTurn.getLocation();
            direction.z = 0;

            for (int range = attackAngle - 10; range > 0; range--)
            {
                if (checkBounds(selectPos))
                {
                    selectPos.z = cells[selectPos.x + cellxOffset, selectPos.y + cellyOffset].getzAxis();

                    if (tilemap.HasTile(selectPos))
                    {

                        if (cells[selectPos.x + cellxOffset, selectPos.y + cellyOffset].getOcupied())
                        {
                            found = true;
                            break;
                        }
                        
                    }
                }
                selectPos += direction;
            }
            if (!found)
            {
                return;
            }
        }

        foreach (Vector2Int v in d)
        {
            var loc = selectPos;

                loc.x += v.x;
                loc.y += v.y;
            if (checkBounds(loc))
            {
                loc.z = cells[loc.x + cellxOffset, loc.y + cellyOffset].getzAxis();
                if (tilemap.HasTile(loc))
                {
                    loc.z += 1;
                    tilemap.SetTile(loc, highlightedp);
                    tilemap.SetTileFlags(loc, TileFlags.None);
                    Color color = new Color(1.0f, 1.0f, 0.0f, 0.5f);
                    tilemap.SetColor(loc, color);

                    affectedPositions.Add(loc);

                }
            }
        }
    }

    private List<Vector2Int> getDisplayAbility(Vector3Int character, Vector3Int position, List<Vector2Int> highlightedAbility)
    {
        int difx = ((int)(position.x - character.x));
        int dify = ((int)(position.y - character.y));
        List<Vector2Int> ability = new List<Vector2Int>();

        if (attackAngle == 2)
        {
            if (difx > 0)
            {
                if (dify > 0)
                {
                    ability = highlightedAbility;
                }
                else
                {
                    for (int i = 0; i < highlightedAbility.Count; i++)
                    {
                        Vector2Int temp = highlightedAbility[i];
                        temp.y = -temp.y;
                        ability.Add(temp);
                    }
                }
            }
            else
            {
                if (dify > 0)
                {
                    for (int i = 0; i < highlightedAbility.Count; i++)
                    {
                        Vector2Int temp = highlightedAbility[i];
                        temp.x = -temp.x;
                        ability.Add(temp);
                    }
                }
                else
                {
                    for (int i = 0; i < highlightedAbility.Count; i++)
                    {
                        Vector2Int temp = highlightedAbility[i];
                        temp.x = -temp.x;
                        temp.y = -temp.y;
                        ability.Add(temp);
                    }
                }
            }
        }
        else 
        {

            if (dify ==  0)
            {
                if (difx > 0)
                {
                
                    ability = highlightedAbility ;
                }
                else
                {
                    for(int i = 0; i < highlightedAbility.Count; i++)
                    {
                        ability.Add(-highlightedAbility[i]);

                    }
                }
            }
            else
            {
                if (dify > 0)
                {
                    for (int i = 0; i < highlightedAbility.Count; i++)
                    {
                        Vector2Int temp = highlightedAbility[i];
                        int tempx = temp.x;
                        temp.x = temp.y;
                        temp.y = tempx;
                        ability.Add(temp);
                    }
                }
                else
                {
                    for (int i = 0; i < highlightedAbility.Count; i++)
                    {
                        Vector2Int temp = highlightedAbility[i];
                        int tempx = temp.x;
                        temp.x = -temp.y;
                        temp.y = -tempx;
                        ability.Add(temp);
                    }
                }
            }
        }
        return ability;
    }

    private bool checkBounds(Vector3Int v)
    {
        if (v.x >= bounds.xMin && v.x < bounds.xMax)
        {
            if (v.y >= bounds.yMin && v.y < bounds.yMax)
            {
                return true;
            }
        }

        return false;
    }

    private bool checkIfCanPass(Vector3Int v)
    {
        if (!cells[v.x + cellxOffset, v.y + cellyOffset].getOcupied())
        {
            if (cells[v.x + cellxOffset, v.y + cellyOffset].getPassable())
            {
                return true;
            }
        }
        return false;
    }



    private void emptyMovementList()
    {
        for (int i = 0; i < movementPositions.Count;)
        {
            tilemap.SetTile(movementPositions[0], null);
            movementPositions.RemoveAt(0);
        }
    }

    private void emptyDirectionList()
    {
        for (int i = 0; i < movementDirections.Count;)
        {
            movementDirections.RemoveAt(0);
        }
    }

    private void emptyHighlightedList()
    {
        for (int i = 0; i < highlightedPositions.Count;)
        {
            tilemap.SetTile(highlightedPositions[0], null);
            highlightedPositions.RemoveAt(0);
        }
    }

    private void emptyAffectedList()
    {
        for (int i = 0; i < affectedPositions.Count;)
        {
            tilemap.SetTile(affectedPositions[0], null);
            affectedPositions.RemoveAt(0);
        }
    }

    Vector3Int getIsometricCoordinates(Vector3 z)
    {
        double tempx = z.x;
        double tempy = z.y;
        z.x = (float)(tempy * 2 + tempx );
        z.y = (float)(tempy * 2 - tempx );

        Vector3Int zInt = Vector3Int.FloorToInt(z);

        return zInt;
    }

    Vector3 getNonIsometricCoordinates(Vector3Int zInt)
    {
        
        Vector3 z = zInt;
        float tempx = z.x ;
        float tempy = z.y ;
        z.x = ((tempx - tempy) / 2); 
        z.y = ((tempx + tempy) / 4);

        return z;
    }


    Vector3Int getIsometricCoordinatesGo(Vector3 z)
    {
        double tempx = z.x;
        double tempy = z.y - 0.36 * z.z;
        z.x = (float)(tempy * 2 + tempx - 1);
        z.y = (float)(tempy * 2 - tempx - 1);
        Vector3Int zInt = Vector3Int.RoundToInt(z);
        return zInt;

    }

    Vector3 getNonIsometricCoordinatesGo(Vector3Int zInt)
    {

        Vector3 z = zInt;
        float tempx = z.x + 1;
        float tempy = z.y + 1;
        z.x = ((tempx - tempy) / 2);
        z.y = ((tempx + tempy) / 4);



        return z;
    }


    public void setUpUnit(Character c)
    {
        Vector3 zz = c.getGameobject().transform.position;
        Vector3Int zzz = getIsometricCoordinatesGo(zz);
        cells[zzz.x + cellxOffset, zzz.y + cellyOffset].setCharacter(c);
        c.setLocation(zzz);
    }



    public void nextTurn()
    {
        if (Turn >= entityOrder.Count)
        {
            Turn = 0;
            turns++;
        }
        currentTurn = (entityOrder[Turn]);
        if (entityOrder[Turn].GetisPlayer() == true)
        {


            tilemap.SetTile( selectedTurn, null);

            var loc = currentTurn.getLocation();
            loc.z = cells[loc.x + cellxOffset, loc.y + cellyOffset].getzAxis() + 3;


            selectedTurn = loc;
            tilemap.SetTile(loc, selectedp);


            tilemap.SetTileFlags(loc, TileFlags.None);
            Color color = new Color(0.0f, 1.0f, 0.0f, 1.0f);
            tilemap.SetColor(loc, color);

            CameraLoc cameraAccess = camera.GetComponent<CameraLoc>();
            Vector3 too = currentTurn.getGameobject().transform.position;
            too.z = -10;
            cameraAccess.moveCamera(camera.transform.position, too);

            ability1dmg.text = currentTurn.damageAbility1();
            ability2dmg.text = currentTurn.damageAbility2();
            ability3dmg.text = currentTurn.damageAbility3();
            ability4dmg.text = currentTurn.damageAbility4();

            movementTurn();
        }
        else
        {
            ai aintelligence = new ai() ;
            var result = aintelligence.pathFinding(currentTurn, players, cells, cellxOffset, cellyOffset);
            Vector2Int dest = result.Item1;
            Character focus = result.Item2;
            Vector3Int destination = new Vector3Int(dest.x,dest.y,0);


            destination.z = cells[destination.x , destination.y ].getzAxis();

            checkIfSelected(currentTurn.getLocation(), destination);

            

            cells[currentTurn.getLocation().x + cellxOffset, currentTurn.getLocation().y + cellyOffset].removeCharacter();
            cells[destination.x , destination.y].setCharacter(currentTurn);

                
            

            destination.x = destination.x - cellxOffset;
            destination.y = destination.y - cellyOffset;
            currentTurn.setLocation(destination);

            Vector3 too = getNonIsometricCoordinatesGo(destination);


            too.y += zAxisyIncrease * too.z;
            too.y = Mathf.Round(too.y * 100f) / 100f;

            currentTurn.getGameobject().transform.position = too;


            if (enemyMoveRefog(currentTurn))
            {


                CameraLoc cameraAccess = camera.GetComponent<CameraLoc>();
                Vector3 movingToo = currentTurn.getGameobject().transform.position;
                too.z = -10;

                cameraAccess.moveCamera(camera.transform.position, movingToo);
               
            }

            if (focus != null)
            {

                currentTurn.useAbility(this, focus, 1);
               


                if (focus.getHealth() <= 0)
                {
                    focus.death();
                    focus.getGameobject().SetActive(false);
                    players.Remove(focus);

                    entityOrder.Remove(focus);
                    cells[focus.getLocation().x + cellxOffset, focus.getLocation().y + cellyOffset].removeCharacter();
                    getRange(6, focus.getLocation(), new Vector2Int(0, 0), false);

                    

                }
            }
            
            if (players.Count() == 0)
            {
                endLevel();
                objectives.levelFailed();
                
            }
            else
            {
                Turn++;
                nextTurn();
            }
        }
        
    }

    private void movementTurn()
    {
        phase.text = "Movement Phase" ;
        movementAction = true;
        movingToo = currentTurn.getLocation();
        int range = currentTurn.getMovementRange();
        var location = currentTurn.getLocation();
        highlightRangeLoop(range, location, new Vector2Int (0,0));

    }

    private void highlightRangeLoop(int range, Vector3Int location, Vector2Int direction)
    {
        if ( range >= 0)
        {

            var isNot = true;
            foreach (Vector3Int v in highlightedPositions)
            {
                if (v.x == location.x && v.y == location.y)
                {
                    isNot = false;
                }
            }
            if (isNot)
            {
                location.z = cells[location.x + cellxOffset, location.y + cellyOffset].getzAxis();
                if (checkBounds(location))
                {
                    if (tilemap.HasTile(location))
                    {

                        location.z = cells[location.x + cellxOffset, location.y + cellyOffset].getzAxis() + 1;


                        tilemap.SetTile(location, highlightedp);
                        tilemap.SetTileFlags(location, TileFlags.None);
                        Color color = new Color(1.0f, 1.0f, 1.0f, 0.5f);

                        tilemap.SetColor(location, color);

                        highlightedPositions.Add(location);
                    }
                }
            }

            if (attackAction && attackAngle != 0)
            {
                if (attackAngle == 3)
                {
                    highlightAdjacentCellCheck(range, location, new Vector2Int(1, 0));
                    highlightAdjacentCellCheck(range, location, new Vector2Int(-1, 0));
                    highlightAdjacentCellCheck(range, location, new Vector2Int(0, -1));
                    highlightAdjacentCellCheck(range, location, new Vector2Int(0, 1));
                    highlightAdjacentCellCheck(range, location, new Vector2Int(1, 1));
                    highlightAdjacentCellCheck(range, location, new Vector2Int(-1, 1));
                    highlightAdjacentCellCheck(range, location, new Vector2Int(1, -1));
                    highlightAdjacentCellCheck(range, location, new Vector2Int(-1, -1));
                }
                else if (attackAngle == 2)
                {

                    if (direction.x == 1)
                    {
                        if (direction.y == 1)
                        {
                            highlightAdjacentCellCheck(range, location, new Vector2Int(1, 1));
                        }
                        else 
                        {
                            highlightAdjacentCellCheck(range, location, new Vector2Int(1, -1));
                        }
                    }
                    else if(direction.x == -1)
                    {
                        if (direction.y == 1)
                        {
                            highlightAdjacentCellCheck(range, location, new Vector2Int(-1, 1));
                        }
                        else
                        {
                            highlightAdjacentCellCheck(range, location, new Vector2Int(-1, -1));
                        }
                    }
                    else
                    {
                        highlightAdjacentCellCheck(range, location, new Vector2Int(1, 1));
                        highlightAdjacentCellCheck(range, location, new Vector2Int(1, -1));
                        highlightAdjacentCellCheck(range, location, new Vector2Int(-1, 1));
                        highlightAdjacentCellCheck(range, location, new Vector2Int(-1, -1));
                    }
                }
                else
                {

                
                    if (direction.x == 0)
                    {
                        if (direction.y == 1)
                        {
                            highlightAdjacentCellCheck(range, location, new Vector2Int(0, 1));
                        }
                        else if (direction.y == -1)
                        {
                            highlightAdjacentCellCheck(range, location, new Vector2Int(0, -1));
                        }
                        else
                        {
                            highlightAdjacentCellCheck(range, location, new Vector2Int(1, 0));
                            highlightAdjacentCellCheck(range, location, new Vector2Int(-1, 0));
                            highlightAdjacentCellCheck(range, location, new Vector2Int(0, -1));
                            highlightAdjacentCellCheck(range, location, new Vector2Int(0, 1));
                        }
                    }
                    else
                    {
                        if (direction.x == 1)
                        {
                            highlightAdjacentCellCheck(range, location, new Vector2Int(1, 0));
                        }
                        else
                        {
                            highlightAdjacentCellCheck(range, location, new Vector2Int(-1, 0));
                        }
                    }
                }
            }
            else
            {

                if (direction.x == 0)
                {
                    if (direction.y == 1)
                    {
                        highlightAdjacentCellCheck(range, location, new Vector2Int(1, 0));
                        highlightAdjacentCellCheck(range, location, new Vector2Int(-1, 0));
                        highlightAdjacentCellCheck(range, location, new Vector2Int(0, 1));
                    }
                    else if (direction.y == -1)
                    {
                        highlightAdjacentCellCheck(range, location, new Vector2Int(1, 0));
                        highlightAdjacentCellCheck(range, location, new Vector2Int(-1, 0));
                        highlightAdjacentCellCheck(range, location, new Vector2Int(0, -1));
                    }
                    else
                    {
                        highlightAdjacentCellCheck(range, location, new Vector2Int(1, 0));
                        highlightAdjacentCellCheck(range, location, new Vector2Int(-1, 0));
                        highlightAdjacentCellCheck(range, location, new Vector2Int(0, -1));
                        highlightAdjacentCellCheck(range, location, new Vector2Int(0, 1));
                    }
                }
                else
                {
                    if (direction.x == 1)
                    {
                        highlightAdjacentCellCheck(range, location, new Vector2Int(1, 0));
                        highlightAdjacentCellCheck(range, location, new Vector2Int(0, 1));
                        highlightAdjacentCellCheck(range, location, new Vector2Int(0, -1));
                    }
                    else
                    {
                        highlightAdjacentCellCheck(range, location, new Vector2Int(-1, 0));
                        highlightAdjacentCellCheck(range, location, new Vector2Int(0, 1));
                        highlightAdjacentCellCheck(range, location, new Vector2Int(0, -1));
                    }
                }
            }
        }
    }

    private bool highlightAdjacentCellCheck(int range, Vector3Int location, Vector2Int direction)
    {
        var checkk = location;


        
        checkk.x += direction.x;
       
        checkk.y += direction.y;
       

        if (checkBounds(checkk))
        {
            if(attackAction)
            {
                highlightRangeLoop(range - 1, checkk, direction);
            }
            else
            {
                if (checkIfCanPass(checkk))
                {
                     highlightRangeLoop(range - 1, checkk, direction);
                }
            }
        }

        return false;
    }

    private void actionTurn()
    {

    }


    void setUpParty(List<Character> characters)
    {

        foreach (Character i in characters)
        {
            characterOrder.Add(i);
        }

    }

    void setUpEntities(List<Character> ene)
    {
        int charNum = characterOrder.Count;

        int charAdded = 0;
        entityOrder.Add(characterOrder[charAdded]);
        charAdded++;
        int enemyCount = ene.Count;

        float charPerEnemy = (float)enemyCount / charNum;

        for (int i = 0; i < enemyCount; i++)
        {
            if ((float)i / charAdded >= charPerEnemy)
            {
                entityOrder.Add(characterOrder[charAdded]);
                charAdded++;
            }

            entityOrder.Add(ene[i]);
        }
        for (int i = 0; i < entityOrder.Count; i++)
        {
            Debug.Log(entityOrder[i]);
        }

    }

    public void characterMovement(Vector3 touchLastP, Vector3 touchEndP)
    {
        

        Vector3 old = Camera.main.ScreenToWorldPoint(touchLastP);

        Vector3 neew = Camera.main.ScreenToWorldPoint(touchEndP);

        
        Vector3Int from = getSelectedPosition(touchLastP);
        Vector3Int too = getSelectedPosition(touchEndP);


        if (!checkBounds(too))
        {
            return;
        }
        else
        {
            if (cells[too.x + cellxOffset, too.y + cellyOffset].getCharacter() != currentTurn)
            {
                if (!checkIfCanPass(too))
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
                    int z = cells[from.x + cellxOffset, from.y + cellyOffset].getzAxis();

                    from.z = z + 2;

                    if (difx == 1)
                    {
                        if (oldLocation == 1)
                        {
                            movingBack(touchEndP);
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
                            movingBack(touchEndP);
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
                    movingToo = too;
                }

                else if (Mathf.Abs(dify) == 1 && Mathf.Abs(difx) == 0)
                {

                    int z = cells[from.x + cellxOffset, from.y + cellyOffset].getzAxis();

                    from.z = z + 2;

                    if (dify == 1)
                    {
                        if (oldLocation == 2)
                        {
                            movingBack(touchEndP);
                        }
                        else if (movementRange == 0 )
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
                            movingBack(touchEndP);
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
                    movingToo = too;
                }
                
            }
        }
    }

    void movingBack(Vector3 touchEndP)
    {
        
        movementDirections.RemoveAt(movementDirections.Count - 1);

        if (movementDirections.Count == 0)
        {
            oldLocation = 0;
        }
        else
        {
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

    Vector3Int getSelectedPosition(Vector3 loc )
    {

        Vector3 selectWorldPosition = Camera.main.ScreenToWorldPoint(loc);
        Vector3Int tilePos = getIsometricCoordinates(selectWorldPosition);
        Vector3Int otherTilePos = getIsometricCoordinates(selectWorldPosition);
        Vector3Int underTilePos = getIsometricCoordinates(selectWorldPosition);

        Vector3 tileWorldPosition = getNonIsometricCoordinates(tilePos);


        List<Vector3Int> colums = new List<Vector3Int>();

        if (checkBounds(tilePos))
        {
            tilePos.z = cells[tilePos.x + cellxOffset, tilePos.y + cellyOffset].getzAxis();


            if (tilePos.z == 0 )
            {
                return tilePos;
            }


            if (selectWorldPosition.x - tileWorldPosition.x < 0)
            {
                otherTilePos.x += -3;
                otherTilePos.y += -2;

                underTilePos.x += -2;
                underTilePos.y += -2;

            }
            else
            {
                otherTilePos.x += -3;
                otherTilePos.y += -3;

                underTilePos.x += -2;
                underTilePos.y += -3;
            }

            if (checkBounds(otherTilePos))
            {
                otherTilePos.z = cells[otherTilePos.x + cellxOffset, otherTilePos.y + cellyOffset].getzAxis();

                colums.Add(otherTilePos);

            }
            if (checkBounds(underTilePos))
            {
                underTilePos.z = cells[underTilePos.x + cellxOffset, underTilePos.y + cellyOffset].getzAxis();

                colums.Add(underTilePos);
            }

            while (underTilePos.x != tilePos.x)
            {

                otherTilePos.x++;
                otherTilePos.y++;

                underTilePos.x++;
                underTilePos.y++;


                if (checkBounds(otherTilePos))
                {
                    otherTilePos.z = cells[otherTilePos.x + cellxOffset, otherTilePos.y + cellyOffset].getzAxis();
                    colums.Add(otherTilePos);
                }
                if (checkBounds(underTilePos))
                {
                    underTilePos.z = cells[underTilePos.x + cellxOffset, underTilePos.y + cellyOffset].getzAxis();
                    colums.Add(underTilePos);
                }

            }

            float lastz = 0;

            foreach (Vector3Int i in colums)
            {

                Vector3 tempTile = getNonIsometricCoordinates(i);
                float tempx = Mathf.Abs(selectWorldPosition.x - tempTile.x);
                float tempy = Mathf.Abs(selectWorldPosition.y - (tempTile.y + zAxisyIncrease* tempTile.z + tilemap.cellSize.y / 2));


                if (tempx < tilemap.cellSize.x /2)
                {
                    if (tempy < tilemap.cellSize.y / 2)
                    {
                        if((tilemap.cellSize.x / 2 - tempx)*0.5 >= tempy)
                        {
                            return i;
                        }
                    }
                }

                if (lastz < tempTile.z )
                {
                    if (selectWorldPosition.y - (tempTile.y + zAxisyIncrease * tempTile.z + tilemap.cellSize.y / 2) <= - tilemap.cellSize.y / 2)
                    {
                        return i;
                    }
                }
                lastz = tempTile.z;
            }   
        }
        else 
        {
            tileWorldPosition = getNonIsometricCoordinates(tilePos);

            if (selectWorldPosition.x - tileWorldPosition.x < 0)
            {
                otherTilePos.x += -1;
            }
            else
            {
                otherTilePos.y += -1;
            }

            underTilePos.x--;
            underTilePos.y--;


            for (int i = 0; i < 3 ;i++)
            {
                if(checkBounds(otherTilePos))
                {
                    otherTilePos.z = cells[otherTilePos.x + cellxOffset, otherTilePos.y + cellyOffset].getzAxis();

                    Vector3 tempTile = getNonIsometricCoordinates(otherTilePos);
                    float tempx = Mathf.Abs(selectWorldPosition.x - tempTile.x);
                    float tempy = Mathf.Abs(selectWorldPosition.y - (tempTile.y + zAxisyIncrease * tempTile.z + tilemap.cellSize.y / 2));


                    if (tempx < tilemap.cellSize.x / 2)
                    {
                        if (tempy < tilemap.cellSize.y / 2)
                        {
                            if ((tilemap.cellSize.x / 2 - tempx) * 0.5 >= tempy)
                            {
                                return otherTilePos;
                            }
                        }
                    }
                }

                if(checkBounds(underTilePos))
                {
                    underTilePos.z = cells[underTilePos.x + cellxOffset, underTilePos.y + cellyOffset].getzAxis();

                    Vector3 tempTile = getNonIsometricCoordinates(underTilePos);
                    float tempx = Mathf.Abs(selectWorldPosition.x - tempTile.x);
                    float tempy = Mathf.Abs(selectWorldPosition.y - (tempTile.y + zAxisyIncrease * tempTile.z + tilemap.cellSize.y / 2));


                    if (tempx < tilemap.cellSize.x / 2)
                    {
                        if (tempy < tilemap.cellSize.y / 2)
                        {
                            if ((tilemap.cellSize.x / 2 - tempx) * 0.5 >= tempy)
                            {
                                return underTilePos;
                            }
                        }
                    }
                }
                otherTilePos.x--;
                otherTilePos.y--;

                underTilePos.x--;
                underTilePos.y--;
            }
        }
        return tilePos;
    }


    private void getRange(int range, Vector3Int location, Vector2Int direction, bool checks)
    {
        if(range > 0)
        {
            List<Vector2Int> directions = new List<Vector2Int>();

            if (direction.x == 0)
            {
                if (direction.y == 1)
                {
                    checkLocation(range, location, new Vector2Int(1, 0), checks);
                    checkLocation(range, location, new Vector2Int(-1, 0), checks);
                    checkLocation(range, location, new Vector2Int(0, 1), checks);
                }
                else if (direction.y == -1)
                {
                    checkLocation(range, location, new Vector2Int(1, 0), checks);
                    checkLocation(range, location, new Vector2Int(-1, 0), checks);
                    checkLocation(range, location, new Vector2Int(0, -1), checks);
                }
                else
                {
                    checkLocation(range, location, new Vector2Int(1, 0), checks);
                    checkLocation(range, location, new Vector2Int(-1, 0), checks);
                    checkLocation(range, location, new Vector2Int(0, 1), checks);
                    checkLocation(range, location, new Vector2Int(0, -1), checks);
                }
            }
            else
            {
                if (direction.x == 1)
                {
                    checkLocation(range, location, new Vector2Int(1, 0), checks);
                    checkLocation(range, location, new Vector2Int(0, 1), checks);
                    checkLocation(range, location, new Vector2Int(0, -1), checks);
                }
                else
                {
                    checkLocation(range, location, new Vector2Int(-1, 0), checks);
                    checkLocation(range, location, new Vector2Int(0, 1), checks);
                    checkLocation(range, location, new Vector2Int(0, -1), checks);
                }
            }
        }
    }

    private void checkLocation(int range, Vector3Int location, Vector2Int direction, bool c)
    {
        var checkk = location;
        bool vision = false;

        checkk.x += direction.x;

        checkk.y += direction.y;

        if (checkBounds(checkk))
        {
            if (c == true)
            {
                Vector3Int loc = new Vector3Int(checkk.x, checkk.y, location.z);
                loc.z = cells[loc.x + cellxOffset, loc.y + cellyOffset].getzAxis() + 4;

                tilemap.SetTile(loc, null);
                cells[loc.x + cellxOffset, loc.y + cellyOffset].setFog(false);
            }
            else
            {

                foreach (Character cha in players)
                {
                    if (Mathf.Abs(cha.getLocation().x - checkk.x) + Mathf.Abs(cha.getLocation().y - checkk.y) <= 6)
                    {
                        vision = true;
                        break;
                    }
                }


                if (!vision)
                {

                    Vector3Int loc = new Vector3Int(checkk.x, checkk.y, location.z);

                    loc.z = cells[loc.x + cellxOffset, loc.y + cellyOffset].getzAxis() + 4;
                    if (tilemap.HasTile(new Vector3Int(loc.x, loc.y, 0)))
                    {

                        tilemap.SetTile(loc, fog);
                        tilemap.SetTileFlags(loc, TileFlags.None);
                        Color color = new Color(1.0f, 0.4f, 0.0f, 0.5f);
                        tilemap.SetColor(loc, color);
                        cells[loc.x + cellxOffset, loc.y + cellyOffset].setFog(true);
                    }

                }
            }
            getRange(range - 1, checkk, direction, c);

        }
    }


    void playerMoveRefog(Vector3Int from, Vector3Int to)
    {

        Vector3Int tempLoc = from;
        var locations = new List<KeyValuePair<Vector3Int, Vector2Int>>();
        getRange(6, from, new Vector2Int(0, 0), false);

        getRange(6, to, new Vector2Int(0, 0), true);

    }

    bool enemyMoveRefog(Character c)
    {
        Vector3Int loc = c.getLocation();
        if (cells[loc.x + cellxOffset, loc.y + cellyOffset].getFog() == true)
        {
            Renderer tempSprite = c.getGameobject().GetComponent<SpriteRenderer>();
            tempSprite.enabled = false;
            return false;
        }
        else
        {
            Renderer tempSprite = c.getGameobject().GetComponent<SpriteRenderer>();
            tempSprite.enabled = true;
            return true;
        }
    }

    void startGameDefog()
    {
        for (int x = bounds.xMin; x < bounds.xMax; x++)
        {

            for (int y = bounds.yMin; y < bounds.yMax; y++)
            {
                var px = cellxOffset + x;
                var py = cellyOffset + y;


                Vector3Int location = new Vector3Int(px, py, cells[px, py].getzAxis() + 4);
                if (tilemap.HasTile(new Vector3Int(x,y,0)))
                {

                    Vector3Int loc = new Vector3Int(x, y, location.z);

                    tilemap.SetTile(loc, fog);
                    tilemap.SetTileFlags(loc, TileFlags.None);
                    Color color = new Color(1.0f, 0.4f, 0.0f, 0.5f);
                    tilemap.SetColor(loc, color);

                    cells[px, py].setFog(true);
                }
            }
        }
        foreach (Character i in players)
        {
            Vector3Int loc = i.getLocation();

            getRange(6, loc, new Vector2Int(0, 0), true);

        }
        checkVisibleUnits();
    }

    void checkVisibleUnits()
    {
        foreach (Character i in enemie)
        {
            Vector3Int loc = i.getLocation();
            if (cells[loc.x + cellxOffset, loc.y + cellyOffset].getFog() == true)
            {
                Renderer tempSprite = i.getGameobject().GetComponent<SpriteRenderer>();
                tempSprite.enabled = false;
            }
            else
            {
                Renderer tempSprite = i.getGameobject().GetComponent<SpriteRenderer>();
                tempSprite.enabled = true;
            }
        }
    }



    void removeHealthBar()
    {
        foreach(UnityEngine.UI.Image i in HealthListUI)
        {
            i.enabled = (false);
        }
        healthNumber.enabled = false;
    }

    void updateHealthBar(Character cha)
    {
        foreach (UnityEngine.UI.Image i in HealthListUI)
        {
            i.enabled = true;
        }

        healthSlider.value = (float)cha.getHealth()/cha.getMaxHealth();
        healthNumber.enabled = true;
        healthNumber.text = cha.getHealth().ToString() + "/" + cha.getMaxHealth().ToString();
    }


    public void damageDealt(Character cha, int i)
    {
        DamagePopup.Create(damagePopup, cha.getGameobject().transform.position, i);
        cha.setHealth(i);
    }


    public void checkIfSelected(Vector3Int from, Vector3Int too)
    {


        if (selectedPosition == from)
        {
            if (from != too)
            {
                removeHealthBar();
            }
        }
        else if (selectedPosition == too)
        {
            updateHealthBar(currentTurn);
        }
    }

    public int getKills()
    {
        return kills;
    }

    public int getTurn()
    {
        return turns;
    }

    public void endLevel()
    {
        
        enabled = false;
        ability1Button.interactable = (false);
        ability2Button.interactable = (false);
        ability3Button.interactable = (false);
        ability4Button.interactable = (false);

        confirmAbility.interactable = (false);
    }


    public void addEnemy(List<Character> e)
    {
        enemie = new List<Character>();

        foreach (Character enemy in e)
        {

            enemie.Add(enemy);
            setUpUnit(enemy);
        }
    }

    public void haltUpdate()
    {
        enabled = false;
    }
    public void continueUpdate()
    {
        enabled = true;
    }
}
