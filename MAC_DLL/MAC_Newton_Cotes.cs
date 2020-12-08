using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAC_DLL
{
    public class MAC_Newton_Cotes
    {
        public int n { get; protected set; }
        public string txt_H { get; internal set; }
        public string txt_m { get; internal set; }
        private double[] H;

        public MAC_Newton_Cotes(int N)
        {
            if (N > 18 || N < 2) n = 3; else n = N;
            NCotes_Numbers();
        }

        // -- Вычисление коэффициентов Котеса --//
        private void NCotes_Numbers()
        {
            int i, j, k; txt_H = "";
            long[] a = new long[n + 1];
            decimal[] A = new decimal[n + 1];
            decimal[] h = new decimal[n + 1];
            H = new double[n + 1];

            for (i = 0; i <= n; i++)
            {
                a[0] = 1; for (j = 1; j <= n; j++) a[j] = 0;

                for (k = 0; k <= n; k++)
                {
                    if (k == i) continue;
                    for (j = n; j >= 1; j--) a[j] = a[j - 1] - k * a[j];
                    a[0] = -k * a[0];
                }

                decimal koeff = (decimal)Math.Pow(-1, n - i) / fact(i) / fact(n - i);

                for (j = 0; j <= n; j++) A[j] = a[j] * koeff;

                koeff = 1;
                for (j = 0; j <= n; j++)
                {
                    h[i] = h[i] + A[j] * (koeff / (j + 1));
                    koeff = koeff * n;
                }
                H[i] = Convert.ToDouble(h[i]);
                txt_H += $" H[{n,2},{i,2}] = {H[i],24:E15}\r\n";
            }
        }

        // -- Вычисление интегральной суммы по Ньютону-Котесу --//
        private double Summa(double aj, double bj, Func<double, double> f)
        {
            double Integral_j = 0.0, hj = (bj - aj) / n;
            for (int i = 0; i <= n; i++)
                Integral_j += H[i] * f(aj + i * hj);
            return (bj - aj) * Integral_j;
        }

        public double Integral(double a, double b, Func<double, double> f, double eps)
        {
            double aj, bj, hm, I0, I1 = double.MaxValue;
            int j, m = (int)Math.Ceiling(b - a);
            do
            {
                I0 = I1; I1 = 0.0; m++; hm = (b - a) / m;
                for(j = 0; j < m; j++)
                {
                    aj = a + j * hm; bj = aj + hm; I1 += Summa(aj, bj, f);
                }
            } while (Math.Abs(I1 - I0) > eps);
            txt_m = $" m = {m}"; return I1;
        }

        private static decimal fact(int n)
        {
            decimal sum = 1;
            for (int i = 1; i <= n; i++)
                sum *= i;
            return sum;
        }
    }
}
