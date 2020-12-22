using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyF = MAC_DLL.MAC_My_Functions;

namespace MAC_DLL
{
    public class MAC_Quadrature
    {

        public static double 
        Method_Simpsona(double A, double B, Func<double, double> fx, double eps)
        {
            double s0 = double.MaxValue, sk, error, x0, x1, x2, h;
            int j, m, k = 0, n = 10 * (int)Math.Ceiling(B-A);
            do
            {
                k++;
                sk = 0.0;
                x2 = A;
                m = n/2;
                h = (B - A) / n;
                for(j = 1; j<=m; j++)
                {
                    x0 = x2;
                    x1 = x0 + h;
                    x2 = x1 + h;
                    sk += fx(x0) + 4.0 * fx(x1) + fx(x2);
                }
                sk *= h/3.0;
                error = Math.Abs(sk - s0);
                s0 = sk; n*=2;

            }while(error > eps);
            return sk;
        }
    }

    public class MAC_Gauss_Quadrature
    {
        public int n { get; protected set; }
        public string txt_xw { get; internal set; }
        public string txt_k { get; internal set; }
        private double[] x, w; // массивы абсцисс и весов

        //-- Конструктор экземпляра класса MAC_Gauss_Quadrature --//
        
        public MAC_Gauss_Quadrature(int N)
        {
            if ((N > 48) || (N < 2)) n = 2; else n = N;
            x = new double[n + 1]; w = new double[n + 1];
            Gauss_x_w();
        }

        private void Gauss_x_w()
        {
            int i, j, m, k;
            double hx, x_a, x_b, x_c, x_old, fx_a, fx_b, fx_c, pl;

            k = n / 2; m = n - k;

            if (m != k)
            {
                x[m] = 0.0; pl = n * MyF.PL(n - 1, x[m]);
                w[m] = 2.0 / pl / pl;
            }

            hx = 0.1 / k; x_b = hx; x_old = 0.0;

            for (i = 1; i <= k; i++)
            { 
                x_a = x_b; fx_a = MyF.PL(n, x_a);
                do
                {
                    x_b += hx; fx_b = MyF.PL(n, x_b);
                }
                while (fx_a * fx_b > 0);

                j = 0; x_a = x_b - hx;

                do
                {
                    x_c = x_a + (x_b - x_a) * 0.5; j += 1;
                    fx_c = MyF.PL(n, x_c);
                    if ((fx_a * fx_c) < 0) x_b = x_c; else x_a = x_c;
                }
                while ((Math.Abs(fx_c) > 0.5E-15) && (j < 65));

                x[m + i] = x_c; pl = n * MyF.PL(n - 1, x_c);
                w[m + i] = 2.0 * (1.0 - x_c * x_c) / pl / pl;
                x[k - i + 1] = -x_c; w[k - i + 1] = w[m + i];
                hx = (x_c - x_old) / 3; x_old = x_c;
            }

            txt_xw = "";
            for (i = 1; i <= n; i++)
            {
                txt_xw += $" x[{i,2}] = {x[i], 19:F14}  w[{i,2}] = {w[i],19:F14}\r\n";
            }
        }

        private double Summa(double a, double b, Func<double, double> f)
        {
            double B = (b + a) / 2.0, A = (b - a) / 2.0, Int = 0.0;
            for(int i = 1; i<=n; i++)
                Int += w[i] * f(A * x[i] + B);
            return A * Int;
        }

        public double Gauss_Integral(double a, double b, Func<double, double> f, double eps)
        {
            double aj, bj, hk, I0, I1 = Summa(a, b, f); int j, k = 1;
            do
            {
                I0 = I1; I1 = 0.0; k++; hk = (b - a) / k;
                for (j = 0;j<k; j++)
                {
                    aj = a + j * hk; bj = aj + hk; I1 += Summa(aj, bj, f);
                }
            } while (Math.Abs(I1 - I0) > eps);
            txt_k = $"{k}"; return I1;
        }
    }
}