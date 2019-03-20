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

namespace UniformNormal
{
    public partial class Form1 : Form
    {
        double txt1, txt2;
        Normal normal;
        Uniform uniform;
        bool norm = false;
        string str;//checkbox string
        public Form1()
        {
            

            InitializeComponent();
        }
        void Initform()
        {
            txt1 = Convert.ToDouble(textBox1.Text);
            txt2 = Convert.ToDouble(textBox2.Text);
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            str = comboBox1.Text;
            if (str == "Normal")
            {
                norm = true;
                label1.Text = "M=";
                label2.Text = "Q= ";
            }
            else
            {
                label1.Text = "A= ";
                label2.Text = "B= ";
                norm = false;
            }
        }
        public void ShowChartUniform()
        {
            chart1.Series[0].Points.Clear();
            chart2.Series[0].Points.Clear();

            chart1.Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            chart2.Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            //chart1.ChartAreas[0].AxisX.Minimum = uniform.interval_r_begin + (uniform.interval_r_end - uniform.interval_r_begin) / 10;
            //chart1.ChartAreas[0].AxisX.Maximum = uniform.interval_r_end - (uniform.interval_r_end - uniform.interval_r_begin) / 10;
            //chart2.ChartAreas[0].AxisX.Minimum = uniform.interval_r_begin + (uniform.interval_r_end - uniform.interval_r_begin) / 10;
            //chart2.ChartAreas[0].AxisX.Maximum = uniform.interval_r_end - (uniform.interval_r_end - uniform.interval_r_begin) / 10;
            //var series1 = new System.Windows.Forms.DataVisualization.Charting.Series
            //{
            //    Name = "Series1",
            //    Color = System.Drawing.Color.Red,
            //    IsVisibleInLegend = false,
            //    //IsXValueIndexed = true,
            //    ChartType = SeriesChartType.Spline
            //};
                        for (int i = 0; i < uniform.count_p; i++)
            {
                chart1.Series[0].Points.AddXY(Math.Round(uniform.DensityXYArray[0, i], 1), uniform.DensityXYArray[1, i]);
            }

                        for (int i = 0; i < uniform.count_r; i++)
            {
                chart2.Series[0].Points.AddXY(uniform.FuncXYArray[0, i], uniform.FuncXYArray[1, i]);
            }


        }
        void ShowChartNormal()
        {


        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (norm == false)
            {
                uniform = new Uniform();
                uniform.initialize();
                Initform();
                uniform.Calculate(txt1, txt2);
                ShowChartUniform();
            }
            else
            {
                normal = new Normal();
                normal.initialize();
                Initform();
                normal.Calculate(txt1, txt2);
                ShowChartNormal();

            }
        }
        

        private void Form1_Load(object sender, EventArgs e)
        {
            textBox1.Text = "0";
            textBox2.Text = "0";
            comboBox1.Text = "Uniform";
            this.comboBox1.Items.AddRange(new object[] {"Uniform",
                        "Normal"});
        }
    }
}
