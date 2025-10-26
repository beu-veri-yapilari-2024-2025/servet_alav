using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Linkedlist_Works_3
{
    class Node
    {
        public int veri;
        public Node sonraki;
        public Node onceki;
        public Node(int veri)
        {
            this.veri = veri;
            this.sonraki = null;
            this.onceki = null;
        }

    }
    class yiginlinkedlist
        {
        
        Node  node= null;
        
        public void basaekle(int veri)
        {
            Node yeni = new Node(veri);
            if (node == null)
            {
                node = yeni;
                Console.WriteLine($"{veri} Eklendi");
                
            }
            else
            {
                while (node.onceki != null)
                {
                    node = node.onceki;      
                }
                node.onceki = yeni;
                Console.WriteLine($"{veri} Başa Eklendi");
            }
        }
        public void sondanekle(int veri)
        {
            Node yeni = new Node(veri);
            if (node == null)
            {
                node = yeni;
                Console.WriteLine($"{veri} Eklendi");
            }
            else
            {
                Node current = node;
                while (current.sonraki != null)
                {
                    current = current.sonraki;
                }
                current.sonraki = yeni;
                yeni.onceki = current;
                Console.WriteLine($"{veri} Sona Eklendi");
            }
        }
        public void arasondaekle(int veri, int aranan)
        {
            Node yeni = new Node(veri);
            if (node == null)
            {
                node = yeni;
                Console.WriteLine($"{veri} Eklendi");
            }
            else
            {
                Node current = node;
                while (current.veri != null&&current.veri!=aranan)
                {
                    current = current.sonraki;
                }
                if (current == null)
                {
                    Console.WriteLine($"{aranan} eleman Bulunamadı");
                    return;
                }
                yeni.sonraki = current.sonraki;
                yeni.onceki = current;
                if (current.sonraki != null)
                {
                    current.sonraki.onceki = yeni;
                }
                current.sonraki = yeni;

                Console.WriteLine($"{veri} elemani {aranan} elemandan sonra Eklendi");
            }
        }
        public void arabastaekle(int veri, int aranan)
        {

            Node yeni = new Node(veri);
            if (node == null)
            {
                node = yeni;
                Console.WriteLine("Eklendi");
                return;
            }
            else
            {
                Node current = node;
                while (current != null&&current.veri!=aranan)
                {
                    current = current.onceki;
                }
                if (current == null)
                {
                    Console.WriteLine($"HATA: {aranan} elemanı bulunamadı. Ekleme yapılmadı.");
                    return;
                }
                yeni.sonraki = current;
                yeni.onceki = current.onceki;
                if (current == node)
                {
                    node = yeni;
                }
                else
                {
                    current.onceki.sonraki = yeni;  
                    Console.WriteLine($"{veri} elemani {aranan} elemandan once Eklendi");
            }
        }
        }
        public void bastansil()
        {
            if (node == null)
            {
                Console.WriteLine("Liste Boş");
                return;
            }
            else
            {
                while (node.onceki != null)
                {
                    node = node.onceki;
                }
                node = node.sonraki;
                Console.WriteLine("Baştan Silindi");
            }
        }
        public void sondansil()
        {
            if (node == null)
            {
                Console.WriteLine("Liste Boş");
                return;
            }
            else
            {
                Node current = node;
                while (current.sonraki.sonraki != null)
                {
                    current = current.sonraki;
                }
                
                current.sonraki = null;
                Console.WriteLine($"Sondan Silindi");
            }
        }
        public void aradansil(int aranan)
        {
            if (node == null)
            {
                Console.WriteLine("Liste Boş");
                return;
            }
            else
            {
                Node current = node;

                while (current.veri != aranan&&current !=null)
                {
                    current = current.sonraki;
                }
                if (current == node)
                {
                    bastansil();
                    return;
                }
                if(current.sonraki == null)
                {
                    sondansil();
                    return;
                }
                current.onceki.sonraki = current.sonraki;
                current.sonraki.onceki = current.onceki;
                Console.WriteLine($"{aranan} elemani Aradan Silindi");
                
            }
        }
        public void Tumunusil()
        {
            node = null;
            Console.WriteLine("Tüm Liste Silindi");
        }
        public void ara(int aranan)
        {
            if (node == null)
            {
                Console.WriteLine("Liste Boş");
                return;
            }
            else
            {
                while (node != null)
                {
                    if (node.veri == aranan)
                    {
                        Console.WriteLine($"{aranan} eleman Bulundu");
                        return;
                    }
                    node = node.sonraki;
                }
                Console.WriteLine($"{aranan} eleman Bulunamadı");
            }
        }
        public void yazdir()
        {
            if (node == null)
            {
                Console.WriteLine("Liste Boş");
                return;
            }
            else
            {
                Console.WriteLine("Liste Elemanları:");
                Node temp = node;
                while (temp.onceki != null)
                {
                    temp = temp.onceki;
                }
                while (temp != null)
                {
                    Console.WriteLine(temp.veri);
                    temp = temp.sonraki;
                }
            }
        }

    }
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Çift Yönlü Bağlı Liste Yığın Uygulaması:");
            yiginlinkedlist yigin = new yiginlinkedlist();
            int[] dizi;
            yigin.basaekle(10);
            dizi = new int[] {10};
            yigin.sondanekle(20);
            dizi = new int[] { 10, 20 };
            yigin.arasondaekle(15, 10);
            dizi = new int[] { 10, 15, 20 };
            yigin.arabastaekle(5, 10);
            dizi = new int[] { 5, 10, 15, 20 };
            yigin.aradansil(15);
            dizi = new int[] { 5, 10, 20 };
            //yigin.ara(20);
            // yigin.bastansil();
            //yigin.sondansil();
            yigin.Tumunusil();
            dizi = new int[] { };
            yigin.yazdir();
            Console.WriteLine("Dizi Elemanları:");
            foreach (var item in dizi)
            {
                Console.WriteLine(item);
            }


        }
    }
}
