﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAC_DLL
{
    public class MAC_Algebra
    {
        public static Vector Method_Gaussa(Matrix A, Vector B)
        {
            double aik, aMain, determinant = 1.0;
            int N = A.Size, i, j, k, I, znak = 1;

            Matrix a = Matrix.Copy(A); Vector b = Vector.Copy(B);

            for (k = 1; k <= N; k++)
            {
                //Find major element
                aMain = Math.Abs(a[k, k]); I = k;
                for (i = k + 1; i <= N; i++)
                {
                    aik = Math.Abs(a[i, k]);
                    if (aik > a[i, k]) { aMain = aik; I = i; }
                }

                //exchange rows I & k
                if (I != k)
                {
                    for (j = 1; j <= N; j++)
                    {
                        aik = a[I, j]; a[I, j] = a[k, j]; a[k, j] = aik;
                    }
                    aik = b[I]; b[I] = b[k]; b[k] = aik; znak = -znak;
                }


                aMain = a[k, k];
                for (i = k; i <= N; i++)
                    a[k, i] = a[k, i] / aMain;
                b[k] = b[k] / aMain; determinant *= aMain;

                for (i = k + 1; i <= N; i++)
                {
                    aik = a[i, k];
                    for (j = k; j <= N; j++)
                        a[i, j] -= a[k, j] * aik;
                    b[i] -= b[k] * aik;
                }
            }

            Vector X = new Vector(N);
            for (k = N; k >= 1; k--)
            {
                X[k] = b[k];
                for (i = k+1; i <= N; i++)
                    X[k] -= X[i] * a[k, i];
            }

            determinant *= znak; if (double.IsNaN(A.Det)) A.Det = determinant;
            return X;
        }

        public static Vector Method_Jordana_Gaussa(Matrix A, Vector B)
        {
            double aik, aMain, determinant = 1.0;
            int N = A.Size, i, j, k, I, znak = 1;

            Matrix a = Matrix.Copy(A); Vector b = Vector.Copy(B);

            for (k = 1; k <= N; k++)
            {
                //Find major element
                aMain = Math.Abs(a[k, k]); I = k;
                for(i = k+1; i <= N; i++) 
                {
                    aik = Math.Abs(a[i, k]);
                    if (aik > a[i, k]) { aMain = aik;I = i; }
                }

                //exchange rows I & k
                if (I != k)
                {
                    for (j = 1; j <= N; j++)
                    {
                        aik = a[I, j]; a[I, j] = a[k, j];a[k, j] = aik;
                    }
                    aik = b[I]; b[I] = b[k]; b[k] = aik; znak = -znak;
                }


                aMain = a[k, k];
                for(i = k; i <= N; i++)
                    a[k, i] = a[k, i] / aMain;
                b[k] = b[k] / aMain; determinant *= aMain;

                for (i = k + 1; i <= N; i++)
                {
                    aik = a[i, k];
                    for (j = k; j <= N; j++)
                        a[i, j] -= a[k, j]*aik;
                    b[i] -= b[k] * aik;
                }
            }

            for (k = N; k >= 2; k--)
                for (i = 1; i <= (k - 1); i++)
                    b[i] = b[i] - b[k] * a[i, k];

            determinant *= znak; if (double.IsNaN(A.Det)) A.Det = determinant;
            return Vector.Copy(b);
        }

        public static double Error_of_SLAE(Matrix A, Vector x, Vector b)
        {
            int N = x.Size; double error = 0.0; Vector bc = Vector.Multiply(A, x);
            for (int i = 1; i <= N; i++) error += (b[i] - bc[i]) * (b[i] - bc[i]);
            return Math.Sqrt(error) / N;
        } 
    }
}
