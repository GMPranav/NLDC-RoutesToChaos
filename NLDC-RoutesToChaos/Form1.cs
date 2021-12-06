using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NLDC_RoutesToChaos
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        public void draw_logis_graph()
        {
            foreach (var series in chart1.Series)
                series.Points.Clear();
            int n = Convert.ToInt32(numericUpDown1.Value);
            float x = Convert.ToSingle(numericUpDown2.Value);
            float r = Convert.ToSingle(numericUpDown3.Value);
            for (int i = 0; i <= n; i++)
            {
                this.chart1.Series["Population Value"].Points.AddXY(i, x);
                x = x * (1 - x) * r;
            }
        }

        public void draw_inter_graph()
        {
            foreach (var series in chart1.Series)
                series.Points.Clear();
            int n = Convert.ToInt32(numericUpDown1.Value);
            float x = Convert.ToSingle(numericUpDown2.Value);
            float r = Convert.ToSingle(numericUpDown3.Value);
            float max = x;
            for (int i = 0; i <= n; i++)
            {
                x = 1 - (x * x * r);
                if (x > max)
                    max = x;
            }
            x = Convert.ToSingle(numericUpDown2.Value);
            for (int i = 0; i <= n; i++)
            {
                float xn = x / max;
                this.chart1.Series["Population Value"].Points.AddXY(i, xn);
                x = 1 - (x * x * r);
                if (x < 0)
                {
                    break;
                }
            }

        }

        public void draw_crisis_graph()
        {
            foreach (var series in chart1.Series)
                series.Points.Clear();
            int n = Convert.ToInt32(numericUpDown1.Value);
            float x = Convert.ToSingle(numericUpDown2.Value);
            float y = Convert.ToSingle(numericUpDown4.Value);
            float r = Convert.ToSingle(numericUpDown3.Value);
            float max = x;
            for (int i = 0; i <= n; i++)
            {
                float xt = x;
                x = 1 - (x * x * r) + y;
                y = (float)(xt * 0.3);
                if (x > max)
                    max = x;
            }
            x = Convert.ToSingle(numericUpDown2.Value);
            y = Convert.ToSingle(numericUpDown4.Value);
            for (int i = 0; i <= n; i++)
            {
                float xn = x / max;
                float xt = x;
                this.chart1.Series["Population Value"].Points.AddXY(i, xn);
                x = 1 - (x * x * r) + y;
                y = (float)(xt * 0.3);
                if (x < 0)
                {
                    break;
                }
            }
        }

        public void draw_graph()
        {
            if(comboBox1.SelectedIndex == 0)
            {
                draw_logis_graph();
                this.chart1.ChartAreas[0].Axes[1].Title = "Population";
            }
            else if(comboBox1.SelectedIndex == 1)
            {
                draw_inter_graph();
                this.chart1.ChartAreas[0].Axes[1].Title = "Population (normalized)";
            }
            else if (comboBox1.SelectedIndex == 2)
            {
                draw_crisis_graph();
                this.chart1.ChartAreas[0].Axes[1].Title = "Population (normalized)";
            }
        }
        public void readjust_graph(int n)
        {
            chart1.ChartAreas[0].RecalculateAxesScale();
            chart1.ChartAreas[0].AxisX.Crossing = 0;
            chart1.ChartAreas[0].AxisY.Crossing = 0;
            chart1.ChartAreas[0].AxisX.Minimum = 0;
            chart1.ChartAreas[0].AxisX.Maximum = n;
            chart1.ChartAreas[0].AxisY.Minimum = 0;
            chart1.ChartAreas[0].AxisY.Maximum = 1.0;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int n = Convert.ToInt32(numericUpDown1.Value);
            draw_graph();
            readjust_graph(n);
        }

        private void form_resize(object sender, EventArgs e)
        {
            int w = this.Width - 255;
            int h = this.Height - 70;
            chart1.SetBounds(225, 15, w, h);
            int n = Convert.ToInt32(numericUpDown1.Value);
            readjust_graph(n);
            label5.SetBounds(2, h - 5, 193, 13);
            label6.SetBounds(2, h + 10, 225, 13);
        }

        private void n_value_changed(object sender, EventArgs e)
        {
            if(numericUpDown1.Value > 1000)
            {
                numericUpDown1.Value = 1000;
            }
            if (numericUpDown1.Value < 1)
            {
                numericUpDown1.Value = 1;
            }
            int n = Convert.ToInt32(numericUpDown1.Value);
            draw_graph();
            readjust_graph(n);
        }

        private void x_value_changed(object sender, EventArgs e)
        {
            if (numericUpDown2.Value > 1)
            {
                numericUpDown2.Value = 1;
            }
            if (numericUpDown2.Value < 0)
            {
                numericUpDown2.Value = 0;
            }
            int n = Convert.ToInt32(numericUpDown1.Value);
            draw_graph();
            readjust_graph(n);
        }

        private void r_value_changed(object sender, EventArgs e)
        {
            if (numericUpDown3.Value > 4)
            {
                numericUpDown3.Value = 4;
            }
            if (numericUpDown3.Value < 0)
            {
                numericUpDown3.Value = 0;
            }
            int n = Convert.ToInt32(numericUpDown1.Value);
            draw_graph();
            readjust_graph(n);
        }

        private void combobox_changed(object sender, EventArgs e)
        {
            switch (comboBox1.SelectedIndex)
            {
                case 0:
                case 1:
                    this.label4.ForeColor = SystemColors.ControlDark;
                    this.numericUpDown4.Enabled = false;
                    break;
                case 2:
                    this.label4.ForeColor = SystemColors.ControlText;
                    this.numericUpDown4.Enabled = true;
                    break;
            }
        }

        private void y_value_changed(object sender, EventArgs e)
        {
            if (numericUpDown4.Value > 1)
            {
                numericUpDown4.Value = 1;
            }
            if (numericUpDown4.Value < 0)
            {
                numericUpDown4.Value = 0;
            }
            int n = Convert.ToInt32(numericUpDown1.Value);
            draw_graph();
            readjust_graph(n);
        }
    }
}
