using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace linkedlist_works_3
{
    public class Node
    {
        public string OgrenciNo { get; set; }
        public string DersKodu { get; set; }
        public string HarfOrtalamasi { get; set; }

        // Aynı öğrencinin bir sonraki ders kaydını gösterir.
        public Node NextOgrenciDers { get; set; }

        // Aynı dersteki bir sonraki öğrenci kaydını gösterir.
        public Node NextDersOgrenci { get; set; }

        public Node(string ogrenciNo, string dersKodu, string harfOrtalamasi)
        {
            OgrenciNo = ogrenciNo;
            DersKodu = dersKodu;
            HarfOrtalamasi = harfOrtalamasi;
            NextOgrenciDers = null;
            NextDersOgrenci = null;
        }

        public override string ToString()
        {
            return $"Öğr No: {OgrenciNo}, Ders: {DersKodu}, Ortalama: {HarfOrtalamasi}";
        }
    }
    public class ogrenciHeader
    {
        public string OgrenciNo { get; set; }

        // Öğrencinin aldığı ilk ders kaydına işaret eder.
        public Node ilkders { get; set; }

        // Bir sonraki öğrenci başlık düğümüne işaret eder.
        public ogrenciHeader sonrakiogrenci { get; set; }

        public ogrenciHeader(string ogrenciNo)
        {
            OgrenciNo = ogrenciNo;
            ilkders = null;
            sonrakiogrenci = null;
        }
    }
    public class dersHeader
    {
        public string DersKodu { get; set; }

        // Dersi alan ilk öğrenci kaydına işaret eder.
        public Node FirstStudent { get; set; }

        // Bir sonraki ders başlık düğümüne işaret eder.
        public dersHeader NextCourse { get; set; }

        public dersHeader(string dersKodu)
        {
            DersKodu = dersKodu;
            FirstStudent = null;
            NextCourse = null;
        }
    }


public class Manager
    {
        public ogrenciHeader StudentHead { get; private set; }
        public dersHeader CourseHead { get; private set; }

        public Manager()
        {
            StudentHead = null;
            CourseHead = null;
        }

        // Yardımcı Metodlar (Başlık Düğümü Bulma veya Oluşturma)

        private ogrenciHeader FindOrCreateStudent(string ogrenciNo)
        {
            ogrenciHeader current = StudentHead;
            ogrenciHeader last = null;

            while (current != null)
            {
                if (current.OgrenciNo == ogrenciNo)
                    return current; // Öğrenci bulundu

                last = current;
                current = current.sonrakiogrenci;
            }

            // Öğrenci bulunamadı, yeni bir başlık düğümü oluştur ve sona ekle
            ogrenciHeader newHeader = new ogrenciHeader(ogrenciNo);
            if (last == null)
                StudentHead = newHeader; // Liste boştu
            else
                last.sonrakiogrenci = newHeader;

            return newHeader;
        }

        private dersHeader FindOrCreateCourse(string dersKodu)
        {
            dersHeader current = CourseHead;
            dersHeader last = null;

            while (current != null)
            {
                if (current.DersKodu == dersKodu)
                    return current; // Ders bulundu

                last = current;
                current = current.NextCourse;
            }

            // Ders bulunamadı, yeni bir başlık düğümü oluştur ve sona ekle
            dersHeader newHeader = new dersHeader(dersKodu);
            if (last == null)
                CourseHead = newHeader; // Liste boştu
            else
                last.NextCourse = newHeader;

            return newHeader;
        }


        // 1 & 2. BİR ÖĞRENCİYE YENİ BİR DERS VEYA BİR DERSE YENİ BİR ÖĞRENCİ EKLEME (Tek Metot)
        public bool AddGrade(string ogrenciNo, string dersKodu, string harfOrtalamasi)
        {
            // 1. Yeni Not Düğümünü Oluştur
            Node newGrade = new Node(ogrenciNo, dersKodu, harfOrtalamasi);

            // 2. İlgili Öğrenci Başlık Düğümünü Bul/Oluştur
            ogrenciHeader studentHeader = FindOrCreateStudent(ogrenciNo);
            if (studentHeader == null) return false;

            // 3. İlgili Ders Başlık Düğümünü Bul/Oluştur
            dersHeader courseHeader = FindOrCreateCourse(dersKodu);
            if (courseHeader == null) return false;

            // 4. Öğrenci Listesine Ekle (NextOgrenciDers)
            Node currentStudentCourse = studentHeader.ilkders;
            if (currentStudentCourse == null)
            {
                studentHeader.ilkders = newGrade;
            }
            else
            {
                while (currentStudentCourse.NextOgrenciDers != null)
                {
                    // Aynı dersin tekrar eklenip eklenmediğini kontrol et
                    if (currentStudentCourse.DersKodu == dersKodu) return false;
                    currentStudentCourse = currentStudentCourse.NextOgrenciDers;
                }
                if (currentStudentCourse.DersKodu == dersKodu) return false;
                currentStudentCourse.NextOgrenciDers = newGrade;
            }

            // 5. Ders Listesine Ekle (NextDersOgrenci)
            Node currentCourseStudent = courseHeader.FirstStudent;
            if (currentCourseStudent == null)
            {
                courseHeader.FirstStudent = newGrade;
            }
            else
            {
                while (currentCourseStudent.NextDersOgrenci != null)
                {
                    currentCourseStudent = currentCourseStudent.NextDersOgrenci;
                }
                currentCourseStudent.NextDersOgrenci = newGrade;
            }

            return true;
        }


        // 3 & 4. SİLME İŞLEMLERİ (Öğrenci/Ders silme veya Dersteki Öğrenci Silme)
        public bool RemoveGrade(string ogrenciNo, string dersKodu)
        {
            // 1. Öğrenci Listesinden Silme (NextOgrenciDers zinciri)
            ogrenciHeader studentHeader = GetStudentHeader(ogrenciNo);
            if (studentHeader == null) return false;

            Node current = studentHeader.ilkders;
            Node prev = null;
            Node targetNode = null;

            while (current != null)
            {
                if (current.DersKodu == dersKodu)
                {
                    targetNode = current;
                    if (prev == null)
                        studentHeader.ilkders = current.NextOgrenciDers;
                    else
                        prev.NextOgrenciDers = current.NextOgrenciDers;
                    break;
                }
                prev = current;
                current = current.NextOgrenciDers;
            }

            if (targetNode == null) return false; // Ders/Kayıt bulunamadı

            // 2. Ders Listesinden Silme (NextDersOgrenci zinciri)
            dersHeader courseHeader = GetCourseHeader(dersKodu);
            if (courseHeader == null) return false; // Ders Başlığı zaten olmalı

            current = courseHeader.FirstStudent;
            prev = null;

            while (current != null)
            {
                if (current.OgrenciNo == ogrenciNo)
                {
                    if (prev == null)
                        courseHeader.FirstStudent = current.NextDersOgrenci;
                    else
                        prev.NextDersOgrenci = current.NextDersOgrenci;
                    break;
                }
                prev = current;
                current = current.NextDersOgrenci;
            }

            // Opsiyonel: Eğer başlık düğümleri boşalırsa, onları da silme mantığı buraya eklenebilir.

            return true;
        }

        private ogrenciHeader GetStudentHeader(string ogrenciNo)
        {
            ogrenciHeader current = StudentHead;
            while (current != null)
            {
                if (current.OgrenciNo == ogrenciNo) return current;
                current = current.sonrakiogrenci;
            }
            return null;
        }

        private dersHeader GetCourseHeader(string dersKodu)
        {
            dersHeader current = CourseHead;
            while (current != null)
            {
                if (current.DersKodu == dersKodu) return current;
                current = current.NextCourse;
            }
            return null;
        }


        // 5. BİR DERSTEKİ TÜM ÖĞRENCİLERİ NUMARA SIRASINA GÖRE SIRALI LİSTELEME
        public List<Node> ListStudentsByCourseSortedByNo(string dersKodu)
        {
            dersHeader courseHeader = GetCourseHeader(dersKodu);
            if (courseHeader == null) return new List<Node>();

            List<Node> studentsInCourse = new List<Node>();
            Node current = courseHeader.FirstStudent;

            // Dersin tüm öğrenci kayıtlarını topla
            while (current != null)
            {
                studentsInCourse.Add(current);
                current = current.NextDersOgrenci;
            }

            // Öğrenci Numarasına göre sırala (string için LINQ kullanıldı)
            return studentsInCourse.OrderBy(n => n.OgrenciNo).ToList();
        }


        // 6. BİR ÖĞRENCİNİN ALDIĞI TÜM DERSLERİ DERS KODUNA GÖRE SIRALI LİSTELEME
        public List<Node> ListCoursesByStudentSortedByCode(string ogrenciNo)
        {
            ogrenciHeader studentHeader = GetStudentHeader(ogrenciNo);
            if (studentHeader == null) return new List<Node>();

            List<Node> coursesOfStudent = new List<Node>();
            Node current = studentHeader.ilkders;

            // Öğrencinin tüm ders kayıtlarını topla
            while (current != null)
            {
                coursesOfStudent.Add(current);
                current = current.NextOgrenciDers;
            }

            // Ders Koduna göre sırala (string için LINQ kullanıldı)
            return coursesOfStudent.OrderBy(n => n.DersKodu).ToList();
        }
    }
    class Program
    {
        static Manager manager = new Manager();
        static void Main(string[] args)
        {
            SeedData();
            RunMenu();

        }
        static void SeedData()
        {
            Console.WriteLine("Başlangıç verileri yükleniyor...");
            manager.AddGrade("101", "MAT101", "AA");
            manager.AddGrade("101", "FIZ101", "BB");
            manager.AddGrade("202", "MAT101", "CB");
            manager.AddGrade("303", "FIZ101", "BA");
            manager.AddGrade("202", "KIM101", "AA");
            Console.WriteLine("Veriler yüklendi.\n");
        }
        static void RunMenu()
        {
            bool isRunning = true;
            while (isRunning)
            {
                Console.WriteLine("-----------------------------------------------------");
                Console.WriteLine("Öğrenci/Ders Bağlı Liste Uygulaması");
                Console.WriteLine("1- Yeni Ders Kaydı Ekleme");
                Console.WriteLine("2- Ders Kaydı Silme");
                Console.WriteLine("3- Bir Dersteki Öğrencileri Listele (No'ya Göre Sıralı)");
                Console.WriteLine("4- Bir Öğrencinin Derslerini Listele (Koda Göre Sıralı)");
                Console.WriteLine("0- Çıkış");
                Console.WriteLine("-----------------------------------------------------");
                Console.Write("Seçiminizi yapın: ");
                string choice = Console.ReadLine();
                switch (choice)
                {

                    case "1": AddNewGrade(); break;
                    case "2": RemoveGradeEntry(); break;
                    case "3": ListStudentsByCourse(); break;
                    case "4": ListCoursesByStudent(); break;
                    case "0": isRunning = false; break;
                    default: Console.WriteLine("Geçersiz seçim. Lütfen tekrar deneyin."); break;
                }
                Console.WriteLine("\nDevam etmek için bir tuşa basın...");
                Console.ReadKey();
                Console.Clear();
            }
        }
        static void AddNewGrade()
        {
            Console.Write("Öğrenci Numarası: ");
            string ogrNo = Console.ReadLine();
            Console.Write("Ders Kodu: ");
            string dersKod = Console.ReadLine();
            Console.Write("Harf Ortalaması: ");
            string ortalama = Console.ReadLine();

            if (manager.AddGrade(ogrNo, dersKod, ortalama))
            {
                Console.WriteLine($"\nBAŞARILI: '{ogrNo}' numaralı öğrenciye '{dersKod}' dersi eklendi.");
            }
            else
            {
                Console.WriteLine("\nHATA: Kayıt eklenemedi. (Bu öğrencinin bu dersi zaten mevcut olabilir.)");
            }
        }
        static void RemoveGradeEntry()
        {
            Console.Write("Silinecek Öğrenci Numarası: ");
            string ogrNo = Console.ReadLine();
            Console.Write("Silinecek Ders Kodu: ");
            string dersKod = Console.ReadLine();

            if (manager.RemoveGrade(ogrNo, dersKod))
            {
                Console.WriteLine($"\nBAŞARILI: '{ogrNo}' öğrencisinin '{dersKod}' dersi silindi.");
            }
            else
            {
                Console.WriteLine("\nHATA: Kayıt silinemedi. (Öğrenci veya ders bulunamadı.)");
            }
        }
        static void ListStudentsByCourse()
        {
            Console.Write("Listelenecek Ders Kodu: ");
            string dersKod = Console.ReadLine();

            var list = manager.ListStudentsByCourseSortedByNo(dersKod);

            Console.WriteLine($"\n--- '{dersKod}' Dersini Alan Öğrenciler (Numaraya Göre Sıralı) ---");
            if (list.Count == 0)
            {
                Console.WriteLine("Bu dersi alan öğrenci bulunmamaktadır.");
                return;
            }

            foreach (var item in list)
            {
                Console.WriteLine(item.ToString());
            }
        }
        static void ListCoursesByStudent()
        {
            Console.Write("Listelenecek Öğrenci Numarası: ");
            string ogrNo = Console.ReadLine();

            var list = manager.ListCoursesByStudentSortedByCode(ogrNo);

            Console.WriteLine($"\n--- '{ogrNo}' Numaralı Öğrencinin Aldığı Dersler (Koda Göre Sıralı) ---");
            if (list.Count == 0)
            {
                Console.WriteLine("Bu öğrencinin kayıtlı dersi bulunmamaktadır.");
                return;
            }

            foreach (var item in list)
            {
                Console.WriteLine(item.ToString());
            }
        }
    }
}
