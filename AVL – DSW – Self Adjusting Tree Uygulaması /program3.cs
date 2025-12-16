using System;
using System.Collections.Generic;

namespace DSW_Algorithm_App
{
    // Ağaç Düğümü Sınıfı
    public class Node
    {
        public string Data;
        public Node Left, Right;

        public Node(string data)
        {
            Data = data;
            Left = Right = null;
        }
    }

    public class DSWTree
    {
        private Node root;

        public DSWTree()
        {
            root = null;
        }

        // Düğüm Ekleme (Standart BST Ekleme)
        public void Add(string data)
        {
            root = InsertRec(root, data);
        }

        private Node InsertRec(Node root, string data)
        {
            if (root == null)
            {
                return new Node(data);
            }

            // Alfabetik karşılaştırma (String.Compare)
            if (String.Compare(data, root.Data) < 0)
                root.Left = InsertRec(root.Left, data);
            else if (String.Compare(data, root.Data) > 0)
                root.Right = InsertRec(root.Right, data);

            return root;
        }

        // ==========================================
        // DSW ALGORİTMASI METOTLARI
        // ==========================================

        // Sağa Döndürme (Backbone oluşturmak için)
        private void RotateRight(Node grandParent, Node parent, Node child)
        {
            if (grandParent != null)
                grandParent.Right = child;
            else
                root = child;

            parent.Left = child.Right;
            child.Right = parent;
        }

        // Sola Döndürme (Dengelemek için)
        private void RotateLeft(Node grandParent, Node parent, Node child)
        {
            if (grandParent != null)
                grandParent.Right = child;
            else
                root = child;

            parent.Right = child.Left;
            child.Left = parent;
        }

        // 1. ADIM: Backbone (Vine) Oluşturma
        // Tüm sol çocukları sağa döndürerek ağacı düz bir listeye çevirir.
        public void CreateBackbone()
        {
            Node grandParent = null;
            Node temp = root;
            
            while (temp != null)
            {
                if (temp.Left != null)
                {
                    Node child = temp.Left;
                    RotateRight(grandParent, temp, child);
                    temp = child; // Döndürmeden sonra temp child olur
                }
                else
                {
                    grandParent = temp;
                    temp = temp.Right;
                }
            }
        }

        // 2. ADIM: Backbone'dan Dengeli Ağaç Oluşturma
        public void CreateBalancedTree()
        {
            // Önce düğüm sayısını (n) bulalım
            int n = 0;
            Node temp = root;
            while (temp != null)
            {
                n++;
                temp = temp.Right;
            }

            // Mükemmel dengeli ağaç için hesaplamalar
            // m = n - (2^floor(log2(n+1)) - 1)
            int expectedHeight = (int)Math.Log(n + 1, 2);
            int m = n - ((1 << expectedHeight) - 1);

            // İlk tur: 'm' kadar sola dönüş yap
            MakeRotations(m);

            // Sonraki turlar: Kalan düğümleri dengelemek için döngü
            while (n > 1)
            {
                n = n >> 1; // n / 2
                MakeRotations(n);
            }
        }

        // Belirtilen sayıda (count) sola dönüş yapan yardımcı metot
        private void MakeRotations(int count)
        {
            Node grandParent = null;
            Node parent = root;

            for (int i = 0; i < count; i++)
            {
                if (parent == null || parent.Right == null) break;

                Node child = parent.Right;
                RotateLeft(grandParent, parent, child);
                
                // İşaretçileri ilerlet
                grandParent = child;
                parent = grandParent.Right;
            }
        }

        // ==========================================
        // GÖRSELLEŞTİRME VE TEST
        // ==========================================

        public void PrintTree()
        {
            PrintTreeRec(root, "", true);
        }

        // Ağacı konsolda hiyerarşik yazdırmak için yardımcı metot
        private void PrintTreeRec(Node node, string indent, bool last)
        {
            if (node != null)
            {
                Console.Write(indent);
                if (last)
                {
                    Console.Write("└─");
                    indent += "  ";
                }
                else
                {
                    Console.Write("├─");
                    indent += "| ";
                }
                Console.WriteLine(node.Data);

                PrintTreeRec(node.Left, indent, false);
                PrintTreeRec(node.Right, indent, true);
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            DSWTree tree = new DSWTree();
            
            // Verilen karakter dizisi
            string[] inputs = { "S", "E", "L", "İ", "M", "K", "A", "Ç", "T", "I" };

            Console.WriteLine("--- Veriler Ekleniyor ---");
            foreach (var item in inputs)
            {
                tree.Add(item);
                Console.Write(item + " ");
            }
            Console.WriteLine("\n\n--- 1. Başlangıç Durumu (Ekleme Sırasına Göre) ---");
            tree.PrintTree();

            Console.WriteLine("\n--- 2. Adım: Backbone (Vine) Oluşturuluyor... ---");
            tree.CreateBackbone();
            tree.PrintTree(); // Tüm düğümler sağa yaslanmış olmalı

            Console.WriteLine("\n--- 3. Adım: DSW ile Dengeleme Yapılıyor... ---");
            tree.CreateBalancedTree();
            tree.PrintTree(); // Mükemmel dengeli ağaç

            Console.WriteLine("\nİşlem Tamamlandı. Çıkmak için bir tuşa basın.");
            Console.ReadKey();
        }
    }
}
