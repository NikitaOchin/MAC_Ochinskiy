using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAC_LabWork_5_2
{
    class Main_LW_5_2
    {
        static void Main(string[] args)
        {
            int n = 5, i = 1;
            //Step_1(n); 
            //Step_2(n, i);
            Step_3(n);
        }

        static void Step_1(int n)
        {
            int j, k;
            long[] a = new long[n + 1];

            a[0] = 1; for (j = 1; j <= n; j++) a[j] = 0;

            for (k = 0; k < n; k++)
            {
                for (j = k + 1; j >= 1; j--)
                    a[j] = a[j - 1] - k * a[j];
                a[0] = -k * a[0];
            }

            Console.WriteLine($"     n = {n}");
            for (j = n; j >= 0; j--)
                Console.WriteLine($"    a[{j,2} ] = {a[j],15}");
        }

        static decimal fact(int n)
        {
            decimal sum = 1;
            for (int i = 1; i <= n; i++)
                sum *= i;
            return sum;
        }

        static void Step_2(int n, int i)
        {
            if ((i < 0) || (i > n)) return;
            int j, k;
            decimal koeff = 1;
            long[] a = new long[n + 1]; 
            decimal[] A = new decimal[n + 1];

            a[0] = 1; for (j = 1; j <= n; j++) a[j] = 0;

            for (k = 0; k <= n; k++)
            {
                if (k == i) continue;
                for (j = n; j >= 1; j--) a[j] = a[j - 1] - k * a[j];
                a[0] = -k * a[0];
            }

            koeff = (decimal)Math.Pow(-1, n - i) / fact(i) / fact(n - i);

            for (j = 0; j <= n; j++) A[j] = a[j] * koeff;

            Console.WriteLine($" n = {n},   i = {i}   \r\n");
            for (j = n; j >= 0; j--)
                Console.WriteLine($"  a[{i,2}, {j,2} ] = {a[j],16} ," +
                        $"  A[{i,2}, {j,2} ] = {A[j],36:F30}");
            Console.WriteLine($"\r\n koeff = {koeff,34:F30} \r\n");
        }

        static void Step_3(int n)
        {
            int i, j, k;
            long[] a = new long[n + 1];
            decimal[] A = new decimal[n + 1];
            decimal[] h = new decimal[n + 1];
            Console.WriteLine($"     n = {n}");

            for(i = 0; i <= n; i++)
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
                Console.WriteLine($"decimal    h[{n,2},{i,2}] = {h[i],35:F30}");
            }
        }
    }
}
