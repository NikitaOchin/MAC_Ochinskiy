using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MAC_L_W_5_2_Step2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private double fact(int n)
        {
            double sum = 1;
            for(int i = 1; i <= n; i++)
                sum *= i;
            return sum;
        }

        private void btn_Start_Click(object sender, EventArgs e)
        {
            int j, k, n = (int)UpD_n.Value, i = (int)UpD_i.Value; 
            string text = ""; double koeff = 1.0;
            int[] a = new int[n + 1]; double[] A = new double[n + 1];

            a[0] = 1; for (j = 1; j <= n; j++) a[j] = 0;

            for (k = 0; k <= n; k++)
            {
                if (k == i) continue;
                for (j = n; j >= 1; j--)
                    a[j] = a[j - 1] - k * a[j];
                a[0] = -k * a[0];
            }

            koeff = Math.Pow(-1, n - i) / fact(i) / fact(n - i);

            for (j = 0; j <= n; j++)
                A[j] = a[j] * koeff;

            text += $" n = {n},   i = {i}   \r\n";
            for (j = n; j >= 0; j--)
                text += $"  a[{i,2}, {j,2} ] = {a[j],16} ," +
                        $"  A[{i,2}, {j,2} ] = {A[j],36:F30}\r\n";
            tBx_Rezult.Text = text + $"\r\n koeff = {koeff, 34:F30} \r\n";
        }

        private void UpD_i_ValueChanged(object sender, EventArgs e)
        {
            tBx_Rezult.Text = "";
        }

        private void UpD_n_ValueChanged(object sender, EventArgs e)
        {
            tBx_Rezult.Text = ""; UpD_i.Maximum = UpD_n.Value; UpD_i.Value = 0;
        }
    }
}
