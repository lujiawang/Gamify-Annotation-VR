using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class createObject : MonoBehaviour
{
    public Transform spawnPoint;
    public GameObject intersectFlag;
    public GameObject endFlag;

    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.R)) //intersection
        {
            Instantiate(intersectFlag, new Vector3(spawnPoint.position.x, 0f, spawnPoint.position.z), Quaternion.identity);

        }

        if (Input.GetKeyDown(KeyCode.E)) //end
        {
            Instantiate(endFlag, new Vector3(spawnPoint.position.x, 0f, spawnPoint.position.z), Quaternion.identity);
        }
    }
}
