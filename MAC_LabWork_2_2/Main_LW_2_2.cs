using MAC_DLL;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PT = MAC_DLL.PrintType;

namespace MAC_LabWork_2_2
{
    class Main_LW_2_2
    {
        static void Main(string[] args)
        {
            StreamWriter SW = new StreamWriter("Results_LW_2_2.txt");

            string file = "LW_2_2_Ab_v00.txt"; int Variant = 0;
            SW.WriteLine($"\r\n {file} Variant = {Variant}");

            Matrix.Read(file, out Matrix A, out Vector b, out int n);
            SW.Write(Matrix.Print(A, b, true, 3, 3, "Matrix Ab"));

            Vector X = MAC_Algebra.Method_Zeidela(A, b, 1.0E-8, out int K);

            SW.Write("\r\n Solving SLAE with Method_Zeidela :");
            SW.Write(Vector.Print(X, PT.Horizontal, true, 3, 7, "Vector X"));

            double error = MAC_Algebra.Error_of_SLAE(A, X, b);
            SW.WriteLine($"\r\n Error = {error,10:E1} iterations = {K}");

            //HOMEWORK
            string file_h = "LW_2_2_Ab_3_v02.txt"; int Variant_h = 2;
            SW.WriteLine($"\r\n HOME_WORK Ochinskiy \r\n {file_h} Variant = {Variant_h}");

            Matrix.Read(file_h, out Matrix A_h, out Vector b_h, out int n_h);
            SW.Write(Matrix.Print(A_h, b_h, true, 3, 3, "Matrix Ab"));

            Vector X_h = MAC_Algebra.Method_Zeidela(A_h, b_h, 1.0E-8, out int K_h);

            SW.Write("\r\n Solving SLAE with Method_Zeidela :");
            SW.Write(Vector.Print(X_h, PT.Horizontal, true, 3, 7, "Vector X"));

            double error_h = MAC_Algebra.Error_of_SLAE(A_h, X_h, b_h);
            SW.WriteLine($"\r\n Error = {error_h,10:E1} iterations = {K_h}");

            SW.Close();

        }
    }
}
