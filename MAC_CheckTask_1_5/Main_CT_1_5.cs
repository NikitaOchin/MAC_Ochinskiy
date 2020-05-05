using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MAC_DLL.MAC_My_Definitions;
using MyETF = MAC_DLL.MAC_My_Definitions.MyExtendedTableOfFunction;

namespace MAC_CheckTask_1_5
{
    class Main_CT_1_5
    {
        static double a, b, s, xo, xn; static int n;

        static void Main(string[] args)
        {
            //Test(a = 0.1, b = -0.1, s = Math.Sqrt(10.0));
            Test_home(xo = 2.0, xn = 5.0, a = -0.2, b = 0.2, n = 300, "MAC_CheckTask_1_5_test_home.txt");
            Test_home(xo = 3.6, xn = 6.6, a = 0.9, b = 0.4, n = 340, "MAC_CheckTask_1_5_home.txt");

        }

        static void Test(double a, double b, double s)
        {
            StreamWriter SW = new StreamWriter("MAC_CheckTask_1_5.txt");
            MyETF ET_Fx = new MyETF(0.0, 4.0, 200, Fx, d1Fx, d2Fx, "- Newton -");
            ET_Fx.Roots_correction(1.0E-12);

            SW.WriteLine(ET_Fx.Table_of_Roots("--- All Roots ---"));

            SW.WriteLine("\r\n Точки пересечения траекторий");
            Point_xf xy;
            for (int i = 0; i < ET_Fx.Roots.Count; i++)
            {
                xy = new Point_xf(ET_Fx.Roots[i].X, f1(ET_Fx.Roots[i].X));
                SW.WriteLine($"{i,3}" + xy.ToPrint());
            }
            SW.Close();
        }

        public static double Fx(double x) { return f1(x) - f2(x); }
        public static double d1Fx(double x) { return d1f1(x) - d1f2(x); }
        public static double d2Fx(double x) { return d2f1(x) - d2f2(x); }

        public static double f1(double x) { return Math.Tanh(x + a); }
        public static double d1f1(double x) { return 1.0 / Math.Cosh(x + a) / Math.Cosh(x + a); }
        public static double d2f1(double x) { return -2.0 * Math.Tanh(x + a) / Math.Cosh(x + a) / Math.Cosh(x + a); }

        public static double f2(double x) { return 2.0 * Math.Cos(x * s + b); }
        public static double d1f2(double x) { return -2.0 * s * Math.Sin(x * s + b); }
        public static double d2f2(double x) { return -2.0 * s * s * Math.Cos(x * s + b); }


        static void Test_home(double xo, double xn, double a, double b, int n, string path)
        {
            StreamWriter SW = new StreamWriter(path);
            MyETF ET_Fx = new MyETF(xo, xn, n, My_Fx, My_d1Fx, My_d2Fx, "- Newton -");
            ET_Fx.Roots_correction(1.0E-12);

            SW.WriteLine(ET_Fx.Table_of_Roots("--- All Roots ---"));

            SW.WriteLine("\r\n Точки пересечения траекторий");
            Point_xf xy;
            for (int i = 0; i < ET_Fx.Roots.Count; i++)
            {
                xy = new Point_xf(ET_Fx.Roots[i].X, My_f1(ET_Fx.Roots[i].X));
                SW.WriteLine($"{i,3}" + xy.ToPrint());
            }
            SW.Close();
        }

        public static double My_Fx(double x) { return My_f1(x) - My_f2(x); }
        public static double My_d1Fx(double x) { return My_d1f1(x) - My_d1f2(x); }
        public static double My_d2Fx(double x) { return My_d2f1(x) - My_d2f2(x); }

        public static double My_f1(double x) { return 4.0 * Math.Cos(Math.Sqrt(17.0) * x + a); }
        public static double My_d1f1(double x) { return -4.0 * Math.Sqrt(17.0) * Math.Sin(Math.Sqrt(17.0) * x + a); }
        public static double My_d2f1(double x) { return -4.0 * Math.Sqrt(17.0) * Math.Sqrt(17.0) * Math.Cos(Math.Sqrt(17.0) * x + a); }

        public static double My_f2(double x) { return -0.3 * Math.Sqrt(11.0 * x + b); }
        public static double My_d1f2(double x) { return -0.3 * 11.0 / 2.0 / Math.Sqrt(11.0 * x + b); }
        public static double My_d2f2(double x) { return 0.3 * 11.0 / 2.0 / 2.0 / Math.Sqrt(Math.Pow(11.0 * x + b,3)); }
    }
}
