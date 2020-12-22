using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using ODE_2_A = MAC_DLL.MAC_ODE_Order_2_RungeKutta_4_A;
using ODE_2_B = MAC_DLL.MAC_ODE_Order_2_RungeKutta_4_B;
using CP = MAC_DLL.MAC_My_Definitions.Cauchy_Point;

namespace MAC_LabWork_8_3
{
    partial class Main_LW_8_3
    {
        static StreamWriter SW = new StreamWriter("MAC_CT_8_3.txt");
        static double eps = 1.0E-6, S1, err;
        static void Main(string[] args)
        {
            //Test_A(4.0);
            //Test_B(1.0);
            Test_Ah(Math.Sqrt(6));
            Test_Bh(Math.Log(2));
            SW.Close();
        }

        private static void Test_Bh(double x1)
        {
            CP cpO = new CP(0.0, 36.0/37.0, 3.0/37.0);
            ODE_2_B RG_B = new ODE_2_B(cpO, f_OBh);
            CP cp1 = RG_B.Solve_with_Precision(x1, eps);

            S1 = S_OBh(x1); err = Math.Abs(S1 - cp1.y);
            SW.WriteLine($"\r\n Test - MAC_ODE_Order_2_RungeKutta_4_B  :");
            SW.WriteLine($"{cp1.x,8:F4} {cp1.y,14:F9} {S1,14:F9} {err,11:E1} {RG_B.iter}");
        }

        private static double S_OBh(double x)
        {
            return 0.5 * (Math.Exp(3 * x) + Math.Exp(-3 * x)) + Math.Exp(3 * x) * (6 * Math.Sin(x) - Math.Cos(x)) / 37.0;
        }

        private static double f_OBh(double x, double y)
        {
            return 9*y + Math.Exp(3 * x) * Math.Cos(x);
        }


        private static void Test_Ah(double x1)
        {
            CP cpO = new CP(0.0, -6.0, 14.0);
            ODE_2_A RG_A = new ODE_2_A(cpO, f_OAh);
            CP cp1 = RG_A.Solve_with_Precision(x1, eps);

            S1 = S_OAh(x1); err = Math.Abs(S1 - cp1.y);
            SW.WriteLine($"\r\n Test - MAC_ODE_Order_2_RungeKutta_4_A  :");
            SW.WriteLine($"{cp1.x,8:F4} {cp1.y,14:F9} {S1,14:F9} {err,11:E1} {RG_A.iter}");
        }

        private static double S_OAh(double x)
        {
            return 5 * Math.Exp(-x) * (1 + x) + (2 * x*x*x) - 8*x*x +14*x - 11;
        }

        private static double f_OAh(double x, double y, double dy)
        {
            return -2*dy - y + 2*x*x*x + 4*x*x - 6*x + 1;
        }


        //LABWORK
        private static void Test_B(double x1)
        {
            CP cpO = new CP(0.0, 1.0, 5.0);
            ODE_2_B RG_B = new ODE_2_B(cpO, f_OB);
            CP cp1 = RG_B.Solve_with_Precision(x1, eps);

            S1 = S_OB(x1); err = Math.Abs(S1 - cp1.y);
            SW.WriteLine($"\r\n Test - MAC_ODE_Order_2_RungeKutta_4_B  :");
            SW.WriteLine($"{cp1.x,8:F4} {cp1.y,14:F9} {S1,14:F9} {err,11:E1} {RG_B.iter}");
        }

        private static double S_OB(double x)
        {
            return Math.Cos(3.0 * x) * (1.0 + 2.0 * x) +
                   Math.Sin(3.0 * x) * (1.0 + 3.0 * x);
        }

        private static double f_OB(double x, double y)
        {
            return 18.0 * Math.Cos(3.0 *x) - 12.0 * Math.Sin(3.0 * x) - 9.0 * y;
        }


        private static void Test_A(double x1)
        {
            CP cpO = new CP(0.0, 1.0, 0.0);
            ODE_2_A RG_A = new ODE_2_A(cpO, f_OA);
            CP cp1 = RG_A.Solve_with_Precision(x1, eps);

            S1 = S_OA(x1); err = Math.Abs(S1 - cp1.y);
            SW.WriteLine($"\r\n Test - MAC_ODE_Order_2_RungeKutta_4_A  :");
            SW.WriteLine($"{cp1.x,8:F4} {cp1.y,14:F9} {S1,14:F9} {err,11:E1} {RG_A.iter}");
        }

        private static double S_OA(double x)
        {
            return (1.0 + x + 3.0 * x * x) * Math.Exp(-x);
        }

        private static double f_OA(double x, double y, double dy)
        {
            return 6.0 * Math.Exp(-x) - y - 2.0 * dy;
        }
    }
}
