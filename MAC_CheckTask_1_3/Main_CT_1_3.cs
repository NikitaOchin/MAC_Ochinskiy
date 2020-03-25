using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyF = MAC_DLL.MAC_My_Functions;
using MyTF = MAC_DLL.MAC_My_Definitions.MyTableOfFunction;

namespace MAC_CheckTask_1_3
{
    class Main_CT_1_3
    {
        static double par_a, par_b, par_e;

        static void Main(string[] args)
        {
            //par_a = 3.0; par_b = 1.8; par_e = 1.0E-9;

            //MyTF TF = new MyTF(1.0, 7.0, 60, dummy_fx, "dummy f(x)");
            ////Console.WriteLine(TF.ToPrint("Test table"));
            //TF.To_txt_File("Test_CT_1_3.txt", "New result's form");

            //HOME WORK REGION
            //Test for home
            par_a = 0.5; par_b = 1.5; par_e = 1.0E-9;
            double E1 = 2.5; double E2 = 3.2;

            MyTF TF = new MyTF(1.3, 4.4, 155, home_dummy_fx, "home_dummy f(x)");
            Console.WriteLine("\r\nTest for home");
            Console.WriteLine("Count root of function on interval: " + TF.Roots.Count() + "\r\n");
            Console.WriteLine("Maximum: \r\n" + $"{TF.Maximum.F:F10}" + "\r\nMinimum: \r\n"+ $"{TF.Minimum.F:F10}");
            Console.WriteLine("F(E1): \r\n" + $"{home_dummy_fx(E1):F10}" + "\r\nF(E2): \r\n" + $"{home_dummy_fx(E2):F10}\r\n");

            //Home
            par_a = 0.8; par_b = 1.4; par_e = 1.0E-9;
            E1 = 2.7; E2 = 3.4;

            Console.WriteLine("\r\nHome");
            MyTF Home_TF = new MyTF(1.5, 4.3, 140, home_dummy_fx, "home_dummy f(x)");
            Console.WriteLine("Count root of function on interval: " + Home_TF.Roots.Count() + "\r\n");
            Console.WriteLine("Maximum: \r\n" + $"{Home_TF.Maximum.F:F10}" + "\r\nMinimum: \r\n" + $"{Home_TF.Minimum.F:F10}");
            Console.WriteLine("F(E1): \r\n" + $"{home_dummy_fx(E1):F10}" + "\r\nF(E2): \r\n" + $"{home_dummy_fx(E2):F10}");
            Console.WriteLine("\r\nУТОЧНИ КОЛ-ВО ЗНАКОВ ПОСЛЕ ЗАПЯТОЙ!\r\n");

        }

        static double dummy_fx(double x)
        {
            return MyF.f0(x, par_a, par_b, par_e);
        }

        static double home_dummy_fx(double x)
        {
            return MyF.f2(x, par_a, par_b, par_e);
        }
    }
}
