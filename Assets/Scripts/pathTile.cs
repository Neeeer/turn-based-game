using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pathTile 
{



    int f = 0;
    int g = 0;
    int h = 0;

    pathTile lastTile = null;
    Vector2Int loc = new Vector2Int(0,0);

    public void setPathTile(int ff, int gg, int hh, pathTile last, Vector2Int l )
    {
        f = ff;
        g = gg;
        h = hh;
        lastTile = last;
        loc = l;
    }

    public pathTile getPath()
    {
        return lastTile;
    }

    public int getF()
    {
        return f;
    }
    public int getG()
    {
        return g;
    }

    public int getH()
    {

        return h;

    }


    public void setF(int ff)
    {
        f = ff;
    }
    public void setG(int gg)
    {
        g = gg;

    }
    public void setH(int hh)
    {
        h = hh;

    }


    public Vector2Int getLoc()
    {
        return loc;
    }


    public void setLoc(Vector2Int l)
    {
        loc = l;
    }
}
