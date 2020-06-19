using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MAC_DLL;
using System.IO;
using PT = MAC_DLL.PrintType;

namespace MAC_CheckTask_2_3
{
    class Main_CT_2_3
    {
        static void Main(string[] args)
        {
            StreamWriter SW = new StreamWriter("Results_CT_2_3.txt");

            string file = "CT_2_3_Ab_v00.txt"; int Variant = 0;
            SW.WriteLine($"\r\n {file} Variant = {Variant}");

            Matrix.Read(file, out Matrix A, out Vector b, out int n);
            SW.Write(Matrix.Print(A, b, true, 2, 1, "Matrix Ab"));

            Vector X = MAC_Algebra.Method_Kramera(A, b, out Vector Dk);
            SW.Write(Vector.Print(Dk, PT.Vertical, true, 2, 2, "Vector Dk"));

            SW.Write("\r\n Solving SLAE with Method Kramera :");
            SW.Write(Vector.Print(X, PT.Horizontal, true, 2, 2, "Vector X"));


            double error = MAC_Algebra.Error_of_SLAE(A, X, b);
            SW.WriteLine($"\r\n Determinant|A| = {A.Det,12:F2}" +
                         $"     Error = {error,10:E1}");

            //HOMEWORK
            file = "CT_2_3_3_Ab_v02.txt"; Variant = 2;
            SW.WriteLine($"\r\n HOME WORK \r\n {file} Variant = {Variant}");

            Matrix.Read(file, out A, out b, out n);
            SW.Write(Matrix.Print(A, b, true, 2, 1, "Matrix Ab"));

            X = MAC_Algebra.Method_Kramera(A, b, out Dk);
            SW.Write(Vector.Print(Dk, PT.Vertical, true, 2, 2, "Vector Dk"));

            SW.Write("\r\n Solving SLAE with Method Kramera :");
            SW.Write(Vector.Print(X, PT.Horizontal, true, 2, 2, "Vector X"));


            error = MAC_Algebra.Error_of_SLAE(A, X, b);
            SW.WriteLine($"\r\n Determinant|A| = {A.Det,12:F2}" +
                         $"     Error = {error,10:E1}");


            SW.Close();
        }
    }
}
