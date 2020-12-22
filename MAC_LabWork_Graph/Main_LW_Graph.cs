using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fwd = MAC_Graph_DLL.Form_with_Data;
using ToD = MAC_DLL.MAC_My_Definitions.MyTableOfData;

namespace MAC_LabWork_Graph
{
    class Main_LW_Graph
    {
        static void Main(string[] args)
        {
            //Test_Sin(20, -Math.PI, Math.PI, 800, 300);

            //Fwd.SingleGraphXY(new ToD("LW_Graph_0.bin", "Test_BIN"), 500, 300);
            Fwd.DoublyGraph(new ToD("LW_Graph_0.bin", "Test_BIN"), cos, 500, 300);
        }

        static void Test_Sin(int n, double xo, double xn, int N, int M)
        {
            double h = (xn - xo) / n;
            double[] x = new double[n + 1];
            double[] f = new double[n + 1];

            for (int i = 0; i <= n; i++)
            {
                x[i] = xo + i * h; f[i] = Math.Sin(x[i]);
            }

            Fwd.SingleGraphXY(x, f, " My Graph of Sin(x) ", N, M);
        }

        static double cos(double t)
        {
            return Math.Cos(t);
        }

    
    }
}
