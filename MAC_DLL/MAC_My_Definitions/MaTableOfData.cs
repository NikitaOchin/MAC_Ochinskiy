using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CI = System.Globalization.CultureInfo;

namespace MAC_DLL.MAC_My_Definitions
{
    public class MyTableOfData : MyTable
    {
        public string Table_in_File { get; }

        /// <summary>
        /// Степінь інтерполяційного многочлена
        /// </summary>
        private int Power;
        public int power
        {
            get { return Power; }
            set
            {
                Power = value;
                if (value < 1) Power = 1;
                if (value >= Length) Power = Length - 1;
            }
        }

        /// <summary>
        /// Тип інтерполяційного многочлена при апроксимації таблиці
        /// </summary>
        public TypeOfInterpolation TypeInt { get; set; }
            = TypeOfInterpolation.ByLagrange;
        
        // <--- Constructor --->
        public MyTableOfData(string path,string title)
        {
            List<Point_xf> Temp = new List<Point_xf>();
            FileInfo file = new FileInfo(path);
            Table_in_File = "\r\n Table " + title +
                            " in file " + file.Name + " :\r\n";

            #region <---Read bin file in list of data--->

            if(file.Extension == ".bin")
            {
                BinaryReader bin_rdr = new BinaryReader(file.OpenRead());
                int i = -1;
                try
                {
                    while (true)
                    {
                        Temp.Add(new Point_xf(bin_rdr.ReadDouble(), bin_rdr.ReadDouble()));
                        i++;
                        Table_in_File += $"{i,4}" + Temp[i].ToPrint() + "\r\n";
                    }
                }
                catch (IOException) { }
                bin_rdr.Close();
            }

            #endregion <---Read bin file in list of data--->

            #region <---Read text file in the list of data--->

            if (file.Extension == ".txt")
            {
                bool dot_or_comma = true ; int i = -1;
                if (CI.CurrentCulture.NumberFormat.CurrencyDecimalSeparator == ",")
                    dot_or_comma = false;
                StreamReader txt_rdr = new StreamReader(file.OpenRead());
                string[] txt; string line;
                while (!txt_rdr.EndOfStream)
                {
                    i++; line = txt_rdr.ReadLine();
                    if (dot_or_comma)
                        line = (line.Replace(",", ".")).Trim();
                    else
                        line = (line.Replace(".", ",")).Trim();

                    txt = line.Split(new char[] { ' ', ';' },
                                     StringSplitOptions.RemoveEmptyEntries);
                    Temp.Add(new Point_xf(Convert.ToDouble(txt[0]), Convert.ToDouble(txt[1])));
                    Table_in_File += $"{i,4}" + Temp[i].ToPrint() + "\r\n";
                }
                txt_rdr.Close();
            }

            #endregion <---Read text file in the list of data--->

            #region <---Data table generation--->
            if(Temp != null)
            {
                Temp.Sort((point_i, point_j) => point_i.X.CompareTo(point_j.X));
                Points = Temp.ToArray();
                Minimum = new Point_xf(double.NaN, double.MaxValue);
                Maximum = new Point_xf(double.NaN, double.MinValue);
                for(int i = 0; i < Length; i++)
                {
                    if (Minimum.F > Points[i].F) Minimum = Points[i];
                    if (Maximum.F < Points[i].F) Maximum = Points[i];
                }
            }
            else
            {
                Points = null; Minimum = null; Maximum = null;
            }
            Title = title;
            #endregion <---Data table generation--->

            Roots_Location();
        }

        #region <---Override MyTable class methods--->

        public override void Roots_correction(double eps)
        {
            if (Roots != null)
                foreach (Root root in Roots)
                    MAC_Equations.Dichotomy(PLn, root, eps);
        }

        double PLn(double xz)
        {
            switch (TypeInt)
            {
                case TypeOfInterpolation.ByLagrange:
                    return MAC_Interpolation.Polynomial_Lagrange(Power, xz, Points);

                default: return double.NaN;
            }
        }

        public override string ToPrint(string comment)
        {
            return comment + "\r\n" + Table_in_File + Table_of_Function();
        }

        #endregion <---Override MyTable class methods--->
    }
}
