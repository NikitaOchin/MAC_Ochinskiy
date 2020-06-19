using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MAC_DLL.MAC_My_Definitions;

namespace MAC_DLL
{
    public class MAC_Interpolation
    {
        /// <summary>
        /// Інтерполювання за лагранжем для масива даних Point_xf
        /// </summary>
        public static double Polynomial_Lagrange(double xz, Point_xf[] points)
        {
            int n = points.Length - 1;
            if ((xz < points[0].X) || (xz > points[n].X)) return double.NaN;

            double alfa_i, Ln = 0.0;
            for(int i = 0; i <= n; i++)
            {
                alfa_i = points[i].F;
                for(int j = 0; j <= n; j++)
                    if (j != i)
                        alfa_i *= (xz - points[j].X) / (points[i].X - points[j].X);
                Ln += alfa_i;
            }
            return Ln;
        }

        /// <summary>
        /// Интерполирование по Лагранжу заданой степени m
        /// </summary>
        /// <param name="m">степень многочлена</param>
        /// <param name="xz">аргумент</param>
        /// <param name="points">таблица функции</param>
        
        public static double Polynomial_Lagrange(int m, double xz, Point_xf[] points)
        {
            int n = points.Length - 1;
            if (m < 1) m = 1;
            if (m > n) m = n;

            //Определение индексных границ интервала интерполирования

            int M, i, i_Left = 0, i_Right = 0;
            for(i = 1; i <= n; i++)
            {
                if((points[i-1].X<=xz) && (xz <= points[i].X))
                {
                    i_Left = i - 1; i_Right = i; break;
                }
            }
            if (i_Left + i_Right != 0)
            {
                M = 1;
                while (M < m)
                {
                    if (i_Left > 0) { i_Left--; M++; if (M == m) break; }
                    if (i_Right < n) { i_Right++; M++; }
                }
            }
            else return double.NaN;

            // Интерполирование по формуле Лагранжа (4.*)

            double alfa_i, Lm = 0.0;
            for (i = i_Left; i <= i_Right; i++)
            {
                alfa_i = points[i].F;
                for (int j = i_Left; j <= i_Right; j++)
                    if (i != j)
                        alfa_i *= (xz - points[j].X) / (points[i].X - points[j].X);
                Lm += alfa_i;
            }
            return Lm;
        }
    }
}
