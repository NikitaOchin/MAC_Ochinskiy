using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GQ = MAC_DLL.MAC_Gauss_Quadrature;

namespace MAC_LabWork_5_4
{
    class Main_LW_5_4
    {
        static double a = 0.5, pi = 2.0 * Math.PI;

        static void Main(string[] args)
        {
            int N = 3; GQ gauss = new GQ(N);
            double test = gauss.Gauss_Integral(0.0, pi, f_534, 1.0E-13);
            double orig = 2.0 * Math.PI / Math.Sqrt(1.0 - a * a);

            Console.WriteLine($"    N = {N}  k = " + gauss.txt_k);
            Console.WriteLine($" test = {test,18:F14} ");
            Console.WriteLine($" orig = {orig,18:F14}");
            Console.WriteLine(gauss.txt_xw);
        }

        public static double f_534(double x)
        {
            return 1.0 / (1.0 + a * Math.Sin(x));
        }
    }
}
