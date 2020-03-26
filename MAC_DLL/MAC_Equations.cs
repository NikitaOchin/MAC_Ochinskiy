using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MAC_DLL.MAC_My_Definitions;

namespace MAC_DLL
{
    public class MAC_Equations
    {
        public static double Dichotomy
            (double a, double b, double eps, Func<double,double> f, ref int K)
        {
            double fa = f(a), fc, c = 0.0; K = 0;
            while (K < 70)
            {
                if (a * b < c) c = (a + b) * 0.5; else c = a + (b - a) * 0.5;
                fc = f(c); K++;
                if (fa * fc < 0) b = c; else a = c;
                if (Math.Abs(fc) < eps || (b - a) * 10 < eps) break;
            }
            return c;
        }

        public static void Dichotomy
            (Func<double, double> f, Root root, double eps)
        {
            double a = root.XL, c = 0.0, b = root.XR, fa = f(a), fc;
            root.Iters = 0; root.Err = eps;
            while (root.Iters < 70)
            {
                if (a * b < c) c = (a + b) * 0.5; else c = a + (b - a) * 0.5;
                fc = f(c); root.Iters++;
                if (fa * fc < 0) b = c; else a = c;
                if (Math.Abs(fc) < eps || (b - a) * 10 < eps) break;
            }
            root.X = c;
        }
    }
}
