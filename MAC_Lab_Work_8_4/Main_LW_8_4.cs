using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using SODE_1 = MAC_DLL.MAC_Sys_of_ODE_Order_1_RungeKutta_1;
using SODE_2 = MAC_DLL.MAC_Sys_of_ODE_Order_1_RungeKutta_2;
using SODE_3 = MAC_DLL.MAC_Sys_of_ODE_Order_1_RungeKutta_3;
using SODE_4 = MAC_DLL.MAC_Sys_of_ODE_Order_1_RungeKutta_4;
using CSP = MAC_DLL.MAC_My_Definitions.Cauchy_Sys_Point;

namespace MAC_LabWork_8_4
{
    partial class Main_LW_8_4
    {
        static StreamWriter SW = new StreamWriter("MAC_LW_8_4_Очинский_v7.txt");
        static double eps = 1.0E-6, Sy1, Sz1, err, x1;
        static CSP csp0;

        static void Main(string[] args)
        {
            //csp0 = new CSP(0.0, 3.0, 5.0); x1 = 0.25;

            //Sy1 = Sy_00(x1); Sz1 = Sz_00(x1);
            //Test_1_00(x1); Test_2_00(x1); 
            //Test_3_00(x1); 
            //Test_4_00(x1);

            csp0 = new CSP(0.0, 1.0, 3.0); x1 = 0.27;

            Sy1 = Sy_07(x1); Sz1 = Sz_07(x1);
            Test_1_07(x1); Test_2_07(x1);
            Test_3_07(x1);
            Test_4_07(x1);
            SW.Close();
        }
        static void Test_1_07(double x1)
        {
            SODE_1 RG_1 = new SODE_1(csp0, f_07, g_07);
            CSP csp1 = RG_1.Solve_with_Precision(x1, eps);
            err = Math.Abs(Sy1 - csp1.y) + Math.Abs(Sz1 - csp1.z);
            SW.WriteLine($"\r\n Test - MAC_Sys_of_ODE_O1_RungeKutta_1:");
            SW.WriteLine($" {csp1.x,8:F4}{csp1.y,14:F9}{Sy1,14:F9}"
                       + $" {csp1.z,14:F9}{Sz1,14:F9}"
                       + $"{err,11:E1}   {RG_1.iter}");
        }
        static void Test_2_07(double x1)
        {
            SODE_2 RG_2 = new SODE_2(csp0, f_07, g_07);
            CSP csp1 = RG_2.Solve_with_Precision(x1, eps);
            err = Math.Abs(Sy1 - csp1.y) + Math.Abs(Sz1 - csp1.z);
            SW.WriteLine($"\r\n Test - MAC_Sys_of_ODE_O1_RungeKutta_2:");
            SW.WriteLine($" {csp1.x,8:F4}{csp1.y,14:F9}{Sy1,14:F9}"
                       + $" {csp1.z,14:F9}{Sz1,14:F9}"
                       + $"{err,11:E1}   {RG_2.iter}");
        }
        static void Test_3_07(double x1)
        {
            SODE_3 RG_3 = new SODE_3(csp0, f_07, g_07);
            CSP csp1 = RG_3.Solve_with_Precision(x1, eps);
            err = Math.Abs(Sy1 - csp1.y) + Math.Abs(Sz1 - csp1.z);
            SW.WriteLine($"\r\n Test - MAC_Sys_of_ODE_O1_RungeKutta_2:");
            SW.WriteLine($" {csp1.x,8:F4}{csp1.y,14:F9}{Sy1,14:F9}"
                       + $" {csp1.z,14:F9}{Sz1,14:F9}"
                       + $"{err,11:E1}   {RG_3.iter}");
        }
        static void Test_4_07(double x1)
        {
            SODE_4 RG_4 = new SODE_4(csp0, f_07, g_07);
            CSP csp1 = RG_4.Solve_with_Precision(x1, eps);
            err = Math.Abs(Sy1 - csp1.y) + Math.Abs(Sz1 - csp1.z);
            SW.WriteLine($"\r\n Test - MAC_Sys_of_ODE_O1_RungeKutta_2:");
            SW.WriteLine($" {csp1.x,8:F4}{csp1.y,14:F9}{Sy1,14:F9}"
                       + $" {csp1.z,14:F9}{Sz1,14:F9}"
                       + $"{err,11:E1}   {RG_4.iter}");
        }

        static double f_07(double x, double y, double z)
        { return 2 * z  - y + 1; }
        static double g_07(double x, double y, double z)
        { return 3*z - 2*y; }

        static double Sy_07(double x)
        { return 2 * (2 + x) * Math.Exp(x) - 3; }
        static double Sz_07(double x)
        { return (5 + 2*x) * Math.Exp(x) - 2; }



        //static void Test_1_00(double x1)
        //{
        //    SODE_1 RG_1 = new SODE_1(csp0, f_00, g_00);
        //    CSP csp1 = RG_1.Solve_with_Precision(x1, eps);
        //    err = Math.Abs(Sy1 - csp1.y) + Math.Abs(Sz1 - csp1.z);
        //    SW.WriteLine($"\r\n Test - MAC_Sys_of_ODE_O1_RungeKutta_1:");
        //    SW.WriteLine($" {csp1.x,8:F4}{csp1.y,14:F9}{Sy1,14:F9}"
        //               + $" {csp1.z,14:F9}{Sz1,14:F9}"
        //               + $"{err,11:E1}   {RG_1.iter}");
        //}
        //static void Test_2_00(double x1)
        //{
        //    SODE_2 RG_2 = new SODE_2(csp0, f_00, g_00);
        //    CSP csp1 = RG_2.Solve_with_Precision(x1, eps);
        //    err = Math.Abs(Sy1 - csp1.y) + Math.Abs(Sz1 - csp1.z);
        //    SW.WriteLine($"\r\n Test - MAC_Sys_of_ODE_O1_RungeKutta_2:");
        //    SW.WriteLine($" {csp1.x,8:F4}{csp1.y,14:F9}{Sy1,14:F9}"
        //               + $" {csp1.z,14:F9}{Sz1,14:F9}"
        //               + $"{err,11:E1}   {RG_2.iter}");
        //}
        //static void Test_3_00(double x1)
        //{
        //    SODE_3 RG_3 = new SODE_3(csp0, f_00, g_00);
        //    CSP csp1 = RG_3.Solve_with_Precision(x1, eps);
        //    err = Math.Abs(Sy1 - csp1.y) + Math.Abs(Sz1 - csp1.z);
        //    SW.WriteLine($"\r\n Test - MAC_Sys_of_ODE_O1_RungeKutta_2:");
        //    SW.WriteLine($" {csp1.x,8:F4}{csp1.y,14:F9}{Sy1,14:F9}"
        //               + $" {csp1.z,14:F9}{Sz1,14:F9}"
        //               + $"{err,11:E1}   {RG_3.iter}");
        //}
        //static void Test_4_00(double x1)
        //{
        //    SODE_4 RG_4 = new SODE_4(csp0, f_00, g_00);
        //    CSP csp1 = RG_4.Solve_with_Precision(x1, eps);
        //    err = Math.Abs(Sy1 - csp1.y) + Math.Abs(Sz1 - csp1.z);
        //    SW.WriteLine($"\r\n Test - MAC_Sys_of_ODE_O1_RungeKutta_2:");
        //    SW.WriteLine($" {csp1.x,8:F4}{csp1.y,14:F9}{Sy1,14:F9}"
        //               + $" {csp1.z,14:F9}{Sz1,14:F9}"
        //               + $"{err,11:E1}   {RG_4.iter}");
        //}

        //static double f_00(double x, double y, double z)
        //{ return 2 * y + z + Math.Exp(x); }
        //static double g_00(double x, double y, double z)
        //{ return 2 * x - 2 * y; }

        //static double Sy_00(double x)
        //{ return (1 + Math.Cos(x) + 9 * Math.Sin(x)) * Math.Exp(x) + x + 1; }
        //static double Sz_00(double x)
        //{ return (8 * Math.Cos(x) - 10 * Math.Sin(x) - 2) * Math.Exp(x) - 2 * x - 1; }
    }
}

