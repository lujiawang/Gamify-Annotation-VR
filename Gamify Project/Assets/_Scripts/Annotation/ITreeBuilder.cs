using System.Collections.Generic;
public interface ITreeBuilder<T>
{
    ICollection<ITreeNode<T>> CreateForrestFromUnstructuredList(IList<T> unstructuredData);
}