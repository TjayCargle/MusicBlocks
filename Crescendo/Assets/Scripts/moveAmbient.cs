using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveAmbient : MonoBehaviour
{

    public List<Vector3> startLocations;
    public List<Vector3> targetLocations;
    int cindex = 0;

    // Update is called once per frame
    void FixedUpdate()
    {
        if (cindex < targetLocations.Count)
        {
            if (Vector3.Distance(targetLocations[cindex], transform.position) < 2.0f)
            {
                transform.Translate(Vector3.MoveTowards(transform.position, startLocations[cindex], 2.0f));
            }
            else
            {
                cindex++;
                if (cindex >= startLocations.Count)
                {
                    cindex = 0;
                    if(startLocations.Count > 0)
                    {
                        transform.position = startLocations[cindex];
                    }
                }
            }
        }
    }
}
