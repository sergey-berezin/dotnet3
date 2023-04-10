using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathLib
{
    public static class Utils
    {
        public static Result ArgMaxMin(double[] a)
        {
            if(a.Length == 0)
            {
                throw new ArgumentException();
            }
            int minIndex = 0, maxIndex = 0;
            for (int i = 1; i < a.Length; i++)
            {
                if (a[i] < a[minIndex])
                {
                    minIndex = i;
                }

                if (a[i] > a[maxIndex])
                {
                    maxIndex = i;
                }
            }
            return new Result(minIndex, maxIndex);
        }
    }

    public record Result(int MinIndex, int MaxIndex);
}
