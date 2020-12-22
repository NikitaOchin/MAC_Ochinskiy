using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MAC_DLL.MAC_My_Definitions;

namespace MAC_DLL
{
    abstract public class MAC_ODE_Order_2
    {
        internal readonly Cauchy_Point CPO;
        public int iter { get; internal set; } = 0;

        public MAC_ODE_Order_2(Cauchy_Point cauchy_point)
        {
            CPO = new Cauchy_Point(cauchy_point);
        }

        abstract public Cauchy_Point Method(Cauchy_Point cpO, double x1);

        virtual public Cauchy_Point Solve_with_Step(double x1)
        {
            if (x1 <= CPO.x) return null;
            return Method(CPO, x1);
        }

        virtual public Cauchy_Point Solve_with_Precision(double x1, double eps)
        {
            if (x1 <= CPO.x) return null;
            int m = 10 * (int)(Math.Ceiling(x1 - CPO.x));
            Cauchy_Point cpj, cpj1; iter = 0;
            double hx, ym = double.MaxValue, err;
            do
            {
                hx = (x1 - CPO.x) / m; iter++;
                cpj1 = CPO;
                for (int j = 1; j <= m; j++)
                {
                    cpj = cpj1; cpj1 = Method(cpj, cpj.x + hx);
                }
                err = Math.Abs(ym - cpj1.y); 
                ym = cpj1.y;
                m *= 2;
            } while (err > eps);
            return cpj1;
        }
    }

    public class MAC_ODE_Order_2_RungeKutta_4_A : MAC_ODE_Order_2
    {
        internal readonly Func<double, double, double, double> f;

        public MAC_ODE_Order_2_RungeKutta_4_A
            (Cauchy_Point cauchy_point, Func<double, double, double, double> f)
    : base(cauchy_point)
        {
            this.f = f;
        }

        public override Cauchy_Point Method(Cauchy_Point cpO, double x1)
        {
            double h = x1 - cpO.x, h2 = 0.5 * h, k1, k2, k3, k4, y1, dy1;
            k1 = h * f(cpO.x, cpO.y, cpO.dy);
            k2 = h * f(cpO.x + h2, cpO.y + h2 * cpO.dy + h * k1 / 8.0, cpO.dy + k1 / 2.0);
            k3 = h * f(cpO.x + h2, cpO.y + h2 * cpO.dy + h * k1 / 8.0, cpO.dy +  k2 / 2.0);
            k4 = h * f(cpO.x + h, cpO.y + h * cpO.dy + h2 * k3, cpO.dy + k3);
            dy1 = cpO.dy + (k1 + 2 * k2 + 2.0 * k3 + k4) / 6.0;
            y1 = cpO.y + h * (cpO.dy + (k1 + k2 + k3) / 6.0);
            return new Cauchy_Point(x1, y1, dy1);
        }
    }

    public class MAC_ODE_Order_2_RungeKutta_4_B : MAC_ODE_Order_2
    {
        internal readonly Func<double, double, double> f;

        public MAC_ODE_Order_2_RungeKutta_4_B
            (Cauchy_Point cauchy_point, Func<double, double, double> f)
    : base(cauchy_point)
        {
            this.f = f;
        }

        public override Cauchy_Point Method(Cauchy_Point cpO, double x1)
        {
            double h = x1 - cpO.x, h2 = 0.5 * h, k1, k2, k3, y1, dy1;
            k1 = h * f(cpO.x, cpO.y);
            k2 = h * f(cpO.x + h2, cpO.y + h2 * cpO.dy + h * k1 / 8.0);
            k3 = h * f(cpO.x + h, cpO.y + h * cpO.dy + h2 * k2);
            dy1 = cpO.dy + (k1 + 4 * k2 + k3) / 6.0;
            y1 = cpO.y + h * (cpO.dy + (k1 + 2*k2) / 6.0);
            return new Cauchy_Point(x1, y1, dy1);
        }
    }
}
