using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ackermann
{
    class Program
    {
        static void Main(string[] args)
        {
            int Ackermann(int a,int b)
            {
                if (a == 0)
                {
                    return b + 1;
                }
                else if(a> 0 && b == 0)
                {
                    return Ackermann(a - 1, 1);
                }
                else
                {
                    return Ackermann(a - 1, Ackermann(a, b - 1));
                }
            }
            Console.WriteLine("m ve n degerlerini giriniz:");
            int m = int.Parse(Console.ReadLine());
            int n = int.Parse(Console.ReadLine());
            Console.WriteLine("Ackermann fonksiyonu sonucu: " + Ackermann(m, n));
            Console.ReadLine();
        }
    }
}
