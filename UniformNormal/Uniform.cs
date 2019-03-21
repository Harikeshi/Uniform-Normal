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
        public int count;
        //public double interval_r_begin;
        //public double interval_r_end;
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
            interval_begin = txt1 - (txt2 - txt1) / 3;
            interval_end = txt2 + (txt2 - txt1) / 3;
            interval_step = (interval_end - interval_begin) / 100;
            count = 100;//количество шагов для распределения
            FuncXYArray = new double[2, count];//массив x, y для распред.
            for (int i = 0; i < count; i++)
            {
                if (i == 0)//в нулевой точке
                {
                    FuncXYArray[0, i] = interval_begin;
                    FuncXYArray[1, i] = F_r(FuncXYArray[0, i], txt1, txt2);
                }
                else
                {
                    FuncXYArray[0, i] = FuncXYArray[0, i - 1] + interval_step;
                    FuncXYArray[1, i] = F_r(FuncXYArray[0, i], txt1, txt2);
                }
            }
            //плотность
            interval_begin = txt1 - (txt2 - txt1) / 2;
            interval_end = txt2 + (txt2 - txt1) / 2;
            interval_step = (interval_end - interval_begin) / count;
            DensityXYArray = new double[2, count];
            for (int i = 0; i < count; i++)
            {
                if (i == 0)
                {
                    DensityXYArray[0, 0] = interval_begin;
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
