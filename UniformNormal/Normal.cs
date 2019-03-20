using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniformNormal
{
    class Normal
    {
        public double[,] DensityXYArray;
        public double[,] FuncXYArray;
        public int count_p, count_r;
        public double interval_r_begin;
        public double interval_r_end;
        public double interval_p_begin;
        public double interval_p_end;
        public double interval_step;

        public void initialize()
        {
            interval_r_begin = 0;
            interval_r_end = 0;
            interval_p_begin = 0;
            interval_p_end = 0;
            interval_step = 0;
            count_p = 0;
            count_r = 0;
        }
        public double F_r(double x,double txt1,double txt2)//распред равномерн
        {
            return (Math.Exp(-(x - txt1) * (x - txt1) / (2 * txt2 * txt2)));
        }
        public double f_r(double x,double txt1,double txt2)//плотность равномерн.
        {
            return extr(txt2) * (Math.Exp(-((x - txt1) * (x - txt1)) / (2 * txt2 * txt2)));
        }
        public double F_n_h(double x,double txt1,double txt2)
        {
            return (Math.Exp(-(x - txt1) * (x - txt1) / (2 * txt2 * txt2)));
        }
        public double F_n(double x,double txt1,double txt2)//распред норм m=txt1,q=txt2//без 
        {
            return (2 / (Math.Sqrt(2 * Math.PI))) * simpson(0, x,txt1,txt2);
        }
        public double P_x(double x,double txt1,double txt2)
        {
            return 1 / 2 + (1 / 2) * F_r(x,txt1,txt2);
        }
        public double simpson(double x1, double x2,double txt1,double txt2)
        {
            double eps = 0.00001;
            double I = eps + 1, I1 = 0;//I-предыдущее вычисленное значение интеграла, I1-новое, с большим N.
            for (int N = 2; (N <= 4) || (Math.Abs(I1 - I) > eps); N *= 2)
            {
                double h, sum2 = 0, sum4 = 0, sum = 0;
                h = (x2 - x1) / (2 * N);//Шаг интегрирования.
                for (int i = 1; i <= 2 * N - 1; i += 2)
                {
                    sum4 += F_n_h(x1 + h * i,txt1,txt2);//Значения с нечётными индексами, которые нужно умножить на 4.
                    sum2 += F_n_h(x1 + h * (i + 1),txt1,txt2);//Значения с чётными индексами, которые нужно умножить на 2.
                }
                sum = F_n_h(x1,txt1,txt2) + 4 * sum4 + 2 * sum2 - F_n_h(x2, txt1, txt2) ;//Отнимаем значение f(b) так как ранее прибавили его дважды. 
                I = I1;
                I1 = (h / 3) * sum;
            }
            return I1;
        }
        public double extr(double txt2)
        {
            return 1 / (txt2 * Math.Sqrt(2 * Math.PI));
        }

        public void Calculate(double txt1,double txt2)
        {
            
        }



    }
}
