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
            try
            {
                txt1 = Convert.ToDouble(textBox1.Text);
                txt2 = Convert.ToDouble(textBox2.Text);
            }
            catch
            {
                MessageBox.Show(
                               "Введено некорректное значение",
                               "Ошибка",
                               MessageBoxButtons.OK,
                               MessageBoxIcon.Warning,
                               MessageBoxDefaultButton.Button1,
                               MessageBoxOptions.RightAlign);
            }
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
            

            for (int i = 0; i < uniform.count; i++)
            {
                chart1.Series[0].Points.AddXY(Math.Round(uniform.DensityXYArray[0, i], 1), uniform.DensityXYArray[1, i]);
            }

            for (int i = 0; i < uniform.count; i++)
            {
                chart2.Series[0].Points.AddXY(uniform.FuncXYArray[0, i], uniform.FuncXYArray[1, i]);
            }
        }
        void ShowChartNormal()
        {
            chart1.Series[0].Points.Clear();
            chart2.Series[0].Points.Clear();
           
            chart1.Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
            chart2.Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
            chart1.ChartAreas[0].AxisY.Maximum = 1;
            chart2.ChartAreas[0].AxisX.Maximum = 13;
            chart2.ChartAreas[0].AxisX.Minimum = -15;
            //chart2.ChartAreas[0].AxisX.Maximum = normal.interval_end;
            //chart2.ChartAreas[0].AxisX.Minimum = Math.Round(normal.FuncXYArray[0, 0],1);

            for (int i = 0; i < normal.count; i++)
            {
                chart1.Series[0].Points.AddXY(Math.Round(normal.DensityXYArray[0, i],1), normal.DensityXYArray[1, i]);
            }
           
            for (int i = 0; i < normal.count; i++)
            {
                chart2.Series[0].Points.AddXY(Math.Round(normal.FuncXYArray[0, i],1), normal.FuncXYArray[1, i]);
            }
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Initform();
            double txt=txt2;
            if (txt2 < 0.4) {  txt2 = 0.4; }
            if (txt2 != 0 && txt2 >0)
            {
                if (norm == true)
                {
                    normal.Calculate(txt1, txt2);
                    ShowChartNormal();
                }
                else
                {
                    uniform.Calculate(txt1, txt);
                    ShowChartUniform();
                    txt = 0;
                }
            }
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            textBox1.Text = "0";
            textBox2.Text = "0";
            comboBox1.Text = "Uniform";
            this.comboBox1.Items.AddRange(new object[] {"Uniform",
                        "Normal"});
            uniform = new Uniform();
            normal = new Normal();
            //normal.initialize();
            uniform.initialize();
            Initform();
        }
    }
}
