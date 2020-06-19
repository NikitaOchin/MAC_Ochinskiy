using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAC_DLL.MAC_My_Definitions
{
    /// <summary>
    /// Перелік інтерполяційних многочленів
    /// </summary>
    public enum TypeOfInterpolation
    {
        ByLagrange,
        ByNewton_FD,
        ByNewton_DD,
        BySplines
    }
}
