using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAC_DLL
{
    public class MAC_Ordinary_Differential_Equations
    {
        public static void 
            Taylor_Method(int n, double eps, double[] x, double [] y,
                          Func<double, double, double> f,
                          Func<double, double, double> fx,
                          Func<double, double, double> fy,
                          Func<double, double, double> fxx,
                          Func<double, double, double> fyy,
                          Func<double, double, double> fxy)
        {
            int m; double f_1, f_2, f_3, err;
            double ksi_j, ksi_ji, phi_j, phi_j1, phi_m, dksi;

            for (int i = 1; i <= n; i++)
            {
                m = 10 * Convert.ToInt32(Math.Ceiling(x[i] - x[i - 1]));
                err = double.MaxValue; phi_m = err;
                while (err > eps)
                {
                    dksi = (x[i] - x[i - 1]) / m; phi_j1 = y[i - 1];
                    for(int j = 0; j < m; j++) 
                    {
                        ksi_j = x[i - 1] + dksi * j;
                        ksi_ji = ksi_j + dksi;
                        phi_j = phi_j1;

                        f_1 = f(ksi_j, phi_j);
                        f_2 = fx(ksi_j, phi_j) + f_1 * fy(ksi_j, phi_j);
                        f_3 = fxx(ksi_j, phi_j) + fy(ksi_j, phi_j) * f_2 +
                            f_1 * (2.0 * fxy(ksi_j, phi_j) + f_1 * fyy(ksi_j, phi_j));

                        phi_j1 = phi_j + dksi * (f_1 + dksi * (f_2 + dksi * f_3 / 3.0) / 2.0);
                    }
                    err = Math.Abs(phi_m - phi_j1); m = 2 * m; phi_m = phi_j1;
                }
                y[i] = phi_m;
            }
        }
    }
}
