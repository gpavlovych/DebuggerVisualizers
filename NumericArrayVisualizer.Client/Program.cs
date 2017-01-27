using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NumericArrayVisualizer.Client
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            double[] myString = new double[43434];
            for (var i = 0; i < myString.Length; i++)
            {
                myString[i] = Math.Sin(2*Math.PI*i/myString.Length);
            }
            float[][] myString2 = new float[434][];
            for (var i = 0; i < myString2.Length; i++)
            {
                myString2[i] = new float[400];
                for (var j = 0; j < myString2[i].Length; j++)
                {myString2[i][j] = (float)(Math.Sin(2 * Math.PI * i / myString2.Length)*Math.Sin(2 * Math.PI * j / myString2[i].Length)); }
            }
            DebuggerSide.TestShowVisualizer(myString2);
        }
    }
}
