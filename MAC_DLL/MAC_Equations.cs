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
        public static double Dichotomy(double a, double b, double eps, Func<double, double> f, ref int K)
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

        public static void Dichotomy(Func<double, double> f, Root root, double eps)
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

        public static double Tangent(Func<double, double> Fx,
                                     Func<double, double> D1Fx,
                                     Func<double, double> D2Fx,
                                     double xL, double xR,
                                     double eps, out int K)
        {
            double xK = (xR + xL) * 0.5; K = 0;
            if ((Fx(xL) * D2Fx(xL)) > 0) xK = xL;
            if (Fx(xR) * D2Fx(xR) > 0) xK = xR;
            while (Math.Abs(Fx(xK)) > eps && K <= 15)
            {
                xK = xK - Fx(xK) / D1Fx(xK); K++;
            }
            return xK;
        }

        public static double Tangent(Func<double, double> Fx,
                                    double xL, double xR,
                                    double eps, out int K)
        {
            double xK = (xR + xL) * 0.5; K = 0;
            double dF = (Fx(xR) - Fx(xL)) / (xR - xL);
            while (Math.Abs(Fx(xK)) > eps && K <= 25)
            {
                xK = xK - Fx(xK) / dF; K++;
            }
            return xK;
        }

        public static void Tangent(Func<double, double> Fx,
                                     Func<double, double> D1Fx,
                                     Func<double, double> D2Fx,
                                     Root root, double eps)
        {
            root.X = (root.XR + root.XL) * 0.5; root.Iters = 0;
            if ((Fx(root.XL) * D2Fx(root.XL)) > 0) root.X = root.XL;
            if (Fx(root.XR) * D2Fx(root.XR) > 0) root.X = root.XR;

            while (Math.Abs(Fx(root.X)) > eps && root.Iters <= 15)
            {
                root.X = root.X - Fx(root.X) / D1Fx(root.X); root.Iters++;
            }
            root.Err = Math.Abs(Fx(root.X));
        }

    }
}
