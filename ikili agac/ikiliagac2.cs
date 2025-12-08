using System;
using System.Collections.Generic;

namespace BinarySearchTreeApp
{
    // 1. Düğüm Sınıfı
    public class Node
    {
        public int Data;
        public Node Left, Right;

        public Node(int item)
        {
            Data = item;
            Left = Right = null;
        }
    }

    // 2. BST Sınıfı
    public class BST
    {
        public Node Root;

        public BST() { Root = null; }

        // --- EKLEME (Insert) ---
        public void Insert(int key) { Root = InsertRec(Root, key); }
        private Node InsertRec(Node root, int key)
        {
            if (root == null) return new Node(key);
            if (key < root.Data) root.Left = InsertRec(root.Left, key);
            else if (key > root.Data) root.Right = InsertRec(root.Right, key);
            return root;
        }

        // --- ARAMA (Search) ---
        public bool Search(int key) { return SearchRec(Root, key) != null; }
        private Node SearchRec(Node root, int key)
        {
            if (root == null || root.Data == key) return root;
            if (root.Data < key) return SearchRec(root.Right, key);
            return SearchRec(root.Left, key);
        }

        // --- SİLME (Delete) ---
        public void Delete(int key) { Root = DeleteRec(Root, key); }
        private Node DeleteRec(Node root, int key)
        {
            if (root == null) return root;

            if (key < root.Data) root.Left = DeleteRec(root.Left, key);
            else if (key > root.Data) root.Right = DeleteRec(root.Right, key);
            else
            {
                // Tek çocuklu veya çocuksuz düğümler
                if (root.Left == null) return root.Right;
                else if (root.Right == null) return root.Left;

                // İki çocuklu düğüm: Sağ alt ağacın en küçüğünü bul
                root.Data = MinValue(root.Right);
                root.Right = DeleteRec(root.Right, root.Data);
            }
            return root;
        }

        private int MinValue(Node root)
        {
            int minv = root.Data;
            while (root.Left != null) { minv = root.Left.Data; root = root.Left; }
            return minv;
        }

        // --- DOLAŞIM YÖNTEMLERİ (Traversals) ---

        // Pre-order (Root-Left-Right)
        public void PreOrder(Node node)
        {
            if (node == null) return;
            Console.Write(node.Data + " ");
            PreOrder(node.Left);
            PreOrder(node.Right);
        }

        // In-order (Left-Root-Right) - Küçükten büyüğe sıralar
        public void InOrder(Node node)
        {
            if (node == null) return;
            InOrder(node.Left);
            Console.Write(node.Data + " ");
            InOrder(node.Right);
        }

        // Post-order (Left-Right-Root)
        public void PostOrder(Node node)
        {
            if (node == null) return;
            PostOrder(node.Left);
            PostOrder(node.Right);
            Console.Write(node.Data + " ");
        }

        // Level-order (BFS - Genişlik Öncelikli Dolaşım)
        public void LevelOrder()
        {
            if (Root == null) return;
            Queue<Node> queue = new Queue<Node>();
            queue.Enqueue(Root);
            while (queue.Count > 0)
            {
                Node temp = queue.Dequeue();
                Console.Write(temp.Data + " ");
                if (temp.Left != null) queue.Enqueue(temp.Left);
                if (temp.Right != null) queue.Enqueue(temp.Right);
            }
        }
    }

    // 3. Ana Program
    class Program
    {
        static void Main(string[] args)
        {
            BST tree = new BST();

            // Eleman ekleme
            int[] values = { 50, 30, 20, 40, 70, 60, 80 };
            foreach (int v in values) tree.Insert(v);

            Console.WriteLine("İkili Arama Ağacı Oluşturuldu (Kök: 50)");
            Console.WriteLine("---------------------------------------");

            Console.Write("Pre-order:   "); tree.PreOrder(tree.Root); Console.WriteLine();
            Console.Write("In-order:    "); tree.InOrder(tree.Root); Console.WriteLine();
            Console.Write("Post-order:  "); tree.PostOrder(tree.Root); Console.WriteLine();
            Console.Write("Level-order: "); tree.LevelOrder(); Console.WriteLine();

            Console.WriteLine("\n70 değeri aranıyor: " + (tree.Search(70) ? "Bulundu" : "Bulunamadı"));

            Console.WriteLine("\n20 (yaprak) ve 30 (tek çocuklu) siliniyor...");
            tree.Delete(20);
            tree.Delete(30);

            Console.Write("Yeni In-order: "); tree.InOrder(tree.Root);
            Console.WriteLine();

            Console.ReadLine();
        }
    }
}
