using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyF = MAC_DLL.MAC_My_Functions;

namespace MAC_LabWork_1_2
{
    class Main_LW_1_2
    {
        static double A, B, C, e;

        static void Main(string[] args)
        {
            //Console.WriteLine(TestMySinCos());
            //Console.WriteLine(TestMySinhCosh());
            //Console.WriteLine($"{Math.Log(MyF.MyExp(-10.3, 1.0E-20)),36:F22}");

            A = 15.0; B = 17.0; C = -11.0; e = 1.0E-20;
            A = 1.50; B = 1.70; C = -1.10; e = 1.0E-20;
            Console.WriteLine(Test_DLL());
            Console.WriteLine(Test_Math());

            Console.WriteLine(" \r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n  HOMEWORK");
            A = 12.30; B = 22.40; e = 1.0E-20;
            Console.WriteLine($"Test 1 A = 12.30 B = 22.40 \r\n{Test_DLL_Home()}");
            Console.WriteLine(Test_Math_Home());

            A = 3.57; B = 1.98; e = 1.0E-20;
            Console.WriteLine($"Test 2 A = 3.57 B = 1.98 \r\n{Test_DLL_Home()}");
            Console.WriteLine(Test_Math_Home());
        }

        static string Test_DLL()
        {
            double d0 = MyF.MySin(A+B+C,e);
            double d1 = MyF.MySin(A,e) * MyF.MyCos(B,e) * MyF.MyCos(C,e);
            double d2 = MyF.MyCos(A, e) * MyF.MySin(B, e) * MyF.MyCos(C, e);
            double d3 = MyF.MyCos(A, e) * MyF.MyCos(B, e) * MyF.MySin(C, e);
            double d4 = MyF.MySin(A, e) * MyF.MySin(B, e) * MyF.MySin(C, e);
            double error = Math.Abs(d0 - (d1 + d2 + d3 - d4));
            return $"   MAC = {d0,19:F16}   error = {error,10:E2}";
        }

        static string Test_Math()
        {
            double d0 = Math.Sin(A + B + C);
            double d1 = Math.Sin(A) * Math.Cos(B) * Math.Cos(C);
            double d2 = Math.Cos(A) * Math.Sin(B) * Math.Cos(C);
            double d3 = Math.Cos(A) * Math.Cos(B) * Math.Sin(C);
            double d4 = Math.Sin(A) * Math.Sin(B) * Math.Sin(C);
            double error = Math.Abs(d0 - (d1 + d2 + d3 - d4));
            return $"   Math = {d0,19:F16}   error = {error,10:E2}";
        }

        static string TestMySinCos()
        {
            string txt = "           Test of MySinCos() \r\n";
            double unit, error, e = 1.0E-20;
            for(double x = 1.0; x <= 40; x += 1.0)
            {
                unit = MyF.MyCos(x, e) * MyF.MyCos(x, e) + MyF.MySin(x, e) * MyF.MySin(x, e);
                error = (1.0 - unit);
                txt += $"{x,7:F1}{unit,20:F16}{error,20:F16}\r\n";
            }
            return txt;
        }

        static string TestMySinhCosh()
        {
            string txt = "            Test of MySinhCosh() \r\n";
            double unit, error, e = 1.0E-20;
            for (double x = 0.0; x <= 20; x += 1.0)
            {
                unit = (MyF.MyCosh(x, e) - MyF.MySinh(x, e)) * 
                       (MyF.MyCosh(x, e) + MyF.MySinh(x, e));
                error = (1.0 - unit);
                txt += $"{x,7:F1}{unit,20:F16}{error,20:F16}\r\n";
            }
            return txt;
        }

        static string Test_DLL_Home()
        {
            double d0 = MyF.MyCos(A, e) + MyF.MyCos(B,e);
            double d1 = 2* MyF.MyCos((A+B)/2,e)*MyF.MyCos((A-B)/2,e);
            double error = Math.Abs(d0 - d1);
            return $"   MAC Ф1 = {d0,19:F15}   MAC Ф2 = {d1,19:F15}  error = {error,10:E2}";
        }

        static string Test_Math_Home()
        {
            double d0 = Math.Cos(A) + Math.Cos(B);
            double d1 = 2 * Math.Cos((A + B) / 2) * Math.Cos((A - B) / 2);
            double error = Math.Abs(d0 - d1);
            return $"   Math Ф1 = {d0,19:F15}   Math Ф2 = {d1,19:F15}  error = {error,10:E2}";
        }
    }
}
