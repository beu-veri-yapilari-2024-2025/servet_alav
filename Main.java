import java.util.Scanner;

public class Main {
    
    public static class Ogrenciler {
        private String ad;
        private String soyad;
        private int okulno;

        public Ogrenciler(String ad, String soyad, int okulno) {
            this.ad = ad;
            this.soyad = soyad;
            this.okulno = okulno;
        }


        @Override
        public String toString() {
            return ad + " " + soyad + " (" + okulno + ")";
        }


        @Override
        public boolean equals(Object obj) {
            if (this == obj) return true;
            if (obj == null || getClass() != obj.getClass()) return false;
            Ogrenciler ogrenci = (Ogrenciler) obj;

            return okulno == ogrenci.okulno;

        }


        @Override
        public int hashCode() {
            return okulno;
        }
    }


    public static class Node {
        public Ogrenciler ogrenci;
        public Node Next;
        public Node(Ogrenciler ogrenci){
            this.ogrenci=ogrenci;
            Next =null;
        }
    }

    public static class BagliList{
        private Node bas;


        public BagliList(){
            bas=null;

        }


        public void BasaEkle(Ogrenciler ogrenci){
            Node yeniNode = new Node(ogrenci);
            yeniNode.Next = bas;
            bas = yeniNode;
            System.out.println(ogrenci +" basa eklendi.");
        }

        public void SonaEkle(Ogrenciler ogrenci){
            Node yeniNode = new Node(ogrenci);
            if (bas==null){
                bas=yeniNode;

                System.out.println(ogrenci + " sona eklendi");
                return;
            }
            Node current = bas;
            while (current.Next!=null){
                current=current.Next;
            }
            current.Next=yeniNode;

            System.out.println(ogrenci + " sona eklendi");
        }


        public boolean OgrenciAra(Ogrenciler ogrenci){
            Node current = bas;
            while (current!=null){
                if (current.ogrenci.equals(ogrenci)){
                    System.out.println(ogrenci + " listede bulunuyor.");
                    return true;
                }
                current=current.Next;
            }
            System.out.println(ogrenci + " listede yok.");
            return false;
        }
        public void OgrenciSil(Ogrenciler ogrenci){
            if (bas == null) {
                System.out.println("Liste boş, silinecek öğrenci yok.");
                return;
            }
            if (bas.ogrenci.equals(ogrenci)) {
                System.out.println(bas.ogrenci + " siliniyor (baştan).");
                bas = bas.Next;
                return;
            }

            Node current = bas;
            Node prev = null;

            while (current != null && !current.ogrenci.equals(ogrenci)) {
                prev = current;
                current = current.Next;
            }

            if (current != null) {
                System.out.println(current.ogrenci + " siliniyor.");
                prev.Next = current.Next;

            } else {

                System.out.println("OgrenciSil: " + ogrenci + " listede yok.");
            }
        }


        public void Lİste(){
            if(bas==null){
                System.out.println("Listede eleman yok.");
                return;
            }
            Node current = bas;
            System.out.println("Liste:");
            while (current != null){
                System.out.print(current.ogrenci);
                if(current.Next != null){
                    System.out.print(" --> ");
                }
                current = current.Next;
            }
            System.out.println();
        }
    }


    public static void main(String[] args)
    {
        Scanner scanner = new Scanner(System.in);
        BagliList OgrenciList = new BagliList();

        String islemler = "1.islem: islemleri göster\n" +
                "2.islem: ogrenci ekle\n" +
                "3.islem: ogrenci sil\n" +
                "4.islem: listeyi bastir\n" +
                "5.islem: ogrenci ara\n" +
                "q.islem: cikis yap\n";

        System.out.println("Öğrenci listeleme sistemine hoş geldiniz...");
        System.out.println(islemler);
        System.out.println("<-------------------------->");
        while (true){
            System.out.print("Lütfen yapmak istediğiniz islemi giriniz: ");
            String islem = scanner.nextLine();
            if (islem.equals("1")) {
                System.out.println(islemler);
            }
            else if(islem.equals("2")){
                System.out.print("Öğrenci adı giriniz: ");
                String ad = scanner.nextLine();
                System.out.print("Öğrenci soyadı giriniz: ");
                String soyad = scanner.nextLine();
                System.out.print("Öğrenci okul no giriniz: ");

                if (!scanner.hasNextInt()) {
                    System.out.println("Hata: Okul numarası sayı olmalıdır. İşlem iptal edildi.");
                    scanner.nextLine();
                    continue;
                }
                int okulno = scanner.nextInt();
                scanner.nextLine();

                Ogrenciler ogrenci = new Ogrenciler(ad,soyad,okulno);

                if(OgrenciList.OgrenciAra(ogrenci)){
                    System.out.println("Bu okul numarası zaten kayıtlı. Kayıt yapamazsınız.");
                }else{
                    System.out.println("Kayıt yapılıyor...");
                    System.out.print("Öğrenciyi nereye kaydetmek istersiniz? (bas/son): ");
                    String cevap = scanner.nextLine().toLowerCase();
                    if (cevap.equals("bas")){
                        OgrenciList.BasaEkle(ogrenci);
                    }
                    else if(cevap.equals("son")){
                        OgrenciList.SonaEkle(ogrenci);
                    }else {
                        System.out.println("Hatalı cevap verdiniz. Kayıt yapılmadı.");
                    }
                }
            }
            else if (islem.equals("3")) {
                System.out.print("Silinecek öğrencinin adı giriniz: ");
                String ad = scanner.nextLine();
                System.out.print("Silinecek öğrencinin soyadı giriniz: ");
                String soyad = scanner.nextLine();
                System.out.print("Silinecek öğrencinin okul no giriniz: ");
                if (!scanner.hasNextInt()) {
                    System.out.println("Hata: Okul numarası sayı olmalıdır. İşlem iptal edildi.");
                    scanner.nextLine();
                    continue;
                }
                int okulno = scanner.nextInt();
                scanner.nextLine();

                Ogrenciler ogrenci = new Ogrenciler(ad,soyad,okulno);


                OgrenciList.OgrenciSil(ogrenci);

            }
            else if (islem.equals("4")) {
                OgrenciList.Lİste();
            }
            else if (islem.equals("5")) {
                System.out.print("Aranacak öğrencinin adı giriniz: ");
                String ad = scanner.nextLine();
                System.out.print("Aranacak öğrencinin soyadı giriniz: ");
                String soyad = scanner.nextLine();
                System.out.print("Aranacak öğrencinin okul no giriniz: ");
                if (!scanner.hasNextInt()) {
                    System.out.println("Hata: Okul numarası sayı olmalıdır. İşlem iptal edildi.");
                    scanner.nextLine();
                    continue;
                }
                int okulno = scanner.nextInt();
                scanner.nextLine();

                Ogrenciler ogrenci = new Ogrenciler(ad,soyad,okulno);
                OgrenciList.OgrenciAra(ogrenci);
            }
            else if (islem.equals("q")) {
                System.out.println("Sistemden çıkılıyor...");
                break;
            }else {
                System.out.println("Hatalı bir islem yaptınız. Tekrar deneyin.");
            }
        }
        scanner.close();
    }
}