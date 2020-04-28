using System.Collections.Generic;
public class AnnotationTreeBuilder:ITreeBuilder<AnnotationNode>
{
    public ICollection<ITreeNode<AnnotationNode>> CreateForrestFromUnstructuredList(IList<AnnotationNode> annotations)
    {
        List<ITreeNode<AnnotationNode>> unlinkedForrest = new List<ITreeNode<AnnotationNode>>();
        for (int annotationPos = 0; annotationPos < annotations.Count; annotationPos++)
        {
            AnnotationNode current = annotations[annotationPos];

            ITreeNode<AnnotationNode> nodeToAdd = new AnnotationTreeNode(current, null);

            for (int childCandidatePos = unlinkedForrest.Count - 1; childCandidatePos > 0; childCandidatePos--)
            {
                if (unlinkedForrest[childCandidatePos].Data.parent == current.n)
                {
                    ITreeNode<AnnotationNode> removedTree = unlinkedForrest[childCandidatePos];
                    removedTree.Parent = nodeToAdd;
                    nodeToAdd.Children.Add(removedTree);
                    unlinkedForrest.RemoveAt(childCandidatePos);
                }
            }

            bool insertFound = false;
            for (int parentCandidatePos = unlinkedForrest.Count - 1; parentCandidatePos > 0; parentCandidatePos--)
            {
                ITreeNode<AnnotationNode> treeHead = unlinkedForrest[parentCandidatePos];
                ITreeNode<AnnotationNode> insertLocation = treeHead.Contains(current);
                if (insertLocation != null)
                {
                    ITreeNode<AnnotationNode> newNode = new AnnotationTreeNode(current, insertLocation);
                    insertLocation.Children.Add(newNode);
                    insertFound = true;
                    break;
                }
            }
            if (!insertFound)
            {
                unlinkedForrest.Add(new AnnotationTreeNode(current, null));
            }
        }
        return unlinkedForrest;
    }
}

