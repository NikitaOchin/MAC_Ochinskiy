using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAC_DLL.MAC_My_Definitions
{
    public class Cauchy_Point
    {
        public double x { get; internal set; } = double.NaN;
        public double y { get; internal set; } = double.NaN;
        public double dy { get; internal set; } = double.NaN;


        public Cauchy_Point(double x, double y, double dy)
        {
            this.x = x; this.y = y; this.dy = dy;
        }

        public Cauchy_Point(Cauchy_Point CP)
        {
            x = CP.x; y = CP.y; dy = CP.dy;
        }

        public string ToPrint()
        {
            return $"   ({x,16:F10} , {y,16:F10}, {dy,16:F10}  )";
        }
    }

    public class Cauchy_Sys_Point
    {
        public double x { get; internal set; } = double.NaN;
        public double y { get; internal set; } = double.NaN;
        public double z { get; internal set; } = double.NaN;


        public Cauchy_Sys_Point(double x, double y, double z)
        {
            this.x = x; this.y = y; this.z = z;
        }

        public Cauchy_Sys_Point(Cauchy_Sys_Point CP)
        {
            x = CP.x; y = CP.y; z = CP.z;
        }

        public string ToPrint()
        {
            return $"   ({x,16:F10} , {y,16:F10}, {z,16:F10}  )";
        }
    }
}
