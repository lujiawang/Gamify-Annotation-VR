using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrainObjUtility : MonoBehaviour
{
    public Vector3 minExtents;
    public Vector3 maxExtents;

    MeshRenderer renderer;

    void Start()
    {/*
        Bounds dataBounds = new Bounds() { max = maxExtents, min = minExtents };
        transform.position = dataBounds.center;
        //transform.localScale = dataBounds.size;
        //transform.localScale = -1 * transform.localScale;
    
        */
    }

    void OnDrawGizmos()
    {
    }
}
