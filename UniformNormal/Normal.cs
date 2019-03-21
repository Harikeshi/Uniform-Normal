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
        public int count;
        public double interval_begin;
        public double interval_end;
        public double interval_step;
        public void initialize()
        {
            interval_begin = 0;
            interval_end = 0;
            interval_step = 0;
            count = 0;
        }      
        public double DistNormal(double x,double txt1,double txt2)
        {
            return (Func(x,txt1,txt2))/(txt2*Math.Sqrt(2*Math.PI));
        }
        public double Func(double x,double txt1,double txt2)
        {
            double res = Math.Pow((x - txt1) ,2) / (2 * Math.Pow((txt2),2));
            return Math.Exp(-Math.Pow(res,2));
        }
        public double FuncNormal(double x,double txt1,double txt2)
        {
            return 0.5 + Integral(0, x, txt1, txt2) / (txt2*1.45*Math.Sqrt(Math.PI));
        }     
        public double Integral(double x1, double x,double txt1,double txt2)
        {
            double eps = 0.00001;
            double I = eps + 1, I1 = 0;
            for (int N = 2; (N <= 4) || (Math.Abs(I1 - I) > eps); N *= 2)
            {
                double h, sum2 = 0, sum4 = 0, sum = 0;
                h = (x - x1) / (2 * N);
                for (int i = 1; i <= 2 * N - 1; i += 2)
                {
                    sum4 += Func(x1 + h * i,txt1,txt2);
                    sum2 += Func(x1 + h * (i + 1),txt1,txt2);
                }
                sum = Func(x1, txt1, txt2) + 4 * sum4 + 2 * sum2 - Func(x, txt1, txt2) ;
                I = I1;
                I1 = (h / 3) * sum;
            }
            return I1;
        }
        public void Calculate(double txt1,double txt2)
        {
            
            double tmp_x = txt1;
            interval_step = 0.1;
            while (DistNormal(tmp_x,txt1,txt2) >= 0.0001)
            {
                tmp_x -= interval_step;
            }
            double interval_begin=tmp_x;
            tmp_x += interval_step;
            count++;
            while (DistNormal(tmp_x, txt1, txt2) >= 0.0001)
            {
                count++;
                tmp_x += interval_step;
            }
            interval_end = tmp_x;
            tmp_x = 0;
            DensityXYArray = new double[2, count + 1];
            for (int i = 0; i < count + 1; i++)
            {
                if (i == 0)
                {
                    DensityXYArray[0, 0] = interval_begin;
                    DensityXYArray[1, 0] = DistNormal(DensityXYArray[0, 0],txt1,txt2);
                }
                else
                {
                    DensityXYArray[0, i] = DensityXYArray[0, i - 1] + interval_step;
                    DensityXYArray[1, i] = DistNormal(DensityXYArray[0, i],txt1,txt2);
                }
            }
            FuncXYArray = new double[2, count + 1];
            
            for (int i = 0; i < count + 1; i++)
            {
                if (i == 0)
                {
                    FuncXYArray[0, 0] = interval_begin;
                    FuncXYArray[1, 0] = FuncNormal(interval_begin,txt1,txt2);
                }
                else
                {
                    FuncXYArray[0, i] = FuncXYArray[0, i - 1] + interval_step;
                    FuncXYArray[1, i] = FuncNormal(FuncXYArray[0, i],txt1,txt2);
                }
            }
        }
    }
}
