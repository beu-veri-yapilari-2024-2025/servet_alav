using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hanoi_kuleleri_odev
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    // Her bir iğneyi (kuleyi) temsil eden sınıf
    public class Igne
    {
        // Diskleri tutmak için Stack (Yığın) kullanıyoruz. 
        // Stack, LIFO (Son Giren İlk Çıkar) prensibiyle çalışır, 
        // bu da bir iğnenin en üstündeki diski temsil eder.
        public Stack<int> Diskler { get; private set; }
        public char Adi { get; private set; }

        public Igne(char adi)
        {
            Adi = adi;
            Diskler = new Stack<int>();
        }
    }

    public class HanoiKuleleriOyun
    {
        private List<Igne> kuleler;
        private int toplamDiskSayisi;
        private int hamleSayisi;

        public HanoiKuleleriOyun(int diskSayisi)
        {
            // Türkçe karakter desteği
            Console.OutputEncoding = Encoding.UTF8;

            toplamDiskSayisi = diskSayisi;
            hamleSayisi = 0;
            kuleler = new List<Igne>
        {
            new Igne('A'), // Kaynak
            new Igne('B'), // Yardımcı
            new Igne('C')  // Hedef
        };

            // Başlangıçta tüm diskleri A kulesine yerleştir (En büyük altta)
            for (int i = toplamDiskSayisi; i >= 1; i--)
            {
                kuleler[0].Diskler.Push(i);
            }
        }

        // Oyunun mevcut durumunu konsola çizen metod
        public void DurumuCiz()
        {
            Console.Clear();
            Console.WriteLine("------------------------------------------------------------------");
            
            

            int maksimumGenislik = toplamDiskSayisi * 2 + 1; // Disk görseli için maksimum genişlik

            // Diskin görsel karşılığını oluşturur (Örn: Disk 3 -> '===*===')
            string DiskGorunumu(int boyut)
            {
                if (boyut == 0) return new string(' ', maksimumGenislik);

                int cizgiSayisi = boyut;
                int boslukSayisi = (maksimumGenislik - cizgiSayisi) / 2;

                return new string(' ', boslukSayisi) + new string('█', cizgiSayisi) + new string(' ', boslukSayisi);
            }

            // Kulelerin en altından en üstüne doğru çizilmesi için yükseklikleri hesapla
            for (int satir = toplamDiskSayisi - 1; satir >= 0; satir--)
            {
                Console.Write("|");
                foreach (var kule in kuleler)
                {
                    // Mevcut satırdaki disk boyutunu al
                    int diskBoyut = 0;
                    if (kule.Diskler.Count > satir)
                    {
                        // Dizilerdeki gibi, alttan yukarı doğru disk boyutunu alıyoruz.
                        // Stack'i tersine çevirip belirli bir indekse erişmek karmaşık, bu yüzden ToArray kullanıyoruz.
                        int[] diskDizisi = kule.Diskler.ToArray();
                        diskBoyut = diskDizisi[diskDizisi.Length - 1 - satir]; // En alttaki disk 0. indexe denk gelir.
                    }

                    // Diski çiz
                    Console.Write(DiskGorunumu(diskBoyut));
                    Console.Write("|");
                }
                Console.WriteLine();
            }

            // Kulelerin tabanını çiz
            Console.WriteLine("+-------------------------+-------------------------+-------------------------+");
            Console.WriteLine($"|{new string(' ', (maksimumGenislik - 1) / 2)}A{new string(' ', (maksimumGenislik - 1) / 2)}|{new string(' ', (maksimumGenislik - 1) / 2)}B{new string(' ', (maksimumGenislik - 1) / 2)}|{new string(' ', (maksimumGenislik - 1) / 2)}C{new string(' ', (maksimumGenislik - 1) / 2)}|");
            Console.WriteLine("+-------------------------+-------------------------+-------------------------+");
        }

        // Hamle kurallara uygunsa diski hareket ettirir
        public bool HamleYap(char kaynakAdi, char hedefAdi)
        {
            Igne kaynak = kuleler.Find(k => k.Adi == kaynakAdi);
            Igne hedef = kuleler.Find(k => k.Adi == hedefAdi);

            // İğne kontrolü
            if (kaynak == null || hedef == null)
            {
                Console.WriteLine("\nHata: Geçersiz iğne adı. Sadece A, B veya C kullanın.");
                return false;
            }

            // Kaynak iğne boş mu?
            if (kaynak.Diskler.Count == 0)
            {
                Console.WriteLine($"\nHata: {kaynak.Adi} iğnesi boş. Taşınacak disk yok.");
                return false;
            }

            int tasinacakDisk = kaynak.Diskler.Peek();

            // Kural Kontrolü: Büyük disk küçük diskin üzerine konamaz
            if (hedef.Diskler.Count > 0)
            {
                int usttekiDisk = hedef.Diskler.Peek();
                if (tasinacakDisk > usttekiDisk)
                {
                    Console.WriteLine("\nHata: Büyük disk (Boyut: {0}), küçük diskin (Boyut: {1}) üzerine konulamaz.", tasinacakDisk, usttekiDisk);
                    return false;
                }
            }

            // Kurallara uygun, taşıma işlemini yap
            kaynak.Diskler.Pop();
            hedef.Diskler.Push(tasinacakDisk);
            hamleSayisi++;
            return true;
        }

        // Oyunun kazanılıp kazanılmadığını kontrol eder
        public bool OyunBittiMi()
        {
            // Oyun, tüm diskler hedef (C) kulesine taşındığında biter.
            return kuleler[2].Diskler.Count == toplamDiskSayisi;
        }

        public void OyunuBaslat()
        {
            while (!OyunBittiMi())
            {
                DurumuCiz();

                Console.WriteLine("\n[Kaynak] ve [Hedef] iğne harflerini girin (Örn: AC)");
                Console.Write("Hamle: ");
                string hamleGirdisi = Console.ReadLine().ToUpper();

                if (hamleGirdisi.Length == 2)
                {
                    char kaynak = hamleGirdisi[0];
                    char hedef = hamleGirdisi[1];

                    HamleYap(kaynak, hedef);
                }
                else
                {
                    Console.WriteLine("\nHata: Giriş formatı hatalı. Lütfen iki harf girin (Örn: AB).");
                }
                Console.WriteLine("\nDevam etmek için bir tuşa basın...");
                Console.ReadKey();
            }

            // Oyun Bitti
            DurumuCiz();
            Console.WriteLine("\n******************************************************************");
            Console.WriteLine($"Tebrikler! Oyunu {hamleSayisi} hamlede tamamladınız!");
            int minHamle = (int)Math.Pow(2, toplamDiskSayisi) - 1;
            Console.WriteLine($"Minimum gerekli hamle sayısı: {minHamle}");
            if (hamleSayisi == minHamle)
            {
                Console.WriteLine("Mükemmel! En kısa yoldan çözdünüz!");
            }
            Console.WriteLine("******************************************************************");
            Console.ReadKey();
        }
    }

    public class Program
    {
        public static void Main(string[] args)
        {
            int diskAdedi = 0;
            bool gecerliGirdi = false;

            // Kullanıcıdan disk sayısını alma
            while (!gecerliGirdi)
            {
                Console.Clear();
                
                Console.WriteLine("------------------------------------------\n");
                Console.Write("Lütfen disk sayısını girin: ");
                string girdi = Console.ReadLine();

                if (int.TryParse(girdi, out diskAdedi) && diskAdedi >= 3)
                {
                    gecerliGirdi = true;
                }
                else
                {
                    Console.WriteLine("\nHata: Lütfen 3 veya daha büyük bir tam sayı girin.");
                    Console.ReadKey();
                }
            }

            HanoiKuleleriOyun oyun = new HanoiKuleleriOyun(diskAdedi);
            oyun.OyunuBaslat();
        }
    }
}
