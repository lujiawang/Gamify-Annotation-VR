using UnityEngine;

public struct AnnotationNode
{
    //guaranteed unique by client
    public long n;
    public int type;
    public Vector3 location;
    public float radius;
    public long parent;
    public int seg_id;
    public long level;
    public int mode;
    public int timestamp;
    public int TFresindex;
    public override string ToString()
    {
        return string.Format("{0} {1} {2} {3} {4} {5} {6} {7} {8} {9} {10} {11}", n, type, location.x, location.y, location.z, radius, parent, seg_id, level, mode, timestamp, TFresindex);
    }

    public override bool Equals(object obj)
    {
        return obj is AnnotationNode && this.n ==((AnnotationNode)obj).n;
    }

    public override int GetHashCode()
    {
        return n.GetHashCode();
    }

    public static bool operator ==(AnnotationNode left, AnnotationNode right)
    {
        return left.Equals(right);
    }

    public static bool operator !=(AnnotationNode left, AnnotationNode right)
    {
        return !(left == right);
    }
}

