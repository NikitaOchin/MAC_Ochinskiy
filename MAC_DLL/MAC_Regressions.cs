using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyToD = MAC_DLL.MAC_My_Definitions.MyTableOfData;

namespace MAC_DLL
{
    public enum TypeOfRegression
    {
        Unknown, Linear, Quadratic, Power, Exponential,
        Fractional_Linear, Logarithmic, Fractional_Rational, Hyperbolic
    }

    public class MAC_Regressions
    {
        public string Name { get; internal set; } = "UnKnown";
        public double a { get; internal set; }
        public double b { get; internal set; }
        public double c { get; internal set; }
        public double error { get; internal set; }
        public int n { get; internal set; }

        // Вспомогательные переменные
        TypeOfRegression Kind;
        double[] x, f, X, F;
        double aa, bb, cc, ee, fi;

        public MAC_Regressions(MyToD ToD)
        {
            bool flag_X, flag_F, flag_Fo, flag_Xo; n = ToD.Length;

            flag_X = flag_F = flag_Fo = flag_Xo = true;

            // Копирование данных из таблицы в основные массивы
            ToD.ToArrays(out X, out F);

            // Копирование данных из таблицы во "вспомогательные" массивы
            ToD.ToArrays(out x, out f);

            // установка значение флагов
            for (int i = 0; i < n; i++)
            {
                if (x[i] <= 0) flag_X = false; if (x[i] == 0) flag_Xo = false;
                if (f[i] <= 0) flag_F = false; if (f[i] == 0) flag_Fo = false;
            }

            #region <--- 1. Вычисление коэффициентов для Линейной функции --->
            Linear_Coefficients();

            for (int i = 0; i < n; i++)
            {
                fi = aa * X[i] + bb; ee += (F[i] - fi) * (F[i] - fi);
            }
            ee = Math.Sqrt(ee) / n;

            a = aa; b = bb; c = double.NaN; error = ee;
            Kind = TypeOfRegression.Linear; Name = "Линейная функция";
            #endregion <--- 1. Вычисление коэффициентов для Линейной функции --->

            #region <--- 2. Вычисление коэффициентов для Квадратичной функции --->
            Quadratic_Coefficients();

            for (int i = 0; i < n; i++)
            {
                fi = aa * X[i] * X[i] + bb * X[i] + cc; ee += (F[i] - fi) * (F[i] - fi);
            }
            ee = Math.Sqrt(ee) / n;

            if (ee < error)
            {
                a = aa; b = bb; c = cc; error = ee;
                Kind = TypeOfRegression.Quadratic; Name = "Квадратичная функция";
            }
            #endregion <--- 2. Вычисление коэффициентов для Квадратичной функции --->

            #region <--- 3. Вычисление коэффициентов для Степенной функции --->
            if (flag_X && flag_F)
            {
                for (int i = 0; i < n; i++)
                {
                    x[i] = Math.Log(X[i]); f[i] = Math.Log(F[i]);
                }
                
                Linear_Coefficients();
                bb = Math.Exp(bb);

                for (int i = 0; i < n; i++)
                {
                    fi = bb * Math.Pow(X[i], aa); ee += (F[i] - fi) * (F[i] - fi);
                }
                ee = Math.Sqrt(ee) / n;
                if (ee < error)
                {
                    a = aa; b = bb; c = double.NaN; error = ee;
                    Kind = TypeOfRegression.Power; Name = "Степенная функция";
                }
            }
            #endregion <--- 3. Вычисление коэффициентов для Степенной функции --->

            #region <--- 4. Вычисление коэффициентов для Показательной функции --->
            if (flag_F)
            {
                for (int i = 0; i < n; i++)
                {
                    x[i] = X[i]; f[i] = Math.Log(F[i]);
                }

                Linear_Coefficients();
                bb = Math.Exp(bb);

                for (int i = 0; i < n; i++)
                {
                    fi = bb * Math.Exp(X[i] * aa); ee += (F[i] - fi) * (F[i] - fi);
                }
                ee = Math.Sqrt(ee) / n;

                if (ee < error)
                {
                    a = aa; b = bb; c = double.NaN; error = ee;
                    Kind = TypeOfRegression.Exponential; Name = "Показательная функция";
                }
            }
            #endregion <--- 4. Вычисление коэффициентов для Показательной функции --->

            #region <--- 5. Вычисление коэффициентов для Дробно-линейной функции --->
            if (flag_Fo)
            {
                for (int i = 0; i < n; i++)
                {
                    x[i] = X[i]; f[i] = 1.0 / F[i];
                }

                Linear_Coefficients();

                for (int i = 0; i < n; i++)
                {
                    fi = 1.0 / (bb  + X[i] * aa); ee += (F[i] - fi) * (F[i] - fi);
                }
                ee = Math.Sqrt(ee) / n;

                if (ee < error)
                {
                    a = aa; b = bb; c = double.NaN; error = ee;
                    Kind = TypeOfRegression.Fractional_Linear; Name = "Дробно-линейная функция";
                }
            }
            #endregion <--- 5. Вычисление коэффициентов для Дробно-линейной функции --->

            #region <--- 6. Вычисление коэффициентов для Логарифмической функции --->
            if (flag_X)
            {
                for (int i = 0; i < n; i++)
                {
                    x[i] = Math.Log(X[i]); f[i] = F[i];
                }

                Linear_Coefficients();

                for (int i = 0; i < n; i++)
                {
                    fi = bb + Math.Log(X[i]) * aa; ee += (F[i] - fi) * (F[i] - fi);
                }
                ee = Math.Sqrt(ee) / n;
                if (ee < error)
                {
                    a = aa; b = bb; c = double.NaN; error = ee;
                    Kind = TypeOfRegression.Logarithmic; Name = "Логарифмическая функция";
                }
            }
            #endregion <--- 6. Вычисление коэффициентов для Логарифмическойфункции --->

            #region <--- 7. Вычисление коэффициентов для Дробно-рациональной функции --->
            if (flag_Xo && flag_Fo)
            {
                for (int i = 0; i < n; i++)
                {
                    x[i] = 1.0 / X[i]; f[i] = 1.0 / F[i];
                }

                Linear_Coefficients();

                for (int i = 0; i < n; i++)
                {
                    fi = X[i] / (bb * X[i] + aa);
                    ee += (F[i] - fi) * (F[i] - fi);
                }
                ee = Math.Sqrt(ee) / n;
                if (ee < error)
                {
                    a = bb; b = aa; c = double.NaN; error = ee;
                    Kind = TypeOfRegression.Fractional_Rational; Name = "Дробно-рациональная функция";
                }
            }
            #endregion <--- 7. Вычисление коэффициентов для Дробно-рациональной --->

            #region <--- 8. Вычисление коэффициентов для Гиперболической функции --->
            if (flag_Xo)
            {
                for (int i = 0; i < n; i++)
                {
                    x[i] = 1.0 / X[i]; f[i] = F[i];
                }

                Linear_Coefficients();

                for (int i = 0; i < n; i++)
                {
                    fi = aa  / X[i] + bb; ee += (F[i] - fi) * (F[i] - fi);
                }
                ee = Math.Sqrt(ee) / n;
                if (ee < error)
                {
                    a = aa; b = bb; c = double.NaN; error = ee;
                    Kind = TypeOfRegression.Hyperbolic; Name = "Гиперболическая функция";
                }
            }
            #endregion <--- 8. Вычисление коэффициентов для Гиперболической --->
        }

        private void Linear_Coefficients()
        {
            double Ax = 0, Af = 0, Ax2 = 0, Axf = 0, D, Da, Db;
            for (int i = 0; i<n; i++)
            {
                Ax += x[i]; 
                Af += f[i]; 
                Ax2 += x[i] * x[i]; 
                Axf += f[i] * x[i];
            }
            Ax /= n; Af /= n; Ax2 /= n; Axf /= n;

            D = Ax2 - Ax * Ax; 
            Da = Axf - Ax * Af;
            Db = Ax2 * Af - Axf * Ax;

            aa = Da / D; 
            bb = Db / D; 
            ee = 0.0;
        }

        private void Quadratic_Coefficients()
        {
            double Ax = 0, Af = 0, Ax2 = 0, Axf = 0, Ax3 = 0, Ax4 = 0, Ax2f = 0,
             D, Da, Db, Dc;
            for (int i = 0; i < n; i++)    // формулы (7.11) и (7.15)
            {
                Ax += x[i]; Ax2 += x[i] * x[i]; Ax3 += x[i] * x[i] * x[i];
                Ax4 += x[i] * x[i] * x[i] * x[i];
                Af += f[i]; Axf += x[i] * f[i]; Ax2f += x[i] * x[i] * f[i];
            }

            Ax = Ax / n; Af = Af / n; Axf = Axf / n; Ax2 = Ax2 / n;
            Ax3 = Ax3 / n; Ax4 = Ax4 / n; Ax2f = Ax2f / n;

            D = Ax4 * Ax2 + 2.0 * Ax3 * Ax2 * Ax - Ax2 * Ax2 * Ax2
              - Ax4 * Ax * Ax - Ax3 * Ax3;

            Da = Ax2f * Ax2 + Ax3 * Ax * Af + Axf * Ax2 * Ax
               - Ax2 * Ax2 * Af - Ax * Ax * Ax2f - Axf * Ax3;
            Db = Ax4 * Axf + Ax2f * Ax * Ax2 + Ax3 * Af * Ax2
               - Axf * Ax2 * Ax2 - Ax4 * Ax * Af - Ax3 * Ax2f;
            Dc = Ax4 * Ax2 * Af + Ax3 * Axf * Ax2 + Ax3 * Ax * Ax2f
               - Ax2 * Ax2 * Ax2f - Ax4 * Ax * Axf - Ax3 * Af * Ax3;

            aa = Da / D; bb = Db / D; cc = Dc / D; 
            ee = 0.0;
        }

        public double Regression(double arg)
        {
            switch((int)Kind)
            {
                case 1: return a * arg + b;
                case 2: return a * arg * arg + b * arg + c;
                case 3: return b * Math.Pow(arg, a);
                case 4: return b * Math.Exp(a * arg);
                case 5: return 1.0 / (a * arg + b);
                case 6: return a * Math.Log(arg) + b;
                case 7: return arg / (a * arg + b);
                case 8: return a / arg + b;
            }
            return double.NaN;
        }

        public string ToPrint()
        {
            string txt = Name + $"\r\na = {a,6:F2}   b = {b,6:F2}";
            if ((int)Kind == 2) txt += $"   c = {c,6:F2}";
            txt += $"  err = {error,10:E1}";
            return txt;
        }

    }
}
