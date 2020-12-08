using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    
    
}