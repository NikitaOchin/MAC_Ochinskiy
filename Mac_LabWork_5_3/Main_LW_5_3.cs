using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NCQ = MAC_DLL.MAC_Newton_Cotes;

namespace Mac_LabWork_5_3
{
    class Main_LW_5_3
    {
        static double a = 0.5, pi = 2.0 * Math.PI;

        static void Main(string[] args)
        {
            int N = 17; NCQ Test_NC = new NCQ(N);

            double test = Test_NC.Integral(0.0, pi, f_534, 1.0E-11);
            double orig = 2.0 * Math.PI / Math.Sqrt(1.0 - a * a);

            Console.WriteLine($"      N = {N} " + Test_NC.txt_m);
            Console.WriteLine($"   test = {test,18:F14} ");
            Console.WriteLine($"   orig = {orig,18:F14}\r\n");
            Console.WriteLine(Test_NC.txt_H);
        }

        public static double f_534(double x)
        {
            return 1.0 / (1.0 + a * Math.Sin(x));
        }
    }
}
