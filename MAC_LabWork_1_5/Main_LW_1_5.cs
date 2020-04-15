using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyTF = MAC_DLL.MAC_My_Definitions.MyTableOfFunction;
using Eq = MAC_DLL.MAC_Equations;

namespace MAC_LabWork_1_5
{
    class Main_LW_1_5
    {

        static void Main(string[] args)
        {
            //BaseCode();
            //HomeCode(-4.0, -2.8, 500);
            HomeCode(1.8, 3.5, 440);
        }

        public static void HomeCode(double x0, double xn, int n)
        {
            double eps = 1.0E-12;
            StreamWriter sw = new StreamWriter("MAC_LabWork_1_5_home.txt");
            MyTF T_Fx = new MyTF(x0, xn, n, My_Fx, "Fx");
            T_Fx.Roots_correction(eps);

            int K, M = T_Fx.Roots.Count; double xa, xb, xr = double.NaN;

            sw.WriteLine("\r\n Table of zeros, that counted by shema (1.5.1):");
            for (int j = 0; j < M; j++)
            {
                xa = T_Fx.Roots[j].XL; xb = T_Fx.Roots[j].XR;
                xr = Eq.Tangent(My_Fx, My_D1F, My_D2F, xa, xb, eps, out K);
                sw.WriteLine($"{j,3}{xr,17:F10}{Math.Abs(Fx(xr)),10:E1}{K,3}{My_D1F(xr),17:F10}{My_D2F(xr),17:F10}");

            }

            sw.Close();
        }

        public static double My_Fx(double x)
        {
            return Math.Sin(2.0 * x + 1) - 0.25 * Math.Sqrt(5 - x);
        }

        public static double My_D1F(double x)
        {
            return 2.0 * Math.Cos(2.0 * x + 1) + 0.125 / Math.Sqrt(5 - x);
        }

        public static double My_D2F(double x)
        {
            return -4.0 * Math.Sin(2.0 * x + 1) + 0.25 / 4.0 / Math.Sqrt(Math.Pow((5 - x),3));
        }

        public static void BaseCode()
        {
            double eps = 1.0E-12;
            StreamWriter sw = new StreamWriter("MAC_LabWork_1_5.txt");
            MyTF T_Fx = new MyTF(0.0, 15.0, 500, Fx, "Fx");
            T_Fx.Roots_correction(eps);
            sw.Write(T_Fx.Table_of_Roots("- Dichotomy -"));

            int K, M = T_Fx.Roots.Count; double xa, xb, xr = double.NaN;

            sw.WriteLine("\r\n Table of zeros, that counted by shema (1.5.1):");
            for (int j = 0; j < M; j++)
            {
                xa = T_Fx.Roots[j].XL; xb = T_Fx.Roots[j].XR;
                xr = Eq.Tangent(Fx, D1F, D2F, xa, xb, eps, out K);
                sw.WriteLine($"{j,3}{xr,17:F12}{Math.Abs(Fx(xr)),10:E1}{K,3}");
            }

            sw.WriteLine("\r\n Table of zeros, that counted by shema (1.5.2):");
            for (int j = 0; j < M; j++)
            {
                xa = T_Fx.Roots[j].XL; xb = T_Fx.Roots[j].XR;
                xr = Eq.Tangent(Fx, xa, xb, eps, out K);
                sw.WriteLine($"{j,3}{xr,17:F12}{Math.Abs(Fx(xr)),10:E1}{K,3}");
            }

            sw.Close();
        }

        public static double Fx(double x)
        {
            return Math.Tanh(x) - 2.0 * Math.Cos(Math.Sqrt(10.0) * x);
        }

        public static double D1F(double x)
        {
            return 1.0 / Math.Cosh(x) / Math.Cosh(x) + 2.0 * Math.Sqrt(10.0) * Math.Sin(Math.Sqrt(10.0) * x);
        }

        public static double D2F(double x)
        {
            return 2.0 / (10.0 * Math.Cos(Math.Sqrt(10.0) * x) - Math.Tanh(x) / Math.Cosh(x) / Math.Cosh(x));
        }
    }
}
