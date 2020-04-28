using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class AnnotationPrinter
{
    public static void PrintFile(String fileName, AnnotationSet set)
    {
        using (System.IO.StreamWriter file =
            new System.IO.StreamWriter(@"C:\Users\Public\" + fileName + ".txt"))
        {
            for(int i = 0; i < set.annotations.Count; i++)
            {
                file.WriteLine(set.annotations[i]);
            }
        }
    }
}

