using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class grid : MonoBehaviour
{

    public Tilemap tilemap;
    private Vector3Int[,] spots;

    // Start is called before the first frame update
    void Start()
    {

            tilemap.CompressBounds();
            var bounds = tilemap.cellBounds;
            spots = new Vector3Int[bounds.size.x, bounds.size.y];

            Debug.Log("Bounds:" + bounds);

            for (int x = 0; x < bounds.size.x; x++)
            {
                for (int y = 0; y < bounds.size.y; y++)
                {
                    var px = bounds.xMin + x;
                    var py = bounds.yMin + y;

                    if (tilemap.HasTile(new Vector3Int(px, py, 0)))
                    {
                        spots[x, y] = new Vector3Int(px, py, 0);
                    }
                    else
                    {
                        spots[x, y] = new Vector3Int(px, py, 1);
                    }
                }
            }



        }


    

    // Update is called once per frame
    void Update()
    {
        
    }
}
