using System;
using System.Collections.Generic;

// Kitap bilgilerini tutan sınıf
public class Kitap
{
    public string KitapAdi { get; set; }
    public int BasimYili { get; set; }

    public Kitap(string kitapAdi, int basimYili)
    {
        KitapAdi = kitapAdi;
        BasimYili = basimYili;
    }

    // Ekrana yazdırma işlemini kolaylaştırmak için ToString metodunu eziyoruz (override)
    public override string ToString()
    {
        return $"[{BasimYili}] - {KitapAdi}";
    }
}

class Program
{
    static void Main(string[] args)
    {
        // 1. En az 6 kitap içeren bir liste oluşturuyoruz
        List<Kitap> kitaplik = new List<Kitap>
        {
            new Kitap("Sefiller", 1862),
            new Kitap("1984", 1949),
            new Kitap("Simyacı", 1988),
            new Kitap("Suç ve Ceza", 1866),
            new Kitap("Nutuk", 1927),
            new Kitap("Harry Potter ve Felsefe Taşı", 1997),
            new Kitap("Don Kişot", 1605)
        };

        // 2. Sıralamadan önceki listeyi yazdır
        Console.WriteLine("-----------------------------------");
        Console.WriteLine("SIRALAMADAN ÖNCE KİTAP LİSTESİ:");
        Console.WriteLine("-----------------------------------");
        ListeyiYazdir(kitaplik);

        // 3. Merge Sort algoritması ile sıralama işlemi
        kitaplik = MergeSort(kitaplik);

        // 4. Sıralamadan sonraki listeyi yazdır
        Console.WriteLine("\n-----------------------------------");
        Console.WriteLine("SIRALAMADAN SONRA (Basım Yılına Göre):");
        Console.WriteLine("-----------------------------------");
        ListeyiYazdir(kitaplik);

        // Konsolun kapanmasını önlemek için bekleme
        Console.ReadLine();
    }

    // Listeyi ekrana yazdıran yardımcı metot
    static void ListeyiYazdir(List<Kitap> liste)
    {
        foreach (var kitap in liste)
        {
            Console.WriteLine(kitap);
        }
    }

    // --- MERGE SORT ALGORİTMASI ---

    // Ana MergeSort fonksiyonu (Recursive / Özyinelemeli)
    static List<Kitap> MergeSort(List<Kitap> liste)
    {
        // Temel Durum (Base Case): Liste 1 elemanlı veya boşsa zaten sıralıdır.
        if (liste.Count <= 1)
            return liste;

        // Listeyi ortadan ikiye böl
        int ortaNokta = liste.Count / 2;

        List<Kitap> sol = new List<Kitap>();
        List<Kitap> sag = new List<Kitap>();

        // Sol tarafı doldur
        for (int i = 0; i < ortaNokta; i++)
            sol.Add(liste[i]);

        // Sağ tarafı doldur
        for (int i = ortaNokta; i < liste.Count; i++)
            sag.Add(liste[i]);

        // Parçaları recursive olarak tekrar böl ve sırala
        sol = MergeSort(sol);
        sag = MergeSort(sag);

        // Sıralanmış parçaları birleştir (Merge)
        return Birlestir(sol, sag);
    }

    // İki sıralı listeyi birleştiren (Merge) yardımcı fonksiyon
    static List<Kitap> Birlestir(List<Kitap> sol, List<Kitap> sag)
    {
        List<Kitap> sonuc = new List<Kitap>();
        int solIndex = 0;
        int sagIndex = 0;

        // Her iki listede de eleman olduğu sürece karşılaştırma yap
        while (solIndex < sol.Count && sagIndex < sag.Count)
        {
            // Basım yılına göre karşılaştırma (Küçük olan önce eklenir)
            if (sol[solIndex].BasimYili <= sag[sagIndex].BasimYili)
            {
                sonuc.Add(sol[solIndex]);
                solIndex++;
            }
            else
            {
                sonuc.Add(sag[sagIndex]);
                sagIndex++;
            }
        }

        // Sol listede kalan elemanları ekle (varsa)
        while (solIndex < sol.Count)
        {
            sonuc.Add(sol[solIndex]);
            solIndex++;
        }

        // Sağ listede kalan elemanları ekle (varsa)
        while (sagIndex < sag.Count)
        {
            sonuc.Add(sag[sagIndex]);
            sagIndex++;
        }

        return sonuc;
    }
}
