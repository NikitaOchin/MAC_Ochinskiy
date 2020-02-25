using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CLS = MAC_DLL.MAC_Series;

namespace MAC_LabWork_1_1
{
    class Main_LW_1_1
    {
        static void Main(string[] args)
        {
            double True_Sum1 = Math.PI * Math.PI / 8.0 - 1.0;
            double True_Sum2 = Math.Pow(Math.PI,3.0) / 32.0;
            int N = 10000, kf = 0; double Eps = 1.0E-9, delta = 1.0E-8;

            double S1_N = CLS.Sum_of_Number_Series(1,N,My_ak);
            Console.WriteLine("\r\n Summa 1 :");
            Console.WriteLine($"{N,8}{S1_N,20:F15}\r\n{True_Sum1,28:F15}");
            
            double S1_A = CLS.Sum_of_Number_Series_A(1, Eps, My_ak, ref kf);
            Console.WriteLine($"{kf,8}{S1_A,20:F15}");

            double S1_D = CLS.Sum_of_Number_Series_D(1, delta, My_ak, ref kf);
            Console.WriteLine($"{kf,8}{S1_D,20:F15}");

            double S2_N = CLS.Sum_of_Number_Series(0, N, My_bk);
            Console.WriteLine("\r\n Summa 2 :");
            Console.WriteLine($"{N,8}{S2_N,20:F15}\r\n{True_Sum2,28:F15}");

            double S2_A = CLS.Sum_of_Number_Series_A(0, Eps, My_bk, ref kf);
            Console.WriteLine($"{kf,8}{S2_A,20:F15}");

            double S2_D = CLS.Sum_of_Number_Series_D(0, delta, My_bk, ref kf);
            Console.WriteLine($"{kf,8}{S2_D,20:F15}");

            Console.WriteLine("\r\n HOME_WORK");
            double TrueSum1_H = 0.3794715778;
            double TrueSum2_H = 3.0/4.0*(1.0-2.0*Math.PI/Math.Sqrt(27));

            N = 18600;
            double S1_H = CLS.Sum_of_Number_Series(1, N, My_ck);
            int T_N = 17500;
            double S1_H_T = CLS.Sum_of_Number_Series(1, T_N, My_ck);
            Console.WriteLine("\r\n Summa 1 for home :");
            Console.WriteLine($"{N,8}{S1_H,20:F10}\r\n");
            bool check = Math.Round(S1_H_T,10) == Math.Round(TrueSum1_H,10);
            Console.WriteLine($"{T_N,8}{S1_H_T,20:F10}\r\n{TrueSum1_H,28:F10}\r\n{check,20}");

            double S2_H_A = CLS.Sum_of_Number_Series_A(1, Eps, My_dk, ref kf);
            Console.WriteLine("\r\n Summa 2 for home :");
            Console.WriteLine($"{kf,8}{S2_H_A,20:F10}\r\n{TrueSum2_H,28:F10}");

            Console.ReadLine();
        }

        static double My_ak(int k)
        {
            double a = 2.0 * k + 1.0; return 1.0 / a / a;
        }

        static double My_bk(int k)
        {
            double b = 2.0 * k + 1.0;
            return 1.0 / b / b / b * (k % 2 == 0 ? 1.0 : -1.0);
        }

        static double My_ck(int k)
        {
            return 1.0 / k / (3.0 * k + 2.0);
        }

        static double My_dk(int k)
        {
            return 1.0 / k / (3.0 * k + 2) * (k % 2 == 0 ? 1.0 : -1.0);
        }
    }
}
