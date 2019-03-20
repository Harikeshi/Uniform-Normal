using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniformNormal
{
    class Uniform
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
        public double F_r(double x, double txt1, double txt2)//распред равномерн
        {
            if (x <= txt1)
            {
                return 0;
            }
            else if (x > txt2)
            {
                return 1;
            }
            else
            {
                return (x - txt1) / (txt2 - txt1);
            }
        }
        public double f_r(double x, double txt1, double txt2)//плотность равномерн.
        {
            if (x <= txt1 || x > txt2)
            {
                return 0;
            }
            else return 1 / (txt2 - txt1);
        }

        public void Calculate(double txt1, double txt2)
        {
            interval_r_begin = txt1 - (txt2 - txt1) / 3;
            interval_r_end = txt2 + (txt2 - txt1) / 3;
            interval_step = (interval_r_end - interval_r_begin) / 100;
            count_r = 100;//количество шагов для распределения
            FuncXYArray = new double[2, count_r];//массив x, y для распред.
            for (int i = 0; i < count_r; i++)
            {
                if (i == 0)//в нулевой точке
                {
                    FuncXYArray[0, i] = interval_r_begin;
                    FuncXYArray[1, i] = F_r(FuncXYArray[0, i], txt1, txt2);
                }
                else
                {
                    FuncXYArray[0, i] = FuncXYArray[0, i - 1] + interval_step;
                    FuncXYArray[1, i] = F_r(FuncXYArray[0, i], txt1, txt2);
                }
            }
            //плотность
            count_p = 100;
            interval_p_begin = txt1 - (txt2 - txt1) / 2;
            interval_p_end = txt2 + (txt2 - txt1) / 2;
            interval_step = (interval_p_end - interval_p_begin) / count_p;
            DensityXYArray = new double[2, count_p];
            for (int i = 0; i < count_p; i++)
            {
                if (i == 0)
                {
                    DensityXYArray[0, 0] = interval_p_begin;
                    DensityXYArray[1, 0] = f_r(DensityXYArray[0, 0], txt1, txt2);
                }
                else
                {
                    DensityXYArray[0, i] = DensityXYArray[0, i - 1] + interval_step;
                    DensityXYArray[1, i] = f_r(DensityXYArray[0, i], txt1, txt2);
                }
            }

        }
    }
}
