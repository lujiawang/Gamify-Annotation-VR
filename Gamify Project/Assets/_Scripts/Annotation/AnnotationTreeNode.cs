using System;
using System.Collections.Generic;
public class AnnotationTreeNode:ITreeNode<AnnotationNode>
{
	public AnnotationNode data;
	public AnnotationNode Data { get => data; set => data= value; }

	public ITreeNode<AnnotationNode> parent;
	public ITreeNode<AnnotationNode> Parent { get => parent; set => parent = value; }

	public ICollection<ITreeNode<AnnotationNode>> children = new List<ITreeNode<AnnotationNode>>();

	public ICollection<ITreeNode<AnnotationNode>> Children { get => children; set => children=value; }

	public AnnotationTreeNode(AnnotationNode data, ITreeNode<AnnotationNode> parent)
	{
		this.data = data;
		this.parent = parent;     
	}
	public ITreeNode<AnnotationNode> Contains(AnnotationNode id)
	{
		if (this.data.Equals(id))
		{
			return this;
		}else if (this.children.Count > 0)
		{
			foreach(ITreeNode<AnnotationNode> childNode in this.children)
			{
				ITreeNode<AnnotationNode> retVal = childNode.Contains(id);
				if(retVal != null)
				{
					return retVal;
				}
			}
			return null;
		}
		else
		{
			return null;
		}
	}

	public override string ToString()
	{
		return String.Format("({0}|({1},{2})",this.data.n,(this.parent==null)?"-1":this.parent.ToString(),this.children.Count.ToString());

	}
}

