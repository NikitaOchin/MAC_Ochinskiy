﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAC_DLL
{
    public class MAC_My_Functions
    {

        public static double PL(int n, double x)
        {
            if (n == 0) return 1.0;
            if (n == 1) return x;
            double pn_1 = x, pn_2 = 1.0, pn = double.NaN, an;
            for (int j = 2; j<=n; j++)
            {
                an = 1.0 - 1.0 / j;
                pn = (1.0 + an) * x * pn_1 - an * pn_2;
                pn_2 = pn_1; pn_1 = pn; 
            }
            return pn;
        }

        public static double f0(double x, double a, double b, double eps)
        {
            if (x == 0) return 0.0;
            double xz = 11.0 * x / 7.0, bx = b * x;
            double Ao = (Math.Cos(bx) + Math.Sin(bx/a)) / a;
            double pk = 1.0, Ak = 1.0, Summa = 0.0;

            for(int k = 1; Math.Abs(Ak) > eps; k++)
            {
                pk = -pk * xz / (k + 1.0);
                Ak = (Math.Cos(bx / (k + 1.0)) + Math.Sin(bx / (k + a))) * pk / (2.0 * k + a);
                Summa += Ak;
            }
            return 0.25 * xz * (Ao + Summa);
        }

        public static double f2(double x, double a, double b, double eps)
        {
            if (x == 0) return 0.0;
            double Ao = Math.Cos(Math.PI * a * x * x / 2.0) / b;
            double pk = x, Ak = 1.0, Summa = 0.0;

            for (int k = 1; Math.Abs(Ak) > eps; k++)
            {
                pk = -pk * x * x / (2 * k) / (2 * k + 1.0);
                Ak = Math.Cos(Math.PI / 2.0 * a * x * x / (k * x + 1.0))
                    * pk / (2.0 * k + b);
                Summa += Ak;
            }
            return Ao * x + Summa;
        }

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
