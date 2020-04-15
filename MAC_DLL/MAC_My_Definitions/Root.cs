using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAC_DLL.MAC_My_Definitions
{
    public class Root
    {
        public double XL, XR, X, Err; public int Iters;
        public Root(double x_left, double x_right)
        {
            XL = x_left; XR = x_right;
            X = double.NaN; Err = double.NaN; Iters = 0;
        }
        public string ToPrint()
        {
            return $"  [{XL,10:F5},{XR,10:F5} ] Root = {X,16:F10}" +
                   $"  Err ={Err,10:E1} Iters = {Iters}";
        }
    }
}
