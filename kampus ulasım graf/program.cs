using System;
using System.Collections.Generic;
using System.Linq;

namespace KampusUlasimSistemi
{
    // --- 1. VERİ YAPILARI ---

    // Kenar sınıfı: Gidilecek bina ve mesafeyi tutar
    public class Kenar
    {
        public string Hedef { get; set; }
        public int Sure { get; set; }

        public Kenar(string hedef, int sure)
        {
            Hedef = hedef;
            Sure = sure;
        }
    }

    // Graf sınıfı: Tüm algoritmaları ve veri yapısını barındırır
    public class KampusGrafi
    {
        private Dictionary<string, List<Kenar>> komsulukListesi;

        public KampusGrafi()
        {
            komsulukListesi = new Dictionary<string, List<Kenar>>();
        }

        // Grafı oluşturma (Yönsüz - Çift taraflı ekleme)
        public void KenarEkle(string baslangic, string bitis, int sure)
        {
            if (!komsulukListesi.ContainsKey(baslangic)) komsulukListesi[baslangic] = new List<Kenar>();
            if (!komsulukListesi.ContainsKey(bitis)) komsulukListesi[bitis] = new List<Kenar>();

            komsulukListesi[baslangic].Add(new Kenar(bitis, sure));
            komsulukListesi[bitis].Add(new Kenar(baslangic, sure));
        }

        // Graf yapısını ekrana yazdırma
        public void GrafiYazdir()
        {
            Console.WriteLine("\n--- Mevcut Kampüs Ağı ---");
            foreach (var dugum in komsulukListesi)
            {
                Console.Write($"[{dugum.Key}] -> ");
                foreach (var kenar in dugum.Value)
                {
                    Console.Write($"{kenar.Hedef} ({kenar.Sure}dk) ");
                }
                Console.WriteLine();
            }
        }

        // --- 2. BFS ALGORİTMASI (Genişlik Öncelikli Arama) ---
        public void BFS(string baslangic)
        {
            if (!komsulukListesi.ContainsKey(baslangic)) { Console.WriteLine("Hata: Başlangıç düğümü bulunamadı."); return; }

            HashSet<string> ziyaretEdilenler = new HashSet<string>();
            Queue<string> kuyruk = new Queue<string>();

            ziyaretEdilenler.Add(baslangic);
            kuyruk.Enqueue(baslangic);

            Console.Write($"\nBFS ({baslangic} çıkışlı): ");

            while (kuyruk.Count > 0)
            {
                string mevcut = kuyruk.Dequeue();
                Console.Write(mevcut + " ");

                foreach (var kenar in komsulukListesi[mevcut])
                {
                    if (!ziyaretEdilenler.Contains(kenar.Hedef))
                    {
                        ziyaretEdilenler.Add(kenar.Hedef);
                        kuyruk.Enqueue(kenar.Hedef);
                    }
                }
            }
            Console.WriteLine();
        }

        // --- 3. DFS ALGORİTMASI (Derinlik Öncelikli Arama - Recursive) ---
        public void DFS(string baslangic)
        {
            if (!komsulukListesi.ContainsKey(baslangic)) { Console.WriteLine("Hata: Başlangıç düğümü bulunamadı."); return; }

            HashSet<string> ziyaretEdilenler = new HashSet<string>();
            Console.Write($"\nDFS ({baslangic} çıkışlı): ");
            DFS_Yardimci(baslangic, ziyaretEdilenler);
            Console.WriteLine();
        }

        private void DFS_Yardimci(string mevcut, HashSet<string> ziyaretEdilenler)
        {
            ziyaretEdilenler.Add(mevcut);
            Console.Write(mevcut + " ");

            if (komsulukListesi.ContainsKey(mevcut))
            {
                foreach (var kenar in komsulukListesi[mevcut])
                {
                    if (!ziyaretEdilenler.Contains(kenar.Hedef))
                    {
                        DFS_Yardimci(kenar.Hedef, ziyaretEdilenler);
                    }
                }
            }
        }

        // --- 4. DIJKSTRA ALGORİTMASI (En Kısa Yol) ---
        public void EnKisaYolBul(string baslangic, string bitis)
        {
            if (!komsulukListesi.ContainsKey(baslangic) || !komsulukListesi.ContainsKey(bitis))
            {
                Console.WriteLine("Hata: Başlangıç veya bitiş düğümü grafikte yok.");
                return;
            }

            var mesafeler = new Dictionary<string, int>();
            var oncekiDugum = new Dictionary<string, string>();
            var ziyaretEdilmemis = new List<string>();

            foreach (var dugum in komsulukListesi.Keys)
            {
                mesafeler[dugum] = int.MaxValue;
                oncekiDugum[dugum] = null;
                ziyaretEdilmemis.Add(dugum);
            }

            mesafeler[baslangic] = 0;

            while (ziyaretEdilmemis.Count > 0)
            {
                ziyaretEdilmemis.Sort((x, y) => mesafeler[x].CompareTo(mesafeler[y]));
                string mevcut = ziyaretEdilmemis[0];
                ziyaretEdilmemis.RemoveAt(0);

                if (mevcut == bitis) break;
                if (mesafeler[mevcut] == int.MaxValue) break;

                foreach (var kenar in komsulukListesi[mevcut])
                {
                    int alternatif = mesafeler[mevcut] + kenar.Sure;
                    if (alternatif < mesafeler[kenar.Hedef])
                    {
                        mesafeler[kenar.Hedef] = alternatif;
                        oncekiDugum[kenar.Hedef] = mevcut;
                    }
                }
            }
            SonucuYazdir(baslangic, bitis, mesafeler, oncekiDugum);
        }

        private void SonucuYazdir(string baslangic, string bitis, Dictionary<string, int> mesafeler, Dictionary<string, string> oncekiDugum)
        {
            if (mesafeler[bitis] == int.MaxValue)
            {
                Console.WriteLine($"\n{baslangic} -> {bitis} arası yol yok.");
                return;
            }

            List<string> rota = new List<string>();
            string temp = bitis;
            while (temp != null)
            {
                rota.Add(temp);
                temp = oncekiDugum[temp];
            }
            rota.Reverse();

            Console.WriteLine($"\n--- En Kısa Yol ({baslangic} -> {bitis}) ---");
            Console.WriteLine($"Toplam Süre: {mesafeler[bitis]} dakika");
            Console.WriteLine($"Rota: {string.Join(" -> ", rota)}");
        }
    }

    // --- 5. ANA PROGRAM (MAIN) ---
    class Program
    {
        static void Main(string[] args)
        {
            KampusGrafi graf = new KampusGrafi();

            // Verilerin Yüklenmesi (Senin gönderdiğin tablo)
            graf.KenarEkle("A", "B", 4);
            graf.KenarEkle("A", "C", 2);
            graf.KenarEkle("B", "D", 5);
            graf.KenarEkle("C", "B", 1);
            graf.KenarEkle("C", "E", 7);
            graf.KenarEkle("D", "F", 3);
            graf.KenarEkle("E", "F", 2);
            graf.KenarEkle("B", "E", 6);

            Console.WriteLine("Kampüs Ulaşım Ağı Sistemi Başlatıldı...");

            while (true)
            {
                Console.WriteLine("\n==================================");
                Console.WriteLine("1. Graf Yapısını Gör (Komşuluk Listesi)");
                Console.WriteLine("2. BFS Çalıştır (Sıralı Gezinti)");
                Console.WriteLine("3. DFS Çalıştır (Derinlemesine Gezinti)");
                Console.WriteLine("4. En Kısa Yol Bul (Dijkstra)");
                Console.WriteLine("0. Çıkış");
                Console.WriteLine("==================================");
                Console.Write("Seçiminiz: ");

                string secim = Console.ReadLine();

                switch (secim)
                {
                    case "1":
                        graf.GrafiYazdir();
                        break;
                    case "2":
                        Console.Write("Başlangıç düğümü (örn: A): ");
                        string bfsBaslangic = Console.ReadLine().ToUpper();
                        graf.BFS(bfsBaslangic);
                        break;
                    case "3":
                        Console.Write("Başlangıç düğümü (örn: A): ");
                        string dfsBaslangic = Console.ReadLine().ToUpper();
                        graf.DFS(dfsBaslangic);
                        break;
                    case "4":
                        Console.Write("Başlangıç düğümü (örn: A): ");
                        string bas = Console.ReadLine().ToUpper();
                        Console.Write("Bitiş düğümü (örn: F): ");
                        string bit = Console.ReadLine().ToUpper();
                        graf.EnKisaYolBul(bas, bit);
                        break;
                    case "0":
                        Console.WriteLine("Çıkış yapılıyor...");
                        return;
                    default:
                        Console.WriteLine("Geçersiz seçim, tekrar deneyin.");
                        break;
                }
            }
        }
    }
}
