using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CLE = MAC_DLL.MAC_Equations;
using CTF = MAC_DLL.MAC_My_Definitions.MyTableOfFunction;


namespace MAC_LabWork_1_4
{
    class Main_LW_1_4
    {
        static double x0, xn, a, b; static int n;
        
        static void Main(string[] args)
        {
            //Test_LW_14_1();
            //Test_43(1.0E-12);

            //x0 = -20.0; xn = -15.0; a = -0.05; b = 2.0; n = 1000;
            //Test_Home(1.0E-12);
            //x0 = -19.6; xn = -15.2; a = 0.11; b = 1.4; n = 1000;
            //Test_Home(1.0E-12);
            Console.WriteLine("x* = -19,2835656620 x** = -15,3655155907 ");
        }

        static void Test_Home(double eps)
        {
            CTF table_43 = new CTF(x0, xn, n, F_home, "Test_Home");
            table_43.Roots_correction(eps);
            table_43.To_txt_File("HOME_MAC_LW_1_4.txt", " Test F_home");
        }

        static double F_home(double x)
        {
            return Math.Tanh(x*a) + b * Math.Cos(Math.Sqrt(Math.Log10(3)+Math.PI*Math.PI) * x);
        }

        static void Test_43(double eps)
        {
            CTF table_43 = new CTF(0.0, 15.0, 300, F_43, "Test_43");
            table_43.Roots_correction(eps);
            table_43.To_txt_File("MAC_LW_1_4.txt", " Test F_43");
        }

        static double F_43(double x)
        {
            return Math.Tanh(x) - 2.0 * Math.Cos(Math.Sqrt(10.0) * x);
        }

        static void Test_LW_14_1()
        {
            int k = 0;
            double root = CLE.Dichotomy(0.3, 0.6, 1.0E-12, Cos_pi_x, ref k);
            string res = $"x = {root,18:F15}   err = {Cos_pi_x(root),7:E1}   K = {k}";
            Console.WriteLine(res);
            Console.WriteLine(res);
        }

        static double Cos_pi_x(double x)
        {
            return Math.Cos(Math.PI * x);
        }
    }
}
