using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyF = MAC_DLL.MAC_My_Functions;

namespace MAC_CheckTask_1_2
{
    class Main_CT_1_2
    {
        static double a, b, x1, x2, eps = 1.0E-9;

        static void Main(string[] args)
        {
            //a = 3.0; b = 1.8; x1 = 3.9; x2 = 5.7;
            //Console.WriteLine($"f({x1,3:F1}) = {MyF.f0(x1, a, b, eps),13:F10}");
            //Console.WriteLine($"f({x2,3:F1}) = {MyF.f0(x2, a, b, eps),13:F10}");
            
            a = 0.50; b = 1.50; x1 = 2.50; x2 = 3.20;
            Console.WriteLine("Test Value f2");
            Console.WriteLine($"f({x1,3:F1}) = {MyF.f2(x1, a, b, eps),13:F10}");
            Console.WriteLine($"f({x2,3:F1}) = {MyF.f2(x2, a, b, eps),13:F10}");

            a = 0.87; b = 1.33; x1 = 2.36; x2 = 3.71;
            Console.WriteLine("\r\nMy Value f2");
            Console.WriteLine($"f({x1,3:F1}) = {MyF.f2(x1, a, b, eps),13:F10}");
            Console.WriteLine($"f({x2,3:F1}) = {MyF.f2(x2, a, b, eps),13:F10}");

        }
    }
}
