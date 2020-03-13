using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAC_DLL.MAC_My_Definitions
{
    public class MyTable
    {
        #region <---Key features MyTable--->

        //An array of nodes (x,f) in this table
        protected Point_xf[] Points { get; set; }

        //Returns count of nodes in this table
        public int Lenght { get { if (Points == null) return 0; else return Points.Length; } }

        //Returns the link to the node with the highest function value
        public Point_xf Maximum { get; protected set; }

        //Returns the link to the node with the lowest function value
        public Point_xf Minimum { get; protected set; }

        //Returns the lenght of function definition area
        public double Region_x { get { return Points[Lenght - 1].X - Points[0].X}; }

        //Returns the lenght of function value range
        public double Region_f { get { return Maximum.F - Minimum.F; } }

        //Math error value
        public double Epsilon { get; set; }

        //Title of table
        public double Title { get; protected set; }

        #endregion <---Key features MyTable--->

        #region <---basic methods MyTable--->




        #endregion <---basic methods MyTable--->

    }
}
