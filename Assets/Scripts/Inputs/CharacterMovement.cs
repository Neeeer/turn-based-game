using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{

    public int interpolationFramesCount ; // Number of frames to completely interpolate between the 2 positions
    int elapsedFrames = 0;
    List<Vector3Int> movementPositions;
    List<Vector3> screenPositions;
    int indexTile;
    public LevelLogic grid;
    private Cell[,] cells;
    private Character currentTurn;

    bool on = false;


    public void moveCharacter( List<Vector3Int> tooPos, Vector3Int movingPos)
    {
        movementPositions = new List<Vector3Int>(tooPos) ;
        movingPos.z += 2;
        movementPositions.Add(movingPos);
        screenPositions = new List<Vector3>();
        cells = grid.getCellGrid();


        if (movementPositions.Count == 1)
        {
            grid.endTurn();
            return;
        }
        
        for (int i = 0; i <movementPositions.Count ;i++ )
        {
            Vector3Int tile = movementPositions[i];
            
            Vector3 v = grid.getNonIsometricCoordinatesForSprite(tile);

            v.z = cells[tile.x + grid.getXoffset(), tile.y + grid.getYoffset()].zAxis;

            v.y += grid.zAxisyIncrease * v.z;
            v.y = Mathf.Round(v.y * 100f) / 100f;

            screenPositions.Add(v);
        }

        elapsedFrames = 0;
        indexTile = 0;
        currentTurn = grid.getCurrentTurn();
        on = true;
        StartCoroutine("characterMovement");

    }


    IEnumerator characterMovement()
    {
        while (on)
        {
            float interpolationRatio = (float)elapsedFrames / interpolationFramesCount;

            currentTurn.CharGameobject.transform.position = Vector3.Lerp(screenPositions[indexTile], screenPositions[indexTile + 1], interpolationRatio);

            elapsedFrames++;  // reset elapsedFrames to zero after it reached (interpolationFramesCount + 1)
            if (elapsedFrames >= interpolationFramesCount)
            {
                indexTile++;
                elapsedFrames = 0;

                if (indexTile == movementPositions.Count - 1)
                {
                    currentTurn.CharGameobject.transform.position = screenPositions[indexTile];
                    on = false;
                }

                // checks later

            }
            yield return new WaitForSeconds(0.01f);
        }
        grid.endTurn();
    }
}
