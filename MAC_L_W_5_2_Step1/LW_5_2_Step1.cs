using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MAC_L_W_5_2_Step1
{
    public partial class LW_5_2_Step1 : Form
    {
        public LW_5_2_Step1()
        {
            InitializeComponent();
        }

        private void btn_Start_Click(object sender, EventArgs e)
        {
            int j, k, n = (int)UpD_N.Value; string text = "";
            int[] a = new int[n + 1];

            a[0] = 1; for (j = 1; j <= n; j++) a[j] = 0;

            for (k = 0; k < n; k++)
            {
                for (j = k+1; j >= 1; j--)
                    a[j] =  a[j - 1] - k * a[j];
                a[0] = -k * a[0];
            }

            for (j = n; j >= 0; j--)
                text += $"  a[{j,2} ] = {a[j],10} \r\n";
            tBx_Rezult.Text = text;
        }

        private void UpD_N_ValueChanged(object sender, EventArgs e)
        {
            tBx_Rezult.Text = "";
        }
    }
}
