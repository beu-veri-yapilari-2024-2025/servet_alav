using System;
using System.Collections.Generic;

namespace BinaryTreeOdev
{
    class Node
    {
        public int data;
        public Node left, right;

        public Node(int item)
        {
            data = item;
            left = right = null;
        }
    }


    class BinaryTree
    {
        public Node root;

        public BinaryTree()
        {
            root = null;
        }


        public void Insert(int key)
        {
            root = InsertRec(root, key);
        }

        private Node InsertRec(Node root, int key)
        {
       
            if (root == null)
            {
                root = new Node(key);
                return root;
            }

            
            if (key < root.data)
                root.left = InsertRec(root.left, key);
            else if (key > root.data)
                root.right = InsertRec(root.right, key);

            return root;
        }

        
        public void PrintPreorder(Node node)
        {
            if (node == null) return;

            Console.Write(node.data + " ");
            PrintPreorder(node.left);       
            PrintPreorder(node.right);     
        }

        public void PrintInorder(Node node)
        {
            if (node == null) return;

            PrintInorder(node.left);        
            Console.Write(node.data + " "); 
            PrintInorder(node.right);       
        }

      
        public void PrintPostorder(Node node)
        {
            if (node == null) return;

            PrintPostorder(node.left);      
            PrintPostorder(node.right);    
            Console.Write(node.data + " ");
        }

        
        public void PrintLevelOrder()
        {
            if (root == null) return;

            
            Queue<Node> queue = new Queue<Node>();
            queue.Enqueue(root);

            while (queue.Count > 0)
            {
                
                Node tempNode = queue.Dequeue();
                Console.Write(tempNode.data + " ");

                
                if (tempNode.left != null)
                    queue.Enqueue(tempNode.left);

                
                if (tempNode.right != null)
                    queue.Enqueue(tempNode.right);
            }
        }
    }

  
    class Program
    {
        static void Main(string[] args)
        {
            BinaryTree tree = new BinaryTree();

            Console.WriteLine("--- İKİLİ ARAMA AĞACI OLUŞTURUCU ---");
            Console.WriteLine("Örnekteki ağacı oluşturmak için sırasıyla şu sayıları giriniz: 10, 6, 15, 3, 8, 20");
            Console.WriteLine("Veri girişini bitirmek için '0' veya negatif bir sayı giriniz.\n");

            
            while (true)
            {
                Console.Write("Eklenecek sayıyı girin: ");
                try
                {
                    int input = Convert.ToInt32(Console.ReadLine());

                    
                    if (input <= 0) break;

                    tree.Insert(input);
                }
                catch (FormatException)
                {
                    Console.WriteLine("Lütfen geçerli bir tam sayı giriniz.");
                }
            }

            Console.WriteLine("\n--- DOLAŞMA SONUÇLARI ---");

            Console.Write("Preorder (Kök-Sol-Sağ) : ");
            tree.PrintPreorder(tree.root);
            Console.WriteLine();

            Console.Write("Inorder (Sol-Kök-Sağ)  : ");
            tree.PrintInorder(tree.root);
            Console.WriteLine();

            Console.Write("Postorder (Sol-Sağ-Kök): ");
            tree.PrintPostorder(tree.root);
            Console.WriteLine();

            Console.Write("Level-order (Satır Satır): ");
            tree.PrintLevelOrder();
            Console.WriteLine();

            Console.WriteLine("\nProgram sonlandı. Çıkmak için bir tuşa basın...");
            Console.ReadKey();
        }
    }
}
