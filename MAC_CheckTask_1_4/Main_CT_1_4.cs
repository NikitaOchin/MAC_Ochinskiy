using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyF = MAC_DLL.MAC_My_Functions;
using CTF = MAC_DLL.MAC_My_Definitions.MyTableOfFunction;

namespace MAC_CheckTask_1_4
{
    class Main_CT_1_4
    {
        static double par_a, par_b, par_e, fx, x0, xn;
        static int n;

        static void Main(string[] args)
        {
            ////
            //x0 = 0.3; xn = 6.0; par_a = 0.5; par_b = 2.0; par_e = 1.0E-11; n = 1000; fx = 0.1;
            //Test_Home(par_e);
            //
            x0 = 1.7; xn = 4.3; par_a = 0.7; par_b = 1.8; par_e = 1.0E-11; n = 1100; fx = 1.15;
            Test_Home(par_e);
        }

        static void Test_Home(double eps)
        {
            CTF table_43 = new CTF(x0, xn, n, Fx, "Home_CT_1_4");
            table_43.Roots_correction(eps);
            table_43.To_txt_File("HOME_WORK_CT_1_4.txt", " Test Fx");
            Console.WriteLine(table_43.Table_of_Roots("Roots of F(x)"));
            int k = table_43.Roots.Count();
            if (k > 1)
            {
                double minR = table_43.Roots[0].X; double maxR = table_43.Roots[k-1].X;
                Console.WriteLine($"Minor Root = {minR:F10}");
                Console.WriteLine($"Major Root = {maxR:F10}");
                Console.WriteLine($"Distance = {(maxR - minR):F10}");
            }
        }

        static void HOME_WORK(double eps)
        {
            CTF table_43 = new CTF(x0, xn, n, Fx, "HOME_WORK_CT_1_4");
            table_43.Roots_correction(eps);
            table_43.To_txt_File("HOME_WORK_CT_1_4.txt", " Test Fx");
        }

        static double Fx(double x)
        {
            return MyF.f2(x, par_a, par_b, par_e) - fx;
        }
    }
}
