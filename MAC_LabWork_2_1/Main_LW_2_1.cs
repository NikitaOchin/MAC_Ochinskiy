using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PT = MAC_DLL.PrintType;
using MAC_DLL;

namespace MAC_LabWork_2_1
{
    class Main_LW_2_1
    {
        static StreamWriter SW = new StreamWriter("Results_LW_2_1.txt");

        static void Main(string[] args)
        {
            //Test_MJG();
            //Test_LW_2_1();
            Home_LW_2_1();
        }

        static void Home_LW_2_1()
        {
            Matrix.Read("LW_2_1_Ab_v02.txt", out Matrix A, out Vector b, out int n);
            SW.WriteLine($"LW_2_1_Ab_v02.txt  Variant - 2 Ochinskiy Nikita");

            SW.Write(Matrix.Print(A, b, true, 3, 2, "Matrix Ab"));

            Vector X = MAC_Algebra.Method_Jordana_Gaussa(A, b);
            SW.Write("\r\n Solving SLAE with Method Jordana-Gaussa :");
            SW.Write(Vector.Print(X, PT.Vertical, true, 3, 2, "Vector X"));
            SW.WriteLine($"\r\n Determinant|A| = {A.Det,7:F1}");

            double error = MAC_Algebra.Error_of_SLAE(A, X, b);
            SW.WriteLine($"\r\n error = {error,10:E1}");
            SW.Close();
        }

        static void Test_LW_2_1() 
        {
            Matrix.Read("LW_2_1_A_v00.txt", out Matrix A, out int n);
            SW.Write(Matrix.Print(A, true, 3, 2, "Matrix A"));

            Vector.Read("LW_2_1_b_v00.txt", out Vector b, out n);
            SW.Write(Vector.Print(b, PT.Horizontal, true, 3, 2, "Vector b"));

            Vector X = MAC_Algebra.Method_Jordana_Gaussa(A, b);
            SW.Write("\r\n Solving SLAE with Method Jordana-Gaussa :");
            SW.Write(Vector.Print(X, PT.Vertical, true, 3, 2, "Vector X"));
            SW.WriteLine($"\r\n Determinant|A| = {A.Det,7:F1}");

            double error = MAC_Algebra.Error_of_SLAE(A, X, b);
            SW.WriteLine($"\r\n error = {error,10:E1}");
            SW.Close();

        }
        static void Test_MJG()
        {
            Matrix.Read("test_LW_2_1_A.txt", out Matrix A, out int n);
            SW.Write(Matrix.Print(A, true, 3, 2, "Matrix A"));

            Vector.Read("test_LW_2_1_b.txt", out Vector b, out n);
            SW.Write(Vector.Print(b, PT.Vertical, true, 3, 2, "Vector b"));

            Vector X = MAC_Algebra.Method_Jordana_Gaussa(A, b);
            SW.Write(Vector.Print(X, PT.Vertical, true, 3, 2, "Vector X"));
            SW.WriteLine($"\r\n Determinant|A| = {A.Det,7:F1}");

            SW.Write(Matrix.Print(A, true, 3, 2, "Matrix A"));
            SW.Write(Vector.Print(b, PT.Horizontal, true, 3, 2, "Vector b"));
            SW.Close();

        }
    }
}
