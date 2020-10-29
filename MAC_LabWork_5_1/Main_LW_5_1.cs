using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CLQ = MAC_DDL.MAC_Quadrature;

namespace MAC_LabWork_5_1
{
    class Main_LW_5_1
    {

        static double a, b, Al, Bt;

        static void Main(string[] args){
            double a1 = 0.0, b1 = 1.0, a2 = 0.0, b2 = Math.PI;
            double eps = 1.0E-5;
            double I1 = F1(b1) - F1(a1);
            double I2 = F2(b2) - F2(a2);
            double i1 = CLQ.Method_Simpsona(a1,b1,f1,eps);
            double i2 = CLQ.Method_Simpsona(a2,b2,f2,eps);
            //Console.WriteLine($"I1 = {I1:F11} i1 = {i1:F11}");
            //Console.WriteLine($"I2 = {I2:F11} i2 = {i2:F11}");


            eps = 1.0E-9;
            a = -1.4; b = 6.2; Al = 0.7; Bt = -0.9;
            double H1 = CLQ.Method_Simpsona(a, b, h1, eps);
            a = -3.5; b = 2.6; Al = 0.8;
            double H2 = CLQ.Method_Simpsona(a, b, h2, eps);
            a = -0.3; b = 2.9; Al = -0.6; Bt = 1.7;
            double H3 = CLQ.Method_Simpsona(a, b, h3, eps);

            Console.WriteLine($"I1 = {H1:F11}");
            Console.WriteLine($"I2 = {H2:F11}");
            Console.WriteLine($"I3 = {H3:F11}");


        }

        //Home function
        //f1
        public static double h1(double x)
        {
            return Math.Cos(Al*x + Bt) * Math.Cos(Al*x - Bt);
        }

        //f2
        public static double h2(double x)
        {
            return Math.Pow(x, 4) / Math.Pow(x*x + Al*Al, 3);
        }

        //f3
        public static double h3(double x)
        {
            return Math.Exp(Al*x) * Math.Sin(Math.Sqrt(7)*x)*Math.Cos(Bt*x);
        }



        // Test function
        public static double f1(double x){
            return x* Math.Log(1.0  + x);
        }

        public static double F1(double x){
            return ((x*x - 1.0) * Math.Log(1.0 + x) + x - x*x / 2.0) / 2.0;
        }

        public static double f2(double x){
            return Math.Sin(x);
        }

        public static double F2(double x){
            return -Math.Cos(x);
        }
    
    }
}