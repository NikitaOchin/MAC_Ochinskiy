using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAC_DLL.MAC_My_Definitions
{
    public class Point_xf
    {
        public double X { get; internal set; } = double.NaN;
        public double F { get; internal set; } = double.NaN;

        public Point_xf(double x, double f) { X = x; F = f; }

        public  string ToPrint()
        {
            return $"   ({X,16:F10},{F,16:F10}   )";
        }
    }
}
