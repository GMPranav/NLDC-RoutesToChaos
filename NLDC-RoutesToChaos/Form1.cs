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
        private bool isUpdatingControls = false;

        public Form1()
        {
            InitializeComponent();
            combobox_changed(this, EventArgs.Empty);
        }

        public void draw_logis_graph()
        {
            chart1.Series[0].Points.Clear();
            int n = Convert.ToInt32(numericUpDown1.Value);
            double x = Convert.ToDouble(numericUpDown2.Value);
            double r = Convert.ToDouble(numericUpDown3.Value);
            for (int i = 0; i <= n; i++)
            {
                chart1.Series[0].Points.AddXY(i, x);
                x = r * x * (1.0 - x);
                if (double.IsNaN(x) || double.IsInfinity(x) || Math.Abs(x) > 10.0)
                {
                    break;
                }
            }
        }

        public void draw_inter_graph()
        {
            chart1.Series[0].Points.Clear();
            int n = Convert.ToInt32(numericUpDown1.Value);
            double x = Convert.ToDouble(numericUpDown2.Value);
            double r = Convert.ToDouble(numericUpDown3.Value);
            for (int i = 0; i <= n; i++)
            {
                chart1.Series[0].Points.AddXY(i, x);
                x = 1.0 - (r * x * x);
                if (double.IsNaN(x) || double.IsInfinity(x) || Math.Abs(x) > 10.0)
                {
                    break;
                }
            }
        }

        public void draw_crisis_graph()
        {
            chart1.Series[0].Points.Clear();
            int n = Convert.ToInt32(numericUpDown1.Value);
            double x = Convert.ToDouble(numericUpDown2.Value);
            double y = Convert.ToDouble(numericUpDown4.Value);
            double r = Convert.ToDouble(numericUpDown3.Value);
            for (int i = 0; i <= n; i++)
            {
                chart1.Series[0].Points.AddXY(i, x);
                double xt = x;
                x = 1.0 - (r * x * x) + y;
                y = 0.3 * xt;
                if (double.IsNaN(x) || double.IsInfinity(x) || Math.Abs(x) > 10.0)
                {
                    break;
                }
            }
        }

        public void draw_graph()
        {
            if (comboBox1.SelectedIndex == 0)
            {
                draw_logis_graph();
                chart1.ChartAreas[0].Axes[1].Title = "Population x";
            }
            else if (comboBox1.SelectedIndex == 1)
            {
                draw_inter_graph();
                chart1.ChartAreas[0].Axes[1].Title = "State Variable x";
            }
            else if (comboBox1.SelectedIndex == 2)
            {
                draw_crisis_graph();
                chart1.ChartAreas[0].Axes[1].Title = "State Variable x";
            }
        }

        public void readjust_graph(int n)
        {
            chart1.ChartAreas[0].AxisX.Minimum = 0;
            chart1.ChartAreas[0].AxisX.Maximum = n;
            chart1.ChartAreas[0].AxisX.Crossing = 0;

            if (comboBox1.SelectedIndex == 0)
            {
                chart1.ChartAreas[0].AxisY.Crossing = 0;
                chart1.ChartAreas[0].AxisY.Minimum = 0;
                chart1.ChartAreas[0].AxisY.Maximum = 1.0;
                chart1.ChartAreas[0].AxisY.Interval = 0.2;
            }
            else
            {
                chart1.ChartAreas[0].AxisY.Crossing = 0;
                chart1.ChartAreas[0].AxisY.Minimum = -1.5;
                chart1.ChartAreas[0].AxisY.Maximum = 1.5;
                chart1.ChartAreas[0].AxisY.Interval = 0.5;
            }
            chart1.ChartAreas[0].RecalculateAxesScale();
        }

        private void SetNumericRange(NumericUpDown nud, decimal min, decimal max, decimal val, decimal inc, int decimals)
        {
            nud.DecimalPlaces = decimals;
            nud.Increment = inc;
            if (val < min) val = min;
            if (val > max) val = max;

            if (nud.Minimum > min) nud.Minimum = min;
            if (nud.Maximum < max) nud.Maximum = max;

            nud.Value = val;
            nud.Minimum = min;
            nud.Maximum = max;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int n = Convert.ToInt32(numericUpDown1.Value);
            draw_graph();
            readjust_graph(n);
        }

        private void form_resize(object sender, EventArgs e)
        {
            int w = this.ClientSize.Width - 240;
            int h = this.ClientSize.Height - 30;
            if (w > 100 && h > 100)
            {
                chart1.SetBounds(225, 15, w, h);
                int n = Convert.ToInt32(numericUpDown1.Value);
                readjust_graph(n);
            }
            label5.Top = this.ClientSize.Height - 35;
            label6.Top = this.ClientSize.Height - 20;
        }

        private void n_value_changed(object sender, EventArgs e)
        {
            if (isUpdatingControls) return;
            int n = Convert.ToInt32(numericUpDown1.Value);
            draw_graph();
            readjust_graph(n);
        }

        private void x_value_changed(object sender, EventArgs e)
        {
            if (isUpdatingControls) return;
            int n = Convert.ToInt32(numericUpDown1.Value);
            draw_graph();
            readjust_graph(n);
        }

        private void r_value_changed(object sender, EventArgs e)
        {
            if (isUpdatingControls) return;
            int n = Convert.ToInt32(numericUpDown1.Value);
            draw_graph();
            readjust_graph(n);
        }

        private void y_value_changed(object sender, EventArgs e)
        {
            if (isUpdatingControls) return;
            int n = Convert.ToInt32(numericUpDown1.Value);
            draw_graph();
            readjust_graph(n);
        }

        private void combobox_changed(object sender, EventArgs e)
        {
            isUpdatingControls = true;
            try
            {
                switch (comboBox1.SelectedIndex)
                {
                    case 0:
                        label2.Text = "x(0) [Population]";
                        label3.Text = "Rate (r)";
                        label4.Text = "y(0)";
                        label4.ForeColor = SystemColors.ControlDark;
                        numericUpDown4.Enabled = false;

                        SetNumericRange(numericUpDown2, 0m, 1m, 0.5m, 0.05m, 2);
                        SetNumericRange(numericUpDown3, 0m, 4m, 3.2m, 0.01m, 3);
                        break;

                    case 1:
                        label2.Text = "Initial x(0)";
                        label3.Text = "Parameter (r)";
                        label4.Text = "y(0)";
                        label4.ForeColor = SystemColors.ControlDark;
                        numericUpDown4.Enabled = false;

                        SetNumericRange(numericUpDown2, -2m, 2m, 0.5m, 0.05m, 2);
                        SetNumericRange(numericUpDown3, 0m, 2m, 1.748m, 0.005m, 3);
                        break;

                    case 2:
                        label2.Text = "Initial x(0)";
                        label3.Text = "Parameter (a)";
                        label4.Text = "Initial y(0)";
                        label4.ForeColor = SystemColors.ControlText;
                        numericUpDown4.Enabled = true;

                        SetNumericRange(numericUpDown2, -2m, 2m, 0.1m, 0.05m, 2);
                        SetNumericRange(numericUpDown4, -2m, 2m, 0.1m, 0.05m, 2);
                        SetNumericRange(numericUpDown3, 0m, 1.42m, 1.4m, 0.005m, 3);
                        break;
                }
            }
            finally
            {
                isUpdatingControls = false;
            }

            int n = Convert.ToInt32(numericUpDown1.Value);
            draw_graph();
            readjust_graph(n);
        }
    }
}
