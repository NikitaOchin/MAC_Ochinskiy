using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAC_DLL.MAC_My_Definitions
{
    public class MyTableOfFunction: MyTable
    {
        protected Func<double, double> Fx;

        public MyTableOfFunction(double xo, double xn, int n, Func<double, double> f_x, string title)
        {
            Title = title;  Fx = f_x;
            Points = new Point_xf[n + 1];

            Maximum = new Point_xf(double.NaN, double.MinValue);
            Minimum = new Point_xf(double.NaN, double.MaxValue);

            double hx = (xn - xo) / n, xi;
            for(int i = 0; i <= n; i++)
            {
                xi = xo + hx * i; Points[i] = new Point_xf(xi, Fx(xi));
                if (Maximum.F < Points[i].F) Maximum = Points[i];
                if (Minimum.F > Points[i].F) Minimum = Points[i];
            }
            Roots_Location();
        }

        public override string ToPrint(string comment)
        {
            return comment + "\r\n" + Table_of_Function();
        }
    }
}
