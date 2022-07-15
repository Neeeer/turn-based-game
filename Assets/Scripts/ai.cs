using System;
using System.Collections.Generic;
using UnityEngine;

public class ai
{
    Cell[,] cells;
    int offsetx;
    int offsety;

    public Tuple <Vector2Int, Character>   pathFinding(Character currentTurn, List<Character> players, Cell[,] c , int offx, int offy )
    {
        offsetx = offx;
        offsety = offy;
        List<Vector2Int> priorityRoute = new List<Vector2Int>();
        cells = c;
        int range = currentTurn.MovementRange;
        Vector2Int loc = new Vector2Int(0, 0);
        loc.x = currentTurn.Location.x + offsetx;
        loc.y = currentTurn.Location.y + offsety;
        Vector2Int closest = loc;
        Character enemyFound = null;
        int attackRange = currentTurn.rangeAbility1();

        bool found = false;

        List<Character> priorityChars = new List<Character>();

        Vector2Int playerLocation = new Vector2Int(0, 0);

       
        // for each player still alive 
        foreach (Character i in players)
        {

            playerLocation.x = i.Location.x + offsetx;
            playerLocation.y = i.Location.y + offsety;

            int xdif = Mathf.Abs(loc.x - playerLocation.x);
            int ydif = Mathf.Abs(loc.y - playerLocation.y);


            // make a priority focus list of player units acording to if reachable, and lowest health, simple ai for now to be updated
            if (priorityChars.Count == 0)
            {
                priorityChars.Add(i);
            }
            else
            {

                for (int j = 0; j < priorityChars.Count ; j++)
                {
                    playerLocation.x = priorityChars[j].Location.x + offsetx;
                    playerLocation.y = priorityChars[j].Location.y + offsety;

                    int tempDiff = Mathf.Abs(loc.x - playerLocation.x) + Mathf.Abs(loc.y - playerLocation.y);

                    

                    if (xdif + ydif <= range + attackRange)
                    {

                        if (tempDiff <= range + attackRange)
                        {
                            if (priorityChars[j].Health > i.Health)
                            {
                                priorityChars.Insert(j, i);
                                break;
                            }
                            else if (j == priorityChars.Count - 1)
                            {
                                priorityChars.Add(i);
                                break;
                            }
                        }
                        else
                        {
                            priorityChars.Insert(j, i);
                            break;
                        }
                    }
                    else
                    { 
                        if (xdif + ydif < tempDiff)
                        {
                            priorityChars.Insert(j, i);
                            break;

                        }
                        else if (j == priorityChars.Count - 1)
                        {
                            priorityChars.Add(i);
                            break;
                        } 
                    }

                }
            }
        }

        int z = 0;

        // if player units are reachable and +-10 health order might be switch to make it a bit more random, in the future order might be altered acording to character classes
        if (priorityChars.Count > 1)
        {
            for (int i = 1; i < priorityChars.Count; i++)
            {

                int xdif = Mathf.Abs(loc.x - (priorityChars[i].Location.x + +offsetx));
                int ydif = Mathf.Abs(loc.y - (priorityChars[i].Location.y + offsety));


                if (xdif + ydif <= range + attackRange)
                {
                    if (priorityChars[0].Health + 10 > priorityChars[i].Health)
                    {
                        z++;
                    }
                }
            }
        }

        if(z != 0)
        {
            System.Random rnd = new System.Random();
            int rand = rnd.Next(0, z);

            if (rand != 0)
            {
                Character tempp = priorityChars[rand];
                priorityChars.RemoveAt(rand);
                priorityChars.Insert(0, tempp);
            }
        }

        // for each character try to reach them using a star algorithm
        foreach (Character i in priorityChars)
        {
            List<Vector2Int> checks = new List<Vector2Int>();

            pathTile[,] map = new pathTile[cells.Length , cells.Length];

            List<Vector2Int> openBracket = new List<Vector2Int>();
            List<Vector2Int> closedBracket = new List<Vector2Int>();

            Vector2Int check = loc;


            if (Mathf.Abs(check.x - (i.Location.x + offsetx)) + Mathf.Abs(check.y - (i.Location.y + offsety)) <= attackRange)
            {
                found = true;
                closest.x = check.x;
                closest.y = check.y;
                enemyFound = i;
            }


            if (attackRange == 1)
            {
                check.x--;
                if (checkPassable(check))
                {
                    checks.Add(check);
                }
                check.x += 2;
                if (checkPassable(check))
                {
                    checks.Add(check);
                }

                check.x--;
                check.y--;

                if (checkPassable(check))
                {
                    checks.Add(check);
                }
                check.y += 2;
                if (checkPassable(check))
                {
                    checks.Add(check);
                }
            }
           

            if(checks.Count == 0 & attackRange == 1)
            {
                
            }
            else
            {
                pathTile tile = new pathTile();
                int xdif = Mathf.Abs(loc.x - (i.Location.x + offsetx));
                int ydif = Mathf.Abs(loc.y - (i.Location.y + offsety));
               
                tile.setPathTile(xdif + ydif, 0, xdif + ydif, null, loc);

                map[tile.getLoc().x, tile.getLoc().y] = tile;
                openBracket.Add(tile.getLoc());

                int leastF = xdif + ydif;
                int leastG = 0;
                int leastH = xdif + ydif;

                pathTile tempTile = null;

                pathTile bestTile = tile;



                while (!found && openBracket.Count != 0)
                {
                    bestTile = tile;
                    leastF = 100;
                    leastG = 0;
                    leastH = 100;


                    int tempG = 0;
                    int tempH = 0;
                    int tempF = 0;

                    

                    foreach (Vector2Int j in openBracket)
                    {
                        tempTile = map[j.x, j.y];

                        if (tempTile.getF() < leastF)
                        {
                            bestTile = tempTile;
                            leastF = tempTile.getF();
                            leastG = tempTile.getG();
                            leastH = tempTile.getH();
                        }
                        else if (tempTile.getF() == leastF)
                        {
                            if (tempTile.getG() < leastG)
                            {
                                bestTile = tempTile;
                                leastF = tempTile.getF();
                                leastG = tempTile.getG();
                                leastH = tempTile.getH();
                            }

                        }
                    }


                    if (bestTile.getG() != range)
                    {


                        xdif = Mathf.Abs(bestTile.getLoc().x + 1 - (i.Location.x + offsetx));
                        ydif = Mathf.Abs(bestTile.getLoc().y - (i.Location.y + offsety));

                        Vector2Int tempLoc = bestTile.getLoc();
                        tempLoc.x++;

                        if (checkPassable(tempLoc))
                        {

                            if (!closedBracket.Contains(tempLoc))
                            {
                                tempG = bestTile.getG() + 1;
                                tempH = ydif + xdif;
                                tempF = tempG + tempH;

                                tile = new pathTile();

                                tile.setPathTile(tempF, tempG, tempH, bestTile, tempLoc);

                                map[tempLoc.x, tempLoc.y] = tile;
                                openBracket.Add(tempLoc);

                                if (ydif + xdif <= attackRange)
                                {
                                    found = true;
                                    closest = tempLoc;
                                    enemyFound = i;
                                }
                            }
                        }
                    



                        xdif = Mathf.Abs(bestTile.getLoc().x - 1 - (i.Location.x + offsetx));

                        tempLoc = bestTile.getLoc();
                        tempLoc.x--;

                        if (checkPassable(tempLoc))
                        {
                            if (!closedBracket.Contains(tempLoc))
                            {

                                tempG = bestTile.getG() + 1;
                                tempH = ydif + xdif;
                                tempF = tempG + tempH;

                                tile = new pathTile();

                                tile.setPathTile(tempF, tempG, tempH, bestTile, tempLoc);

                                map[tempLoc.x, tempLoc.y] = tile;
                                openBracket.Add(tempLoc);

                                if (ydif + xdif <= attackRange)
                                {
                                    found = true;
                                    closest = tempLoc;
                                    enemyFound = i;
                                }
                            }
                        }



                        xdif = Mathf.Abs(bestTile.getLoc().x  - (i.Location.x + offsetx));
                        ydif = Mathf.Abs(bestTile.getLoc().y + 1 - (i.Location.y + offsety));

                        tempLoc = bestTile.getLoc();
                        tempLoc.y++;

                        if (checkPassable(tempLoc))
                        {
                            if (!closedBracket.Contains(tempLoc))
                            {

                                tempG = bestTile.getG() + 1;
                                tempH = ydif + xdif;
                                tempF = tempG + tempH;

                                tile = new pathTile();

                                tile.setPathTile(tempF, tempG, tempH, bestTile, tempLoc);

                                map[tempLoc.x, tempLoc.y] = tile;
                                openBracket.Add(tempLoc);

                                if (ydif + xdif <= attackRange)
                                {
                                    found = true;
                                    closest = tempLoc;
                                    enemyFound = i;
                                }
                            }
                        }


                        ydif = Mathf.Abs(bestTile.getLoc().y - 1 - (i.Location.y + offsety));


                        tempLoc = bestTile.getLoc();
                        tempLoc.y--;

                        if (checkPassable(tempLoc))
                        {
                            if (!closedBracket.Contains(tempLoc))
                            {


                                tempG = bestTile.getG() + 1;
                                tempH = ydif + xdif;
                                tempF = tempG + tempH;

                                tile = new pathTile();

                                tile.setPathTile(tempF, tempG, tempH, bestTile, tempLoc);

                                map[tempLoc.x, tempLoc.y] = tile;
                                openBracket.Add(tempLoc);


                                if (ydif + xdif <= attackRange)
                                {
                                    
                                    found = true;
                                    closest = tempLoc;
                                    enemyFound = i;
                                }
                            }
                        }
                    }

                    openBracket.Remove(bestTile.getLoc());
                    closedBracket.Add(bestTile.getLoc());
                }

                if (i == priorityChars[0] && found == false)
                {
                   
                    foreach (Vector2Int l in closedBracket)
                    {
                        if (map[l.x, l.y].getH() < map[closest.x, closest.y].getH())
                        {
                            closest = l;
                        }
                    }
                }
            }
        }
        return Tuple.Create(closest, enemyFound);
    }

    private bool checkXBounds(Vector2Int loc)
    {
        if (loc.x  < cells.GetLength(0) && loc.x  >= 0)
        {
            return true;
        }
        return false;
    }


    private bool checkYBounds(Vector2Int loc)
    {
       
        if (loc.y < cells.GetLength(1) && loc.y >= 0)
        {
            return true;
        }
        return false;
    }

    private bool checkPassable(Vector2Int loc)
    {

        if (checkXBounds(loc) && checkYBounds(loc))
        {
            if (!cells[loc.x, loc.y].Occupied)
            {
                if (cells[loc.x, loc.y].Passable)
                {
                    return true;
                }
            }
        }
        return false;
    }

}
