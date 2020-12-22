using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LW81 = MAC_LabWork_8_1.Main_LW_8_1;
using ODE_1_EM = MAC_DLL.MAC_ODE_Order_1_Eulers_Method;
using ODE_1_TM = MAC_DLL.MAC_ODE_Order_1_Taylor_Method;
using ODE_1_RK2 = MAC_DLL.MAC_ODE_Order_1_RungeKutta_2;
using ODE_1_RK3 = MAC_DLL.MAC_ODE_Order_1_RungeKutta_3;
using ODE_1_RK4 = MAC_DLL.MAC_ODE_Order_1_RungeKutta_4;
using ODE_1_RK5 = MAC_DLL.MAC_ODE_Order_1_RungeKutta_5;
using ODE_1 = MAC_DLL.MAC_ODE_Order_1;


using System.IO;

namespace MAC_LabWork_8_2
{
    class Main_LW_8_2
    {

        static StreamWriter SW = new StreamWriter("MAC_LW_8_2.txt");
        static double x0, x1, y0, y1, eps = 1.0E-6, S1, err;

        //static void Main(string[] args)
        //{
        //    x0 = 0.5 * Math.PI; x1 = 4.0; y0 = 1.0; S1 = LW81.Y(x1, x0, y0);

        //    Test_Eulers_Method();
        //    Test_Taylor_Method();

        //    Test_Runge_Kutta_2(1);
        //    Test_Runge_Kutta_2(2);
        //    Test_Runge_Kutta_2(3);

        //    Test_Runge_Kutta_3(1);
        //    Test_Runge_Kutta_3(2);
        //    Test_Runge_Kutta_3(3);

        //    Test_Runge_Kutta_4(1);
        //    Test_Runge_Kutta_4(2);
        //    Test_Runge_Kutta_4(3);

        //    Test_Runge_Kutta_5();


        //    SW.Close();
        
    //}

        static void Main(string[] args)
        {
            x0 = 0.5 * Math.PI; x1 = 4.0; y0 = 1.0; S1 = LW81.Y(x1, x0, y0);

            //Eulers_Method
            ODE_1 EM = new ODE_1_EM(x0, y0, LW81.f);
            Test_ODE_Method(EM, "MAC_ODE_Order_1_Eulers_Method");

            //Taylor_Method
            ODE_1 TM = new ODE_1_TM(x0, y0, LW81.f, LW81.fx, LW81.fxx, LW81.fy, LW81.fyy, LW81.fxy);
            Test_ODE_Method(TM, "MAC_ODE_Order_1_Taylor_Method");

            //RungeKutta_2
            ODE_1 RG2 = new ODE_1_RK2(x0, y0, LW81.f, 1);
            Test_ODE_Method(RG2, "MAC_ODE_Order_1_RungeKutta_2");
            RG2 = new ODE_1_RK2(x0, y0, LW81.f, 2);
            Test_ODE_Method(RG2, "MAC_ODE_Order_1_RungeKutta_2");
            RG2 = new ODE_1_RK2(x0, y0, LW81.f, 3);
            Test_ODE_Method(RG2, "MAC_ODE_Order_1_RungeKutta_2");

            //RungeKutta_3
            ODE_1 RG3 = new ODE_1_RK3(x0, y0, LW81.f, 1);
            Test_ODE_Method(RG3, "MAC_ODE_Order_1_RungeKutta_3");
            RG3 = new ODE_1_RK3(x0, y0, LW81.f, 2);
            Test_ODE_Method(RG3, "MAC_ODE_Order_1_RungeKutta_3");
            RG3 = new ODE_1_RK3(x0, y0, LW81.f, 3);
            Test_ODE_Method(RG3, "MAC_ODE_Order_1_RungeKutta_3");

            //RungeKutta_4
            ODE_1 RG4 = new ODE_1_RK4(x0, y0, LW81.f, 1);
            Test_ODE_Method(RG4, "MAC_ODE_Order_1_RungeKutta_4");
            RG4 = new ODE_1_RK4(x0, y0, LW81.f, 2);
            Test_ODE_Method(RG4, "MAC_ODE_Order_1_RungeKutta_4");
            RG4 = new ODE_1_RK4(x0, y0, LW81.f, 3);
            Test_ODE_Method(RG4, "MAC_ODE_Order_1_RungeKutta_4");

            //RungeKutta_5
            ODE_1 RG5 = new ODE_1_RK5(x0, y0, LW81.f);
            Test_ODE_Method(RG5, "MAC_ODE_Order_1_RungeKutta_5");


            SW.Close();
        }

        private static void Test_ODE_Method(ODE_1 ode, string Method_name)
        {
            y1 = ode.Solve_with_Precision(x0, y0, x1, eps); err = Math.Abs(S1 - y1);

            SW.WriteLine("\r\n "+ Method_name + ":");
            SW.WriteLine($"  {x1,8:F4}  {y1,12:F9}  {S1,12:F9}  {err,11:E1}  {ode.iter}");
        }


        private static void Test_Taylor_Method()
        {
            ODE_1_TM TM = new ODE_1_TM(x0, y0, LW81.f, LW81.fx, LW81.fxx, LW81.fy, LW81.fyy, LW81.fxy);

            y1 = TM.Solve_with_Precision(x0, y0, x1, eps); err = Math.Abs(S1 - y1);

            SW.WriteLine("\r\n MAC_ODE_Order_1_Taylor_Method:");
            SW.WriteLine($"  {x1,8:F4}  {y1,12:F9}  {S1,12:F9}  {err,11:E1}  {TM.iter}");
        }

        private static void Test_Eulers_Method()
        {
            ODE_1_EM EM = new ODE_1_EM(x0, y0, LW81.f);
            y1 = EM.Solve_with_Precision(x0, y0, x1, eps); err = Math.Abs(S1 - y1);

            SW.WriteLine("\r\n MAC_ODE_Order_1_Eulers_Method:");
            SW.WriteLine($"  {x1,8:F4}  {y1,12:F9}  {S1,12:F9}  {err,11:E1}  {EM.iter}");
        }

        private static void Test_Runge_Kutta_2(int kind)
        {
            ODE_1_RK2 RG2 = new ODE_1_RK2(x0, y0, LW81.f, kind);

            y1 = RG2.Solve_with_Precision(x0, y0, x1, eps); err = Math.Abs(S1 - y1);

            SW.WriteLine("\r\n MAC_ODE_Order_1_RungeKutta_2:");
            SW.WriteLine($"  {x1,8:F4}  {y1,12:F9}  {S1,12:F9}  {err,11:E1}  {RG2.iter}");
        }

        private static void Test_Runge_Kutta_3(int kind)
        {
            ODE_1_RK3 RG3 = new ODE_1_RK3(x0, y0, LW81.f, kind);

            y1 = RG3.Solve_with_Precision(x0, y0, x1, eps); err = Math.Abs(S1 - y1);

            SW.WriteLine("\r\n MAC_ODE_Order_1_RungeKutta_3:");
            SW.WriteLine($"  {x1,8:F4}  {y1,12:F9}  {S1,12:F9}  {err,11:E1}  {RG3.iter}");
        }

        private static void Test_Runge_Kutta_4(int kind)
        {
            ODE_1_RK4 RG4 = new ODE_1_RK4(x0, y0, LW81.f, kind);

            y1 = RG4.Solve_with_Precision(x0, y0, x1, eps); err = Math.Abs(S1 - y1);

            SW.WriteLine("\r\n MAC_ODE_Order_1_RungeKutta_4:");
            SW.WriteLine($"  {x1,8:F4}  {y1,12:F9}  {S1,12:F9}  {err,11:E1}  {RG4.iter}");
        }

        private static void Test_Runge_Kutta_5()
        {
            ODE_1_RK5 RG5 = new ODE_1_RK5(x0, y0, LW81.f);

            y1 = RG5.Solve_with_Precision(x0, y0, x1, eps); err = Math.Abs(S1 - y1);

            SW.WriteLine("\r\n MAC_ODE_Order_1_RungeKutta_5:");
            SW.WriteLine($"  {x1,8:F4}  {y1,12:F9}  {S1,12:F9}  {err,11:E1}  {RG5.iter}");
        }

    }
}
