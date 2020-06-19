using System;
using System.IO;
using CLTF = MAC_DLL.MAC_My_Definitions.MyTableOfFunction;
using CLIn = MAC_DLL.MAC_Interpolation;
using CLTD = MAC_DLL.MAC_My_Definitions.MyTableOfData;

namespace MAC_LabWork_3_1
{
    class Main_LW_3_1
    {
        static StreamWriter SW = new StreamWriter("LW_3_1_Results.txt");

        static CLTF Table_G, Table_L; static int m, n;

        static void Main(string[] args)
        {
            //Test_1();
            //Test_2(9);
            //Test_3("LW_3_1_v00.bin");
            m = 7; n = 20; Test_4();
        }

        static void Test_4()
        {
            double xo = -0.4, xn = 1.3, eps = 1.0E-13, err_G, err_L;
            const double root = Math.PI / 6.0;

            Table_G = new CLTF(xo, xn, n, G316, " Table_G");
            Table_G.Roots_correction(eps);
            err_G = Math.Abs(Table_G.Roots[0].X - root);

            Table_L = new CLTF(xo, xn, n, L316, " Table_L");
            Table_L.Roots_correction(eps);
            err_L = Math.Abs(Table_L.Roots[0].X - root);

            SW.WriteLine($"            n = {n} m = {m}");
            SW.WriteLine($"   true value = {root:F14}\r\n");
            SW.WriteLine($" Table_G root = {Table_G.Roots[0].X:F14}");
            SW.WriteLine($"        err_G = {err_G:F14}  K = {Table_G.Roots[0].Iters}");
            SW.WriteLine($" Table_L root = {Table_L.Roots[0].X:F14}");
            SW.WriteLine($"        err_L = {err_L:F14}  K = {Table_L.Roots[0].Iters}");
            SW.Close();
        }
        public static double G316(double x)
        {
            return 2.0* Math.Cos(x) - Math.Sqrt(3.0);
        }
        public static double L316(double x)
        {
            return CLIn.Polynomial_Lagrange(m, x, Table_G.Points);
        }

        static void Test_3(string file)
        {
            CLTD test = new CLTD(file, " Unknown Function");
            //SW.WriteLine(test.Table_in_File);
            test.power = 9;
            test.Roots_correction(1.0E-12);
            SW.WriteLine(test.Table_of_Function());
            SW.Close();
        }

        static void Test_2(int m)
        {
            int i, n = 20;
            CLTF cos = new CLTF(-3.0, 3.0, n, F314, " Test_2");

            SW.Write(" Интерполяцияю Полином лагранжа {0}-го порядка:", m);
            string txt = "\r\n"; double xz, fz, Ln, err;
            for(i = 1; i <= n; i++)
            {
                xz = 0.5 * (cos.Points[i - 1].X + cos.Points[i].X);
                Ln = CLIn.Polynomial_Lagrange(m, xz, cos.Points);
                fz = F314(xz);
                err = Math.Abs(Ln - fz);
                txt += $"{i,4}{xz,8:F3}{fz,18:F12}{Ln,18:F12}{err,18:F12}\r\n";
            }
            SW.Write(txt);
            SW.Close();
        }
        public static double F314(double x)
        {
            return Math.Cos(Math.PI * x);
        }

        static void Test_1()
        {
            CLTF table = new CLTF(-1.5, 1.5, 5, F311, " Test_1");
            table.Roots_correction(1.0E-10);
            SW.WriteLine(table.Table_of_Function());

            SW.WriteLine("\r\n Інтерполяція за Лагранжем: ");
            double xz, fz, Ln, err; string txt = "";
            for(int i = 1; i< table.Length; i++)
            {
                xz = 0.5 * (table.Points[i - 1].X + table.Points[i].X);
                fz = F311(xz);
                Ln = CLIn.Polynomial_Lagrange(xz, table.Points);
                err = Math.Abs(Ln - fz);
                txt += $"{i,4}{xz,8:F3}{fz,18:F12}{Ln,18:F12}{err,18:F12}\r\n";
            }
            SW.Write(txt);
            SW.Close();
        }
        public static double F311(double x)
        {
            return (x - 2.0) * (x - 1.0) * x * (x + 1.0) * (x + 2.0);
        }
    }
}
