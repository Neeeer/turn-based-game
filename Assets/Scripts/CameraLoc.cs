using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLoc : MonoBehaviour
{
    public int interpolationFramesCount = 100; // Number of frames to completely interpolate between the 2 positions
    int elapsedFrames = 0;
    bool on = false;
    Vector3 from;
    Vector3 too;
    public Gridd gridd;

    // Start is called before the first frame update
    public void moveCamera(Vector3 curPos, Vector3 tooPos)
    {
        from = curPos;
        too = tooPos;
        elapsedFrames = 0;
        on = true;
        gridd.haltUpdate();
    }
    void Update()
    {
        if (on)
        {
            float interpolationRatio = (float)elapsedFrames / interpolationFramesCount;

            transform.position = Vector3.Lerp(from, too, interpolationRatio);

            elapsedFrames ++;  // reset elapsedFrames to zero after it reached (interpolationFramesCount + 1)

            if (elapsedFrames >= interpolationFramesCount)
            {
                on = false;
                gridd.continueUpdate();
            }
        }
        

    }

}
