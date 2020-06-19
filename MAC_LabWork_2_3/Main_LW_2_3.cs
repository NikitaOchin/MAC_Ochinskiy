using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MAC_DLL;
using System.IO;
using PT = MAC_DLL.PrintType;

namespace MAC_LabWork_2_3
{
    class Main_LW_2_3
    {
        static StreamWriter SW;

        static void Main(string[] args)
        {
            SW = new StreamWriter("Test_SLAE_LW_2_3.txt");
            Test_SLAE();// Test_Inversion();
            Test_SLAE_home();
            SW.Close();
        }

        static void Test_SLAE()
        {
            //SW = new StreamWriter("Test_SLAE_LW_2_3.txt");

            string file = "LW_2_3_Ab_v00.txt"; int Variant = 0;
            SW.WriteLine($"\r\n {file} Variant = {Variant}");

            Matrix.Read(file, out Matrix A, out Vector b, out int n);
            SW.Write(Matrix.Print(A, b, true, 2, 3, "Matrix Ab"));

            Matrix V = Matrix.Inversion_1(A, out double err);
            SW.Write("\r\n Inversion_1 : ");
            SW.Write(Matrix.Print(V, true, 2, 7, "Matrix V"));
            SW.WriteLine($"\r\n Determinant|A| = {A.Det,12:F2}" +
                         $"     Error  = {err,10:E1}");

            Vector X = Vector.Multiply(V, b); //Розв'язування СЛАР

            SW.Write("\r\n Solving SLAE with Inversion_1 Matrix :");
            SW.Write(Vector.Print(X, PT.Vertical, true, 2, 12, "Vector X"));

            double error = MAC_Algebra.Error_of_SLAE(A, X, b);
            SW.WriteLine($"\r\n Error_of_SLAE = {error,10:E1}");

            //SW.Close();
        }

        static void Test_SLAE_home()
        {

            string file = "LW_2_3_3_Ab_v02.txt"; int Variant = 2;
            SW.WriteLine($"\r\n HOME WORK  \r\n {file} Variant = {Variant}");

            Matrix.Read(file, out Matrix A, out Vector b, out int n);
            SW.Write(Matrix.Print(A, b, true, 2, 3, "Matrix Ab"));

            Matrix V = Matrix.Inversion_1(A, out double err);
            SW.Write("\r\n Inversion_1 : ");
            SW.Write(Matrix.Print(V, true, 2, 7, "Matrix V"));
            SW.WriteLine($"\r\n Determinant|A| = {A.Det,12:F2}" +
                         $"     Error  = {err,10:E1}");

            Vector X = Vector.Multiply(V, b); //Розв'язування СЛАР

            SW.Write("\r\n Solving SLAE with Inversion_1 Matrix :");
            SW.Write(Vector.Print(X, PT.Vertical, true, 2, 10, "Vector X"));

            double error = MAC_Algebra.Error_of_SLAE(A, X, b);
            SW.WriteLine($"\r\n Error_of_SLAE = {error,10:E1}");

        }

        static void Test_Inversion()
        {
            SW = new StreamWriter("Test_Inversion_LW_2_3.txt");
            string file = "LW_2_3_A_v00.txt"; int Variant = 0;
            SW.WriteLine($"\r\n {file} Variant = {Variant}");

            Matrix.Read(file, out Matrix A, out int n);
            SW.Write(Matrix.Print(A, true, 2, 1, "Matrix A"));

            Matrix V = Matrix.Inversion_1(A, out double err1);
            SW.Write("\r\n Inversion_1 : ");
            SW.Write(Matrix.Print(V, true, 2, 7, "Matrix V"));

            SW.WriteLine($"\r\n Determinant|A| = {A.Det,12:F2}" +
                         $"     Error 1 = {err1,10:E1}");

            V = Matrix.Inversion_2(A, out double err2);
            SW.Write("\r\n Inversion_2 : ");
            SW.Write(Matrix.Print(V, true, 2, 7, "Matrix V"));

            SW.WriteLine($"\r\n Determinant|A| = {A.Det,12:F2}" +
                         $"     Error 2 = {err2,10:E1}");

            SW.Close();
        }
    }
}
