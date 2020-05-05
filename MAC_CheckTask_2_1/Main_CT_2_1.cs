using MAC_DLL;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PT = MAC_DLL.PrintType;

namespace MAC_CheckTask_2_1
{
    class Main_CT_2_1
    {
        static void Main(string[] args)
        {
            StreamWriter SW = new StreamWriter("Results_CT_2_1.txt");
            SW.WriteLine($"CT_2_1_Ab_3_v12.txt   Variant - 12 Ochinskiy Nikita");

            Matrix.Read("CT_2_1_Ab_3_v12.txt", out Matrix A, out Vector b, out int n);
            SW.Write(Matrix.Print(A, b, true, 3, 2, "Matrix Ab"));

            Vector X = MAC_Algebra.Method_Gaussa(A, b);
            SW.Write("\r\n Solving SLAE with Method Gaussa :");
            SW.Write(Vector.Print(X, PT.Horizontal, true, 3, 2, "Vector X"));
            SW.WriteLine($"\r\n Determinant|A| = {A.Det,7:F1}");

            double error = MAC_Algebra.Error_of_SLAE(A, X, b);
            SW.WriteLine($"\r\n error = {error,10:E1}");
            SW.Close();
        }
    }
}
