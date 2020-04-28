using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    public Animator anim;
    private bool isRun;
    public float rotateSpeed = 0.2f;
    public float moveSpeed = 0.1f;

    public float waitTime = 2f;
    private float count = 0f;

    private DFSWalkAgent agent;
    private List<GameObject> truthNodes;

    public GameObject target;

    
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<DFSWalkAgent>();
        anim = GetComponent<Animator>();
        isRun = false;

        
        //transform.position = GetTarget();
    }

    // Update is called once per frame
    void Update()
    {
        //arrive
        if (transform.position == target.transform.position)
        {
            isRun = false;
            anim.SetBool("isRun", isRun);
        }

        /*
        //get target and run
        if (Input.GetKeyDown("space"))
        {
            isRun = !isRun;
            anim.SetBool("isRun", isRun);
            if (isRun)
            {
                target = GetTarget();
            }
        }*/

        //apply running animation
        if (isRun)
        {
            if (Vector3.Distance(target.transform.position, transform.position) >= 3f)
            {
                transform.position = target.transform.position;
                print("too far");
            }
            else
            {
                Vector3 lookAt = new Vector3(target.transform.position.x - transform.position.x, transform.position.y, target.transform.position.z - transform.position.z);
                Quaternion rot = Quaternion.LookRotation(lookAt);

                //Rotate
                transform.rotation = Quaternion.Slerp(transform.rotation, rot, rotateSpeed * Time.deltaTime);
                //Move
                transform.position = Vector3.MoveTowards(transform.position, target.transform.position, moveSpeed * Time.deltaTime);
            }
        }
        else
        {
            count += Time.deltaTime;
            if (count >= waitTime)
            {
                isRun = true;
                anim.SetBool("isRun", isRun);
                target = GetTarget();
                count = 0f;
            }
        }
    }

    public GameObject GetTarget()
    {
        truthNodes = new List<GameObject>(GameObject.FindGameObjectsWithTag("Node"));
        long nodeId = agent.GetNext();

        foreach (GameObject obj in truthNodes)
        {
            if (obj.name.Equals(nodeId.ToString()+"(Clone)"))
            {
                target = obj;
                agent.ArrivedAt(nodeId);
                return target;
            }
        }
        //reached that target
        
        return target;
    }
    
}
