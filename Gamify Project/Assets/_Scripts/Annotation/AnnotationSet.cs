using System;
using System.Collections.Generic;
using System.Text;

public struct AnnotationSet
{
    public string name;
    public string comment;
    public List<AnnotationNode> annotations;

    public override string ToString()
    {
        string s = string.Format("({0}:{1}{2}", name, comment, Environment.NewLine);
        StringBuilder annotationBuilder = new StringBuilder();
        foreach (AnnotationNode node in annotations)
        {
            annotationBuilder.Append(node.ToString() + Environment.NewLine);
        }
        return s + annotationBuilder.ToString();
    }
}

