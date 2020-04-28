using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    public Animator anim;
    private bool isRun;
    //public Transform target;
    public float rotateSpeed = 5;
    public float moveSpeed = 5;


    DFSWalkAgent agent = new DFSWalkAgent();
    private Vector3 target;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        isRun = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            isRun = !isRun;
            anim.SetBool("isRun", isRun);
        }
        if (isRun)
        {
            Vector3 lookAt = new Vector3(target.position.x - transform.position.x, transform.position.y, target.position.z - transform.position.z);
            Quaternion rot = Quaternion.LookRotation(lookAt);

            //Rotate
            transform.rotation = Quaternion.Slerp(transform.rotation, rot, rotateSpeed * Time.deltaTime);
            //Move
            transform.position = Vector3.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);

        }
    }
    
}
