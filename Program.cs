using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwoSearch
{
    class Program
    {
        
        static void Main (string[] args)
        {
            int[] dizi1 = new int[10];
            dizi1[0] = 10;
            dizi1[1] = 20;
            dizi1[2] = 30;
            dizi1[3] = 40;
            dizi1[4] = 50;
            dizi1[5] = 60;
            dizi1[6] = 70;
            dizi1[7] = 80;
            dizi1[8] = 90;
            dizi1[9] = 100;
            int[,] dizi2 = new int[2, 3];
            dizi2[0, 0] = 1;
            dizi2[0, 1] = 2;
            dizi2[0, 2] = 3;
            dizi2[1, 0] = 4;
            dizi2[1, 1] = 5;
            dizi2[1, 2] = 6;
            

            foreach (var item in dizi1)
            {
                Console.WriteLine(item);
            }
            int sol = 0;
            int sag = dizi1.Length - 1;
            int orta=(sol + sag) / 2;
            int aranan_sayi_index = 3;
            int islem_sayisi = 0;
            while (sol <= sag)
            {
                if (orta == aranan_sayi_index)
                {
                    Console.WriteLine("aranan sayi bulundu: " + dizi1[orta]);
                    Console.WriteLine("islem sayisi: " + islem_sayisi);
                    break;
                }
                else if (orta < aranan_sayi_index)
                {
                    sol = orta + 1;
                    orta = (sol + sag) / 2;
                    islem_sayisi++;
                }
                else if (orta > aranan_sayi_index)
                {
                    sag = orta - 1;
                    orta = (sol + sag) / 2;
                    islem_sayisi++;
                }
                else
                {
                    Console.WriteLine("aranan sayi bulunamadi");
                    break;
                }

            }
            DiziToplama(dizi1);
            MatrisCarpımı(dizi2);
            Console.ReadLine();
        }
        public static void DiziToplama(int[] dizi)
        {
            int toplam = 0;
            for (int i = 0; i <dizi.Length-1; i++)
            {
                toplam += dizi[i];
            }
            Console.WriteLine("Dizi Toplam: " + toplam);
        }
        public static void MatrisCarpımı(int[,] dizi)
        {
            int carpim;
            for (int i = 0; i < dizi.GetLength(0); i++)
            {
                carpim = 1;
                for (int j = 0; j < dizi.GetLength(1); j++)
                {
                    carpim *= dizi[i, j];
                }
                Console.WriteLine("Satır " + (i + 1) + " çarpımı: " + carpim);
            }
        }
    }
}
