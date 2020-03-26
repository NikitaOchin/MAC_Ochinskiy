using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAC_DLL.MAC_My_Definitions
{
    public abstract class MyTable
    {
        #region <---Key features MyTable--->

        //An array of nodes (x,f) in this table
        protected Point_xf[] Points { get; set; }

        //Returns count of nodes in this table
        public int Length { get { if (Points == null) return 0; else return Points.Length; } }

        //Returns the link to the node with the highest function value
        public Point_xf Maximum { get; protected set; }

        //Returns the link to the node with the lowest function value
        public Point_xf Minimum { get; protected set; }

        //Returns the lenght of function definition area
        public double Region_x { get { return Points[Length - 1].X - Points[0].X; } }

        //Returns the lenght of function value range
        public double Region_f { get { return Maximum.F - Minimum.F; } }

        //Math error value
        public double Epsilon { get; set; }

        //Title of table
        public string Title { get; protected set; }

        #endregion <---Key features MyTable--->

        #region <---basic methods MyTable--->

        //Returns value x for row of table with "index" number
        public double X(int index)
        {
            if (index >= 0 && index < Length) return Points[index].X;
            else return double.NaN;
        }

        //Returns value f for row of table with "index" number
        public double F(int index)
        {
            if (index >= 0 && index < Length) return Points[index].F;
            else return double.NaN;
        }

        //Create function table in text format
        public virtual string Table_of_Function()
        {
            string txt = "\r\n Function table " + Title + " :\r\n";

            for (int i = 0; i < Length; i++)
                txt += $"{i,4}" + Points[i].ToPrint() + "\r\n";

            txt += $"\r\n x = [{Points[0].X,17:F12} :" +
                $"{Points[Length - 1].X,17:F12} ]";

            txt += $"\r\n x_Reg = {Region_x,18:F12}\r\n";

            txt += $"\r\n Min ({Minimum.X,18:F12},{Minimum.F,18:F12})";

            txt += $"\r\n Max ({Maximum.X,18:F12},{Maximum.F,18:F12})";

            txt += $"\r\n f_Reg = {Region_f,18:F12}\r\n";

            return txt + Table_of_Roots("");
        }

        //Method for keep table in a text file path with addiction comment
        public virtual void To_txt_File(string path, string comment)
        {
            if (path == "") path = "My_Table.txt";
            FileInfo file = new FileInfo(path);
            if (file.Exists) file.Delete();
            StreamWriter sw = new StreamWriter(file.OpenWrite());
            sw.Write(comment + "\r\n " + Table_of_Function());
            sw.Close();
        }

        //Fill two arrays with value of argument and function
        public void ToArrays(out double[] x, out double[] f)
        {
            x = new double[Length]; f = new double[Length];
            for(int i = 0; i< Length;i++)
            { x[i] = Points[i].X; f[i] = Points[i].F; }
        }

        //Addiction method for output information of table in text format 
        public virtual string ToPrint(string comment)
        {
            return comment;
        }

        #endregion <---basic methods MyTable--->

        #region <---additional features MyTable--->

        public List<Root> Roots { get; internal set; }

        #endregion <---additional features MyTable--->

        #region <---additional methods MyTable--->

        public virtual void Roots_correction(double eps) { }
        protected void Roots_Location()
        {
            int counter = 0;
            for (int i = 1; i < Length; i++)
            {
                if (Points[i - 1].F * Points[i].F < 0)
                {
                    counter++;
                    if (counter == 1) Roots = new List<Root>();
                    Roots.Add(new Root(Points[i - 1].X, Points[i].X));
                }
            }
        }
        public string Table_of_Roots(string comment)
        {
            string table = comment + "\r\n";
            if (Roots != null)
            {
                table += " Table of zeros of function " + Title + " : \r\n";
                for (int j = 0; j < Roots.Count; j++)
                    table += $"{j,3}" + Roots[j].ToPrint() + "\r\n";
            }
            else table += " Table of zeros of function " + Title + " is empty!\r\n";
            return table;
        }
        
        #endregion <---additional methods MyTable--->


    }
}
