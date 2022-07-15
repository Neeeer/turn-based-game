using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileSelect 
{
    Tilemap tilemap;
    Cell[,] cells;
    LevelLogic grid;

    private float zAxisyIncrease = (float)0.36;

    public TileSelect(LevelLogic g, Tilemap map, Cell[,] cellsMap)
    {
        grid = g;
        cells = cellsMap;
        tilemap = map;
    }



    public Vector3Int getCorrectSelectedPosition(Vector3 loc)
    {


        Vector3 selectWorldPosition = Camera.main.ScreenToWorldPoint(loc);
        Vector3Int tilePos = grid.getIsometricCoordinates(selectWorldPosition);
        Vector3Int otherTilePos = grid.getIsometricCoordinates(selectWorldPosition);
        Vector3Int underTilePos = grid.getIsometricCoordinates(selectWorldPosition);

        Vector3 tileWorldPosition = grid.getNonIsometricCoordinates(tilePos);


        List<Vector3Int> colums = new List<Vector3Int>();

        if (grid.checkBounds(tilePos))
        {
            tilePos.z = cells[tilePos.x + grid.getXoffset(), tilePos.y + grid.getYoffset()].zAxis;


            if (tilePos.z == 0)
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

            if (grid.checkBounds(otherTilePos))
            {
                otherTilePos.z = cells[otherTilePos.x + grid.getXoffset(), otherTilePos.y + grid.getYoffset()].zAxis;

                colums.Add(otherTilePos);

            }
            if (grid.checkBounds(underTilePos))
            {
                underTilePos.z = cells[underTilePos.x + grid.getXoffset(), underTilePos.y + grid.getYoffset()].zAxis;

                colums.Add(underTilePos);
            }

            while (underTilePos.x != tilePos.x)
            {

                otherTilePos.x++;
                otherTilePos.y++;

                underTilePos.x++;
                underTilePos.y++;


                if (grid.checkBounds(otherTilePos))
                {
                    otherTilePos.z = cells[otherTilePos.x + grid.getXoffset(), otherTilePos.y + grid.getYoffset()].zAxis;
                    colums.Add(otherTilePos);
                }
                if (grid.checkBounds(underTilePos))
                {
                    underTilePos.z = cells[underTilePos.x + grid.getXoffset(), underTilePos.y + grid.getYoffset()].zAxis;
                    colums.Add(underTilePos);
                }

            }

            float lastz = 0;

            foreach (Vector3Int i in colums)
            {

                Vector3 tempTile = grid.getNonIsometricCoordinates(i);
                float tempx = Mathf.Abs(selectWorldPosition.x - tempTile.x);
                float tempy = Mathf.Abs(selectWorldPosition.y - (tempTile.y + zAxisyIncrease * tempTile.z + tilemap.cellSize.y / 2));


                if (tempx < tilemap.cellSize.x / 2)
                {
                    if (tempy < tilemap.cellSize.y / 2)
                    {
                        if ((tilemap.cellSize.x / 2 - tempx) * 0.5 >= tempy)
                        {
                            return i;
                        }
                    }
                }

                if (lastz < tempTile.z)
                {
                    if (selectWorldPosition.y - (tempTile.y + zAxisyIncrease * tempTile.z + tilemap.cellSize.y / 2) <= -tilemap.cellSize.y / 2)
                    {
                        return i;
                    }
                }
                lastz = tempTile.z;
            }
        }
        else
        {
            tileWorldPosition = grid.getNonIsometricCoordinates(tilePos);

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


            for (int i = 0; i < 3; i++)
            {
                if (grid.checkBounds(otherTilePos))
                {
                    otherTilePos.z = cells[otherTilePos.x + grid.getXoffset(), otherTilePos.y + grid.getYoffset()].zAxis;

                    Vector3 tempTile = grid.getNonIsometricCoordinates(otherTilePos);
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

                if (grid.checkBounds(underTilePos))
                {
                    underTilePos.z = cells[underTilePos.x + grid.getXoffset(), underTilePos.y + grid.getYoffset()].zAxis;

                    Vector3 tempTile = grid.getNonIsometricCoordinates(underTilePos);
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
}
