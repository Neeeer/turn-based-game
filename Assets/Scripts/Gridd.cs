using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;



public class Gridd : MonoBehaviour
{

    public Tilemap tilemap;
    public TileHighlighter tileHighlighter;
    private TileSelect tileSelector;
    private Fog fog;

    private Cell[,] cells;
    public lvlEnemies lvlenemies;
    public objectives objectives;
    public ButtonSelector ButtonSelect;

    public new Camera camera;
    public Inputs inputs;

    private Character currentTurn;

    private int cellxOffset;
    private int cellyOffset;

    private float zAxisyIncrease = (float)0.36;
    private int kills;
    private int turns;

    public Druid character1;
    public Frog character2;
    public Assasin character3;
    public Paladin character4;


    public GameObject char1;
    public GameObject char2;
    public GameObject char3;
    public GameObject char4;


    public Healthbar healthbar;


    public Text phase;


    public Button confirmAbility;


    List<Character> characterOrder;
    List<Character> entityOrder;
    List<Character> players;
    List<Character> enemie;

    private bool movementAction = false;
    private bool attackAction = false;

    private int Turn;

    private BoundsInt bounds;

    private Tile lavaTile;

    private int abilitySelected = 0;
    private Vector3Int movingToo;


    [SerializeField] private Transform damagePopup;


    private void Awake()
    {
        Turn = 0;

        characterOrder = new List<Character>();
        entityOrder = new List<Character>();
        players = new List<Character>();

        movingToo = new Vector3Int(0, 0, 0);

        lavaTile = Resources.Load<Tile>("isometric tilemap/25-ground-blocks/lava");


        TilemapSetup tilemapSetup = new TilemapSetup();
        tilemap.CompressBounds();
        bounds = tilemap.cellBounds;
        cellxOffset = Mathf.Abs(bounds.xMin);
        cellyOffset = Mathf.Abs(bounds.yMin);

        cells = tilemapSetup.setUpTilemap(tilemap, lavaTile);


        setUpParty();

        tileSelector = new TileSelect(this, tilemap, cells);
        fog = new Fog(this, tilemap, cells);

        
    }



    // Start is called before the first frame update
    void Start()
    {
        addEnemy(lvlenemies.getEnemies());

        setUpEntities(enemie);
        fog.startGameDefog();
        healthbar.removeHealthBar();
        nextTurn();
        
    }

     IEnumerator startGame()
    {

        yield return new WaitForSeconds(3);
        nextTurn();
    }

    public void selectAbility()
    {
        tileHighlighter.highlightAbility();
    }

    public void endTurn()
    {
        if (getMovementAction())
        {
            
            inputs.emptyMovementList();
            inputs.emptyDirectionList();
            tileHighlighter.emptyHighlightedList();
            attackAction = true;
            abilitySelected = 1;
            ButtonSelect.ability1Button.Select();

            ButtonSelect.ability1Button.interactable = (true);
            ButtonSelect.ability2Button.interactable = (true);
            ButtonSelect.ability3Button.interactable = (true);
            ButtonSelect.ability4Button.interactable = (true);

            movementAction = false;
            ButtonSelect.selectAbility1();
            phase.text = "Action Phase";


        }
        else if (getAttackAction())
        {
            tileHighlighter.emptyHighlightedList();
            tileHighlighter.emptyAffectedList();

            movementAction = false;
            attackAction = false;
            ButtonSelect.ability1Button.interactable = (false);
            ButtonSelect.ability2Button.interactable = (false);
            ButtonSelect.ability3Button.interactable = (false);
            ButtonSelect.ability4Button.interactable = (false);

            tileHighlighter.removeCurrentTurnTile(currentTurn.getLocation());
            Turn++;
            nextTurn();

        }

    }

    public void confirmAction()
    {
        if (getMovementAction())
        {


            var currentCharacterLocation = currentTurn.getLocation();

            tileHighlighter.removeCurrentTurnTile(currentCharacterLocation);

            Vector3 v = getNonIsometricCoordinatesGo(movingToo);

            v.z = cells[movingToo.x + getXoffset(), movingToo.y + getYoffset()].getzAxis();

            v.y += zAxisyIncrease * v.z;
            v.y = Mathf.Round(v.y * 100f) / 100f;


            checkIfSelected(currentCharacterLocation, movingToo);

            cells[currentCharacterLocation.x + getXoffset(), currentCharacterLocation.y + getYoffset()].removeCharacter();
            cells[movingToo.x + getXoffset(), movingToo.y + getYoffset()].setCharacter(currentTurn);


            getCurrentTurn().setLocation(movingToo);
            getCurrentTurn().getGameobject().transform.position = v;

            tileHighlighter.setCurrentTurnTile(movingToo);


            fog.playerMoveRefog(currentCharacterLocation, movingToo);

            endTurn();
        }
        else if (getAttackAction())
        {
            foreach (Vector3Int s in tileHighlighter.getAffectedPositions())
            {

                var position = s;
                position.z = cells[position.x + getXoffset(), position.y + getYoffset()].getzAxis();

                if (cells[position.x + getXoffset(), position.y + getYoffset()].getOcupied() == true)
                {
                    Character currentChara = cells[position.x + getXoffset(), position.y + getYoffset()].getCharacter();

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
                        cells[currentChara.getLocation().x + getXoffset(), currentChara.getLocation().y + getYoffset()].removeCharacter();
                    }
                }
            }
            endTurn();
        }

    }

    


    


    

    public bool checkBounds(Vector3Int v)
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

    

    public bool checkIfCanPass(Vector3Int v)
    {
        if (!cells[v.x + getXoffset(), v.y + getYoffset()].getOcupied())
        {
            if (cells[v.x + getXoffset(), v.y + getYoffset()].getPassable())
            {
                return true;
            }
        }
        return false;
    }


    public Vector3Int getIsometricCoordinates(Vector3 z)
    {
        double tempx = z.x;
        double tempy = z.y;
        z.x = (float)(tempy * 2 + tempx);
        z.y = (float)(tempy * 2 - tempx);

        Vector3Int zInt = Vector3Int.FloorToInt(z);

        return zInt;
    }

    public Vector3 getNonIsometricCoordinates(Vector3Int zInt)
    {

        Vector3 z = zInt;
        float tempx = z.x;
        float tempy = z.y;
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
        cells[zzz.x + getXoffset(), zzz.y + getYoffset()].setCharacter(c);
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

            tileHighlighter.setCurrentTurnTile(currentTurn.getLocation());

            CameraLoc cameraAccess = camera.GetComponent<CameraLoc>();
            Vector3 too = currentTurn.getGameobject().transform.position;
            too.z = -10;
            cameraAccess.moveCamera(camera.transform.position, too);

            ButtonSelect.setCurrentTurnAbilities();

            movementTurn();
        }
        else
        {
            ai aintelligence = new ai();
            var result = aintelligence.pathFinding(currentTurn, players, cells, getXoffset(), getYoffset());
            Vector2Int dest = result.Item1;
            Character focus = result.Item2;
            Vector3Int destination = new Vector3Int(dest.x, dest.y, 0);


            destination.z = cells[destination.x, destination.y].getzAxis();

            checkIfSelected(currentTurn.getLocation(), destination);



            cells[currentTurn.getLocation().x + getXoffset(), currentTurn.getLocation().y + getYoffset()].removeCharacter();
            cells[destination.x, destination.y].setCharacter(currentTurn);




            destination.x = destination.x - getXoffset();
            destination.y = destination.y - getYoffset();
            currentTurn.setLocation(destination);

            Vector3 too = getNonIsometricCoordinatesGo(destination);


            too.y += zAxisyIncrease * too.z;
            too.y = Mathf.Round(too.y * 100f) / 100f;

            currentTurn.getGameobject().transform.position = too;


            if (fog.enemyMoveRefog(currentTurn))
            {

                CameraLoc cameraAccess = camera.GetComponent<CameraLoc>();
                Vector3 movingToo = currentTurn.getGameobject().transform.position;

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
                    cells[focus.getLocation().x + getXoffset(), focus.getLocation().y + getYoffset()].removeCharacter();
                    fog.checkAreaFog(6, focus.getLocation(), new Vector2Int(0, 0), false);

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
        phase.text = "Movement Phase";
        movementAction = true;
        movingToo = currentTurn.getLocation();
        int range = currentTurn.getMovementRange();
        var location = currentTurn.getLocation();
        tileHighlighter.highlightTiles(range, location);
    }


    private void setUpParty()
    {
        character1 = new Druid();
        character2 = new Frog();
        character3 = new Assasin();
        character4 = new Paladin();

        character1.setGameobject(char1);
        character2.setGameobject(char2);
        character3.setGameobject(char3);
        character4.setGameobject(char4);

        players.Add(character1);
        players.Add(character2);
        players.Add(character3);
        players.Add(character4);

        foreach (Character player in players)
        {
            setUpUnit(player);
            characterOrder.Add(player);
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

    }



    public void damageDealt(Character cha, int i)
    {
        DamagePopup.Create(damagePopup, cha.getGameobject().transform.position, i);
        cha.setHealth(i);
    }


    public void checkIfSelected(Vector3Int from, Vector3Int too)
    {


        if (inputs.getSelectedPosition() == from)
        {
            if (from != too)
            {
                healthbar.removeHealthBar();
            }
        }
        else if (inputs.getSelectedPosition() == too)
        {
            healthbar.updateHealthBar(currentTurn);
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
        ButtonSelect.disableButtons();
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


    public Character getCurrentTurn()
    {
        return currentTurn;
    }

    public bool getAttackAction()
    {
        return attackAction;
    }

    public bool getMovementAction()
    {
        return movementAction;
    }


    public int getXoffset()
    {
        return cellxOffset;
    }

    public int getYoffset()
    {
        return cellyOffset;
    }



    public List<Character> getPlayers()
    {
        return players;
    }

    public BoundsInt getBounds()
    {
        return bounds;
    }

    public Vector3Int selectATile(Vector3 location) { 
        

       Vector3Int test = tileSelector.getCorrectSelectedPosition(location);
        
        //Debug.Log(" press or release" + test);

        /*
        if (checkBounds(test))
        {

            Debug.Log("fog" + cells[test.x + getXoffset(), test.y + getYoffset()].getzAxis());
            Debug.Log("fog" + cells[test.x + getXoffset(), test.y + getYoffset()].getFog());

        }
        */
        return test;
    }

    
    public TileSelect getTileSelector()
    {
        return tileSelector;
    }
    public Cell[,] getCellGrid()
    {
        return cells;
    }
    

    public void setMovingToo(Vector3Int v)
    {
        movingToo = v;
    }

    public List<Character> getEnemies()
    {
        return enemie;
    }


}
