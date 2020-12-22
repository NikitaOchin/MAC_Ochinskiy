using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ODE = MAC_DLL.MAC_Ordinary_Differential_Equations;
using System.IO;

namespace MAC_LabWork_8_1
{
    public class Main_LW_8_1
    {
        static void Main(string[] args)
        {
            int n = 23, i;
            double x0 = 0.5 * Math.PI, y0 = 1.0, eps = 1.0E-5, dx, Si;
            double[] x = new double[n + 1]; double[] y = new double[n + 1];

            x[0] = x0; x[n] = 4.0; y[0] = y0; dx = (x[n] - x[0]) / n;
            for (i = 1; i < n; i++) x[i] = x0 + dx * i;

            ODE.Taylor_Method(n, eps, x, y, f, fx, fy, fxx, fyy, fxy);

            StreamWriter SW = new StreamWriter("Taylor_Method.txt");
            for (i = 0; i <= n; i++)
            {
                Si = Y(x[i], x0, y0); eps = Math.Abs(Si - y[i]);
                SW.WriteLine
                ($"{i,5} {x[i],8:F4} {y[i],14:F9} {Si,14:F9}  {eps,11:E1}");
            }
            SW.Close();

            n = 19;
            x0 = -1.0; y0 = 0.5; eps = 1.0E-5;
            x = new double[n + 1]; y = new double[n + 1];

            x[0] = x0; x[n] = 1.0; y[0] = y0; dx = (x[n] - x[0]) / n;
            for (i = 1; i < n; i++) x[i] = x0 + dx * i;

            ODE.Taylor_Method(n, eps, x, y, mf, mfx, mfy, mfxx, mfyy, mfxy);

            SW = new StreamWriter("Taylor_Method_Очинский_v4.txt");
            for (i = 0; i <= n; i++)
            {
                Si = mY(x[i], x0, y0); eps = Math.Abs(Si - y[i]);
                SW.WriteLine
                ($"{i,5} {x[i],8:F4} {y[i],14:F9} {Si,14:F9}  {eps,11:E1}");
            }
            SW.Close();
        }

        public static double mf(double x, double y)
        { return Math.Sin(2*x) - y * Math.Tan(x); }

        public static double mfx(double x, double y)
        { return 2 * Math.Cos(2 * x) - y * (1 / Math.Cos(x) / Math.Cos(x)); }

        public static double mfy(double x, double y)
        { return Math.Tan(x); }

        public static double mfxx(double x, double y)
        { return -4 * Math.Sin(2*x) - (2* Math.Tan(x) * (1 / Math.Cos(x) / Math.Cos(x))); }

        public static double mfxy(double x, double y)
        { return 1 / Math.Cos(x) / Math.Cos(x); }

        public static double mfyy(double x, double y)
        { return 0.0; }

        public static double mY(double x, double x0, double y0)
        { 
            double C = (y0 + 2 * Math.Cos(x0) * Math.Cos(x0)) / Math.Cos(x0);
            return (C * Math.Cos(x) - 2 * Math.Cos(x) * Math.Cos(x)); 
        }



        public static double f(double x, double y)
        { return y / x + x * Math.Sin(x); }

        public static double fx(double x, double y)
        { return Math.Sin(x) + x * Math.Cos(x) - y / x / x; }

        public static double fy(double x, double y)
        { return 1.0 / x; }

        public static double fxx(double x, double y)
        { return 2.0 * (Math.Cos(x) + y / x / x / x) - x * Math.Sin(x); }

        public static double fxy(double x, double y)
        { return -1.0 / x / x; }

        public static double fyy(double x, double y)
        { return 0.0; }

        public static double Y(double x, double x0, double y0)
        { double C = y0 / x0 + Math.Cos(x0); return x * (C - Math.Cos(x)); }
    }
}
