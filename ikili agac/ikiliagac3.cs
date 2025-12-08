using System;
using System.Collections.Generic;

// 1. ADIM: Oyuncu Düğümü (Node) Yapısı
// Her düğüm bir oyuncuyu temsil eder.
public class Oyuncu
{
    public int FormaNo { get; set; }      // BST için Anahtar (Key)
    public string Ad { get; set; }
    public string Soyad { get; set; }
    
    public Oyuncu Sol { get; set; }       // Sol çocuk (Daha küçük forma numaraları)
    public Oyuncu Sag { get; set; }       // Sağ çocuk (Daha büyük forma numaraları)

    public Oyuncu(int formaNo, string ad, string soyad)
    {
        FormaNo = formaNo;
        Ad = ad;
        Soyad = soyad;
        Sol = null;
        Sag = null;
    }
}

// 2. ADIM: Takım Ağacı (BST) Yapısı
public class TakimBST
{
    public Oyuncu Kok { get; private set; }

    public TakimBST()
    {
        Kok = null;
    }

    // --- EKLEME (INSERT) ---
    // Not: "Kaleci kök olmalıdır" kuralı, bu metodu ilk kez çağırırken
    // kaleciyi ekleyerek main kısmında sağlanır.
    public void Ekle(int formaNo, string ad, string soyad)
    {
        Kok = EkleRekursif(Kok, formaNo, ad, soyad);
    }

    private Oyuncu EkleRekursif(Oyuncu aktifDugum, int formaNo, string ad, string soyad)
    {
        // Eğer yer boşsa yeni oyuncuyu buraya ekle
        if (aktifDugum == null)
        {
            return new Oyuncu(formaNo, ad, soyad);
        }

        // Forma numarasına göre yönlendirme
        if (formaNo < aktifDugum.FormaNo)
        {
            aktifDugum.Sol = EkleRekursif(aktifDugum.Sol, formaNo, ad, soyad);
        }
        else if (formaNo > aktifDugum.FormaNo)
        {
            aktifDugum.Sag = EkleRekursif(aktifDugum.Sag, formaNo, ad, soyad);
        }
        else
        {
            // Aynı forma numarası eklenemez kuralı (Opsiyonel kontrol)
            Console.WriteLine($"Hata: {formaNo} numaralı forma zaten dolu!");
        }

        return aktifDugum;
    }

    // --- ARAMA (SEARCH) ---
    public Oyuncu Ara(int formaNo)
    {
        return AraRekursif(Kok, formaNo);
    }

    private Oyuncu AraRekursif(Oyuncu aktifDugum, int formaNo)
    {
        if (aktifDugum == null || aktifDugum.FormaNo == formaNo)
        {
            return aktifDugum;
        }

        if (formaNo < aktifDugum.FormaNo)
            return AraRekursif(aktifDugum.Sol, formaNo);
            
        return AraRekursif(aktifDugum.Sag, formaNo);
    }

    // --- SİLME (DELETE) ---
    public void Sil(int formaNo)
    {
        Kok = SilRekursif(Kok, formaNo);
    }

    private Oyuncu SilRekursif(Oyuncu kok, int formaNo)
    {
        if (kok == null) return kok;

        // 1. Silinecek düğümü bulma aşaması
        if (formaNo < kok.FormaNo)
        {
            kok.Sol = SilRekursif(kok.Sol, formaNo);
        }
        else if (formaNo > kok.FormaNo)
        {
            kok.Sag = SilRekursif(kok.Sag, formaNo);
        }
        else
        {
            // Düğüm bulundu. Şimdi silme senaryoları:
            
            // Durum 1: Yaprak düğüm veya tek çocuklu düğüm
            if (kok.Sol == null) return kok.Sag;
            if (kok.Sag == null) return kok.Sol;

            // Durum 2: İki çocuklu düğüm
            // Sağ alt ağacın en küçüğünü (successor) buluyoruz.
            Oyuncu yedek = MinDegerBul(kok.Sag);

            // Bilgileri kopyala (Sadece forma no değil, isimleri de güncellemeliyiz)
            kok.FormaNo = yedek.FormaNo;
            kok.Ad = yedek.Ad;
            kok.Soyad = yedek.Soyad;

            // Kopyaladığımız yedeği eski yerinden siliyoruz
            kok.Sag = SilRekursif(kok.Sag, yedek.FormaNo);
        }
        return kok;
    }

    // --- MIN / MAX BULMA ---
    // En küçük forma numaralı oyuncu (Ağacın en solu)
    public Oyuncu EnKucukBul()
    {
        return MinDegerBul(Kok);
    }

    private Oyuncu MinDegerBul(Oyuncu dugum)
    {
        if (dugum == null) return null;
        
        Oyuncu mevcut = dugum;
        while (mevcut.Sol != null)
        {
            mevcut = mevcut.Sol;
        }
        return mevcut;
    }

    // En büyük forma numaralı oyuncu (Ağacın en sağı)
    public Oyuncu EnBuyukBul()
    {
        if (Kok == null) return null;
        Oyuncu mevcut = Kok;
        while (mevcut.Sag != null)
        {
            mevcut = mevcut.Sag;
        }
        return mevcut;
    }

    // --- DOLAŞIM (TRAVERSAL) YÖNTEMLERİ ---

    // 1. PreOrder (Önce Kök): Kök -> Sol -> Sağ
    public void PreOrder(Oyuncu dugum)
    {
        if (dugum != null)
        {
            Yazdir(dugum);
            PreOrder(dugum.Sol);
            PreOrder(dugum.Sag);
        }
    }

    // 2. InOrder (Ortada Kök): Sol -> Kök -> Sağ (Küçükten Büyüğe Sıralar)
    public void InOrder(Oyuncu dugum)
    {
        if (dugum != null)
        {
            InOrder(dugum.Sol);
            Yazdir(dugum);
            InOrder(dugum.Sag);
        }
    }

    // 3. PostOrder (Sonda Kök): Sol -> Sağ -> Kök
    public void PostOrder(Oyuncu dugum)
    {
        if (dugum != null)
        {
            PostOrder(dugum.Sol);
            PostOrder(dugum.Sag);
            Yazdir(dugum);
        }
    }

    // 4. Level Order (Breadth-First): Seviye Seviye Dolaşım
    public void LevelOrder()
    {
        if (Kok == null) return;

        Queue<Oyuncu> kuyruk = new Queue<Oyuncu>();
        kuyruk.Enqueue(Kok);

        while (kuyruk.Count > 0)
        {
            Oyuncu aktif = kuyruk.Dequeue();
            Yazdir(aktif);

            if (aktif.Sol != null) kuyruk.Enqueue(aktif.Sol);
            if (aktif.Sag != null) kuyruk.Enqueue(aktif.Sag);
        }
    }

    // Yardımcı yazdırma metodu
    private void Yazdir(Oyuncu o)
    {
        Console.WriteLine($"[{o.FormaNo}] {o.Ad} {o.Soyad}");
    }
}

// 3. ADIM: Program Testi (Main)
class Program
{
    static void Main(string[] args)
    {
        TakimBST takim = new TakimBST();

        Console.WriteLine("--- Takım Oluşturuluyor ---");
        
        // KURAL: İlk eklenen kök olur. Kaleciyi (Muslera) ilk ekliyoruz.
        // Forma numarası 1 olduğu için değil, ilk eklendiği için kök olur.
        // Ancak dengeli bir ağaç örneği görmek için kökü ortalama bir numara seçmek daha iyidir.
        // Sorudaki "Kaleci kök olacak" kuralına sadık kalarak 1 numarayı ekliyoruz.
        // Not: 1 numara kök olunca diğer herkes sağ tarafa yığılacaktır (Sağ-çarpık ağaç).
        // Örnekte ağacın dallanmasını görebilmek için Kaleciye '25' numara verelim,
        // ya da normal oyuncuları ekleyelim.
        
        // Senaryo Gereği: Kaleci 1 numara ve KÖK olmak zorunda.
        takim.Ekle(1, "Fernando", "Muslera"); // KÖK (Root)

        // Diğer oyuncular (Karışık sırayla ekliyoruz ki ağaç yapısı oluşsun)
        takim.Ekle(10, "Dries", "Mertens");
        takim.Ekle(4, "Serdar", "Aziz");
        takim.Ekle(7, "Kerem", "Aktürkoğlu");
        takim.Ekle(53, "Barış", "Yılmaz");
        takim.Ekle(9, "Mauro", "Icardi");
        takim.Ekle(3, "Angelino", "Tasende");

        Console.WriteLine("\n--- 1. PreOrder Dolaşım (Kök-Sol-Sağ) ---");
        takim.PreOrder(takim.Kok);

        Console.WriteLine("\n--- 2. InOrder Dolaşım (Küçükten Büyüğe Sıralı) ---");
        takim.InOrder(takim.Kok);

        Console.WriteLine("\n--- 3. PostOrder Dolaşım (Sol-Sağ-Kök) ---");
        takim.PostOrder(takim.Kok);

        Console.WriteLine("\n--- 4. Level Order Dolaşım (Seviye Seviye) ---");
        takim.LevelOrder();

        Console.WriteLine("\n--- Arama İşlemi (No: 10) ---");
        Oyuncu aranan = takim.Ara(10);
        if (aranan != null)
            Console.WriteLine($"Bulundu: {aranan.Ad} {aranan.Soyad}");
        else
            Console.WriteLine("Oyuncu bulunamadı.");

        Console.WriteLine("\n--- Min ve Max Forma Numaraları ---");
        Console.WriteLine($"En Küçük Forma No: {takim.EnKucukBul().FormaNo} ({takim.EnKucukBul().Ad})");
        Console.WriteLine($"En Büyük Forma No: {takim.EnBuyukBul().FormaNo} ({takim.EnBuyukBul().Ad})");

        Console.WriteLine("\n--- Silme İşlemi (No: 10 - Mertens Siliniyor) ---");
        takim.Sil(10);
        Console.WriteLine("Silme sonrası InOrder Liste:");
        takim.InOrder(takim.Kok);
        
        Console.ReadLine();
    }
}
