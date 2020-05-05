



using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAC_DLL.MAC_My_Definitions
{
    public class MyExtendedTableOfFunction: MyTableOfFunction
    {
        protected Func<double, double> D1Fx, D2Fx;

        public MyExtendedTableOfFunction(double xo, double xn, int n,
                                         Func<double,double> fx,
                                         Func<double, double> d1fx,
                                         Func<double, double> d2fx, string title)
        : base(xo,xn,n,fx,title)
        {
            D1Fx = d1fx; D2Fx = d2fx;
        }

        public override void Roots_correction(double eps)
        {
            foreach (Root root in Roots)
                MAC_Equations.Tangent(Fx, D1Fx, D2Fx, root,eps);
        }
    }
}
