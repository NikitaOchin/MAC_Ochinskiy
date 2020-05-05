using MAC_DLL;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            SW.Close();
        }
    }
}
