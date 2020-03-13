using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAC_DLL
{
    public class MAC_My_Functions
    {
        public static double MySin(double x, double eps)
        {
            if (x == 0) return 0.0;
            double sin = x, pk = x, x2 = 0.5 * x;
            for(int k = 2; Math.Abs(pk) > eps;k++)
            {
                pk = -pk * (x2 / (k - 1.0)) * (x2 / (k - 0.5));
                sin += pk;
            }
            return sin;
        }

        public static double MyCos(double x, double eps)
        {
            if (x == 0) return 1.0;
            double cos = 1.0, pk = 1.0, x2 = 0.5 * x;
            for (int k = 1; Math.Abs(pk) > eps; k++)
            {
                pk = -pk * (x2 / (k - 0.5)) * (x2 / k);
                cos += pk;
            }
            return cos;
        }

        public static double MySinh(double x, double eps)
        {
            if (x == 0) return 0.0;
            double sinh = x, pk = x, x2 = 0.5 * x;
            for (int k = 2; Math.Abs(pk) > eps; k++)
            {
                pk = pk * (x2 / (k - 1.0)) * (x2 / (k - 0.5));
                sinh += pk;
            }
            return sinh;
        }

        public static double MyCosh(double x, double eps)
        {
            if (x == 0) return 1.0;
            double cosh = 1.0, pk = 1.0, x2 = 0.5 * x;
            for (int k = 1; Math.Abs(pk) > eps; k++)
            {
                pk = pk * (x2 / (k - 0.5)) * (x2 / k);
                cosh += pk;
            }
            return cosh;
        }

        public static double MyExp(double x, double eps)
        {
            if (x == 0) return 1.0;
            double exp = 1.0, pk = 1.0, x2 = 0.5 * x;
            for (int k = 1; Math.Abs(pk) > eps; k++)
            {
                pk = pk * (x / k);
                exp += pk;
                Console.WriteLine($"{k,6}{pk,30:F22}");
            }
            return exp;
        }
    }
}
