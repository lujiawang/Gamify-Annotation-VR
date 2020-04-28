using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DFSWalkAgent : MonoBehaviour
{
    List<long> visitedNodeIDs = new List<long>();
    long currentNodeID = -1;
    void Awake()
    {
        visitedNodeIDs.Add(-1);
    }

    public long GetNext()
    {
        ITreeNode<AnnotationNode> currentNode = AnnotationReader.Instance.annotationsAsTree;

        do
        {
            foreach (ITreeNode<AnnotationNode> child in currentNode.Children)
            {

                if (!visitedNodeIDs.Contains(child.Data.n))
                {
                    return child.Data.n;
                }
            }
            
            currentNode = currentNode.Parent;
        } while (currentNode.Parent != null);
        return -1;
    }
    public bool ArrivedAt(long id)
    {
        visitedNodeIDs.Add(id);
        currentNodeID = id;
        return true;
    }

}
