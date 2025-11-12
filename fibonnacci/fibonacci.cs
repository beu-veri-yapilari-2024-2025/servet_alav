using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fibonnacci_Recursive
{
    class Program
    {
        
        static void Main(string[] args)
        {
            int fibonnaciRecursive(int a)
            {
                if (a == 0)
                {
                    return 0;
                }
                else if (a == 1)
                {
                    return 1;
                }
                return fibonnaciRecursive(a - 1) + fibonnaciRecursive(a - 2);
            }
            Console.WriteLine("kaçıncı fibonacci sayisini istiyorsun:");
            int n = Convert.ToInt32(Console.ReadLine());
            int sonuc = fibonnaciRecursive(n);
            Console.WriteLine(sonuc);
            Console.ReadLine();
        }

       
    }
}
