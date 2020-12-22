using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using ToD = MAC_DLL.MAC_My_Definitions.MyTableOfData;

namespace MAC_Graph_DLL
{
    public partial class Form_with_Data : Form
    {

        public static void LW_7_1_Graph(ToD tod, Func<double, double> F, string comment)
        {
            tod.ToArrays(out double[] X, out double[] Y);
            Form_with_Data fwd = new Form_with_Data(X, Y, F, comment);
            fwd.StartPosition = FormStartPosition.CenterScreen;
            fwd.ShowDialog();
        }

        public static void DoublyGraph(ToD tod, Func<double, double> F, int Nx, int Ny)
        {
            tod.ToArrays(out double[] X, out double[] Y);
            Form_with_Data fwd = new Form_with_Data(X, Y, F, tod.Title);
            fwd.StartPosition = FormStartPosition.Manual;
            fwd.Location = new Point(Nx, Ny);
            fwd.ShowDialog();
        }

        private Form_with_Data(double[] x, double[] y, Func<double, double> F, string title)
        {
            InitializeComponent();
            chart_with_data.Series[0].Points.Clear(); // Очистка точек
            chart_with_data.Series[1].Points.Clear();

            chart_with_data.Titles[0].Text = title;
            chart_with_data.ChartAreas[0].AxisX.Interval = 1.0;
            chart_with_data.ChartAreas[0].AxisY.Interval = 1.0;

            Series S1 = new Series(); int n = x.Length - 1;
            double f_min = double.MaxValue, f_max = double.MinValue;
            for (int i = 0; i <= n; i++)
            {
                S1.Points.AddXY(x[i], y[i]);
                f_min = Math.Min(f_min, y[i]);
                f_max = Math.Max(f_max, y[i]);
            }

            S1.ChartType = SeriesChartType.Point;
            S1.MarkerStyle = MarkerStyle.Circle;
            S1.MarkerSize = 9;
            S1.MarkerColor = Color.Red;
            S1.MarkerBorderColor = Color.DarkRed;
            chart_with_data.Series[0] = S1;

            Series S2 = new Series();
            int k = (int)Math.Ceiling(x[n] - x[0]) * 10;
            double hz = (x[n] - x[0]) / k, z, f;
            for (int i = 0; i <= k; i++)
            {
                z = x[0] + i * hz; f = F(z);
                S2.Points.AddXY(z, f);
                f_min = Math.Min(f_min, f);
                f_max = Math.Max(f_max, f);
            }

            S2.ChartType = SeriesChartType.Spline;
            S2.Color = Color.Blue;
            S2.MarkerStyle = MarkerStyle.None;
            S2.BorderWidth = 3;
            chart_with_data.Series[1] = S2;

            chart_with_data.ChartAreas[0].AxisX.Minimum = Math.Floor(x[0]);
            chart_with_data.ChartAreas[0].AxisX.Maximum = Math.Ceiling(x[n]);
            chart_with_data.ChartAreas[0].AxisY.Minimum = Math.Floor(f_min);
            chart_with_data.ChartAreas[0].AxisY.Maximum = Math.Ceiling(f_max);
            chart_with_data.Invalidate();
        }

        public static void
          SingleGraphXY(double[] X, double[] Y, string Title, int Nx, int Ny)
        {
            Form_with_Data sin = new Form_with_Data(X, Y, Title);
            sin.StartPosition = FormStartPosition.Manual;
            sin.Location = new Point(Nx, Ny);
            sin.ShowDialog();
        }

        public static void
          SingleGraphXY(ToD tod, int Nx, int Ny)
        {
            tod.ToArrays(out double[] X, out double[] Y);
            Form_with_Data fwd = new Form_with_Data(X, Y, tod.Title);
            fwd.StartPosition = FormStartPosition.Manual;
            fwd.Location = new Point(Nx, Ny);
            fwd.ShowDialog();
        }

        private Form_with_Data(double[] x, double[] y, string title)
        {
            InitializeComponent();
            chart_with_data.Series[0].Points.Clear(); // Очистка точек
            chart_with_data.Series[1].Points.Clear();

            chart_with_data.Titles[0].Text = title;
            chart_with_data.ChartAreas[0].AxisX.Interval = 1.0;
            chart_with_data.ChartAreas[0].AxisY.Interval = 1.0;

            Series S1 = new Series(); int n = x.Length - 1;
            double f_min = double.MaxValue, f_max = double.MinValue;
            for (int i = 0; i <= n; i++)
            {
                S1.Points.AddXY(x[i], y[i]);
                f_min = Math.Min(f_min, y[i]);
                f_max = Math.Max(f_max, y[i]);
            }

            S1.ChartType = SeriesChartType.Point;
            S1.MarkerStyle = MarkerStyle.Circle;
            S1.MarkerSize = 9;
            S1.MarkerColor = Color.Red;
            S1.MarkerBorderColor = Color.DarkRed;
            chart_with_data.Series[0] = S1;

            chart_with_data.ChartAreas[0].AxisX.Minimum = Math.Floor(x[0]);
            chart_with_data.ChartAreas[0].AxisX.Maximum = Math.Ceiling(x[n]);
            chart_with_data.ChartAreas[0].AxisY.Minimum = Math.Floor(f_min);
            chart_with_data.ChartAreas[0].AxisY.Maximum = Math.Ceiling(f_max);

            chart_with_data.Invalidate();
        }
    }
}
