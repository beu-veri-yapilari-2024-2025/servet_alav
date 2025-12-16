using System;
using System.Collections.Generic;

namespace AdvancedTreeApp
{
    // --- DÜĞÜM YAPISI ---
    public class Node
    {
        public string Data;
        public Node Left, Right, Parent;
        public int Height;     // AVL için
        public int Frequency;  // Self-Adjusting için

        public Node(string data)
        {
            Data = data;
            Left = Right = Parent = null;
            Height = 1; // Yeni düğüm yüksekliği 1
            Frequency = 0;
        }
    }

    // --- GELİŞMİŞ AĞAÇ SINIFI ---
    public class AdvancedTree
    {
        public Node Root;

        public AdvancedTree()
        {
            Root = null;
        }

        // ==========================================
        // 1. AVL FONKSİYONLARI
        // ==========================================

        // Yükseklik Al
        public int GetHeight(Node n)
        {
            return (n == null) ? 0 : n.Height;
        }

        // Denge Faktörü Al
        public int GetBalance(Node n)
        {
            return (n == null) ? 0 : GetHeight(n.Left) - GetHeight(n.Right);
        }

        // Sağa Döndürme (Hem AVL hem DSW hem Self-Adjusting kullanır)
        public Node RotateRight(Node y)
        {
            Node x = y.Left;
            Node T2 = x.Right;

            // Dönüş
            x.Right = y;
            y.Left = T2;

            // Parent Güncellemeleri (Self-Adjusting ve DSW için kritik)
            if (T2 != null) T2.Parent = y;
            x.Parent = y.Parent;
            y.Parent = x;

            if (x.Parent != null)
            {
                if (x.Parent.Right == y) x.Parent.Right = x;
                else x.Parent.Left = x;
            }

            // Yükseklik Güncelleme (AVL için)
            y.Height = Math.Max(GetHeight(y.Left), GetHeight(y.Right)) + 1;
            x.Height = Math.Max(GetHeight(x.Left), GetHeight(x.Right)) + 1;

            return x; // Yeni kök
        }

        // Sola Döndürme
        public Node RotateLeft(Node x)
        {
            Node y = x.Right;
            Node T2 = y.Left;

            // Dönüş
            y.Left = x;
            x.Right = T2;

            // Parent Güncellemeleri
            if (T2 != null) T2.Parent = x;
            y.Parent = x.Parent;
            x.Parent = y;

            if (y.Parent != null)
            {
                if (y.Parent.Left == x) y.Parent.Left = y;
                else y.Parent.Right = y;
            }

            // Yükseklik Güncelleme
            x.Height = Math.Max(GetHeight(x.Left), GetHeight(x.Right)) + 1;
            y.Height = Math.Max(GetHeight(y.Left), GetHeight(y.Right)) + 1;

            return y; // Yeni kök
        }

        // AVL Ekleme (Insert)
        public void Insert(string data)
        {
            Root = InsertRec(Root, null, data);
        }

        private Node InsertRec(Node node, Node parent, string data)
        {
            // 1. Standart BST Ekleme
            if (node == null)
            {
                Node newNode = new Node(data);
                newNode.Parent = parent;
                return newNode;
            }

            int compare = String.Compare(data, node.Data);
            if (compare < 0)
                node.Left = InsertRec(node.Left, node, data);
            else if (compare > 0)
                node.Right = InsertRec(node.Right, node, data);
            else
                return node; // Eşit veriye izin yok

            // 2. Yükseklik Güncelle
            node.Height = 1 + Math.Max(GetHeight(node.Left), GetHeight(node.Right));

            // 3. Denge Kontrolü ve Döndürmeler
            int balance = GetBalance(node);

            // Left Left
            if (balance > 1 && String.Compare(data, node.Left.Data) < 0)
                return RotateRight(node);

            // Right Right
            if (balance < -1 && String.Compare(data, node.Right.Data) > 0)
                return RotateLeft(node);

            // Left Right
            if (balance > 1 && String.Compare(data, node.Left.Data) > 0)
            {
                node.Left = RotateLeft(node.Left);
                return RotateRight(node);
            }

            // Right Left
            if (balance < -1 && String.Compare(data, node.Right.Data) < 0)
            {
                node.Right = RotateRight(node.Right);
                return RotateLeft(node);
            }

            return node;
        }

        // --- TRAVERSAL FONKSİYONLARI ---
        public void PrintInOrder(Node node)
        {
            if (node != null) { PrintInOrder(node.Left); Console.Write(node.Data + " "); PrintInOrder(node.Right); }
        }
        public void PrintPreOrder(Node node)
        {
            if (node != null) { Console.Write(node.Data + " "); PrintPreOrder(node.Left); PrintPreOrder(node.Right); }
        }
        public void PrintPostOrder(Node node)
        {
            if (node != null) { PrintPostOrder(node.Left); PrintPostOrder(node.Right); Console.Write(node.Data + " "); }
        }
        public void PrintLevelOrder()
        {
            if (Root == null) return;
            Queue<Node> q = new Queue<Node>();
            q.Enqueue(Root);
            while (q.Count > 0)
            {
                Node temp = q.Dequeue();
                Console.Write(temp.Data + " ");
                if (temp.Left != null) q.Enqueue(temp.Left);
                if (temp.Right != null) q.Enqueue(temp.Right);
            }
        }

        // ==========================================
        // 2. DSW (Day-Stout-Warren) FONKSİYONLARI
        // ==========================================

        public void ApplyDSW()
        {
            CreateBackbone();
            BalanceBackbone();
        }

        public void CreateBackbone()
        {
            // Pseudo-root (grandParent) oluşturulabilir ama burada basitleştirilmiş mantık kullanıyoruz
            // Backbone: Sola dönüşlerle tüm sol çocukları yok et
            Node gp = null;
            Node p = Root;
            
            while (p != null)
            {
                if (p.Left != null)
                {
                    Node child = p.Left;
                    RotateRight(p); // Sağa döndür
                    // Rotasyon sonrası p'nin ne olduğunu güncellemek gerekir
                    // RotateRight fonksiyonumuz yeni kökü döndürür
                    if (gp == null) Root = child; 
                    p = child; // Kontrolü yeni tepeye ver
                }
                else
                {
                    gp = p;
                    p = p.Right;
                }
            }
        }

        public void BalanceBackbone()
        {
            int n = 0;
            Node temp = Root;
            while (temp != null) { n++; temp = temp.Right; }

            int expectedHeight = (int)Math.Log(n + 1, 2);
            int m = n - ((1 << expectedHeight) - 1);

            MakeRotations(m);
            while (n > 1)
            {
                n = n >> 1;
                MakeRotations(n);
            }
        }

        private void MakeRotations(int count)
        {
            Node gp = null;
            Node p = Root;

            for (int i = 0; i < count; i++)
            {
                if (p == null || p.Right == null) return;
                
                Node child = p.Right;
                RotateLeft(p);
                
                if (gp == null) Root = child; // Kök değişti
                
                gp = child;
                p = gp.Right;
            }
        }


        // ==========================================
        // 3. SELF-ADJUSTING (FREQUENCY) FONKSİYONLARI
        // ==========================================

        public void SearchWithFrequency(string key)
        {
            Console.WriteLine($"\n>> Arama: {key}");
            Node found = FindNode(Root, key);
            if (found != null)
            {
                found.Frequency++;
                Console.WriteLine($"   Bulundu. Yeni Frekans: {found.Frequency}");
                AdjustTowardsRoot(found);
            }
            else
            {
                Console.WriteLine("   Bulunamadı.");
            }
        }

        private Node FindNode(Node node, string key)
        {
            if (node == null || node.Data == key) return node;
            if (String.Compare(key, node.Data) < 0) return FindNode(node.Left, key);
            return FindNode(node.Right, key);
        }

        public void AdjustTowardsRoot(Node node)
        {
            // Mantık: Parent'ın frekansından yükseksem, yer değiştir (Priority Rotate)
            while (node.Parent != null && node.Frequency > node.Parent.Frequency)
            {
                Console.WriteLine($"   Priority Rotate: {node.Data}({node.Frequency}) <-> {node.Parent.Data}({node.Parent.Frequency})");
                PriorityRotate(node);
            }
            // Eğer root olduysak güncelle
            if (node.Parent == null) Root = node;
        }

        public void PriorityRotate(Node node)
        {
            Node p = node.Parent;
            if (p == null) return;

            // Eğer node, parent'ın soluysa -> Sağa döndür
            if (node == p.Left)
            {
                // AVL'deki RotateRight fonksiyonu generic yazıldığı için kullanılabilir,
                // ancak Root güncellemesini kontrol etmeliyiz.
                Node newSubRoot = RotateRight(p);
                if (p == Root) Root = newSubRoot;
            }
            // Eğer node, parent'ın sağıysa -> Sola döndür
            else
            {
                Node newSubRoot = RotateLeft(p);
                if (p == Root) Root = newSubRoot;
            }
        }

        // ==========================================
        // GÖRSELLEŞTİRME YARDIMCISI
        // ==========================================
        public void PrintStructure()
        {
            PrintRecursive(Root, "", true);
        }

        private void PrintRecursive(Node node, string indent, bool last)
        {
            if (node != null)
            {
                Console.Write(indent);
                if (last) { Console.Write("└─"); indent += "  "; }
                else { Console.Write("├─"); indent += "| "; }
                Console.WriteLine($"{node.Data} [F:{node.Frequency}]");
                PrintRecursive(node.Left, indent, false);
                PrintRecursive(node.Right, indent, true);
            }
        }
    }

    // --- MAIN PROGRAM ---
    class Program
    {
        static void Main(string[] args)
        {
            AdvancedTree tree = new AdvancedTree();
            string[] inputs = { "S", "E", "L", "İ", "M", "K", "A", "Ç", "T", "I" };

            // 1. ADIM: AVL OLUŞTURMA
            Console.WriteLine("=====================================");
            Console.WriteLine("1. ADIM - AVL AĞACI OLUŞTURULUYOR");
            Console.WriteLine("=====================================");
            foreach (var s in inputs)
            {
                tree.Insert(s);
                Console.Write(s + " ");
            }
            Console.WriteLine("\n\n--- AVL Ağacının Son Hali ---");
            tree.PrintStructure();
            Console.Write("Level Order: "); tree.PrintLevelOrder();
            Console.WriteLine();

            // 2. ADIM: DSW UYGULAMA
            Console.WriteLine("\n=====================================");
            Console.WriteLine("2. ADIM - DSW METODU UYGULANIYOR");
            Console.WriteLine("=====================================");
            Console.WriteLine("Backbone oluşturuluyor ve dengeleniyor...");
            tree.ApplyDSW(); // Önce backbone yapar, sonra dengeler
            
            Console.WriteLine("\n--- DSW Sonrası Dengelenmiş Ağaç ---");
            tree.PrintStructure();

            // 3. ADIM: SELF-ADJUSTING (FREKANS) TESTİ
            Console.WriteLine("\n=====================================");
            Console.WriteLine("3. ADIM - SELF ADJUSTING (Erişim Testi)");
            Console.WriteLine("=====================================");
            
            // Senaryo: 'K' harfine sık erişim yapalım (K'nın yükselmesini bekliyoruz)
            tree.SearchWithFrequency("K"); 
            tree.PrintStructure();

            tree.SearchWithFrequency("K");
            tree.PrintStructure();

            tree.SearchWithFrequency("T"); // Biraz da T'ye erişelim
            tree.PrintStructure();

            // K'ya tekrar erişim (Root'a çıkması muhtemel)
            tree.SearchWithFrequency("K");
            Console.WriteLine("\n--- Son Durum (K en üstte olmalı) ---");
            tree.PrintStructure();

            Console.WriteLine("\nProgram sonlandı. Çıkış için Enter'a basın.");
            Console.ReadLine();
        }
    }
}
