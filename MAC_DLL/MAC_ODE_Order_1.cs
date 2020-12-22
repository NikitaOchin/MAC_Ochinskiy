using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CSP = MAC_DLL.MAC_My_Definitions.Cauchy_Sys_Point;

namespace MAC_DLL
{
    abstract public class MAC_Sys_of_ODE_Order_1
    {
        internal readonly CSP CSPO;
        internal readonly Func<double, double, double, double> f;
        internal readonly Func<double, double, double, double> g;
        public int iter { get; internal set; } = 0;

        public MAC_Sys_of_ODE_Order_1
            (CSP csp, Func<double, double, double, double> f, 
                      Func<double, double, double, double> g)
        {
            this.CSPO = new CSP(csp); this.g = g; this.f = f;
        }

        abstract public CSP Method(CSP csp0, double x1);

        virtual public CSP Solve_with_Step(double x1)
        {
            if (x1 <= CSPO.x) return null;
            return Method(CSPO, x1);
        }

        virtual public CSP Solve_with_Precision(double x1, double eps)
        {
            if (x1 <= CSPO.x) return null;
            int m = 10 * (int)(Math.Ceiling(x1 - CSPO.x));
            CSP cpj, cpj1; iter = 0;
            double hx, ym = double.MaxValue, zm = double.MaxValue, err;
            do
            {
                hx = (x1 - CSPO.x) / m; iter++;
                cpj1 = CSPO;
                for (int j = 1; j <= m; j++)
                {
                    cpj = cpj1; cpj1 = Method(cpj, cpj.x + hx);
                }
                err = Math.Max(Math.Abs(cpj1.y - ym), Math.Abs(cpj1.z - zm));
                ym = cpj1.y;
                zm = cpj1.z;
                m *= 2;
            } while (err > eps);
            return cpj1;

        }
    }

    public class MAC_Sys_of_ODE_Order_1_RungeKutta_1 : MAC_Sys_of_ODE_Order_1
    {

        public MAC_Sys_of_ODE_Order_1_RungeKutta_1
            (CSP csp, Func<double, double, double, double> f,
                      Func<double, double, double, double> g)
    : base(csp, f, g){}

        public override CSP Method(CSP csp0, double x1)
        {
            double h = x1 - csp0.x;
            double y1 = csp0.y + h * f(csp0.x, csp0.y, csp0.z);
            double z1 = csp0.z + h * g(csp0.x, csp0.y, csp0.z);
            return new CSP(x1, y1, z1);
        }
    }

    public class MAC_Sys_of_ODE_Order_1_RungeKutta_2 : MAC_Sys_of_ODE_Order_1
    {

        public MAC_Sys_of_ODE_Order_1_RungeKutta_2
            (CSP csp, Func<double, double, double, double> f,
                      Func<double, double, double, double> g)
    : base(csp, f, g) { }

        public override CSP Method(CSP csp0, double x1)
        {
            double h = x1 - csp0.x;
            double k1 = h * f(csp0.x, csp0.y, csp0.z);
            double l1 = h * g(csp0.x, csp0.y, csp0.z);
            double k2 = h * f(csp0.x + h, csp0.y + k1, csp0.z + l1);
            double l2 = h * g(csp0.x + h, csp0.y + k1, csp0.z + l1);
            double y1 = csp0.y + 0.5 * (k1 + k2);
            double z1 = csp0.z + 0.5 * (l1 + l2);
            return new CSP(x1, y1, z1);
        }
    }

    public class MAC_Sys_of_ODE_Order_1_RungeKutta_3 : MAC_Sys_of_ODE_Order_1
    {

        public MAC_Sys_of_ODE_Order_1_RungeKutta_3
            (CSP csp, Func<double, double, double, double> f,
                      Func<double, double, double, double> g)
    : base(csp, f, g) { }

        public override CSP Method(CSP csp0, double x1)
        {
            double h = x1 - csp0.x, h2 = 0.5 * h;
            double k1 = h * f(csp0.x, csp0.y, csp0.z);
            double l1 = h * g(csp0.x, csp0.y, csp0.z);
            double k2 = h * f(csp0.x + h2, csp0.y + k1*0.5, csp0.z + l1*0.5);
            double l2 = h * g(csp0.x + h2, csp0.y + k1 * 0.5, csp0.z + l1 * 0.5);
            double k3 = h * f(csp0.x + h, csp0.y - k1 + 2 * k2, csp0.z - l1 + 2 * l2);
            double l3 = h * g(csp0.x + h, csp0.y - k1 + 2 * k2, csp0.z - l1 + 2 * l2);
            double y1 = csp0.y + (k1 + 4*k2 + k3) / 6.0;
            double z1 = csp0.z + (l1 + 4 * l2 + l3) / 6.0;
            return new CSP(x1, y1, z1);
        }
    }

    public class MAC_Sys_of_ODE_Order_1_RungeKutta_4 : MAC_Sys_of_ODE_Order_1
    {

        public MAC_Sys_of_ODE_Order_1_RungeKutta_4
            (CSP csp, Func<double, double, double, double> f,
                      Func<double, double, double, double> g)
    : base(csp, f, g) { }

        public override CSP Method(CSP csp0, double x1)
        {
            double h = x1 - csp0.x, h2 = 0.5 * h;
            double k1 = h * f(csp0.x, csp0.y, csp0.z);
            double l1 = h * g(csp0.x, csp0.y, csp0.z);
            double k2 = h * f(csp0.x + h2, csp0.y + k1 * 0.5, csp0.z + l1 * 0.5);
            double l2 = h * g(csp0.x + h2, csp0.y + k1 * 0.5, csp0.z + l1 * 0.5);
            double k3 = h * f(csp0.x + h2, csp0.y + k2 * 0.5, csp0.z + l2 * 0.5);
            double l3 = h * g(csp0.x + h2, csp0.y + k2 * 0.5, csp0.z + l2 * 0.5);
            double k4 = h * f(csp0.x + h, csp0.y + k3, csp0.z + l3);
            double l4 = h * g(csp0.x + h, csp0.y + k3, csp0.z + l3);
            double y1 = csp0.y + (k1 + 2 * k2 + 2*k3 + k4) / 6.0;
            double z1 = csp0.z + (l1 + 2 * l2 + 2*l3 + l4) / 6.0;
            return new CSP(x1, y1, z1);
        }
    }


    abstract public class MAC_ODE_Order_1
    {
        internal readonly double xo, yo;
        internal readonly Func<double, double, double> f;
        public int iter;

        public MAC_ODE_Order_1(double xo, double yo, Func<double, double, double> f)
        {
            this.xo = xo; this.yo = yo; this.f = f;
        }

        abstract public double Method(double x0, double y0, double x1);

        virtual public double Solve_with_Step(double x0, double y0, double x1)
        {
            return Method(x0, y0, x1);
        }

        virtual public double Solve_with_Precision(double x0, double y0, double x1, double eps)
        {
            int m = 10 * Convert.ToInt32(Math.Ceiling(x1 - x0)); iter = 0;
            double xj1, xj, yj1, yj, hx, ym = double.MaxValue, err;
            do
            {
                hx = (x1 - x0) / m; yj = y0; xj = x0; iter++;
                for (int j = 1; j <= m; j++)
                {
                    xj1 = xj + hx;
                    yj1 = Method(xj, yj, xj1);
                    xj = xj1; yj = yj1;
                }
                err = Math.Abs(ym - yj);
                ym = yj; m *= 2;
            } while (err > eps);
            return ym;

        }
    }

    public class MAC_ODE_Order_1_Eulers_Method : MAC_ODE_Order_1
    {
        public MAC_ODE_Order_1_Eulers_Method(double xo, double yo, Func<double, double, double> f)
            : base(xo, yo, f) { }

        public override double Method(double x0, double y0, double x1)
        {
            return y0 + (x1 - x0) * f(x0, y0);
        }
    }

    public class MAC_ODE_Order_1_Taylor_Method : MAC_ODE_Order_1
    {

        readonly Func<double, double, double> fx;
        readonly Func<double, double, double> fy;
        readonly Func<double, double, double> fxx;
        readonly Func<double, double, double> fxy;
        readonly Func<double, double, double> fyy;

        public MAC_ODE_Order_1_Taylor_Method(double xo, double yo,
            Func<double, double, double> f,
            Func<double, double, double> fx,
            Func<double, double, double> fxx,
            Func<double, double, double> fy,
            Func<double, double, double> fyy,
            Func<double, double, double> fxy)
            : base(xo, yo, f)
        {
            this.fx = fx; this.fxx = fxx; this.fxy = fxy;
            this.fy = fy; this.fyy = fyy;
        }

        public override double Method(double x0, double y0, double x1)
        {
            double dx = x1 - x0;
            double f_1 = f(x0, y0);
            double f_2 = fx(x0, y0) + f_1 * fy(x0, y0);
            double f_3 = fxx(x0, y0) + fy(x0, y0) * f_2 +
                (2.0 * fxy(x0, y0) + f_1 * fyy(x0, y0)) * f_1;
            return y0 + dx * (f_1 + dx * (f_2 + dx * f_3 / 3.0) / 2.0);
        }
    }

    public class MAC_ODE_Order_1_RungeKutta_2 : MAC_ODE_Order_1
    {
        private readonly int kind;

        public MAC_ODE_Order_1_RungeKutta_2
            (double xo, double yo,
            Func<double, double, double> f, int kind)
    : base(xo, yo, f)
        {
            this.kind = kind;
        }

        public override double Method(double x0, double y0, double x1)
        {
            double h = x1 - x0, k1 = h * f(x0, y0), k2;

            switch (kind)
            {
                case 1:
                    k2 = h * f(x0 + h, y0 + k1);
                    return y0 + (k1 + k2) / 2.0;
                case 2:
                    k2 = h * f(x0 + h / 2.0, y0 + k1 / 2.0);
                    return y0 + k2;
                case 3:
                    k2 = h * f(x0 + 2.0 * h/ 3.0, y0 + 2.0 * k1 / 3.0);
                    return y0 + (k1 + 3.0 * k2) / 4.0;
            }
            return double.NaN;
        }
    }

    public class MAC_ODE_Order_1_RungeKutta_3 : MAC_ODE_Order_1
    {
        private readonly int kind;

        public MAC_ODE_Order_1_RungeKutta_3
            (double xo, double yo,
            Func<double, double, double> f, int kind)
    : base(xo, yo, f)
        {
            this.kind = kind;
        }

        public override double Method(double x0, double y0, double x1)
        {
            double h = x1 - x0, k1 = h * f(x0, y0), k2, k3;

            switch (kind)
            {
                case 1:
                    k2 = h * f(x0 + h / 2.0, y0 + k1/ 2.0);
                    k3 = h * f(x0 + h, y0 - k1 + 2 * k2);
                    return y0 + (k1 + 4*k2 + k3) / 6.0;
                case 2:
                    k2 = h * f(x0 + h / 3.0, y0 + k1 / 3.0);
                    k3 = h * f(x0 + 2 * h / 3.0, y0 + 2 * k2 / 3.0);
                    return y0 + (k1 + 3*k3) /4.0;
                case 3:
                    k2 = h * f(x0 + h / 2.0, y0 + k1 / 2.0);
                    k3 = h * f(x0 + 3 * h / 4.0, y0 + 3 * k2 / 4.0);
                    return y0 + (2 * k1 + 3.0 * k2 + 4.0 * k3) / 9.0;
            }
            return double.NaN;
        }
    }

    public class MAC_ODE_Order_1_RungeKutta_4 : MAC_ODE_Order_1
    {
        private readonly int kind;

        public MAC_ODE_Order_1_RungeKutta_4
            (double xo, double yo,
            Func<double, double, double> f, int kind)
    : base(xo, yo, f)
        {
            this.kind = kind;
        }

        public override double Method(double x0, double y0, double x1)
        {
            double h = x1 - x0, k1 = h * f(x0, y0), k2, k3, k4;

            switch (kind)
            {
                case 1:
                    k2 = h * f(x0 + h / 2.0, y0 + k1 / 2.0);
                    k3 = h * f(x0 + h / 2.0, y0 + k2 / 2.0);
                    k4 = h * f(x0 + h, y0 + k3);
                    return y0 + (k1 + 2*k2 + 2*k3 + k4)/6.0;
                case 2:
                    k2 = h * f(x0 + h / 4.0, y0 + k1 / 4.0);
                    k3 = h * f(x0 + h / 2.0, y0 + k2 / 2.0);
                    k4 = h * f(x0 + h, y0 + k1 - 2 * k2 + 2 * k3);
                    return y0 + (k1 + 4*k3 + k4) / 6.0;
                case 3:
                    k2 = h * f(x0 + h / 3.0, y0 + k1 / 3.0);
                    k3 = h * f(x0 + 2*h / 3.0, y0 - k1/3.0 + k2);
                    k4 = h * f(x0 + h, y0 + k1 - k2 + k3);
                    return y0 + (k1 + 3 * k2 + 3*k3 + k4) / 8.0;
            }
            return double.NaN;
        }
    }

    public class MAC_ODE_Order_1_RungeKutta_5 : MAC_ODE_Order_1
    {

        public MAC_ODE_Order_1_RungeKutta_5
            (double xo, double yo,
            Func<double, double, double> f)
    : base(xo, yo, f){}

        public override double Method(double x0, double y0, double x1)
        {
            double h = x1 - x0, k1 = h * f(x0, y0), k2, k3, k4, k5, k6;


            k2 = h * f(x0 + h / 2.0, y0 + k1 / 2.0);
            k3 = h * f(x0 + h / 2.0, y0 + (k1 + k2) / 4.0);
            k4 = h * f(x0 + h, y0 - k2 + 2*k3);
            k5 = h * f(x0 + 2*h/3.0, y0 + (7*k1 + 10*k2 + k4)/27.0);
            k6 = h * f(x0 + h/5.0, y0 + (28*k1 - 125 * k2 + 546*k3 + 54*k4 - 378*k5) / 625);
            return y0 + (14*k1 + 35 * k4 + 162 * k5 + 125*k6) / 336.0;
        }
    }

}
