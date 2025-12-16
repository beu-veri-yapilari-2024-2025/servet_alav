using System;

namespace SelfAdjustingTree_App
{
    // Düğüm Sınıfı (Parent pointer ekledik, yukarı tırmanmayı kolaylaştırmak için)
    public class Node
    {
        public string Data;
        public int Frequency; // Erişim sıklığı
        public Node Left, Right, Parent;

        public Node(string data)
        {
            Data = data;
            Frequency = 0; // Başlangıç frekansı 0 veya 1 olabilir
            Left = Right = Parent = null;
        }
    }

    public class FrequencyTree
    {
        public Node root;

        public FrequencyTree()
        {
            root = null;
        }

        // Standart BST Ekleme (Başlangıç ağacını kurmak için)
        public void Add(string data)
        {
            if (root == null)
            {
                root = new Node(data);
                return;
            }

            Node current = root;
            Node parent = null;

            while (current != null)
            {
                parent = current;
                if (String.Compare(data, current.Data) < 0)
                    current = current.Left;
                else if (String.Compare(data, current.Data) > 0)
                    current = current.Right;
                else
                    return; // Zaten varsa ekleme
            }

            Node newNode = new Node(data);
            newNode.Parent = parent;

            if (String.Compare(data, parent.Data) < 0)
                parent.Left = newNode;
            else
                parent.Right = newNode;
        }

        // ==========================================
        // SELF-ADJUSTING (ARAMA & YÜKSELME) KISMI
        // ==========================================

        public void SearchAndAdjust(string key)
        {
            Console.WriteLine($"\n>> Arama Yapılıyor: '{key}'");
            Node node = FindNode(root, key);

            if (node == null)
            {
                Console.WriteLine("   Bulunamadı!");
                return;
            }

            // 1. Frekansı Artır
            node.Frequency++;
            Console.WriteLine($"   '{key}' bulundu. Yeni Frekans: {node.Frequency}");

            // 2. Kendini Ayarla (Splay/Priority Mantığı)
            // Eğer frekansım Parent'tan büyükse, yukarı tırmanmalıyım.
            AdjustTree(node);
        }

        private Node FindNode(Node node, string key)
        {
            if (node == null || node.Data == key)
                return node;

            if (String.Compare(key, node.Data) < 0)
                return FindNode(node.Left, key);
            
            return FindNode(node.Right, key);
        }

        private void AdjustTree(Node node)
        {
            // Root olana kadar veya Parent'ın frekansını geçemeyene kadar dön
            while (node.Parent != null && node.Frequency > node.Parent.Frequency)
            {
                Console.WriteLine($"   Logic: {node.Data}({node.Frequency}) > Parent {node.Parent.Data}({node.Parent.Frequency}) -> Yukarı Döndürülüyor...");
                
                if (node == node.Parent.Left)
                {
                    RotateRight(node.Parent);
                }
                else
                {
                    RotateLeft(node.Parent);
                }
            }
        }

        // Sağa Döndürme (AVL mantığı ile aynı, ama Parent pointerları güncelleyerek)
        private void RotateRight(Node y)
        {
            Node x = y.Left;
            y.Left = x.Right;

            if (x.Right != null)
                x.Right.Parent = y;

            x.Parent = y.Parent;

            if (y.Parent == null)
                root = x;
            else if (y == y.Parent.Right)
                y.Parent.Right = x;
            else
                y.Parent.Left = x;

            x.Right = y;
            y.Parent = x;
        }

        // Sola Döndürme
        private void RotateLeft(Node x)
        {
            Node y = x.Right;
            x.Right = y.Left;

            if (y.Left != null)
                y.Left.Parent = x;

            y.Parent = x.Parent;

            if (x.Parent == null)
                root = y;
            else if (x == x.Parent.Left)
                x.Parent.Left = y;
            else
                x.Parent.Right = y;

            y.Left = x;
            x.Parent = y;
        }

        // ==========================================
        // GÖRSELLEŞTİRME
        // ==========================================
        public void PrintTree()
        {
            Console.WriteLine("   Güncel Ağaç Yapısı (Data [Freq]):");
            PrintTreeRec(root, "", true);
        }

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
                // Veriyi ve Frekansını yazdır
                Console.WriteLine($"{node.Data} [{node.Frequency}]");

                PrintTreeRec(node.Left, indent, false);
                PrintTreeRec(node.Right, indent, true);
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            FrequencyTree tree = new FrequencyTree();
            string[] inputs = { "S", "E", "L", "İ", "M", "K", "A", "Ç", "T", "I" };

            // 1. Ağacı Kur
            foreach (var s in inputs) tree.Add(s);
            
            Console.WriteLine("--- BAŞLANGIÇ AĞACI (Frekanslar 0) ---");
            tree.PrintTree();

            // 2. Senaryo: 'K' harfine çok sık erişelim.
            // 'K' normalde ağacın altlarında bir yerdedir.
            tree.SearchAndAdjust("K"); // Frekans 1
            tree.PrintTree();

            tree.SearchAndAdjust("K"); // Frekans 2 -> Yukarı çıkmaya başlamalı
            tree.PrintTree();

            tree.SearchAndAdjust("K"); // Frekans 3 -> Muhtemelen Root'a veya yakınına yerleşir
            tree.PrintTree();

            // 3. Senaryo: 'A' harfine biraz erişelim
            tree.SearchAndAdjust("A");
            tree.SearchAndAdjust("A");
            tree.PrintTree(); // A yükselir ama K'yi (Frekans 3) geçemezse altında kalır.

            Console.ReadKey();
        }
    }
}
