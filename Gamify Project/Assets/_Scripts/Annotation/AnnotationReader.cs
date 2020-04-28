using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class AnnotationReader : MonoBehaviour
{
    public GameObject primitive;
    public bool visualizeReadin = false;
    private static AnnotationReader instance;
    public static AnnotationReader Instance { get => instance; }

    [SerializeField]
    TextAsset textToRead;

    public float scaleFactor = .1f;

    public ITreeNode<AnnotationNode> annotationsAsTree = null;
    public ITreeBuilder<AnnotationNode> treeBuilder = new AnnotationTreeBuilder();

    private AnnotationSet annotationsFromFile = new AnnotationSet();
    private Vector3 averagePos = Vector3.zero;

    
    void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            GameObject.Destroy(this.gameObject);
        }
    }
    void Start()
    {
        Coroutine readFileRoutine = StartCoroutine(ReadAnnoationsFromFile());
    }
    void Update()
    {
        
    }
    public IEnumerator ReadAnnoationsFromFile()
    {
        string[] annotationTextFile = textToRead.text.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
        //requirements
        // #name --
        // #comment --
        // #n type x y z radius parent seg_id level mode timestamp TFresindex
        // 58636 2 12768.667 19584.445 4138.643 1.000 58637 0 8680820738548039680 0 0 0
        //***
        /*notes:
         * Parent = -1 means that this row does not have a parent and is thus the root of the reconstruction.
         * The commonly accepted values for Column 2 are: 1 = cell body (or soma); 2 = axon; and 3 = dendrite.
         */
        annotationsFromFile = new AnnotationSet();
        annotationsFromFile.name = annotationTextFile[0].Substring(5);
        annotationsFromFile.comment = annotationTextFile[1].Substring(8);
        annotationsFromFile.annotations = new List<AnnotationNode>();
        for (long i = 3; i < annotationTextFile.Length; i++)
        {
            string[] data = annotationTextFile[i].Split(' ');
            AnnotationNode lineNode = new AnnotationNode();
            lineNode.n = long.Parse(data[0]);
            lineNode.type = int.Parse(data[1]);
            lineNode.location = new Vector3(float.Parse(data[2]), float.Parse(data[3]), float.Parse(data[4]));
            averagePos += lineNode.location;
            lineNode.radius = float.Parse(data[5]);
            lineNode.parent = long.Parse(data[6]);
            lineNode.seg_id = int.Parse(data[7]);
            lineNode.level = long.Parse(data[8]);
            lineNode.mode = int.Parse(data[9]);
            lineNode.timestamp = int.Parse(data[10]);
            lineNode.TFresindex = int.Parse(data[11]);
            annotationsFromFile.annotations.Add(lineNode);
            
        }
        averagePos /= annotationsFromFile.annotations.Count;
        
        ICollection<ITreeNode<AnnotationNode>> unlinkedForrest = treeBuilder.CreateForrestFromUnstructuredList(annotationsFromFile.annotations);
        annotationsAsTree = new AnnotationTreeNode(new AnnotationNode() { n = -1, parent = -2, location = Vector3.zero }, null);
        foreach (ITreeNode<AnnotationNode> treeHead in unlinkedForrest)
        {
            annotationsAsTree.Children.Add(treeHead);
        }
        if(visualizeReadin)
        DrawAnnotations();
        yield return 1.0d;
    }

    void DrawAnnotations()
    {
        
        scaleFactor = 1.0f;
        foreach (AnnotationNode node in annotationsFromFile.annotations)
        {
            primitive.name = node.n.ToString();
            GameObject obj = GameObject.Instantiate(primitive,(node.location),Quaternion.identity,this.transform);
            obj.tag = "Node";
        }
    }
    
    List<Vector3> ExpandTree(ITreeNode<AnnotationNode> head)
    {
        if (head != null)
        {
            List<Vector3> retval = new List<Vector3>();
            retval.Add(head.Data.location);
            foreach (ITreeNode<AnnotationNode> treeNode in head.Children)
            {
                retval.AddRange(ExpandTree(treeNode));
            }
        }
        return new List<Vector3>();
    }
}

