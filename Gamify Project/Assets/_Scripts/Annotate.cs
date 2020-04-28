using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Annotate : MonoBehaviour
{
    public Camera camera;
    public GameObject annoPrefab;
    public GameObject linePrefab;

    private int annoNum;
    private AnnotationNode parentAnno;
    private Vector3 parentPos;
    private AnnotationSet nodeSet;
    public AnnotationSet NodeSet
    {
        get => nodeSet;
        set => nodeSet = value;
    }


    void Start()
    {
        annoNum = 0;
        ResetParent();

        nodeSet = new AnnotationSet();
        nodeSet.annotations = new List<AnnotationNode>();
        nodeSet.name = "userData";
        nodeSet.comment = "";
    }

    void Update()
    {
        RaycastHit hit;
        Ray ray = camera.ScreenPointToRay(Input.mousePosition);

        if (Input.GetKeyDown(KeyCode.R))
        {
            ResetParent();
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            AnnotationPrinter.PrintFile("testdata", nodeSet);
        }

        if (Physics.Raycast(ray, out hit))
        {
            Transform objectHit = hit.transform;

            //If space is pressed, drop an annotation point
            if (Input.GetKeyDown(KeyCode.Space))
            {
                AddAnnotation(hit);
            }
        }
    }

    void AddAnnotation(RaycastHit hit)
    {
        Debug.Log("Annotation attempted");

        while(true/*annoNum is used in current anno data*/)
        {
            //annoNum++;
            break;
        }
        AnnotationNode node = new AnnotationNode();

        node.n = annoNum;
        annoNum++;

        node.type = 2;

        node.location = hit.point;

        node.radius = 1;

        node.parent = parentAnno.n;

        node.seg_id = 0;

        node.level = 8680820738548039680;

        node.mode = 0;

        node.timestamp = 0;

        node.TFresindex = 0;

        //Place an annotation point
        PlaceAnnotationPoint(node);
        //Set node as parent for next node
        parentAnno = node;
        //Set node pos as parent pos for next node
        parentPos = node.location;
        //Add finished node to set
        nodeSet.annotations.Add(node);

        Debug.Log("Anno Results");
        Debug.Log("Node: " + node);
        Debug.Log("Set: " + nodeSet);
    }

    void PlaceAnnotationPoint(AnnotationNode node)
    {
        Instantiate(annoPrefab, node.location, Quaternion.identity);

        if(node.parent != -1)
        {
            GameObject line = Instantiate(linePrefab);
            LineRenderer renderer = line.GetComponent<LineRenderer>();
            renderer.SetPosition(0, parentPos);
            renderer.SetPosition(1, node.location);
        }
    }

    void PlaceAnnotationData(AnnotationSet annoDataSet)
    {
        for (int i = 0; i < annoDataSet.annotations.Count; i++)
        {
            AnnotationNode currentNode = annoDataSet.annotations[i];
            if (currentNode.parent != -1)
            {
                parentAnno = annoDataSet.annotations[(int)currentNode.parent];
                parentPos = parentAnno.location;
            }
            PlaceAnnotationPoint(annoDataSet.annotations[i]);
        }
    }

    void ResetParent()
    {
        parentAnno = new AnnotationNode();
        parentPos = new Vector3();
        parentAnno.n = -1;
    }
}