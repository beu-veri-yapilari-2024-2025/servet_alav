using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace standart_sapma
{
    class Program
    {
        static void Main(string[] args)
        {
            double standartSapma(double[] veri, int index, double sum)
            {
                if (index < veri.Length)
                {
                    sum += Math.Pow((veri[index] - veri.Average()), 2);
                    return standartSapma(veri, index + 1, sum);
                }
                double veriance = sum / veri.Length;
                return Math.Sqrt(veriance);
            }
            double[] data = { 10, 12, 23, 23, 16, 23, 21, 16 };
            double result = standartSapma(data, 0, 0);
            Console.WriteLine("Standart Sapma: " + result);
        }
    }
}
