using System.Collections.Generic;
public interface ITreeNode<T>:ITreeSearchBehavior<T>
{
	T Data { get; set; }
	ITreeNode<T> Parent { get; set; }
	ICollection<ITreeNode<T>> Children { get; set; }
}