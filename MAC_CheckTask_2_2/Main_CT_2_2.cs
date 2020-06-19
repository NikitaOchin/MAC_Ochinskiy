using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using MAC_DLL;
using PT = MAC_DLL.PrintType;


namespace MAC_CheckTask_2_2
{
    class Main_CT_2_2
    {
        static void Main(string[] args)
        {
            StreamWriter SW = new StreamWriter("Results_CT_2_2.txt");
            string file = "CT_2_2_Ab_v00.txt"; int Variant = 0;
            SW.WriteLine($"\r\n {file} Variant = {Variant}");

            Matrix.Read(file, out Matrix A, out Vector b, out int n);
            SW.Write(Matrix.Print(A, b, true, 2, 3, "Matrix Ab"));

            Vector X = MAC_Algebra.Method_Simple_Iteration(A, b, 1.0E-5, out int K);

            SW.Write("\r\n Solving SLAE with Method_Simple_Iteration :");
            SW.Write(Vector.Print(X, PT.Vertical, true, 3, 10, "Vector X"));

            double error = MAC_Algebra.Error_of_SLAE(A, X, b);
            SW.WriteLine($"\r\n Error = {error,10:E1} iterations = {K}");

            X = MAC_Algebra.Method_Simple_Iteration(A, b, 1.0E-10, out K);

            SW.Write(Vector.Print(X, PT.Vertical, true, 3, 10, "Vector X"));

            error = MAC_Algebra.Error_of_SLAE(A, X, b);
            SW.WriteLine($"\r\n Error = {error,10:E1} iterations = {K}");

            // HOMEWORK
            file = "CT_2_2_Ab_3_v02.txt"; Variant = 2;
            SW.WriteLine($"\r\n HOME WORK \r\n {file} Variant = {Variant}");

            Matrix.Read(file, out A, out b, out n);
            SW.Write(Matrix.Print(A, b, true, 2, 3, "Matrix Ab"));

            X = MAC_Algebra.Method_Simple_Iteration(A, b, 1.0E-5, out K);

            SW.Write("\r\n Solving SLAE with Method_Simple_Iteration :");
            SW.Write(Vector.Print(X, PT.Vertical, true, 3, 10, "Vector X"));

            error = MAC_Algebra.Error_of_SLAE(A, X, b);
            SW.WriteLine($"\r\n Error = {error,10:E1} iterations = {K}");

            X = MAC_Algebra.Method_Simple_Iteration(A, b, 1.0E-10, out K);

            SW.Write(Vector.Print(X, PT.Vertical, true, 3, 10, "Vector X"));

            error = MAC_Algebra.Error_of_SLAE(A, X, b);
            SW.WriteLine($"\r\n Error = {error,10:E1} iterations = {K}");
            SW.Close();
        }
    }
}
