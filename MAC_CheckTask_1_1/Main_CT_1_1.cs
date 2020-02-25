using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CLS = MAC_DLL.MAC_Series;

namespace MAC_CheckTask_1_1
{
    class Main_CT_1_1
    {
        static int N = 12800;
        static double a = 1.77, b = -2.82, c = 0.81, d = 2.42;
        static double eps = 1.0E-9, delta = 1.0E-8;

        static void Main(string[] args)
        {
            int kf = 0;
            double S1_N = CLS.Sum_of_Number_Series(0,N,Series_SN);
            Console.WriteLine($"{N,8}{S1_N,20:F10}\r\n");
            double S1_A = CLS.Sum_of_Number_Series_A(-1,eps, Series_S1, ref kf);
            Console.WriteLine($"{kf,8}{S1_A,20:F10}\r\n");
            double S1_D = CLS.Sum_of_Number_Series_D(1, delta, Series_S2,ref kf);
            Console.WriteLine($"{kf,8}{S1_D,20:F10}\r\n");

            N = 10000;
            a = 1.20; b = -2.10; c = -0.50; d = 1.40;
            double S1_NT = CLS.Sum_of_Number_Series(0, N, Series_SN);
            Console.WriteLine($"{N,8}{S1_NT,20:F10}\r\n");
            double S1_AT = CLS.Sum_of_Number_Series_A(-1, eps, Series_S1, ref kf);
            Console.WriteLine($"{kf,8}{S1_AT,20:F10}\r\n");
            double S1_DT = CLS.Sum_of_Number_Series_D(1, delta, Series_S2, ref kf);
            Console.WriteLine($"{kf,8}{S1_DT,20:F10}\r\n");


        }

        public static double Series_SN(int k) 
        {
            double s1 = Math.Pow(k + 1, 1.0/4.0);
            double s2 = 3.0 * k * k - 1;
            double s3 = Math.Sqrt(k) + 3;
            return s1 / s2 / s3;
        }
        public static double Series_S1(int k) 
        {
            double s1 = Math.Sqrt(a+k) + Math.Sqrt(k-b);
            double s2 = Math.Pow(5.0 * k + 2.0 * a, 2);
            double s3 = b * k + 1.0;
            return s1 / s2 / s3;
        }
        public static double Series_S2(int k) 
        {
            double s1 = Math.Pow(k,1.5) + c;
            double s2 = 2.0 * k - d + 1;
            return 1.0 / s1 / s2 * (k % 2 == 0 ? 1.0 : -1.0);
        }

    }
}
