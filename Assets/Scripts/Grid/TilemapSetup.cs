using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TilemapSetup 
{

    public Cell[,] setUpTilemap(Tilemap tilemap, Tile lava)
    {

        BoundsInt bounds = tilemap.cellBounds;
        Cell[,] cells = new Cell[bounds.size.x, bounds.size.y];

        int tilez = 0;

        int cellxOffset = Mathf.Abs(bounds.xMin);
        int cellyOffset = Mathf.Abs(bounds.yMin);

        for (int x = bounds.xMin; x < bounds.xMax; x++)
        {

            for (int y = bounds.yMin; y < bounds.yMax; y++)
            {
                var px = cellxOffset + x;
                var py = cellyOffset + y;

                Cell cell = new Cell();

                cells[px, py] = cell;

                for (int i = 0; i < bounds.size.z; i++)
                {

                    if (tilemap.HasTile(new Vector3Int(x, y, i)))
                    {

                        tilez = i;
                        cells[px, py].zAxis = tilez;
                        var tilemapTile = tilemap.GetTile(new Vector3Int(x, y, i));
                        if (tilemapTile == lava)
                        {
                            cells[px, py].Passable = false;
                        }
                        else
                        {
                            cells[px, py].Passable = true;
                        }
                    }
                }
            }
        }
        return cells;
    }
}
